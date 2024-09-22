using shoe_shop_productAPI.Models.Dto;

namespace shoe_shop_productAPI.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts(string keysearch, int page, int limit);

        Task<int> CreateProduct(ProductDto productDto);
        Task<IEnumerable<ProductDto>> GetTotalRecords();

        Task<int> UpdateProduct(ProductDto product);
        Task<int> DeleteProduct(int id);

    }
}
