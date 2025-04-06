//using ApiCore.DyModels.VMs;
//using ApiCore.Services.Services;
//using APILAHJA.Utilities;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using StripeGateway;
//using VM;

//namespace Api.Controllers
//{

//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProfileController(
//        IUseApplicationUserService userService,
//        IMapper mapper,
//        IUserClaimsHelper userClaims
//        ) : Controller
//    {
//        [HttpGet("user", Name = "GetUser")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        public async Task<ActionResult<ApplicationUserOutputVM>> GetUser()
//        {
//            var user = await userService.GetUser();
//            var response = mapper.Map<ApplicationUserOutputVM>(user);
//            return Ok(response);
//        }

//        [HttpPut("Update")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        public async Task<ActionResult> Update([FromBody] ApplicationUserUpdateVM model)
//        {
//            try
//            {

//                var user = await userManager.FindByIdAsync(userClaims.UserId);
//                user.FirstName = userRequest.FirstName;
//                user.LastName = userRequest.LastName;
//                user.DisplayName = userRequest.DisplayName;
//                user.PhoneNumber = userRequest.PhoneNumber;
//                user.Image = userRequest.Image;
//                if (string.IsNullOrEmpty(user.DisplayName)) user.DisplayName = user.FirstName + " " + user.LastName;
//                await userManager.UpdateAsync(user);
//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(HandelErrors.Problem(ex));
//            }
//        }


//        [HttpGet("GetCustomer")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        public async Task<ActionResult<CustomerResponse>> GetCustomer()
//        {
//            try
//            {
//                var item = await stripeCustomer.GetCustomer(userClaims.CustomerId);

//                return Ok(item);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new ProblemDetails { Detail = ex.Message });
//            }
//        }



//        [HttpGet("subscriptions", Name = "Subscriptions")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        public async Task<ActionResult<List<SubscriptionResponse>>> Subscriptions()
//        {
//            //var desc = "1,000 request";

//            //return Ok(trackSubscription.NumberRequests);
//            var items = await subscriptionRepository.GetAllAsync(u => u.CustomerId == userClaims.CustomerId);
//            var response = mapper.Map<List<SubscriptionResponse>>(items);

//            return Ok(response);
//        }

//        [HttpGet("modelAis", Name = "ModelAis")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        public async Task<ActionResult<List<ModelAiResponse>>> ModelAis()
//        {
//            var items = await userModelAiRepository.GetAll(s => s.UserId == userClaims.UserId).Select(s => s.ModelAi).ToListAsync();
//            var response = mapper.Map<List<ModelAiResponse>>(items);

//            return Ok(response);
//        }

//        [HttpGet("services", Name = "Services")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        public async Task<ActionResult<List<ServiceResponse>>> Services()
//        {
//            var services = await userServiceRepository.GetAll(s => s.UserId == userClaims.UserId).Select(s => s.Service).ToListAsync();
//            var response = mapper.Map<List<ServiceResponse>>(services);

//            return Ok(response);
//        }

//        [HttpGet("services-modelAi/{modelAiId}", Name = "ServicesModelAi")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        public async Task<ActionResult<List<ServiceResponse>>> ServicesModelAi(string modelAiId)
//        {
//            var services = await serviceRepository.GetAllAsync(s => s.ModelAiId == modelAiId);
//            var response = mapper.Map<List<ServiceResponse>>(services);

//            return Ok(response);
//        }


//        [HttpGet("spaces-subscription", Name = "SpacesSubscription")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        public async Task<ActionResult<List<SpaceResponse>>> SpacesSubscription(string? subscriptionId)
//        {
//            var subscription = await subscriptionRepository.GetByAsync(s => s.UserId == userClaims.UserId);
//            if (string.IsNullOrEmpty(subscriptionId))
//            {
//                var items = await spaceRepository.GetAllAsync(s => s.SubscriptionId == subscription.Id);
//                var res = mapper.Map<List<SpaceResponse>>(items);
//                return Ok(res);
//            }
//            if (subscription.Id != subscriptionId) return BadRequest(HandelErrors.NotFound("Incorrect subscription or not belong to you.", "spaces subscription"));

//            var spaces = await spaceRepository.GetBySubscriptionIdAsync(subscriptionId);
//            var response = mapper.Map<List<SpaceResponse>>(spaces);

//            return Ok(response);
//        }

//        [HttpGet("space-subscription", Name = "SpaceSubscription")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        public async Task<ActionResult<SpaceResponse>> SpaceSubscription(string subscriptionId, string spaceId)
//        {
//            var subscription = await subscriptionRepository.GetByAsync(s => s.UserId == userClaims.UserId && s.Id == subscriptionId);
//            if (subscription == null) return BadRequest(HandelErrors.NotFound("Subscription id incorrect or not belong to you.", "spaces subscription"));

//            var item = await spaceRepository.GetByAsync(s => s.SubscriptionId == subscriptionId && s.Id == spaceId);
//            var response = mapper.Map<SpaceResponse>(item);

//            return Ok(response);
//        }

//        [HttpGet("requests-subscription", Name = "RequestsSubscription")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        public async Task<ActionResult<List<RequestResponse>>> RequestsSubscription(string subscriptionId)
//        {
//            var items = await requestRepository.GetByAsync(r => r.SubscriptionId == subscriptionId, r => r.Include(e => e.Events));
//            var response = mapper.Map<List<RequestResponse>>(items);

//            return Ok(response);
//        }

//        [HttpGet("requests-services", Name = "RequestsService")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//        public async Task<ActionResult<List<RequestResponse>>> RequestsService(string serviceId)
//        {
//            var items = await requestRepository.Includes(["Events"]).GetAllAsync(r => r.ServiceId == serviceId);
//            var response = mapper.Map<List<RequestResponse>>(items);

//            return Ok(response);
//        }
//    }
//}