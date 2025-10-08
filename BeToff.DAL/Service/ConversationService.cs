using BeToff.DAL.Interface;
using BeToff.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.DAL.Service
{
    public class ConversationService: IConversationService
    {
        protected readonly IMongoCollection<Conversation> _collection;

        public ConversationService(IOptions<ConversationDatabaseSettings> Settings)
        {
            var Clients = new MongoClient(Settings.Value.ConnectionString);

            var Database = Clients.GetDatabase(Settings.Value.DatabaseName);

            _collection = Database.GetCollection<Conversation>(Settings.Value.ConversationCollectionName);
        }

        public async Task Create(Conversation conversation)
        {
            await _collection.InsertOneAsync(conversation);
        }

        public async Task<Conversation> GetById(string ConversationId)
        {
            var conversation =  await _collection
                .Find(x => x.Id == ConversationId)
                .FirstOrDefaultAsync();
            return conversation;
        }

        public async Task<List<Conversation>> GetByParticipant(string ParticipantId)
        {
            var conversations = await _collection.Find(x => x.Participant.Contains(ParticipantId))
                .ToListAsync();

            return conversations;
        }
    }
}
