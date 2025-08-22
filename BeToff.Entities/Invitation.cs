using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.Entities
{
    public class Invitation: Notification
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }

        public Guid FamillyId { get; set; }

        public DateTime ExpireAt { get; set; }
        public User? Sender { get; set; }
        public User? Receiver { get; set; }

        public Familly? FamillyItem { get; set; }

    }
}
