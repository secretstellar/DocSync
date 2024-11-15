using DocSync.API.DTOs;

namespace DocSync.API.Services.Interfaces
{
    public interface IDocumentInfoService
    {
        Task<ResponseDto> GetAllDocumentInfoAsync();
        Task<ResponseDto> GetDocumentInfoByIdAsync(int id);
        Task<ResponseDto> AddDocumentInfoAsync(DocumentInfoDto documentDto);
        Task<ResponseDto> UpdateDocumentInfoAsync(DocumentInfoDto documentDto);
        Task<ResponseDto> DeleteDocumentInfoAsync(int id);
    }
}
