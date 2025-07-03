using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Interface
{
    public interface IPhotoBc: IBeToffBc<Photo>
    {
        public Task<bool> SavePhoto(Photo photo);

        public Task<List<Photo>> ListPhotoForSpecificUser(string IdAuthor);
    }
}
