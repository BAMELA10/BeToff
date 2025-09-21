using BeToff.BLL.Dto.Request;
using BeToff.BLL.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Service.Interface
{
    public interface IAbstractChatService
    {
        public Task SaveMessage(MessageCreateDto message);

        public Task<List<MessageResponseDto>> LoadMessageForSpecificConversation(string conversationId);
    }
}
