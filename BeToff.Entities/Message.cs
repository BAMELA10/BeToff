using BeToff.Entities.Enum;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.Entities
{
    public class Message : ComEntity
    {
        public string SenderId { get; set; }
        public string Content { get; set; }

        public DateTime SendAt { get; set; }
        public MessageStatus Status { get; set; } = MessageStatus.Send;

        [BsonRepresentation(BsonType.ObjectId)]
        public string conversation { get; set; }
    }
}
