using ApiCore.DyModels.Dso.Requests;
using ApiCore.DyModels.Dso.ResponseFilters;
using ApiCore.DyModels.VMs;
using ApiCore.Repositorys.Builder;
using ApiCore.Services.Services;
using AutoGenerator.Conditions;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApiCore.Validators
{
   


    public enum SubscriptionValidatorStates
    {
        IsSubscriptionId,
        IsCustomerId,
        HasStatus,
        IsValidStartDate,
        IsValidPeriodDates,
        IsAllowedRequests,
        IsIsAllowedSpaces,
        IsActive,
        IsFull,
        IsValid


    }


    public class SubscriptionValidator : BaseValidator<SubscriptionResponseFilterDso, SubscriptionValidatorStates>, ITValidator
    {
        //  «·«›÷· «· ⁄«„·  „⁄  context   
        /// <summary>
        ///  «” Œœ„Â   ›Ì   ⁄„Ì„ «·‘—Êÿ «·Œ«’… »«·ÃœÊ·   
        ///checker  ÌÃÌ» «·«” ›«œÂ „‰ «·‘—Êÿ «·„Õ„·Â   Ê«·„ Ê›—Â ›Ì  
        /// </summary>
        private readonly   SubscriptionBuilderRepository subscription  ; // Â–«  „Ã—œ „À«·  ÌÊ÷Õ ·ﬂ «‰ﬂ Ì„ﬂ‰   «·«” ›œÂ „‰ ﬂ·  ÿ»ﬁ«  «·—Ì»Ê“ Ì   
        private readonly IConditionChecker _checker;
        public SubscriptionValidator(IConditionChecker checker) : base(checker)
        {

            _checker = checker;
            ////subscription = new SubscriptionBuilderRepository(checker.Injector.DataContext,
            ////    checker.Injector.Mapper,
            ////     null
            ////    );
        }

        protected override void InitializeConditions()
        {

            _provider.Register(
                 SubscriptionValidatorStates.IsSubscriptionId,
                 new LambdaCondition<string>(
                     nameof(SubscriptionValidatorStates.IsSubscriptionId),
                     context => isSubscriptionId(context),
                     "Customer Id is required"
                 )
             );


            _provider.Register(
                SubscriptionValidatorStates.IsCustomerId,
                new LambdaCondition<string>(
                    nameof(SubscriptionValidatorStates.IsCustomerId),
                    context => isCustomerId(context),
                    "Customer Id is required"
                )
            );      
            


            _provider.Register(
                SubscriptionValidatorStates.IsActive,
                new LambdaCondition<string>(
                    nameof(SpaceValidatorStates.IsActive),
                    context => isAcive(context),
                    "Space is not active"
                )
            );

            _provider.Register(
            SubscriptionValidatorStates.IsIsAllowedSpaces,
            new LambdaCondition<int>(
                nameof(SubscriptionValidatorStates.IsIsAllowedSpaces),
                context => isIsAllowedSpaces(context),
                "Space is not active"
            )
        );

            _provider.Register(
                SubscriptionValidatorStates.IsValid,
                new LambdaCondition<SubscriptionOutputVM>(
                    nameof(SpaceValidatorStates.IsActive),
                    context => context.AllowedSpaces == 10,
                    "Space is not active"
                )
            );

          

        }



        //bool isAllowedRequests(int count)
        //{
        //    var result = _checker.Injector.DataContext.Set<Subscription>()
        //   .Any(sub => sub.AllowedRequests >= count);

        //    return result;
        //}

        bool isIsAllowedSpaces(int count)
        {


            var result = _checker.Injector.DataContext.Set<Subscription>()
            .Any(sub =>sub.AllowedSpaces<count);

            return result;
          

        }
         bool IsAllowedSpaces(params object[] args)
        {
            if (args.Length != 2 || args[0] is not int count || args[1] is not string subId)
                return false;

            var result = _checker.Injector.DataContext.Set<Subscription>()
                .FirstOrDefault(x => x.Id == subId);

            return result?.AllowedSpaces >= count;
        }
      
        private bool isSubscriptionId(string subId)
        {
            var result = _checker.Injector.DataContext.Set<Subscription>()
             .Any(user => user.Id == subId);
            return result;
        }

        bool isCustomerId(string userId)
        {
            return _checker.Check(ApplicationUserValidatorStates.HasCustomerId, userId);

        }

        bool isAcive(string subId)
        {
             
            var subscription = _checker.Injector.DataContext.Set<Subscription>()
            .FirstOrDefault(x => x.Id == subId);
            if (subscription == null)
                return false;

            return subscription.Status == "active";
           

        }
      
    }
}