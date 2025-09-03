using System.Reflection.Metadata.Ecma335;
using BeToff.Entities;

namespace BeToff.DAL.Interface;

public interface ICommentService 
{
    List<Comment> GetCommentsByDate(Guid id, DateOnly DateCreation);

}