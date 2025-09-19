using BeToff.BLL.Dto.Response;

namespace BeToff.Web.Models
{
    public class MessageViewModel
    {
        public MessageResponseDto Messages { get; set; }
        public UserResponseDto Sender { get; set; }
    }
}
