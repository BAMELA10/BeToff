using BeToff.BLL.Dto.Response;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Mapping
{
    public static class ConversationGroupResponseMapper
    {
        public static ConversationGroupResponseDto ToDto(ConversationGroup conversationGroup)
        {
            return new ConversationGroupResponseDto
            {
                id = conversationGroup.Id,
                Family = conversationGroup.Family,
                Participants = conversationGroup.Participants,
                StartDate = conversationGroup.StartDate
            };
        }
    }
}
