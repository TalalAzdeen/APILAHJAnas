using ApiCore.DyModels.Dto.Build.Requests;
using ApiCore.DyModels.Dto.Build.Responses;
using AutoGenerator.Data;
using AutoGenerator.Models;
using AutoGenerator.Repositorys.Builder;
using AutoMapper;

namespace ApiCore.Repositorys.Builder
{
    /// <summary>
    /// ApplicationUser class property for BuilderRepository.
    /// </summary>
     //
    public class ApplicationUserBuilderRepository : BaseBuilderRepository<ApplicationUser, ApplicationUserRequestBuildDto, ApplicationUserResponseBuildDto>, IApplicationUserBuilderRepository<ApplicationUserRequestBuildDto, ApplicationUserResponseBuildDto>
    {

        /// <summary>
        /// Constructor for ApplicationUserBuilderRepository.
        /// </summary>
        public ApplicationUserBuilderRepository(DataContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger) // Initialize  constructor.
        {
        }

        //public async new Task<ApplicationUserResponseBuildDto> UpdateAsync(ApplicationUserRequestBuildDto requestBuildDto)
        //{
        //    var user = await _userManager.FindByIdAsync(requestBuildDto.Id);
        //    user.FirstName = requestBuildDto.FirstName;
        //    user.LastName = requestBuildDto.LastName;
        //    user.DisplayName = requestBuildDto.DisplayName;
        //    user.PhoneNumber = requestBuildDto.PhoneNumber;
        //    user.Image = requestBuildDto.Image;
        //    if (string.IsNullOrEmpty(user.DisplayName)) user.DisplayName = user.FirstName + " " + user.LastName;
        //    await _userManager.UpdateAsync(user);
        //    return MapToBuildResponseDto(user);
        //}

    }
}