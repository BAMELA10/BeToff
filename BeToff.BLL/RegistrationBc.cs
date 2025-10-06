using BeToff.BLL.Dto.Response;
using BeToff.BLL.Interface;
using BeToff.BLL.Mapping;
using BeToff.DAL;
using BeToff.BLL.EventArg;
using BeToff.DAL.Interface;
using BeToff.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeToff.DAL.Service;

namespace BeToff.BLL
{
    public  class RegistrationBc: BeToffBc<Registration, RegistrationDao>, IRegistrationBc
    {
        private readonly IRegistrationDao _Dao;
        private readonly IFamillyDao _famillyDao;
        private readonly IConversationGroupsService _conversationGroupsService;
        public RegistrationBc(IRegistrationDao dao, IFamillyDao famillyDao, IConversationGroupsService conversationGroupsService)
        {
             _Dao = dao;
            _famillyDao = famillyDao;
            _conversationGroupsService = conversationGroupsService;
        }

        public async Task<List<RegistrationResponseDto>> ListOfRegistrationForFamilly(string FamillyId)
        {
            Guid IdFamilly = Guid.Parse(FamillyId);
            var ListItem = await _Dao.GetRegistrationByFamilly(IdFamilly);
            var FinalList = new List<RegistrationResponseDto>();
            foreach (var item in ListItem)
            {
                var registration = RegistrationMapper.ToDto(item);
                FinalList.Add(registration);
            }
            return FinalList;
        }

        public async Task<List<RegistrationResponseDto>> ListOfRegistrationForUser(string UserId)
        {
            Guid IdFamilly = Guid.Parse(UserId);
            var ListItem = await _Dao.GetRegistrationByUser(IdFamilly);
            var FinalList = new List<RegistrationResponseDto>();
            foreach (var item in ListItem)
            {
                var registration = RegistrationMapper.ToDto(item);
                FinalList.Add(registration);
            }
            return FinalList;
        }
        

        public async Task RegistrationOfMemberOfFamilly(Guid FamillyId, string MemberId)
        {
            Guid IdMember = Guid.Parse(MemberId);
            await _Dao.AddRegistration(FamillyId, IdMember);
            var Conversation = await _conversationGroupsService.AddParticipantInConversation(FamillyId.ToString(), MemberId);
            //Handler.Invoke(this, new ParticipantAddedEventArgs { conversationId = Conversation.Id });
        }

        public async Task RemoveSpecificFamillyMember(string FamillyId, string MemberId, string Userid)
        {
            Guid IdFamilly = Guid.Parse(FamillyId);
            Guid IdMember = Guid.Parse(MemberId);
            Guid IdUser = Guid.Parse(Userid);

            var registration = await _Dao.GetRegistrationByFamillyAndUser(IdFamilly, IdMember);
            
            var registrationDto = RegistrationMapper.ToDto(registration);

            //Check if the user is concern by regitration of if is the head of familly
            if(registrationDto.UserRegisteredId == IdMember || registrationDto.FamillyHeadId == IdUser)
            {
                await _Dao.DeleteRegistration(IdFamilly, IdMember);
                var RegistrationsForfamilly = await _Dao.GetRegistrationByFamilly(IdFamilly);
                var CountRegistration = RegistrationsForfamilly.Count;
                //if the familly do not contains any member now, the familly must be delete after Exit of the last person
                if(CountRegistration == 0)
                {
                    Console.WriteLine("Dernier membre supprimé");
                    await _famillyDao.DeleteFamillyById(IdFamilly);
                }
            }
            else
            {
                throw new Exception("User is not Authorized to Delete this registration");
            }


        }

        public async Task<Guid> SelectRandomIdentiferMenberOfFamilly(string FamillyId)
        {
            Guid IdFamilly = Guid.Parse(FamillyId);
            var RegistrationOfFamilly = await _Dao.GetRegistrationByFamilly(IdFamilly);
            var ListOfIdMember = new List<Guid>();
            RegistrationOfFamilly.ForEach(x =>
            {
                var item = RegistrationMapper.ToDto(x);
                ListOfIdMember.Add(item.UserRegisteredId);
            });
            Random random = new Random();
            int randomNumber = random.Next(0, ListOfIdMember.Count - 1);
            return ListOfIdMember[randomNumber];

        }
    }
}
