using BeToff.BLL.Dto.Request;
using BeToff.BLL.Dto.Response;
using BeToff.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BeToff.BLL.Interface
{
    public interface IPhotoFamilyBc: IBeToffBc<PhotoFamilly>
    {
        public Task AddNewPhotoOnFamillyAlbum(PhotoFamillyCreateDto Dto);
        public Task<List<PhotoFamilyResponseDto>> GenerateAlbumForFamily(string  FamillyId);
        public Task<PhotoFamilyResponseDto> GetSpecificPcitureOfFamily(string Id, string FamillyId);
        public Task RemovePhotoFromFamilyAlbum(string Id, string FamillyId);
        public Task CommentPhotoFamily(CommentCreateDto Dto);


    }
}
