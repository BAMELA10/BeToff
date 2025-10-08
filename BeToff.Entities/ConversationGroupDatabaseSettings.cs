using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.Entities
{
    public class ConversationGroupDatabaseSettings : DatabaseSettings
    {
        public string? ConversationGroupsCollectionName { get; set; }

    }
}
