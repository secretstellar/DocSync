using Dapper;
using DocSync.API.Infrastructure;
using DocSync.API.Models;
using DocSync.API.Repositories.Interfaces;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;

namespace DocSync.API.Repositories
{
    public class DocumentInfoRepository : IDocumentInfoRepository
    {
        private readonly DapperConfiguration _config;
        private readonly IDbConnection _connection;

        public DocumentInfoRepository(DapperConfiguration config)
        {
            _config = config;
            _connection = _config.CreateConnection();
            _connection.Open();
        }

        public async Task<IEnumerable<DocumentInfo>> GetAllAsync()
        {
            var query = "SELECT * FROM DocumentInfo di, DocumentStatus ds where di.StatusId = ds.Id";
            return await _connection.QueryAsync<DocumentInfo, DocumentStatus, DocumentInfo>(query, (di, ds) =>
            {
                di.Status = ds;
                return di;
            }, splitOn: "Id");
        }

        public async Task<DocumentInfo> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM DocumentInfo di, DocumentStatus ds where di.Id = @Id and di.StatusId = ds.Id";
            var result = await _connection.QueryAsync<DocumentInfo, DocumentStatus, DocumentInfo>(query, (di, ds) =>
            {
                di.Status = ds;
                return di;
            }, new { Id = id }, splitOn: "Id");

            return result.FirstOrDefault();
        }

        public async Task<int> CreateAsync(DocumentInfo documentInfo)
        {
            var query = @"INSERT INTO DocumentInfo (DocumentName, StatusId, CreatedBy, CreatedDate) 
                        OUTPUT INSERTED.Id
                        VALUES (@DocumentName, @StatusId, @CreatedBy, @CreatedDate)";
            return await _connection.ExecuteScalarAsync<int>(query, new
            {
                DocumentName = documentInfo.DocumentName,
                StatusId = documentInfo.Status.Id,
                CreatedBy = documentInfo.CreatedBy,
                CreatedDate = documentInfo.CreatedDate
            });
        }

        public async Task<int> UpdateAsync(DocumentInfo documentInfo)
        {
            var query = @"UPDATE DocumentInfo
                      SET DocumentName = @DocumentName, StatusId = @StatusId, UpdatedBy = @UpdatedBy, UpdatedDate = @UpdatedDate
                      WHERE Id = @Id";
            return await _connection.ExecuteAsync(query, new
            {
                Id = documentInfo.Id,
                DocumentName = documentInfo.DocumentName,
                StatusId = documentInfo.Status.Id,
                UpdatedBy = documentInfo.UpdatedBy,
                UpdatedDate = documentInfo.UpdatedDate
            });
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = "DELETE FROM DocumentInfo WHERE Id = @Id";
            return await _connection.ExecuteAsync(query, new { Id = id });
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}

