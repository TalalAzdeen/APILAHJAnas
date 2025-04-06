using AutoGenerator;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// ApplicationUser  property for VM Create.
    /// </summary>
    public class ApplicationUserCreateVM : ITVM
    {
        public String? FirstName { get; set; }
        public String? LastName { get; set; }

        public String? Image { get; set; }
        public String? Email { get; set; }
        public String? PhoneNumber { get; set; }
        public Boolean TwoFactorEnabled { get; set; }
    }
}