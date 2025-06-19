using BeToff.Entities;

namespace BeToff.DAL;

public interface IBetoff<T> where T: BeToffEntity
{
    T GetElement(Guid id);
    T Insert( Guid id);
    T Update( Guid id);
    T Delete(Guid id);
}