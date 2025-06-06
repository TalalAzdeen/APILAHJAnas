using AutoGenerator;
using AutoGenerator.Models;
using AutoGenerator.Helper.Translation;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// AdvertisementTab  property for VM Create.
    /// </summary>
    public class AdvertisementTabCreateVM : ITVM
    {
        ///
        public String? AdvertisementId { get; set; }
        //
        public TranslationData? Title { get; set; }
        //
        public TranslationData? Description { get; set; }
        ///
        public String? ImageAlt { get; set; }
    }
}