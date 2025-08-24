using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Dto.Response
{
    public class FamillyResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public Guid IdCreator { get; set; }
        public string Head { get; set;}
        public Guid IdHead {  get; set; }

        public DateOnly CreateAt {  get; set; }

    }
}
