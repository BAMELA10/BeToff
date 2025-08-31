using BeToff.BLL.Dto.Response;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Mapping
{
    public static class PhotoFamilyResponseMapper
    {
        public static PhotoFamilyResponseDto ToDto(PhotoFamilly picture)
        {
            return new PhotoFamilyResponseDto
            {
                Id = picture.Id.ToString(),
                Title = picture.Title,
                AuthorId = picture.AuthorId.ToString(),
                AuthorName = picture.Author.FullName,
                FamilyId = picture.FamillyId.ToString(),
                FamilyName = picture.Family.Name,
                PubliedAt = picture.DateCreation,
                Image = Path.GetFileName(picture.Image),


            };

        }
    }
}
