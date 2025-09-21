using BeToff.BLL.Dto.Response;

namespace BeToff.Web.Models
{
    public class MessageListViewModel
    {
        public List<MessageViewModel> message { get; set; }
        public int Count  { get; set; }
        public UserResponseDto? Receiver { get; set; }
        public List<UserResponseDto> Receivers { get; set; } = new List<UserResponseDto>();
        public ConversationResponseDto? Conversation{ get; set; }
        public ConversationGroupResponseDto? ConversationGroup { get; set; }
        public FamillyResponseDto? FamillyResponse{ get; set; }
    }
}
