//using Api.Services;
//using Api.Utilities;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;

//public class SubscriptionCheckFilter(SubscriptionService subscriptionService) : IAsyncActionFilter
//{

//    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//    {
//        var subscription = await subscriptionService.GetSubscription();
//        //if (trackSubscription.CancelAtPeriodEnd)
//        //{
//        //    context.Result = new ObjectResult(new { Message = "You have canceled your subscription, renew it if you want use this service." }) { StatusCode = StatusCodes.Status402PaymentRequired };
//        //    //context.Result = new UnauthorizedObjectResult("Your subscription has expired. Please renew.");
//        //}

//        if (subscription == null)
//        {
//            context.Result = new ObjectResult(new ProblemDetails
//            {
//                Title = "Check subscription",
//                Detail = "You do not have a subscription. Please subscribe if you want use this service.",
//                Status = 600
//            })
//            { StatusCode = StatusCodes.Status402PaymentRequired };
//            return;
//            //context.Result = new UnauthorizedObjectResult("Your subscription canceled.");
//        }
//        else if (subscription.Status == SubscriptionStatus.Canceled)
//        {
//            context.Result = new ObjectResult(new ProblemDetails
//            {
//                Title = "Check subscription",
//                Detail = "Your subscription has canceled",
//                Status = 601
//            })
//            { StatusCode = StatusCodes.Status402PaymentRequired };
//            return;
//            //context.Result = new UnauthorizedObjectResult("Please subscribe if you want use this service.");
//        }
//        //else if (!await subscriptionRepository.IsAllowed(subscription))
//        //{
//        //    context.Result = new ObjectResult(new ProblemDetails
//        //    {
//        //        Title = "Requests Ended",
//        //        Detail = "You have exhausted all allowed subscription requests.",
//        //        Status = 602
//        //    })
//        //    { StatusCode = StatusCodes.Status402PaymentRequired };
//        //    //context.Result = new UnauthorizedObjectResult("Please subscribe if you want use this service.");
//        //}

//        await next(); //need to pass the execution to next

//    }


//}

