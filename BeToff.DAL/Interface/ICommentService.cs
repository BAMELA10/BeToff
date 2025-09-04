using System.Reflection.Metadata.Ecma335;
using BeToff.Entities;

namespace BeToff.DAL.Interface;

public interface ICommentService : IComDao<Comment>
{
    Task InsertComment(Comment comment);
    Task<List<Comment>> GetCommentsByFamilyPicture(string IdPictureFamily);



}