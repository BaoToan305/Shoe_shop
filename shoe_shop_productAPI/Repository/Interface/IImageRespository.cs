using shoe_shop_productAPI.Models;

namespace shoe_shop_productAPI.Repository.Interface
{
    public interface IImageRespository
    {
        Task<Image> Upload(Image image,string productId);
        
    }
}
