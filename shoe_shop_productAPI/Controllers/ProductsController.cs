using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shoe_shop_productAPI.Models.Dto;
using shoe_shop_productAPI.Repository;
using System.Net;

namespace shoe_shop_productAPI.Controllers
{
    [Route("api/[controller]")]

    public class ProductsController : ControllerBase
    {
        protected ResponseDto _response;
        private IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _response = new ResponseDto();
            _productRepository = productRepository;
        }

        [HttpGet("get-product")]
        [Authorize(Policy = ("RequireUserRole"))]
        public async Task<object> GetProduct([FromQuery] string keySearch)
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await _productRepository.GetProducts(keySearch);
                _response.Data = productDtos;
                _response.Status = (int)HttpStatusCode.OK;
                _response.Message = "OK";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("find-product-by-id/{id}")]
        public async Task<object> FindProductById([FromRoute] int id)
        {
            try
            {
                ProductDto productDtos = await _productRepository.GetProductById(id);
                _response.Data = productDtos;
                _response.Status = (int)HttpStatusCode.OK;
                _response.Message = "OK";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.Status = (int)HttpStatusCode.BadRequest;
                _response.Data = new object[0];
            }
            return _response;
        }

        [HttpPost("create-product")]
        public async Task<object> CreateProduct([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto productDtos = await _productRepository.CreateProduct(productDto);
                _response.Data = productDtos;
                _response.Status = (int)HttpStatusCode.OK;
                _response.Message = "OK";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.Data = new object[0];
            }
            return _response;
        }
    }
}
