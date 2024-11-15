namespace DocSync.API.Models
{
    public class DocumentModel : BaseModel
    {
        public int DocumentId { get; set; }
        public byte [] Document { get; set; }
    }
}
