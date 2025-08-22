using BeToff.Entities;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeToff.BLL.Dto.Response
{
    public class InvitationResponseDto
    {
        public Guid Id { get; set; }
        public string SenderName { get; set; }
        public string FamilyName { get; set; }
        public DateOnly ExpireAt { get; set; }
        public DateOnly SendAt {  get; set; }
        public string Receiver { get; set; }
        public string Status { get; set; }

    }
}
