using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Dto.Response
{
    public class RegistrationResponseDto
    {
        public Guid Id { get; set; }

        public DateTime DateOfregistation { get; set; }

        public string UserRegistered { get; set; }

        public Guid UserRegisteredId { get; set; }

        public string FamillyConcerned { get; set; }

        public Guid FamillyConcernedId { get; set; }

        public Guid FamillyHeadId { get; set; }
    }
}
