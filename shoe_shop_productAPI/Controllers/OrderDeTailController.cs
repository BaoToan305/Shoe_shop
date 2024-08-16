using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shoe_shop_productAPI.Models;
using shoe_shop_productAPI.Models.Dto;
using shoe_shop_productAPI.Repository;
using shoe_shop_productAPI.Repository.Interface;
using System.Net;

namespace shoe_shop_productAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDeTailController : ControllerBase
    {
        protected ResponseDto _response;
        private IMapper _mapper;
        private IOrderDetailRespository _orderDetailRepository;
        public OrderDeTailController(IOrderDetailRespository orderDetailRepository, IMapper mapper)
        {
            _response = new ResponseDto();
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        //[Authorize(Roles = "User")]
        [HttpGet("get-orderDetail")]
        public async Task<object> GetOrderDetail([FromQuery] string? keySearch) 
        {
            try
            {
                var orderDetailDto = await _orderDetailRepository.GetOrderDetail(keySearch);
                if (orderDetailDto != null && orderDetailDto.Any())
                {
                    _response.Data = orderDetailDto;
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

        [HttpGet("get-order-detail/{id}")]
        public async Task<object> GetDetailById([FromRoute] int id)
        {
            try
            {
                var orderDetailDto = await _orderDetailRepository.GetOrderDetailById(id);
                if (orderDetailDto != null)
                {
                    _response.Data = orderDetailDto;
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
            catch(Exception ex)
            {
                _response.Message = ex.Message;
                _response.Data = new object[0];
            }
            return _response;
        }
    }
}
