using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using shoe_shop_productAPI.DbContexts;
using shoe_shop_productAPI.Models;
using shoe_shop_productAPI.Models.Dto;
using System.Data;

namespace shoe_shop_productAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public UserRepository(DapperContext dapperContext, ApplicationDbContext db, IMapper mapper)
        {
            _dapperContext = dapperContext;
            _db = db;
            _mapper = mapper;
        }

        public async Task<User> GetUserByIdAsync(string userName)
        {
            return await _db.Users
                .FirstOrDefaultAsync(u => u.user_name == userName) ?? throw new InvalidOperationException("Không tìm thấy User");
        }

        public async Task<User> GetUserToCheckAsync(string name,string password)
        {
           User user = await _db.Users.FirstAsync(x => x.user_name == name && x.user_password == password);
            return user;
        }

        public async Task<User?> GetUserToRegisterAsync(string userName)
        {
            const string sql = "sp_get_user";
            using IDbConnection connection = _dapperContext.CreateConnection();
            var user = await connection.QueryAsync(sql, new { userName }, commandType: CommandType.StoredProcedure);
            if(user.Count() > 0)
            {
                return (User?)user;

            }
            else { return null; }
        }
        
        public async Task<int> RegisterUser(UserDto userDto)
        {
            const string sql = "sp_register_user";
            using IDbConnection connection = _dapperContext.CreateConnection();
            int rowAffected = await connection.ExecuteAsync(sql, userDto, commandType: CommandType.StoredProcedure);
            return rowAffected;
        }
    }
}
