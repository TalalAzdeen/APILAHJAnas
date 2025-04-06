namespace AutoGenerator.VM
{
    public class AppUserResponseVM : ITVM
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string DisplayName { get; init; }
        public string? Image { get; init; }
        public string? ProfileUrl { get; init; }
        public required string Email { get; init; }
        public string? CustomerId { get; init; }
        public string? SubscriptionId { get; init; }
        public string? PhoneNumber { get; init; }
        public bool EmailConfirmed { get; init; }
        public bool PhoneNumberConfirmed { get; init; }
        public string? Role { get; init; }
        //public SubscriptionBuilderResponseDto Subscription { get; init; }
    }

    public class AppUserRequestVM : ITVM
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public string? Image { get; init; }
        public string? ProfileUrl { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
