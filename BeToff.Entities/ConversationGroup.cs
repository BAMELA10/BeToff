using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.Entities
{
    public class ConversationGroup : ComEntity
    {
        public string Family {  get; set; } = String.Empty;
        public List<string> Participants { get; set; } = new List<string>();
        public DateTime StartDate { get; set; }

    }
}
