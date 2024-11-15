namespace DocSync.API.DTOs
{
    public class DocumentInfoDto
    {
        public int Id { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get; set; }
    }

}
