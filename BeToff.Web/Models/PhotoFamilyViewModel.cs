using BeToff.BLL.Dto.Response;

namespace BeToff.Web.Models
{
    public class PhotoFamilyViewModel
    {
        public PhotoFamilyResponseDto picture { get; set; }
        public string FileName {  get; set; }
        public List<CommentResponseDto> Comments { get; set; }
    }
}
