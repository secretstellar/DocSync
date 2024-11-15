namespace DocSync.API.DTOs
{
    public class DocumentDto
    {
        public int DocumentId { get; set; }
        public IFormFile Document { get; set; }
    }
}
