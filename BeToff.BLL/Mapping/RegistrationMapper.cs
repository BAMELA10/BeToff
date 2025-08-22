using BeToff.BLL.Dto.Response;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Mapping
{
    public static class RegistrationMapper
    {
        public static RegistrationResponseDto ToDto(Registration registration)
        {
            return new RegistrationResponseDto
            {
                Id = registration.Id,
                DateOfregistation = registration.DateOfregistation,
                UserRegistered = registration.User.FullName,
                UserRegisteredId = registration.UserId,
                FamillyConcerned = registration.Familly.Name,
                FamillyConcernedId = registration.FamillyId,
                FamillyHeadId = registration.Familly.IdHead

            };
        }
        
    }
}
