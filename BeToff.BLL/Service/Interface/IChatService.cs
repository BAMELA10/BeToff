using BeToff.BLL.Dto.Response;

namespace BeToff.BLL.Service.Interface
{
    public interface IChatService: IAbstractChatService
    {
        public Task InitializeConversation(string participant_1, string participant_2);

        public Task<ConversationResponseDto> TakeConversation(string conversationId);

        public Task<List<ConversationResponseDto>> LoadConversationByUser(string participantId);
    }
}
