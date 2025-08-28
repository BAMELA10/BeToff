using BeToff.DAL.Interface;
using BeToff.Entities;
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

        public Task DeletePhotoFamily(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PhotoFamilly>> GetByFamily(Guid IdFamily)
        {
            throw new NotImplementedException();
        }

        public Task<PhotoFamilly> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
