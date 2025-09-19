using BeToff.BLL.Dto.Request;
using BeToff.Entities;
using MongoDB.Driver.Core.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Mapping
{
    public static class MessageCreateMapper
    {
        public static Message FromDto(MessageCreateDto message)
        {
            return new Message
            {
                Content = message.Content,
                SenderId = message.SenderId,
                SendAt = message.SendAt,
                conversation = message.Conversation,
            };
        }
    }
}
