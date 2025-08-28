using BeToff.DAL.Interface;
using BeToff.Entities;
using Microsoft.EntityFrameworkCore;
namespace BeToff.DAL
{
    public class PhotoFamilyDao : BeToffDao, IPhotoFamilyDao
    {
        private readonly BeToffDbContext _dbContext;
        public PhotoFamilyDao( BeToffDbContext dbContext )
        {
            _dbContext = dbContext;
        }
        public async Task CreatePhotoFamily(PhotoFamilly photo)
        {
            await _dbContext.PhotoFamilly.AddAsync( photo );
            _dbContext.SaveChanges();
            
        }

        public async Task<List<PhotoFamilly>> GetByFamily(Guid IdFamily)
        {
            var ListResult = await _dbContext.PhotoFamilly
                .Where(s => s.FamillyId == IdFamily)
                .Include(x => x.Author)
                .Include(y => y.Family)
                .ToListAsync();

            return ListResult;
        }

        public async Task<PhotoFamilly> GetById(Guid Id)
        {
            var result = await _dbContext.PhotoFamilly
               .Where(s => s.Id == Id)
               .Include(x => x.Author)
               .Include(y => y.Family)
               .FirstAsync();

            return result;
        }
    }
}
