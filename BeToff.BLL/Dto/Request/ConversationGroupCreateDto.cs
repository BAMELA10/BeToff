using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Dto.Request
{
    public class ConversationGroupCreateDto
    {
        public string Family { get; set; }
        public List<string> Participants { get; set; }
        public DateTime StartDate { get; set; }
    }
}
