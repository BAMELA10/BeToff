using BeToff.BLL.Dto.Response;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Mapping
{
    public static class UserResponseMapper
    {
        public static UserResponseDto ToDto(User user)
        {
            return new UserResponseDto
            {
                Email = user.Email,
                Id = user.Id.ToString(),
                PhoneNumber = user.PhoneNumber,
                FullName = user.FirstName + " " + user.LastName,
            };
        }
    }
}
