using DocSync.API.DTOs;
using DocSync.API.Models;

namespace DocSync.API.Repositories.Interfaces
{
    public interface IDocumentRepository
    {
        public Task<int> BulkCreateAsync(IEnumerable<PeopleCsv> records);
    }
}
