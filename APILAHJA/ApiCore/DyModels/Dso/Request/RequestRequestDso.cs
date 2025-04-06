using Microsoft.CodeAnalysis;
using AutoGenerator;
using ApiCore.DyModels.Dto.Share.Requests;
using System;
using AutoGenerator.Helper.Translation;

namespace ApiCore.DyModels.Dso.Requests
{
    public class RequestRequestDso : RequestRequestShareDto, ITDso
    {


        public RoleCase? RolesRequestService { get; set; }






        // Add any additional properties or methods specific to the RequestRequestDso class here
    }
}