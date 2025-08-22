using BeToff.Entities;

namespace BeToff.Web.Models
{
    public class MemberViewModel
    {
        public User? Member { get; set; }

        public List<User>? Members { get; set; }
    }
}
