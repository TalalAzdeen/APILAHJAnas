using Microsoft.CodeAnalysis;
using AutoGenerator;
using ApiCore.DyModels.Dto.Share.Requests;
using System;

namespace ApiCore.DyModels.Dso.Requests
{
    public class ModelAiRequestDso : ModelAiRequestShareDto, ITDso
    {
    }
}