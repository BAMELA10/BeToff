using BeToff.BLL.Dto.Response;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeToff.Web.Models
{
    public class ConversationListViewModel
    {
        public int Count { get; set; }
        public List<ConversationViewModel>? Responses { get; set; }
        public IEnumerable<SelectListItem>? User { get; set; }
    }
}
