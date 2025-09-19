using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeToff.DAL.Interface;
using BeToff.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BeToff.DAL.Service
{
    public class MessageService : IMessageService
    {
        protected readonly IMongoCollection<Message> _collection;

        public MessageService(IOptions<MessageDatabaseSettings> Settings)
        {
            var Clients = new MongoClient(Settings.Value.ConnectionString);

            var Database = Clients.GetDatabase(Settings.Value.DatabaseName);

            _collection = Database.GetCollection<Message>(Settings.Value.MessageCollectionName);
        }

        public async Task Create(Message message)
        {
            await _collection.InsertOneAsync(message);
        }

        public async Task<List<Message>> GetMessagesByConversation(string conversationId)
        {
            var result = await _collection.Find(x => x.conversation == conversationId)
                .ToListAsync();
            return result;
        }
    }
}
