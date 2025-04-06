using AutoGenerator;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// ApplicationUser  property for VM Output.
    /// </summary>
    public class ApplicationUserOutputVM : ITVM
    {
        public String? CustomerId { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? DisplayName { get; set; }
        ///
        public String? ProfileUrl { get; set; }
        ///
        public String? Image { get; set; }
        ///
        public Boolean IsArchived { get; set; }
        ///
        public Nullable<DateTime> ArchivedDate { get; set; }
        ///
        public String? LastLoginIp { get; set; }
        ///
        public Nullable<DateTime> LastLoginDate { get; set; }
        ///
        public DateTime CreatedAt { get; set; }
        ///
        public DateTime UpdatedAt { get; set; }
        //
        public String? Id { get; set; }
        ///
        public String? UserName { get; set; }
        ///
        ///
        public String? Email { get; set; }
        ///
        ///
        public Boolean EmailConfirmed { get; set; }
        ///
        public String? PhoneNumber { get; set; }
        ///
        public Boolean PhoneNumberConfirmed { get; set; }
        ///
        public Boolean TwoFactorEnabled { get; set; }
        ///
        public Nullable<DateTimeOffset> LockoutEnd { get; set; }
        ///
        public Boolean LockoutEnabled { get; set; }
        ///
        public Int32 AccessFailedCount { get; set; }
    }
}