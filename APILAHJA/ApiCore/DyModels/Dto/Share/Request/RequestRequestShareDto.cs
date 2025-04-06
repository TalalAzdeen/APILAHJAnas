using Microsoft.CodeAnalysis;
using AutoGenerator;
using AutoGenerator.Helper.Translation;
using AutoGenerator.Models;
using ApiCore.DyModels.Dto.Build.Requests;
using System;

namespace ApiCore.DyModels.Dto.Share.Requests
{
    public class RequestRequestShareDto : RequestRequestBuildDto, ITShareDto
    {
        public RoleCase? RolesRequestShare { get; set; }


        
        

    }
}