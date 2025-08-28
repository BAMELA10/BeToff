using Microsoft.AspNetCore.Http.Features;
using BeToff.Entities;
using BeToff.BLL.Dto.Response;

namespace BeToff.Web.Models
{
    public class PhotoFamillyListViewModel
    {
        public List<PhotoFamilyResponseDto> Items { get; set; }
        public int Count { get; set; }
    }
}
