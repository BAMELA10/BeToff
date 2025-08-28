namespace BeToff.BLL.Dto.Request
{
    public class PhotoFamillyCreateDto
    {
        public string Title { get; set; }
        public string Image {  get; set; }
        public DateOnly DateCreation { get; set; }
        public Guid AuthorId { get; set; }
        public Guid FamilyId { get; set; }
    }
}
