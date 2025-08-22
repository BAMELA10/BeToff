using BeToff.BLL.Dto.Response;
using BeToff.BLL.Interface;
using BeToff.BLL.Mapping;
using BeToff.DAL;
using BeToff.DAL.Interface;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL
{
    public  class RegistrationBc: BeToffBc<Registration, RegistrationDao>, IRegistrationBc
    {
        private readonly IRegistrationDao _Dao;
        public RegistrationBc(IRegistrationDao dao)
        {
             _Dao = dao;
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
        }

        public async Task RemoveSpecificFamillyMember(string FamillyId, string MemberId, string Userid)
        {
            Guid IdFamilly = Guid.Parse(FamillyId);
            Guid IdMember = Guid.Parse(MemberId);
            Guid IdUser = Guid.Parse(Userid);

            var registration = await _Dao.GetRegistrationByFamillyAndUser(IdFamilly, IdMember);
            
            var registrationDto = RegistrationMapper.ToDto(registration);

            //Check if the user is concern by regitration of if is the head of familly
            if(registrationDto.UserRegisteredId == IdMember && registrationDto.FamillyHeadId == IdUser)
            {
                await _Dao.DeleteRegistration(IdFamilly, IdMember);
            }
            else
            {
                throw new Exception("User is not Authorized to Delete this registration");
            }


        }
    }
}
