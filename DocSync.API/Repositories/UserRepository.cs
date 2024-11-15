using Dapper;
using DocSync.API.Infrastructure;
using DocSync.API.Models;
using DocSync.API.Repositories.Interfaces;
using System.Data;
using System.Data.Common;

namespace DocSync.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperConfiguration _config;
        private readonly IDbConnection _connection;

        public UserRepository(DapperConfiguration config)
        {
            _config = config;
            _connection = _config.CreateConnection();
            _connection.Open();
        }
        public async Task<UserDetails> GetByUserNameAndPassword(string userName, string password)
        {
            var query = "SELECT * FROM Users where UserName = @userName and Password = @password ";
            var result = await _connection.QueryAsync<UserDetails>(query, new { userName = userName, password = password });
            return result.FirstOrDefault();
        }

        public async Task<int> CreateAsync(UserDetails userDetails)
        {
            var query = @"INSERT INTO Users (Name, UserName, Password, Email, Role, CreatedBy, CreatedDate) 
                      VALUES (@Name, @UserName,@Password,@Email,@Role, @CreatedBy, @CreatedDate)";
            return await _connection.ExecuteAsync(query, new
            {
                Name = userDetails.Name,
                UserName = userDetails.UserName,
                Password = userDetails.Password,
                Email = userDetails.Email,
                Role = userDetails.Role,
                CreatedBy = userDetails.CreatedBy,
                CreatedDate = userDetails.CreatedDate
            });
        }
        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
