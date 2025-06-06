﻿using AutoGenerator.Data;
using AutoGenerator.Seeds;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AutoGenerator.Services2;

public static class SeedData
{
    public async static void EnsureSeedData(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {

            var context = scope.ServiceProvider.GetService<DataContext>();
            var mapper = scope.ServiceProvider.GetService<IMapper>();
            //await context.Database.EnsureDeletedAsync();
            await context.Database.MigrateAsync();
            //await context.Database.EnsureCreatedAsync();
            /**/

            await DefaultRoles.SeedAsync(scope);
            await DefaultUsers.SeedAdminAsync(scope);


            //await DefaultPlansAndFeatures.SeedAsync(scope);

            //await DefaultModals.SeedAsync(context);


        }
    }
}
