using BeToff.BLL.Dto.Request;
using BeToff.BLL.Dto.Response;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Service.Interface
{
    public interface IChatGroupService : IAbstractChatService
    {
        public Task InitializeConversation(ConversationGroupCreateDto CreateDto);
        public Task<ConversationGroupResponseDto?> TakeConversation(string Id);
        public Task<List<ConversationGroupResponseDto>> LoadConversationByUser(string participantId);

    }
}
