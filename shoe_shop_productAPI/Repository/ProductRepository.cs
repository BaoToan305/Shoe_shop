using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using shoe_shop_productAPI.DbContexts;
using shoe_shop_productAPI.Models;
using shoe_shop_productAPI.Models.Dto;
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

        public async Task<ProductDto> CreateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<ProductDto, Product>(productDto);
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            Product product = await _db.Products.Where(x => x.product_id == id).FirstOrDefaultAsync() ?? throw new InvalidOperationException("Sản phẩm không tìm thấy");
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(string keysearch)
        {
            const string sp = "sp_keysearch_product";
            using IDbConnection connection = _dapperContext.CreateConnection();
            if (string.IsNullOrEmpty(keysearch)) { keysearch = ""; }
            var products = await connection.QueryAsync<ProductDto>(sp, new { keysearch }, commandType: CommandType.StoredProcedure);
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
