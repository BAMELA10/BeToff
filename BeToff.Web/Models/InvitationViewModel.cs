using BeToff.BLL.Dto.Response;
using BeToff.Entities;

namespace BeToff.Web.Models
{
    public class InvitationViewModel
    {
        public Invitation? Invitation { get; set; }
        public List<InvitationResponseDto>? InvitationsReceived { get; set; }
        public List<InvitationResponseDto>? InvitationsSended { get; set; }
    }
}
