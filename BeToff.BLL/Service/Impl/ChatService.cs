using BeToff.BLL.Dto.Request;
using BeToff.BLL.Service.Interface;
using BeToff.DAL.Interface;
using BeToff.Entities;
using BeToff.BLL.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeToff.BLL.Mapping;

namespace BeToff.BLL.Service.Impl
{
    public class ChatService : AbstractChatService,IChatService
    {
        protected readonly IConversationService _conversationService;
        public ChatService(IConversationService conversationService, IMessageService messageService) : base(messageService)
        {
            _conversationService = conversationService;
        }

        public async Task InitializeConversation(string participant_1, string participant_2)
        {
            List<string> participant = [participant_1, participant_2];
            Conversation conversation = new Conversation 
            {
                Participant = participant,
                StartDate = DateTime.Now,
            };
            await _conversationService.Create(conversation);
        }

        public async Task<ConversationResponseDto> TakeConversation(string conversationId)
        {
            var item =  await _conversationService.GetById(conversationId);
            var result = ConversationResponseMapper.ToDto(item);
            return result;

        }
        public async Task<List<ConversationResponseDto>> LoadConversationByUser(string participantId)
        {
            var ConversationList = await _conversationService.GetByParticipant(participantId);
            var conversation = new List<ConversationResponseDto>();

            if (ConversationList.Count == 0)
            {
                return conversation;
            }
            else
            {
                foreach (var item in ConversationList)
                {
                    var items = ConversationResponseMapper.ToDto(item);
                    conversation.Add(items);
                }

                return conversation;
            }
        }
    }
}
