using BeToff.BLL.Dto.Request;
using BeToff.BLL.Dto.Response;
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
        private readonly IPhotoDao _photoDao;
        private readonly ICommentService _commentService;
        public PhotoFamilyBc(IPhotoFamilyDao photoFamilyDao, IPhotoDao photoDao, ICommentService commentService)
        {
            _photoFamilyDao = photoFamilyDao;
            _photoDao = photoDao;
            _commentService = commentService;

        }
        public async Task AddNewPhotoOnFamillyAlbum(PhotoFamillyCreateDto Dto)
        {
            var photo = PhotoFamillyCreateMapper.FromDto(Dto);
            await _photoFamilyDao.CreatePhotoFamily(photo);
        }

        public async Task CommentPhotoFamily(CommentCreateDto Dto)
        {
            var Comment = CommentCreateMapper.FromDto(Dto);
            await _commentService.InsertComment(Comment);
        }

        public async Task<List<PhotoFamilyResponseDto>> GenerateAlbumForFamily(string FamillyId)
        {
            var IdFamily = Guid.Parse(FamillyId);
            var ListResult = await _photoFamilyDao.GetByFamily(IdFamily);
            var NewList = new List<PhotoFamilyResponseDto>();
            foreach (var item in ListResult)
            {
                var NewItem = PhotoFamilyResponseMapper.ToDto(item);
                NewList.Add(NewItem);
            }

            return NewList;

        }

        public async Task<PhotoFamilyResponseDto> GetSpecificPcitureOfFamily(string Id, string FamillyId)
        {
            var NewId = Guid.Parse(Id);
            var IdFamily = Guid.Parse(FamillyId);
            PhotoFamilly Photo = await _photoFamilyDao.GetById(NewId);
            return PhotoFamilyResponseMapper.ToDto(Photo);
        }

        public async Task RemovePhotoFromFamilyAlbum(string Id, string FamillyId)
        {
            var NewId = Guid.Parse(Id);
            var IdFamily = Guid.Parse(FamillyId);
            PhotoFamilly Photo = await _photoFamilyDao.GetById(NewId);
            if (Photo != null && Photo.FamillyId == IdFamily)
            {
                await _photoDao.DeleteById(NewId);
            }
            else
            {
                throw new Exception("Wrong PhotoFamilly: It dosn't exist");
            }
            
        }
    }
}
