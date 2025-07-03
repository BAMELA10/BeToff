using Microsoft.AspNetCore.Http.Features;
using BeToff.Entities;

namespace BeToff.Web.Models
{
    public class PhotoListViewModel
    {
        public List<Photo> Items { get; set; }
        public int Count { get; set; }
    }
}
