using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ApiCore.Services.Services;
using Microsoft.AspNetCore.Mvc;
using ApiCore.DyModels.VMs;
using System.Linq.Expressions;
using ApiCore.DyModels.Dso.Requests;
using AutoGenerator.Helper.Translation;
using System;
using AutoGenerator.Models;

namespace ApiCore.Controllers.Api
{
    //[ApiExplorerSettings(GroupName = "ApiCore")]
    [Route("api/ApiCore/Api/[controller]")]
    [ApiController]
    public class CategoryModelController : ControllerBase
    {
        private readonly IUseCategoryModelService _categorymodelService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CategoryModelController(IUseCategoryModelService categorymodelService, IMapper mapper, ILoggerFactory logger)
        {
            _categorymodelService = categorymodelService;
            _mapper = mapper;
            _logger = logger.CreateLogger(typeof(CategoryModelController).FullName);
        }

        // Get all CategoryModels.
        [HttpGet(Name = "GetCategoryModels")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CategoryModelOutputVM>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Fetching all CategoryModels...");
                var result = await _categorymodelService.GetAllAsync();

                var items = _mapper.Map<IEnumerable<CategoryModelOutputVM>>(result);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all CategoryModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a CategoryModel by ID.
        [HttpGet("{id}", Name = "GetCategoryModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryModelInfoVM>> GetById(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid CategoryModel ID received.");
                return BadRequest("Invalid CategoryModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching CategoryModel with ID: {id}", id);
                var entity = await _categorymodelService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("CategoryModel not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<CategoryModelInfoVM>(entity);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching CategoryModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a CategoryModel by Lg.
        [HttpPost("GetCategoryModelByFilter", Name = "GetCategoryModelByFilter")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryModelOutputVM>> GetCategoryModelByFilter(CategoryModelFilterVM model)
        {
            var id = model.Id;
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid CategoryModel ID received.");
                return BadRequest("Invalid CategoryModel ID.");
            }

            try
            {
                _logger.LogInformation("Fetching CategoryModel with ID: {id}", id);
                var entity = await _categorymodelService.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("CategoryModel not found with ID: {id}", id);
                    return NotFound();
                }

                var item = _mapper.Map<CategoryModelOutputVM>(entity, opt => opt.Items.Add(HelperTranslation.KEYLG, model.Lg));
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching CategoryModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // // Get a CategoryModels by Lg.
        [HttpGet("GetCategoryModelsByLanguage", Name = "GetCategoryModelsByLg")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CategoryModelOutputVM>>> GetCategoryModelsByLg(string? lg)
        {
            if (string.IsNullOrWhiteSpace(lg))
            {
                _logger.LogWarning("Invalid CategoryModel lg received.");
                return BadRequest("Invalid CategoryModel lg null ");
            }

            try
            {
                var categorymodels = (await _categorymodelService.GetAllAsync()).ToList();
                if (categorymodels == null)
                {
                    _logger.LogWarning("CategoryModels not found  by  ");
                    return NotFound();
                }
                var items = _mapper.Map<List<CategoryModelOutputVM>>(categorymodels, opt => opt.Items.Add(HelperTranslation.KEYLG, lg));
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching CategoryModels with Lg: {lg}", lg);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new CategoryModel.
        [HttpPost(Name = "CreateCategoryModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryModelOutputVM>> Create([FromBody] CategoryModelCreateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("CategoryModel data is null in Create.");
                return BadRequest("CategoryModel data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Create: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                var category = new CategoryModelRequestDso
                {
                    Name = new TranslationData
                    {
                        Value = new Dictionary<string, string>
        {
            { "ar", "������������" },
            { "en", "Electronics" }
        }
                    },
                    Description = new TranslationData
                    {
                        Value = new Dictionary<string, string>
        {
            { "ar", "��� ��� �������� �����������" },
            { "en", "Section for electronic devices" }
        }
                    }
                };
                _logger.LogInformation("Creating new CategoryModel with data: {@model}", model);
                 var item = _mapper.Map<CategoryModelRequestDso>(model);
                if (item == null)
                {
                    _logger.LogWarning("Invalid data provided for creating CategoryModel.");
                    return BadRequest("Invalid data provided.");
                }

                var createdEntity = await _categorymodelService.CreateAsync(category);
                var createdItem = _mapper.Map<CategoryModelOutputVM>(createdEntity, opt => opt.Items.Add(HelperTranslation.KEYLG,"ar"));

                //var createdItem = _mapper.Map<CategoryModelInfoVM>(createdEntity);
                return Ok(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new CategoryModel");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create multiple CategoryModels.
        [HttpPost("createRange")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CategoryModelCreateVM>>> CreateRange([FromBody] IEnumerable<CategoryModelCreateVM> models)
        {
            if (models == null)
            {
                _logger.LogWarning("Data is null in CreateRange.");
                return BadRequest("Data is required.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in CreateRange: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Creating multiple CategoryModels.");
                var items = _mapper.Map<List<CategoryModelRequestDso>>(models);
                if (items == null || !items.Any())
                {
                    _logger.LogWarning("No valid data provided for creating multiple CategoryModels.");
                    return BadRequest("No valid data provided.");
                }
                var createdEntities = await _categorymodelService.CreateRangeAsync(items);
                var createdItems = _mapper.Map<List<CategoryModelCreateVM>>(createdEntities);
                return Ok(createdItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple CategoryModels");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing CategoryModel.
        [HttpPut(Name = "UpdateCategoryModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryModelInfoVM>> Update([FromBody] CategoryModelUpdateVM model)
        {
            if (model == null)
            {
                _logger.LogWarning("Invalid data in Update.");
                return BadRequest("Invalid data.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state in Update: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {

                _logger.LogInformation("Updating CategoryModel with ID: {id}", model?.Id);
                var item = _mapper.Map<CategoryModelRequestDso>(model);
                var updatedEntity = await _categorymodelService.UpdateAsync(item);
                if (updatedEntity == null)
                {
                    _logger.LogWarning("CategoryModel not found for update with ID: {id}", model?.Id);
                    return NotFound();
                }

                var updatedItem = _mapper.Map<CategoryModelInfoVM>(updatedEntity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating CategoryModel with ID: {id}", model?.Id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Delete a CategoryModel.
        [HttpDelete("{id}", Name = "DeleteCategoryModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("Invalid CategoryModel ID received in Delete.");
                return BadRequest("Invalid CategoryModel ID.");
            }

            try
            {
                _logger.LogInformation("Deleting CategoryModel with ID: {id}", id);
                await _categorymodelService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting CategoryModel with ID: {id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get count of CategoryModels.
        [HttpGet("CountCategoryModel")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Count()
        {
            try
            {
                _logger.LogInformation("Counting CategoryModels...");
                var count = await _categorymodelService.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while counting CategoryModels");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}