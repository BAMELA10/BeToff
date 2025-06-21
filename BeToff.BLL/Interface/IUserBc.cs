using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Interface
{
    public interface IUserBc : IBeToffBc<User>
    {
        public Task HashPasswordAndInsertUser(User user);
    }
}
