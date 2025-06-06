using AutoGenerator;
using AutoGenerator.Models;
using AutoGenerator.Helper.Translation;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Plan  property for VM Create.
    /// </summary>
    public class PlanCreateVM : ITVM
    {
        ///
        public String? ProductId { get; set; }
        //
        public TranslationData? ProductName { get; set; }
        //
        public TranslationData? Description { get; set; }
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
        //
      //  public ICollection<SubscriptionCreateVM>? Subscriptions { get; set; }
        //
       // public ICollection<PlanFeatureCreateVM>? PlanFeatures { get; set; }
    }
}