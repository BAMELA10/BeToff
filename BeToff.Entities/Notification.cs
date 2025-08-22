using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.Entities
{
    public class Notification : BeToffEntity
    {
        public DateOnly SendAt { get; set; }

    }
}
