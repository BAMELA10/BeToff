using BeToff.BLL.Dto.Response;
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
        public Task<User> ComparePassword(Credentials credentials);

        public Task<List<UserResponseDto>> AllUser();

        public Task<UserResponseDto> GetSpecificuser(string Id);
    }
}
