using AutoGenerator;
using AutoGenerator.Models;
using AutoGenerator.Helper.Translation;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// ModelGateway  property for VM Create.
    /// </summary>
    public class ModelGatewayCreateVM : ITVM
    {
        ///
        public String? Name { get; set; }
        ///
        public String? Url { get; set; }
        ///
        public String? Token { get; set; }
        ///
        public Boolean IsDefault { get; set; }
    }
}