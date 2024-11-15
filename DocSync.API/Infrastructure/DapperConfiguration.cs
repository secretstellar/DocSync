using System.Data.SqlClient;
using System.Data;

namespace DocSync.API.Infrastructure
{
    public class DapperConfiguration
    {
        private readonly IConfiguration _configuration;

        public DapperConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("AppDbConnection"));
        }
    }
}
