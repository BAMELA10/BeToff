using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Service.Interface
{
    public interface IUserInvitationService
    {
        public Task<Task> SendInvitationBySenderToDatabase(string SenderId, string TargetId, string FamillyId);
        public Task<List<Invitation>> ReceiveInvitationByReceiverFromDatabase(string receiverId);
        public Task<List<Invitation>> ReceiveInvitationBySenderFromDatabase(string SenderId);
        public Task<Task> AcceptInvitation(string invitationId);
        public Task<Task> RefuseInvitation(string invitationId);
        public Task<Task> CancelInvitationByTimeWasted(string invitationId);
        public Task<List<Invitation>> AllInvitation();
        public Task<Task> DeleteInvitation(string invitationId);

    }
}
