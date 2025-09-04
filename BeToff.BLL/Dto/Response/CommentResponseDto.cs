using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Dto.Response
{
    public class CommentResponseDto
    {
        public string? Id { get; set; }
        public string? Content { get; set; }

        public DateOnly? PubliedAt { get; set; }

        public string? IdAuthor { get; set; }

        public string? AuthorName { get; set; }
        public string? IdPhotoFamily { get; set; }
        public string? PhotoFamilyTitle { get; set; }
    }
}
