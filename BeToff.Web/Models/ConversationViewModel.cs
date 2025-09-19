using BeToff.BLL.Dto.Response;

namespace BeToff.Web.Models
{
    public class ConversationViewModel
    {
        public ConversationResponseDto Conversation { get; set; }
        public UserResponseDto Sender { get; set; }
        public UserResponseDto Receiver { get; set; }
    }
}
