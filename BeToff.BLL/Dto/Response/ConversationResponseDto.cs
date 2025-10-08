using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Dto.Response
{
    public class ConversationResponseDto
    {
        public string? Id { get; set; }
        public List<string> Participant { get; set; }
        public DateTime StartDate { get; set; }

    }
}
