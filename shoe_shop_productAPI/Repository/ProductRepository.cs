using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using shoe_shop_productAPI.DbContexts;
using shoe_shop_productAPI.Helper;
using shoe_shop_productAPI.Models;
using shoe_shop_productAPI.Models.Dto;
using shoe_shop_productAPI.Repository.Interface;
using System.Data;

namespace shoe_shop_productAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private DapperContext _dapperContext;
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper, DapperContext dapperContext)
        {
            _db = db;
            _mapper = mapper;
            _dapperContext = dapperContext;
        }

        public async Task<int> CreateProduct(ProductDto productDto)
        {
            using IDbConnection connection = _dapperContext.CreateConnection();
            int rowAffected = await connection.ExecuteAsync(StoreProceductLinks.SP_CREATE_PRODUCTS, productDto, commandType: CommandType.StoredProcedure);
            return rowAffected;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(string keysearch, int page, int limits)
        {
            using IDbConnection connection = _dapperContext.CreateConnection();

            // Đảm bảo tham số keysearch không bị null
            if (string.IsNullOrEmpty(keysearch))
            {
                keysearch = "";
            }

            // Tính toán offset dựa trên trang hiện tại (page)
            int offset = (page - 1) * limits;

            // Gọi stored procedure với các tham số keysearch, offset, và limits
            var products = await connection.QueryAsync<ProductDto>(
                StoreProceductLinks.SP_PRODUCTS,
                new { keysearch, limits , offset },
                commandType: CommandType.StoredProcedure
            );

            // Trả về danh sách đã được ánh xạ sang ProductDto
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetTotalRecords()
        {
            using IDbConnection connection = _dapperContext.CreateConnection();
            var products = await connection.QueryAsync<ProductDto>(StoreProceductLinks.SP_GET_PRODUCTS, commandType: CommandType.StoredProcedure);
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
