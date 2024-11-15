using Dapper;
using DocSync.API.Infrastructure;
using DocSync.API.Models;
using DocSync.API.Repositories.Interfaces;
using System.Data;

namespace DocSync.API.Repositories
{
    public class DocumentRepository: IDocumentRepository
    {
        private readonly DapperConfiguration _config;
        private readonly IDbConnection _connection;

        public DocumentRepository(DapperConfiguration config)
        {
            _config = config;
            _connection = _config.CreateConnection();
            _connection.Open();
        }

        public async Task<int> BulkCreateAsync(IEnumerable<PeopleCsv> records)
        {
            var query = @"INSERT INTO People (UserId, FirstName, LastName, Sex, Email, Phone, DateOfBirth, JobTitle,CreatedBy, CreatedDate) 
                        VALUES (@UserId, @FirstName, @LastName, @Sex, @Email, @Phone, @DateOfBirth, @JobTitle, @CreatedBy, @CreatedDate)";

            return await _connection.ExecuteAsync(query, records);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}

