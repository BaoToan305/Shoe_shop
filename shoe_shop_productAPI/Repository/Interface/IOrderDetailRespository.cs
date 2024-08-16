using shoe_shop_productAPI.Models.Dto;

namespace shoe_shop_productAPI.Repository.Interface
{
    public interface IOrderDetailRespository
    {
        Task<IEnumerable<OrderDetailDto>> GetOrderDetail(string keySearch);
        Task<OrderDetailDto> GetOrderDetailById(int id);
    }
}
