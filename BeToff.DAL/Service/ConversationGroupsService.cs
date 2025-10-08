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
    public class ConversationGroupsService: IConversationGroupsService
    {

        protected readonly IMongoCollection<ConversationGroup> _collection;

        public ConversationGroupsService(IOptions<ConversationGroupDatabaseSettings> Settings)
        {
            var clients = new MongoClient(Settings.Value.ConnectionString);
            var db = clients.GetDatabase(Settings.Value.DatabaseName);
            db.CreateCollection(Settings.Value.ConversationGroupsCollectionName);
            _collection = db.GetCollection<ConversationGroup>(Settings.Value.ConversationGroupsCollectionName);
        }

        public async Task Create(ConversationGroup conversation)
        {
            await _collection.InsertOneAsync(conversation);
        }

        public async Task<ConversationGroup> GetById(string ConversationId)
        {
            return await _collection.Find(x => x.Id == ConversationId).FirstOrDefaultAsync();
        }

        public async Task<List<ConversationGroup>> GetByParticipant(string ParticipantId)
        {
            return await _collection.Find(x => x.Participants.Contains(ParticipantId)).ToListAsync();
        }

        public async Task<ConversationGroup> AddParticipantInConversation(string Family, string ParticipantId)
        {
            var filter = Builders<ConversationGroup>.Filter.Eq(x => x.Family , Family);
            var udpate = Builders<ConversationGroup>.Update.Push(x => x.Participants, ParticipantId);
            return await _collection.FindOneAndUpdateAsync<ConversationGroup>(filter, udpate);
        }
    }
}
