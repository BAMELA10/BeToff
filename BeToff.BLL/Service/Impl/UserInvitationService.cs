using BeToff.BLL.Service.Interface;
using BeToff.DAL;
using BeToff.DAL.Interface;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Service.Impl
{
    public class UserInvitationService : IUserInvitationService
    {
        protected readonly IInvitationDao _invitationDao;
        protected readonly IRegistrationDao _registrationDao;
        protected readonly IConversationGroupsService _conversationGroupsService;
        public UserInvitationService(IInvitationDao invitationDao, IRegistrationDao registrationDao,IConversationGroupsService conversationGroupsService)
        {
            _invitationDao = invitationDao;
            _registrationDao = registrationDao;
            _conversationGroupsService = conversationGroupsService;

        }

        public async Task<Task> AcceptInvitation(string invitationId)
        {
            Guid IdInvitation = Guid.Parse(invitationId);
            var Item = await _invitationDao.GetSpecificInvitation(IdInvitation);
            var CkeckedUserFamilly = await _registrationDao.GetRegistrationByFamillyAndUser(Item.FamillyId, Item.ReceiverId);
            
            if(CkeckedUserFamilly != null)
            {
                await _invitationDao.AcceptInvitation(IdInvitation);
                return Task.CompletedTask;
            }
            if (Item != null)
            {
                await _registrationDao.AddRegistration(Item.FamillyId, Item.ReceiverId);
                await _invitationDao.AcceptInvitation(IdInvitation);
                await _conversationGroupsService.AddParticipantInConversation(Item.FamillyId.ToString(), Item.ReceiverId.ToString());
            }
            else {
                throw new Exception("The Current Invitation Doesn't exist");
            }
            return Task.CompletedTask;
        }

        public async Task<Task> DeleteInvitation(string invitationId)
        {

            Guid IdInvitation = Guid.Parse(invitationId);
            await _invitationDao.DeleteInvitation(IdInvitation);
            return Task.CompletedTask;
        }

        public async Task<Task> CancelInvitationByTimeWasted(string invitationId)
        {

            Guid IdInvitation = Guid.Parse(invitationId);
            Invitation InvitationToCancel = await _invitationDao.GetSpecificInvitation(IdInvitation);
            if (InvitationToCancel.ExpireAt <= DateOnly.FromDateTime(DateTime.Today))
            {
                await _invitationDao.CancelInvitation(IdInvitation);
            }
            return Task.CompletedTask;
        }

        public async Task<List<Invitation>> ReceiveInvitationByReceiverFromDatabase(string receiverId)
        {
            Guid receiver = Guid.Parse(receiverId);
            var Invitations = await _invitationDao.GetInvitationByReceiver(receiver);
            return Invitations;

        }

        public async Task<List<Invitation>> ReceiveInvitationBySenderFromDatabase(string SenderId)
        {
            Guid Sender = Guid.Parse(SenderId);
            var Invitations = await _invitationDao.GetInvitationBySender(Sender);
            return Invitations;

        }

        public async Task<Task> RefuseInvitation(string invitationId)
        {
            Guid IdInvitation = Guid.Parse(invitationId);
            await _invitationDao.CancelInvitation(IdInvitation);

            return Task.CompletedTask;
        }
        public async Task<Task> SendInvitationBySenderToDatabase(string SenderId, string TargetId, string FamillyId)
        {
            Guid IdSender = Guid.Parse(SenderId);
            Guid IdTarget = Guid.Parse(TargetId);
            Guid IdFamilly = Guid.Parse(FamillyId);

            await _invitationDao.CreateInvitation(IdSender, IdTarget, IdFamilly);

            return Task.CompletedTask;
        }

        public async Task<List<Invitation>> AllInvitation()
        {
            var Invitations = await _invitationDao.GetAllInvitation();
            return Invitations;
        }
        
    }
}
