using BeToff.DAL;
using BeToff.Entities;

namespace BeToff.BLL
{
    public abstract class BeToffBc<TEntity, TDao> where TEntity : BeToffEntity where TDao : BeToffDao
    {
        public TEntity Entity { get; set; }
        public TDao Dao { get; set; }
    }
}
