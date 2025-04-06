using AutoGenerator;
using AutoGenerator.Models;
using AutoGenerator.Helper.Translation;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// UserService  property for VM Filter.
    /// </summary>
    public class UserServiceFilterVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public string? Lg { get; set; }
    }
}