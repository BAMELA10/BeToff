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
        public Task CreateInvitation();
        public Task ChangeStateInvitation();
    }
}
