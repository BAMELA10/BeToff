using BeToff.Entities.Enum;

namespace BeToff.BLL.Dto.Response
{
    public class MessageResponseDto
    {
        public string Id { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }

        public DateTime SendAt { get; set; }
        public string Status { get; set; }

        public string conversation { get; set; }
    }
}