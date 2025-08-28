using BeToff.Entities;

namespace BeToff.DAL.Interface
{
    public interface IPhotoFamilyDao : IBetoff<PhotoFamilly>
    {
        public Task CreatePhotoFamily(PhotoFamilly photo);
        public Task<List<PhotoFamilly>> GetByFamily(Guid IdFamily);
        public Task<PhotoFamilly> GetById(Guid Id);
    }
}
