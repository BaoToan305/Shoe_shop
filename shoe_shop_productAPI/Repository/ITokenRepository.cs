using shoe_shop_productAPI.Models;

namespace shoe_shop_productAPI.Repository
{
    public interface ITokenRepository
    {
       string CreateJWTToken(User user, List<string> roles);
    }
}
