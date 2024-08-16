using shoe_shop_productAPI.Models.Dto;

namespace shoe_shop_productAPI.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts(string keysearch);

        Task<int> CreateProduct(ProductDto productDto);
    }
}
