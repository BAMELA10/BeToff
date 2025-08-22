using BeToff.BLL.Dto.Response;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Mapping
{
    public static class InvitationMapper 
    {
        public static InvitationResponseDto ToDto (Invitation Invitation)
        {
            return new InvitationResponseDto
            {
                Id = Invitation.Id,
                SenderName = Invitation.Sender.FullName,
                Receiver = Invitation.Receiver.FullName,
                SendAt = Invitation.SendAt,
                ExpireAt = Invitation.ExpireAt,
                Status = Invitation.Status.ToString(),
                FamilyName = Invitation.FamillyItem.Name
            };
        }
    }
}
