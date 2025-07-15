using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeToff.Entities;

namespace BeToff.Entities
{
    public class Registration : BeToffEntity
    {
        public Guid UserId { get; set; }
        public Guid FamillyId { get; set; }

        public DateTime DateOfregistation { get; set; }

        public User? User { get; set; }
        public Familly? Familly { get; set; }


    }
}
