using BeToff.DAL.Interface;
using BeToff.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeToff.DAL;

public class PhotoDao : BeToffDao, IPhotoDao
{
    private readonly BeToffDbContext _PhotoDB;

    public PhotoDao(BeToffDbContext photoDB)
    {
        _PhotoDB = photoDB;
    }

    public async Task<bool> CreatePhoto(Photo photo)
    {
        try
        {
            await _PhotoDB.Photos.AddAsync(photo);
            _PhotoDB.SaveChanges();
            return true;
        }
        catch (Exception ex) {

            return false;
            
        }
    }

    public async Task<List<Photo>> GetPhotoByAuthor(Guid author)
    {
        var Result = await _PhotoDB.Photos
            .Where(opt => opt.IdAuthor == author)
            .ToListAsync();
        return Result;
    }
}