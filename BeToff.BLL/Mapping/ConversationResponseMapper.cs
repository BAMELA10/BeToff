using BeToff.BLL.Dto.Response;
using BeToff.Entities;


namespace BeToff.BLL.Mapping
{
    public static class ConversationResponseMapper
    {
        public static ConversationResponseDto ToDto(Conversation conversation)
        {
            return new ConversationResponseDto
            {
                Id = conversation.Id,
                Participant = conversation.Participant,
                StartDate = conversation.StartDate

            };
        }
    }
}
