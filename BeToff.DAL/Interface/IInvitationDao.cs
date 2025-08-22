using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.DAL.Interface
{
    public interface IInvitationDao : IBetoff<Invitation>
    {
        public Task<Task> CreateInvitation(Guid SenderId, Guid TargetId, Guid FamillyId);
        public Task<Task> CancelInvitation(Guid Id);
        public Task<Task> AcceptInvitation(Guid Id);

        public Task<List<Invitation>> GetAllInvitation();
        public Task<List<Invitation>> GetInvitationBySender(Guid SenderId);
        public Task<List<Invitation>> GetInvitationByReceiver(Guid ReceiverId);
        public Task<Invitation> GetSpecificInvitation(Guid Id);

        public Task<Task> DeleteInvitation(Guid Id);
    }
}
