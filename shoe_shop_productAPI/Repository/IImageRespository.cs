using shoe_shop_productAPI.Models;

namespace shoe_shop_productAPI.Repository
{
    public interface IImageRespository
    {
        Task<Image> Upload(Image image);
    }
}
