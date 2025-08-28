using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.Entities
{
    public class PhotoFamilly: Photo
    {
        [ForeignKey(nameof(Family))]
        public Guid FamillyId { get; set; }
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public Familly Family { get; set; }
    }
}
