using AutoGenerator;
using AutoGenerator.Models;
using AutoGenerator.Helper.Translation;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Plan  property for VM Info.
    /// </summary>
    public class PlanInfoVM : ITVM
    {
        ///
        public string? Id { get; set; }
        public String? ProductId { get; set; }
        //
        public TranslationData? ProductName { get; set; }=new();    
        //
        public TranslationData? Description { get; set; } = new();
        //
        public ICollection<String>? Images { get; set; }
        ///
        public String? BillingPeriod { get; set; }
        ///
        public Double Amount { get; set; }
        ///
        public Boolean Active { get; set; }
        ///
        public DateTime UpdatedAt { get; set; }
        ///
        public DateTime CreatedAt { get; set; }
       // public ICollection<PlanFeatureCreateVM>? PlanFeatures { get; set; }
    }
}