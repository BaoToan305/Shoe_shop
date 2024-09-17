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
        protected ResponseDtoPagin responsePagin;
        private IProductRepository _productRepository;
        private IMapper _mapper;
        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _response = new ResponseDto();
            responsePagin = new ResponseDtoPagin();
            _productRepository = productRepository;
            _mapper = mapper;
        }

        //[Authorize(Roles = "User")]
        [HttpGet("get-product")]
        public async Task<ResponseDtoPagin> GetProduct([FromQuery] string keySearch, [FromQuery] int page = 1, [FromQuery] int limits = 10)
        {
            var response = new ResponseDtoPagin();

            try
            {
                // Gọi repository với keySearch, page, và limits
                var products = await _productRepository.GetProducts(keySearch, page, limits);

                // Tính tổng số bản ghi
                var totalRecords = await _productRepository.GetTotalRecords();

                // Đặt thông tin vào ResponseDtoPagin
               
                var data = new DataPaginTion
                {
                    limit = limits,
                    total_recore = totalRecords.Count(),
                    List = _mapper.Map<List<object>>(products)
                };
                response.Data = data;
                response.Status = (int)HttpStatusCode.OK;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                // Bắt lỗi và trả về thông báo lỗi
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Data = new DataPaginTion
                {
                    List = new List<object>() 
                };
            }

            return response;
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
