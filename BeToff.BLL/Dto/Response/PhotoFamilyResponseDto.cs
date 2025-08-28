
namespace BeToff.BLL.Dto.Response
{
    public class PhotoFamilyResponseDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string FamilyId { get; set; }
        public string FamilyName { get; set; }
        public DateOnly PubliedAt { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }


    }
}
