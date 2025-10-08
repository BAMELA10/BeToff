using BeToff.BLL.Dto.Request;
using BeToff.BLL.Dto.Response;
using BeToff.BLL.Mapping;
using BeToff.BLL.Service.Interface;
using BeToff.DAL.Interface;
using BeToff.DAL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Service.Impl
{
    public abstract class AbstractChatService: IAbstractChatService
    {
        protected readonly IMessageService _messageService;

        protected AbstractChatService(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task SaveMessage(MessageCreateDto message)
        {
            var item = MessageCreateMapper.FromDto(message);
            await _messageService.Create(item);
        }

        public async Task<List<MessageResponseDto>> LoadMessageForSpecificConversation(string conversationId)
        {
            var MessagesList = await _messageService.GetMessagesByConversation(conversationId);
            var messages = new List<MessageResponseDto>();

            if (MessagesList.Count == 0)
            {
                return messages;
            }
            else
            {
                foreach (var message in MessagesList)
                {
                    var items = MessageResponseMapper.ToDto(message);
                    messages.Add(items);
                }

                return messages;
            }

        }
    }
}
