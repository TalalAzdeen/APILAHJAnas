using ApiCore.DyModels.Dso.Requests;
using ApiCore.DyModels.Dso.ResponseFilters;
using ApiCore.DyModels.VMs;
using ApiCore.Services.Services;
using AutoGenerator.Conditions;
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




    public enum ServiceValidatorStates
    {
        IsFull,
        IsServiceId,
        HasName,
        HasAbsolutePath,
        HasToken,
        HasValidModelAi,
        HasMethods,
        HasRequests,
        IsLinkedToUsers
    }

    public class ServiceValidator : BaseValidator<ServiceRequestDso, ServiceValidatorStates>, ITValidator
    {
        private readonly IConditionChecker _checker;

        public ServiceValidator(IConditionChecker checker) : base(checker)
        {
            _checker = checker;
        }

        protected override void InitializeConditions()
        {



            _provider.Register(ServiceValidatorStates.HasName,
                new LambdaCondition<ServiceRequestDso>(
                    nameof(ServiceValidatorStates.HasName),
                    s => !string.IsNullOrWhiteSpace(s.Name),
                    "Service name is required"
                )
            );

            _provider.Register(ServiceValidatorStates.HasAbsolutePath,
                new LambdaCondition<ServiceRequestDso>(
                    nameof(ServiceValidatorStates.HasAbsolutePath),
                    s => Uri.IsWellFormedUriString(s.AbsolutePath, UriKind.Absolute),
                    "AbsolutePath must be a valid URL"
                )
            );

            _provider.Register(ServiceValidatorStates.HasToken,
                new LambdaCondition<ServiceRequestDso>(
                    nameof(ServiceValidatorStates.HasToken),
                    s => !string.IsNullOrWhiteSpace(s.Token) && s.Token.Length >= 10,
                    "Token is required and must be at least 10 characters"
                )
            );

            _provider.Register(ServiceValidatorStates.HasValidModelAi,
                new LambdaCondition<ServiceRequestDso>(
                    nameof(ServiceValidatorStates.HasValidModelAi),
                    s => string.IsNullOrWhiteSpace(s.ModelAiId) || s.ModelAi != null,
                    "Model AI reference is invalid"
                )
            );

            _provider.Register(ServiceValidatorStates.HasMethods,
                new LambdaCondition<ServiceRequestDso>(
                    nameof(ServiceValidatorStates.HasMethods),
                    s => s.ServiceMethods != null && s.ServiceMethods.Any(),
                    "Service must have at least one method"
                )
            );

            _provider.Register(ServiceValidatorStates.HasRequests,
                new LambdaCondition<ServiceRequestDso>(
                    nameof(ServiceValidatorStates.HasRequests),
                    s => s.Requests != null && s.Requests.Any(),
                    "Service must have at least one request"
                )
            );

            _provider.Register(ServiceValidatorStates.IsLinkedToUsers,
                new LambdaCondition<ServiceRequestDso>(
                    nameof(ServiceValidatorStates.IsLinkedToUsers),
                    s => s.UserServices != null && s.UserServices.Any(),
                    "Service must be linked to at least one user"
                )
            );
            _provider.Register(ServiceValidatorStates.IsServiceId,
                new LambdaCondition<string>(
                    nameof(ServiceValidatorStates.IsServiceId),
                    context => IsIdService(context),
                    "Service ID is required"
                )
            );


            //_provider.Where(
            //    ServiceValidatorStates.IsFull,
            //    new LambdaCondition<ServiceRequestDso>(
            //        nameof(ServiceValidatorStates.IsFull),
            //         ,
            //        "Service is not full"
            //    )
            //);


        }
        bool IsIdService(string idServ)
        {



            if (idServ != "")
            {
                var result = _checker.Injector.DataContext.Set<Service>()
                    .Any(x => x.Id == idServ);

                return result;
            }
            else
            {
                return false;

            }
        }

    }
}