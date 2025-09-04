using BeToff.BLL.Dto.Response;
using BeToff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Mapping
{
    public static class CommentResponseMapper
    {
        public static CommentResponseDto ToDto(Comment comment)
        {
            return new CommentResponseDto
            {
                Id = comment.Id,
                PubliedAt = comment.PubliedAt,
                IdAuthor = comment.Author,
                IdPhotoFamily = comment.PhotoFamily,
                Content = comment.Content

            };
        }
    }
}
