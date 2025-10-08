using BeToff.BLL.Dto.Request;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Mapping
{
    public static class ConversationGroupCreateMapper
    {
        public static ConversationGroup FromDto(ConversationGroupCreateDto Dto)
        {
            return new ConversationGroup
            {
                Family = Dto.Family,
                Participants = Dto.Participants,
                StartDate = Dto.StartDate,
            };
        }
    }
}
