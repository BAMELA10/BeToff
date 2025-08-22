using BeToff.BLL.Dto.Response;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Interface
{
    public interface IRegistrationBc : IBeToffBc<Registration>
    {
        public Task RegistrationOfMemberOfFamilly(Guid FamillyId, string MemberId);
        public Task<List<RegistrationResponseDto>> ListOfRegistrationForFamilly(string FamillyId);
        public Task<List<RegistrationResponseDto>> ListOfRegistrationForUser(string UserId);
        public Task RemoveSpecificFamillyMember(string FamillyId, string MemberId);

    }
}
