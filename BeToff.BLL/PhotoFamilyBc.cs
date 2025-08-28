using BeToff.BLL.Dto.Request;
using BeToff.BLL.Interface;
using BeToff.BLL.Mapping;
using BeToff.DAL;
using BeToff.DAL.Interface;
using BeToff.Entities;

namespace BeToff.BLL
{
    public class PhotoFamilyBc : BeToffBc<PhotoFamilly, PhotoFamilyDao> , IPhotoFamilyBc
    {
        private readonly IPhotoFamilyDao _photoFamilyDao;
        public PhotoFamilyBc(IPhotoFamilyDao photoFamilyDao)
        {
            _photoFamilyDao = photoFamilyDao;
        }
        public async Task AddNewPhotoOnFamillyAlbum(PhotoFamillyCreateDto Dto)
        {
            var photo = PhotoFamillyCreateMapper.FromDto(Dto);
            await _photoFamilyDao.CreatePhotoFamily(photo);
        }
    }
}
