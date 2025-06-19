using System.Reflection.Metadata.Ecma335;
using BeToff.Entities;

namespace BeToff.DAL;

public interface ICommentDao : IBetoff<Comment>
{
    List<Comment> GetCommentsByDate(Guid id, DateOnly DateCreation);
    List<Comment> GetCommentsByAuthor(Guid id, string Name);

}