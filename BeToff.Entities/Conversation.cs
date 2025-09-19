using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;


namespace BeToff.Entities
{
    public class Conversation : ComEntity
    {
        [Range(2, 2)]
        public List<string> Participant { get; set; }
        public DateTime StartDate { get; set; }



    }
}
