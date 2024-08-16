using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using shoe_shop_productAPI.DbContexts;
using shoe_shop_productAPI.Models;
using shoe_shop_productAPI.Models.Dto;
using System.Data;
using shoe_shop_productAPI.Helper;
using shoe_shop_productAPI.Repository.Interface;
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

        public async Task<User> GetUserToCheckAsync(string name,string password)
        {
           User user = await _db.Users.FirstOrDefaultAsync(x => x.user_name == name && x.user_password == password);
            return user;
        }

        public async Task<User?> GetUserToRegisterAsync(string userName)
        {
            using IDbConnection connection = _dapperContext.CreateConnection();
            var users = await connection.QueryAsync<User>(StoreProceductLinks.SP_GET_USER, new { userName }, commandType: CommandType.StoredProcedure);
            if (users.Any())
            {
                return users.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
        
        public async Task<int> RegisterUser(UserDto userDto)
        {
            using IDbConnection connection = _dapperContext.CreateConnection();
            int rowAffected = await connection.ExecuteAsync(StoreProceductLinks.SP_REGISTER_USER, userDto, commandType: CommandType.StoredProcedure);
            return rowAffected;
        }

        public async Task<int> UpdateJwtToken(string token, int userId)
        {
            using IDbConnection connection = _dapperContext.CreateConnection();
            int rowAffected = await connection.ExecuteAsync(StoreProceductLinks.SP_UPDATE_JWT_TOKEN,new  { token = token, userId = userId }, commandType: CommandType.StoredProcedure);
            return rowAffected;
        }
    }
}
