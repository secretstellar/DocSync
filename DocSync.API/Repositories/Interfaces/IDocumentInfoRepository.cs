using DocSync.API.Models;

namespace DocSync.API.Repositories.Interfaces
{
    public interface IDocumentInfoRepository
    {
        Task<IEnumerable<DocumentInfo>> GetAllAsync();
        Task<DocumentInfo> GetByIdAsync(int id);
        Task<int> CreateAsync(DocumentInfo documentInfo);
        Task<int> UpdateAsync(DocumentInfo documentInfo);
        Task<int> DeleteAsync(int id);
    }
}
