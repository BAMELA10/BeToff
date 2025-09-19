using BeToff.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Dto.Request
{
    public class MessageCreateDto
    {
        public string SenderId { get; set; }
        public string Content { get; set; }
        public DateTime SendAt { get; set; }
        public string Conversation { get; set; }
    }
}
