using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shoe_shop_productAPI.Models;
using shoe_shop_productAPI.Models.Dto;
using shoe_shop_productAPI.Repository.Interface;
using System.Net;

namespace shoe_shop_productAPI.Controllers
{
    [Route("api/[controller]")]

    public class ProductsController : ControllerBase
    {
        protected ResponseDto _response;
        private IProductRepository _productRepository;
        private IMapper _mapper;
        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _response = new ResponseDto();
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "User")]
        [HttpGet("get-product")]        
        public async Task<object> GetProduct([FromQuery] string keySearch)
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await _productRepository.GetProducts(keySearch);
                _response.Data = _mapper.Map<IEnumerable<Product>>(productDtos);
                _response.Status = (int)HttpStatusCode.OK;
                _response.Message = "OK";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("create-product")]
        public async Task<object> CreateProduct([FromBody] ProductDto productDto)
        {
            try
            {
                int check = await _productRepository.CreateProduct(productDto);
                if(check > 0)
                {
                    _response.Data = _mapper.Map<Product>(productDto);
                    _response.Status = (int)HttpStatusCode.OK;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = new object[0];
                    _response.Status = (int)HttpStatusCode.OK;
                    _response.Message = "OK";
                }
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
