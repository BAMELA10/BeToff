using BeToff.Entities;

namespace BeToff.DAL.Interface;

public interface IPhotoDao : IBetoff<Photo>
{
    
    public Task<bool> CreatePhoto(Photo photo);
    public Task<List<Photo>> GetPhotoByAuthor(Guid author);

}