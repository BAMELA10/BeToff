using BeToff.BLL.Dto.Request;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Mapping
{
    public static class PhotoFamillyCreateMapper
    {
        public static PhotoFamillyCreateDto ToDto(PhotoFamilly photoFamilly)
        {
            return new PhotoFamillyCreateDto
            {
                Title = photoFamilly.Title,
                Image = photoFamilly.Image,
                DateCreation = photoFamilly.DateCreation,
                AuthorId = photoFamilly.AuthorId,
                FamilyId = photoFamilly.FamillyId,
            };
        }

        public static PhotoFamilly FromDto(PhotoFamillyCreateDto photoFamilly)
        {
            return new PhotoFamilly
            {
                Title = photoFamilly.Title,
                Image = photoFamilly.Image,
                DateCreation = photoFamilly.DateCreation,
                AuthorId = photoFamilly.AuthorId,
                FamillyId = photoFamilly.FamilyId,
            };
        }
    }
}
