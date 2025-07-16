using BeToff.BLL.Interface;
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

        public async Task RegistrationOfMemberOfFamilly(Guid FamillyId, string MemberId)
        {
            Guid IdMember = Guid.Parse(MemberId);
            await _Dao.AddRegistration(FamillyId, IdMember);
        }
    }
}
