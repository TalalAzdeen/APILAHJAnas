using ApiCore.DyModels.Dso.Requests;
using ApiCore.DyModels.Dso.Responses;
using ApiCore.DyModels.Dto.Share.Requests;
using ApiCore.DyModels.Dto.Share.Responses;
using ApiCore.Repositorys.Builder;
using ApiCore.Repositorys.Share;
using ApiCore.Validators;
using AutoGenerator;
using AutoGenerator.Conditions;
using AutoGenerator.Helper;
using AutoGenerator.Models;
using AutoGenerator.Repositorys.Base;
using AutoGenerator.Services.Base;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApiCore.Services.Services
{
    public class PlanService : BaseService<PlanRequestDso, PlanResponseDso>, IUsePlanService
    {
        private readonly IPlanShareRepository _builder;
        private readonly IConditionChecker _checker;
        public PlanService(IPlanShareRepository buildPlanShareRepository, IMapper mapper, ILoggerFactory logger,
            IConditionChecker checker) : base(mapper, logger)
        {
            _builder = buildPlanShareRepository;
            _checker = checker;
        }

        public override Task<int> CountAsync()
        {
            try
            {
                _logger.LogInformation("Counting Plan entities...");
                return _builder.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CountAsync for Plan entities.");
                return Task.FromResult(0);
            }
        }

        public override async Task<PlanResponseDso> CreateAsync(PlanRequestDso entity)
        {

            

            _checker.Check(SpaceValidatorStates.IsFull, entity);
            try
            {
                _logger.LogInformation("Creating new Plan entity...");
                var result = await _builder.CreateAsync(entity);
                var output = GetMapper().Map<PlanResponseDso>(result);
                _logger.LogInformation("Created Plan entity successfully.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating Plan entity.");
                return null;
            }
        }

        public override Task DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting Plan entity with ID: {id}...");
                return _builder.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting Plan entity with ID: {id}.");
                return Task.CompletedTask;
            }
        }

        public override async Task<IEnumerable<PlanResponseDso>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all Plan entities...");
                var results = await _builder.GetAllAsync();
                return GetMapper().Map<IEnumerable<PlanResponseDso>>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Plan entities.");
                return null;
            }
        }

        public override async Task<PlanResponseDso?> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Retrieving Plan entity with ID: {id}...");
                var result = await _builder.GetByIdAsync(id);
                var item = GetMapper().Map<PlanResponseDso>(result);
                _logger.LogInformation("Retrieved Plan entity successfully.");
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetByIdAsync for Plan entity with ID: {id}.");
                return null;
            }
        }

        public override IQueryable<PlanResponseDso> GetQueryable()
        {
            try
            {
                _logger.LogInformation("Retrieving IQueryable<PlanResponseDso> for Plan entities...");
                var queryable = _builder.GetQueryable();
                var result = GetMapper().ProjectTo<PlanResponseDso>(queryable);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetQueryable for Plan entities.");
                return null;
            }
        }

        public override async Task<PlanResponseDso> UpdateAsync(PlanRequestDso entity)
        {
            try
            {
                _logger.LogInformation("Updating Plan entity...");
                var result = await _builder.UpdateAsync(entity);
                return GetMapper().Map<PlanResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync for Plan entity.");
                return null;
            }
        }

        public override async Task<bool> ExistsAsync(object value, string name = "Id")
        {
            try
            {
                _logger.LogInformation("Checking if Plan exists with {Key}: {Value}", name, value);
                var exists = await _builder.ExistsAsync(value, name);
                if (!exists)
                {
                    _logger.LogWarning("Plan not found with {Key}: {Value}", name, value);
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking existence of Plan with {Key}: {Value}", name, value);
                return false;
            }
        }

        public override async Task<PagedResponse<PlanResponseDso>> GetAllAsync(string[]? includes = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation("Fetching all Plans with pagination: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
                var results = (await _builder.GetAllAsync(includes, pageNumber, pageSize));
                var items = GetMapper().Map<List<PlanResponseDso>>(results.Data);
                return new PagedResponse<PlanResponseDso>(items, results.PageNumber, results.PageSize, results.TotalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all Plans.");
                return new PagedResponse<PlanResponseDso>(new List<PlanResponseDso>(), pageNumber, pageSize, 0);
            }
        }

        public override async Task<PlanResponseDso?> GetByIdAsync(object id)
        {
            try
            {
                _logger.LogInformation("Fetching Plan by ID: {Id}", id);
                var result = await _builder.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("Plan not found with ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Retrieved Plan successfully.");
                return GetMapper().Map<PlanResponseDso>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving Plan by ID: {Id}", id);
                return null;
            }
        }

        public override async Task DeleteAsync(object value, string key = "Id")
        {
            try
            {
                _logger.LogInformation("Deleting Plan with {Key}: {Value}", key, value);
                await _builder.DeleteAsync(value, key);
                _logger.LogInformation("Plan with {Key}: {Value} deleted successfully.", key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Plan with {Key}: {Value}", key, value);
            }
        }

        public override async Task DeleteRange(List<PlanRequestDso> entities)
        {
            try
            {
                var builddtos = entities.OfType<PlanRequestShareDto>().ToList();
                _logger.LogInformation("Deleting {Count} Plans...", 201);
                await _builder.DeleteRange(builddtos);
                _logger.LogInformation("{Count} Plans deleted successfully.", 202);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple Plans.");
            }
        }

        public override async Task<PagedResponse<PlanResponseDso>> GetAllByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving all Plan entities...");
                var results = await _builder.GetAllAsync();
                var response = await _builder.GetAllByAsync(conditions, options);
                return response.ToResponse(GetMapper().Map<IEnumerable<PlanResponseDso>>(response.Data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllAsync for Plan entities.");
                return null;
            }
        }

        public override async Task<PlanResponseDso?> GetOneByAsync(List<FilterCondition> conditions, ParamOptions? options = null)
        {
            try
            {
                _logger.LogInformation("Retrieving Plan entity...");
                var results = await _builder.GetAllAsync();
                return GetMapper().Map<PlanResponseDso>(await _builder.GetOneByAsync(conditions, options));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOneByAsync  for Plan entity.");
                return null;
            }
        }
    }
}