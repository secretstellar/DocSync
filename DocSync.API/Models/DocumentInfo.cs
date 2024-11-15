namespace DocSync.API.Models
{
    public class DocumentInfo : BaseModel
    {
        public string DocumentName { get; set; }
        public DocumentStatus Status { get; set; }
    }

}
