using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.Entities
{
    public class MessageDatabaseSettings : DatabaseSettings
    {
        public string? MessageCollectionName { get; set; }
    }
}
