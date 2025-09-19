using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.DAL.Interface
{
    public interface IMessageService
    {
        public Task Create(Message message);
        public Task<List<Message>> GetMessagesByConversation(string conversationId);
    }
}
