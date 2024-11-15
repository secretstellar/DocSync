namespace DocSync.API.DTOs
{
    public class ResponseDto
    {
        public object Data { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }

}
