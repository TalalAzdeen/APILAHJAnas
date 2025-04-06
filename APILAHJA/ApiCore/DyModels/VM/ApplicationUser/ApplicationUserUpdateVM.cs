using AutoGenerator;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// ApplicationUser  property for VM Update.
    /// </summary>
    public class ApplicationUserUpdateVM : ITVM
    {
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? Image { get; set; }
        public String? PhoneNumber { get; set; }

    }
}