using BeToff.BLL.Dto.Request;
using BeToff.BLL.Dto.Response;
using BeToff.BLL.Mapping;
using BeToff.BLL.Service.Interface;
using BeToff.DAL.Interface;

namespace BeToff.BLL.Service.Impl
{
    public class ChatGroupService : AbstractChatService, IChatGroupService
    {
        protected readonly IConversationGroupsService _conversationGroupsService;
        public ChatGroupService(IConversationGroupsService conversationGroupsService, IMessageService messageService) :base(messageService)
        {
            _conversationGroupsService = conversationGroupsService;
        }

        public async Task InitializeConversation(ConversationGroupCreateDto CreateDto)
        {
            var ToCreate = ConversationGroupCreateMapper.FromDto(CreateDto);
            await _conversationGroupsService.Create(ToCreate);
        }

        public async Task<ConversationGroupResponseDto?> TakeConversation(string Id)
        {
            var conversation = await _conversationGroupsService.GetById(Id);
            if (conversation != null) {
                return ConversationGroupResponseMapper.ToDto(conversation);
            }
            return null;
        }

        public async Task<List<ConversationGroupResponseDto>> LoadConversationByUser(string participantId)
        {
            var ConversationList = await _conversationGroupsService.GetByParticipant(participantId);
            var conversation = new List<ConversationGroupResponseDto>();

            if (ConversationList.Count == 0)
            {
                return conversation;
            }
            else
            {
                foreach (var item in ConversationList)
                {
                    var items = ConversationGroupResponseMapper.ToDto(item);
                    conversation.Add(items);
                }

                return conversation;
            }
        }
    }
}
