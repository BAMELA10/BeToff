using BeToff.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.DAL
{
    public class InvitationDao : BeToffDao, IInvitationDao
    {
        public Task ChangeStateInvitation()
        {
            throw new NotImplementedException();
        }

        public Task CreateInvitation()
        {
            throw new NotImplementedException();
        }
    }
}
