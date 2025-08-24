using BeToff.BLL.Dto.Response;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Mapping
{
    public static class FamillyMapper
    {
        public static FamillyResponseDto ToDto(Familly familly)
        {
            return new FamillyResponseDto
            {
                Id = familly.Id,
                Name = familly.Name,
                Head = familly.Headof.FullName,
                IdHead = familly.IdHead,
                Creator = familly.CreatedBy.FullName,
                IdCreator = familly.IdCreator,
                CreateAt = familly.DateCreation,
            };
        }
    }
}
