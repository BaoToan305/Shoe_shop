using shoe_shop_productAPI.Models.Dto;

namespace shoe_shop_productAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts(string keysearch);

        Task<ProductDto> GetProductById(int id);
        Task<ProductDto> CreateProduct(ProductDto productDto);
    }
}
