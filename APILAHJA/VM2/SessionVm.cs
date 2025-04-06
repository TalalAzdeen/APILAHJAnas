
using VM;

namespace Api.VM
{
    public class SessionVm
    {
        public string Id { get; set; }

        public string SessionToken { get; set; }
        public string? UserToken { get; set; }
        public string AuthorizationType { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsActive { get; set; }
        public string IpAddress { get; set; }

        public string DeviceInfo { get; set; }

        public string[]? Services { get; set; }

        public string ModelGatewayId { set; get; }

        public string? Subscription { get; set; }
        public DateTime CreationDate => StartTime;

        public string? ApiEndPoint { get; set; }
        public bool Status => IsActive;
        public ICollection<SpaceResponse> Spaces { get; set; }

    }
}
