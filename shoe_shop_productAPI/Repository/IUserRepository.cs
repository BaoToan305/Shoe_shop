using shoe_shop_productAPI.Models;
using shoe_shop_productAPI.Models.Dto;

namespace shoe_shop_productAPI.Repository
{
    public interface IUserRepository
    {
        Task<int> RegisterUser(UserDto userDto);
        Task<User> GetUserToRegisterAsync(string name);
        Task<User> GetUserByIdAsync(string userName);
        Task<User> GetUserToCheckAsync(string name,string password);
      
    }
}
