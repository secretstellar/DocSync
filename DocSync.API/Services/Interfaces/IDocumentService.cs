using DocSync.API.DTOs;

namespace DocSync.API.Services.Interfaces
{
    public interface IDocumentService
    {
        public Task<ResponseDto> UploadDocument(DocumentDto documentDto);
        public Task<ResponseDto> UploadCsv(IFormFile formFile);
    }
}
