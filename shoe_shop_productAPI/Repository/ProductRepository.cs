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

        public async Task<IEnumerable<ProductDto>> GetProducts(string keysearch)
        {
            using IDbConnection connection = _dapperContext.CreateConnection();
            if (string.IsNullOrEmpty(keysearch)) { keysearch = ""; }
            var products = await connection.QueryAsync<ProductDto>(StoreProceductLinks.SP_PRODUCTS, new { keysearch }, commandType: CommandType.StoredProcedure);
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
