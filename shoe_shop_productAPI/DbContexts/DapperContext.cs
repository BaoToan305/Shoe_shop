using MySqlConnector;
using System.Data;

namespace shoe_shop_productAPI.DbContexts
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;

        private readonly string? _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration["ConnectionStrings:DefaultConnection"];
        }

        public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
    }
}
