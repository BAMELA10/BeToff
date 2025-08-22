using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeToff.Web.Models;
public class InvitationCreateViewModel
{
    public Guid? ReceiverId { get; set; }
    public IEnumerable<SelectListItem>? Recievers { get; set; } = new List<SelectListItem>();

    public Guid? FamillyItemId { get; set; }

    public IEnumerable<SelectListItem> Famillies { get; set; } = new List<SelectListItem>();

}

