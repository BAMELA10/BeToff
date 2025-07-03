using BeToff.BLL.Interface;
using BeToff.DAL;
using BeToff.DAL.Interface;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL
{
    public  class PhotoBc : BeToffBc<Photo, PhotoDao>, IPhotoBc
    {
        private readonly IPhotoDao _photoDao;

        public PhotoBc(IPhotoDao photoDao)
        {
            _photoDao = photoDao;
        }

        public async Task<bool> SavePhoto(Photo photo) 
        {
            var resultDao = await _photoDao.CreatePhoto(photo);
            if (resultDao)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Photo>> ListPhotoForSpecificUser(string IdAuthor)
        {
            Guid Author = Guid.Parse(IdAuthor);
            var result = await _photoDao.GetPhotoByAuthor(Author);
            return result;
        }

        public async Task<Photo> GetSpecificPhoto(string Id)
        {
            Guid ID = Guid.Parse(Id);
            var result = await _photoDao.GetPhotoById(ID);
            return result.First();
        }

    }
}
