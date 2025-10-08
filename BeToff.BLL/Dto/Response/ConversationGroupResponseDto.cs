using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Dto.Response
{
    public class ConversationGroupResponseDto
    {
        public string? id { get; set; }
        public string? Family { get; set; }
        public List<string>? Participants { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
