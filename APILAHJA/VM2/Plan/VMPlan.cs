using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VM
{

    public class VMPlanOnes
    {
        public string? Id { set; get; }
        public string? Lg { set; get; }
    }


    public class FAQItemCreate
    {
        public Dictionary<string, string>? Question { get; set; } // الأسئلة باللغتين
        public Dictionary<string, string>? Answer { get; set; }  // الإجابات باللغتين

        public string? Tag { get; set; }


    }
    public class FAQItemView
    {
        public string? Question { get; set; } // الأسئلة باللغتين
        public string? Answer { get; set; }  // الإجابات باللغتين
        public string? Tag { get; set; }

    }



    public class CategoryView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //public bool Active { get; set; }

        //public string Image { get; set; }
        //public string? UrlUsed { get; set; }

        //public int CountFalvet { get; set; }

        //public int Rateing { get; set; }
    }




    public class CategoryModelCreate
    {
        [Required]
        public Dictionary<string, string> Name { get; set; }

        public Dictionary<string, string> Description { get; set; }
    }



    public class AdvertisementCreate
    {
        [Required]
        public Dictionary<string, string> Title { get; set; }

        public Dictionary<string, string> Description { get; set; }

        public string? Image { get; set; }

        public bool Active { get; set; } = true;

        public string? Url { get; set; }

        public ICollection<AdvertisementTabCreate>? AdvertisementTabs { get; set; }
    }

    public class AdvertisementView
    {
        public string Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public bool Active { get; set; } = true;

        public string? Url { get; set; }

        public ICollection<AdvertisementTabView>? AdvertisementTabs { get; set; }
    }

    public class AdvertisementTabCreate
    {
        [DefaultValue("")]
        public string AdvertisementId { get; set; }

        public Dictionary<string, string> Title { get; set; }

        public Dictionary<string, string> Description { get; set; }

        public string? ImageAlt { get; set; }

    }

    public class AdvertisementTabView
    {
        public string Id { get; set; }

        public string? AdvertisementId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? ImageAlt { get; set; }
    }
}
