using BeToff.Entities;

namespace BeToff.DAL.Interface
{
    public interface IConversationGroupsService
    {
        public Task Create(ConversationGroup conversation);
        public Task<ConversationGroup> GetById(string ConversationId);
        public Task<List<ConversationGroup>> GetByParticipant(string ParticipantId);

        public Task<ConversationGroup> AddParticipantInConversation(string Family, string ParticipantId);
    }
}
