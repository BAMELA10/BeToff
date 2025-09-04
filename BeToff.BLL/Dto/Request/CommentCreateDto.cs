using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Dto.Request
{
    public class CommentCreateDto
    {
        public string Content { get; set; }
        public DateOnly PubliedAt { get; set; }
        public string Author { get; set; }
        public string PhotoFamily { get; set; }

    }
}
