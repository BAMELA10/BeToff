using BeToff.Entities;

namespace BeToff.DAL;

public interface IPhotoDao : IBetoff<Photo>
{
    List<Photo> GetCommentsByAuthor(Guid id, string Name);
    List<Comment> GetCommentsByDate(Guid id, DateOnly DateCreation);
}