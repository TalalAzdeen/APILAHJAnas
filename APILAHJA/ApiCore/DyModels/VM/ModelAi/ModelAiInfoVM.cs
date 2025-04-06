using AutoGenerator;
using AutoGenerator.Models;
using AutoGenerator.Helper.Translation;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// ModelAi  property for VM Info.
    /// </summary>
    public class ModelAiInfoVM : ITVM
    {
        ///
        public string? Id { get; set; }
        public String? Name { get; set; }
        ///
        public String? Token { get; set; }
        ///
        public String? AbsolutePath { get; set; }
        //
        public String? Category { get; set; } 
        //
        public String? Language { get; set; } 
        //
        public String? IsStandard { get; set; } 
        //
        public String? Gender { get; set; } 
        //
        public String? Dialect { get; set; } 
        ///
        public String? Type { get; set; }
        ///
        public String? ModelGatewayId { get; set; }
    }
}