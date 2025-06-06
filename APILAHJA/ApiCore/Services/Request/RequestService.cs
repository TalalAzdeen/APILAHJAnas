using AutoGenerator;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AutoGenerator.Services.Base;
using ApiCore.DyModels.Dso.Requests;
using ApiCore.DyModels.Dso.Responses;
using AutoGenerator.Models;
using ApiCore.DyModels.Dto.Share.Requests;
using ApiCore.DyModels.Dto.Share.Responses;
using ApiCore.Repositorys.Share;
using System.Linq.Expressions;
using ApiCore.Repositorys.Builder;
using AutoGenerator.Repositorys.Base;
using AutoGenerator.Helper;
using System;
using AutoGenerator.Conditions;
using ApiCore.Validators;

namespace ApiCore.Services.Services
{
    public class RequestService : BaseService<RequestRequestDso, RequestResponseDso>, IUseRequestService
    {
        private readonly IRequestShareRepository _builder;
        private readonly IConditionChecker _checker;
        public RequestService(IRequestShareRepository buildRequestShareRepository, IMapper mapper, ILoggerFactory logger, IConditionChecker checker) : base(mapper, logger)
        {

            _builder = buildRequestShareRepository;
            _checker = checker;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting Request entities...");
                return _builder.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for Request entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<RequestResponseDso> CreateAsync(RequestRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Creating new Request entity...");


















                string errorMessage = "";
                //var ConditionResult =_checker.Check(RequestValidatorStates.IsFull, entity);
               // var resultt= _checker. CheckAllWithDetails<RequestValidatorStates>(entity, out Dictionary<RequestValidatorStates, string> results);
                var isConditionresult = _checker.CheckAndResult(RequestValidatorStates.IsFull, entity);
               // bool isConditionMetWithError = _checker.CheckWithError(RequestValidatorStates.IsFull, entity, out errorMessage);
                if ((bool)isConditionresult.Success)
                {
                    //_logger.LogInformation("Created Request entity successfully.");
                    // return null;

                    var result = await _builder.CreateAsync(entity);
                    var output = GetMapper().Map<RequestResponseDso>(result);
                    _logger.LogInformation("Created Request entity successfully.");
                    return output;
                }

                else
                {
                    _logger.LogWarning("Space entity creation failed due to validation: {Error}", isConditionresult.Message);
                       return null;
                }
             
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating Request entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting Request entity with ID: {id}...");
                return _builder.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting Request entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<RequestResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all Request entities...");
                var results = await _builder.GetAllAsync();
                return GetMapper().Map<IEnumerable<RequestResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Request entities.");
                return null;
            }
        }

        public override async Task<RequestResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving Request entity with ID: {id}...");
                var result = await _builder.GetByIdAsync(id);
                var item = GetMapper().Map<RequestResponseDso>(result);
                _logger.LogInformation("Retrieved Request entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for Request entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<RequestResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<RequestResponseDso> for Request entities...");
                var queryable = _builder.GetQueryable();
                var result = GetMapper().ProjectTo<RequestResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for Request entities.");
                return null;
            }
        }

        public override async Task<RequestResponseDso> UpdateAsync(RequestRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating Request entity...");
                var result = await _builder.UpdateAsync(entity);
                return GetMapper().Map<RequestResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for Request entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if Request exists with {Key}: {Value}", name, value);
                var exists = await _builder.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("Request not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of Request with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<RequestResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all Requests with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _builder.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<RequestResponseDso>>(results.Data);
                return new PagedResponse<RequestResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Requests.");
                return new PagedResponse<RequestResponseDso>(new List<RequestResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<RequestResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching Request by ID: {Id}", id);
                var result = await _builder.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Request not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved Request successfully.");
                return GetMapper().Map<RequestResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving Request by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting Request with {Key}: {Value}", key, value);
                await _builder.DeleteAsync(value, key);
                _logger.LogInformation("Request with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Request with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<RequestRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<RequestRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} Requests...", 201);
                await _builder.DeleteRange(builddtos);
                _logger.LogInformation("{Count} Requests deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple Requests.");
            }
        }

        public override async Task<PagedResponse<RequestResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all Request entities...");
                var results = await _builder.GetAllAsync();
                var response = await _builder.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<RequestResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Request entities.");
                return null;
            }
        }

        public override async Task<RequestResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving Request entity...");
                var results = await _builder.GetAllAsync();
                return GetMapper().Map<RequestResponseDso>(await _builder.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for Request entity.");
                return null;
            }
        }
    }
}