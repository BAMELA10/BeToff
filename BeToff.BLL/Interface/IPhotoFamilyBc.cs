using BeToff.BLL.Dto.Request;
using BeToff.Entities;

namespace BeToff.BLL.Interface
{
    public interface IPhotoFamilyBc: IBeToffBc<PhotoFamilly>
    {
        public Task AddNewPhotoOnFamillyAlbum(PhotoFamillyCreateDto Dto);
    }
}
