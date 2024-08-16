using AutoMapper;
using Dapper;
using shoe_shop_productAPI.DbContexts;
using shoe_shop_productAPI.Helper;
using shoe_shop_productAPI.Models;
using shoe_shop_productAPI.Models.Dto;
using shoe_shop_productAPI.Repository.Interface;
using System.Data;

namespace shoe_shop_productAPI.Repository
{
    public class OrderDetailRespository : IOrderDetailRespository
    {
        private DapperContext _dapperContext;
        private IMapper _mapper;
        public OrderDetailRespository(DapperContext dapperContext, IMapper mapper)
        {
            _dapperContext = dapperContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDetailDto>> GetOrderDetail(string keySearch)
        {
            using IDbConnection connection = _dapperContext.CreateConnection();
            if (string.IsNullOrEmpty(keySearch)) { keySearch = ""; }
            var orderDetail = await connection.QueryAsync<OrderDetailDto>(StoreProceductLinks.SP_GET_ORDER_DETAIL, new { keySearch = keySearch }, commandType: CommandType.StoredProcedure);
            return orderDetail;
        }

        public async Task<OrderDetailDto> GetOrderDetailById(int id)
        {
            using IDbConnection connection = _dapperContext.CreateConnection();
            var orderDetail = await connection.QueryFirstAsync<OrderDetailDto>(StoreProceductLinks.SP_GET_ORDER_DETAIL_BY_ID, new { orderDetailId = id }, commandType: CommandType.StoredProcedure);
            return orderDetail;
        }
    }
}
