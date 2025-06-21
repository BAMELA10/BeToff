using BeToff.DAL;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL
{
    public abstract class BeToffBc<TEntity, TDao> where TEntity : BeToffEntity where TDao : BeToffDao
    {
        public TEntity Entity { get; set; }
        public TDao Dao { get; set; }
    }
}
