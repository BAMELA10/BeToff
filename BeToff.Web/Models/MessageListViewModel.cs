using BeToff.BLL.Dto.Response;

namespace BeToff.Web.Models
{
    public class MessageListViewModel
    {
        public List<MessageViewModel> message { get; set; }
        public int Count  { get; set; }
        public UserResponseDto Receiver { get; set; }
        public ConversationResponseDto Conversation{ get; set; }
    }
}
