using BeToff.BLL.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeToff.Entities;

namespace BeToff.BLL.Mapping
{
    public static class CommentCreateMapper
    {
        public static Comment FromDto(CommentCreateDto Dto)
        {
            return new Comment
            {
                Content = Dto.Content,
                PubliedAt = Dto.PubliedAt,
                Author = Dto.Author,
                PhotoFamily = Dto.PhotoFamily
            };
        }
    }
}
