﻿using Api;
using APILAHJA.ApiCore.Helper;
using AutoGenerator;
using AutoGenerator.CustomPolicy;
using AutoGenerator.Data;
using AutoGenerator.Models;
using AutoGenerator.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using Utilities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


if (args.Length > 0 && args[0].Contains("generate"))
{
    if (args.Length > 1)
        for (int i = 1; i < args.Length; i++)
            builder.Services.AddAutoBuilderApiCore(new()
            {

                //ProjectPath = Directory.GetCurrentDirectory().Split("bin")[0],
                NameRootApi = args[i],
                IsAutoBuild = true,

            });
    else
        builder.Services.AddAutoBuilderApiCore(new()
        {

            //ProjectPath = Directory.GetCurrentDirectory().Split("bin")[0],
            NameRootApi = "ApiCore",
            IsAutoBuild = true,

        });

    return;

}

builder.Services.AddAutoBuilderApiCore(new()
{
    //ProjectPath = Directory.GetCurrentDirectory().Split("bin")[0],
    NameRootApi = "",
    IsAutoBuild = false,
    IsMapper = true,
    Assembly = Assembly.GetExecutingAssembly()
});
//TODO: preapare the shared
//TODO: preapare the services
//TODO: preapare the controller
//TODO: read about Mock I see it in deepseek => repositories


// Add services to the container.

builder.Services.AddProblemDetails();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.RespectRequiredConstructorParameters = true;
    });
builder.Logging.AddConsole().SetMinimumLevel(LogLevel.Debug);

// Scoped Services
builder.Services.AddApiServices(builder.Configuration);

#region External Services
//Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
//    .WriteTo.Console()
//    .WriteTo.File("logs/myLog-.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services
    .AddStripeGateway(builder.Configuration)
    .AddDataContext(builder.Configuration)
    ;
#endregion

//builder.Services.AddTransient<IClaimsTransformation, MyClaimsTransformation>();

var appSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

// تمكين عرض الأخطاء المتعلقة بـ JWT
IdentityModelEventSource.ShowPII = true;

builder.Services.AddDynamicAuthentication(builder.Configuration, appSettings);

// Configure authorization
builder.Services.AddAuthorizationBuilder();


// Add identity and opt-in to endpoints
builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders()
    //.AddUserConfirmation<ApplicationUser>()
    .AddApiEndpoints();

builder.Services.Configure<IdentityOptions>(options =>
{
    /*
     *  يسمح للمستخدمين الجدد بتجربة تسجيل الدخول دون القلق من حظر حساباتهم.
     */
    options.Lockout.AllowedForNewUsers = true;
});
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanViewPlan", policy =>
        policy.Requirements.Add(new PermissionRequirement("Permission", Permissions.ViewPlan))
        ); // Example: Requires a claim
});

// Add a CORS policy for the client
builder.Services.AddCors(
    options => options.AddPolicy(
        "wasm",
        policy => policy.WithOrigins([builder.Configuration["appsettings:baseurls:api"] ?? "https://localhost:7001",
            builder.Configuration["FrontendUrl"] ?? "https://localhost:7003"])
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    //c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert token with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
            },
            Array.Empty<string>()
        }
    });
    // تخصيص Swagger لتحديد القيم
    //c.OperationFilter<CustomProductParameterFilter>();
});

//builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddLogging(); // ÅÖÇÝÉ ÎÏãÇÊ ÇáÜ Logging

//builder.Services.AddScoped<IInvoiceShareRepository, InvoiceShareRepository>();
//builder.Services.AddScoped<IUserClaims>();

var app = builder.Build();

//SeedData.EnsureSeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        //o.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        //o.RoutePrefix = string.Empty;
        // collapse endpoints 
        o.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

    });
}

app.MapSwagger();
app.CustomMapIdentityApi<ApplicationUser>();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// protection from cross-site request forgery (CSRF/XSRF) attacks with empty body
// form can't post anything useful so the body is null, the JSON call can pass
// an empty object {} but doesn't allow cross-site due to CORS.
app.MapPost("/api/logout", async (
    SignInManager<ApplicationUser> signInManager,
    [FromBody] object empty) =>
{
    if (empty is not null)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }
    return Results.NotFound();
}).RequireAuthorization().WithTags("Auth");

app.Run();
