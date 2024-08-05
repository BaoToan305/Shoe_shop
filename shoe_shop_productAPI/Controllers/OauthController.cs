using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using shoe_shop_productAPI.Models;
using shoe_shop_productAPI.Models.Dto;
using shoe_shop_productAPI.Repository;
using System.Net;

namespace shoe_shop_productAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OauthController : ControllerBase
    {
        protected ResponseDto _response;
        private IUserRepository _userRepository;
        private ITokenRepository _tokenRepository;
        public OauthController(IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _response = new ResponseDto();
            _tokenRepository = tokenRepository;
        }
        [HttpPost("register")]
        public async Task<object> RegisterUser([FromBody] UserDto userDto)
        {
            try
            {
                if(userDto != null)
                {
                    var user = _userRepository.GetUserToRegisterAsync(userDto.user_name);
                    if(user.Result != null)
                    {
                        _response.Message = "Tên tài khoản đã được sử dụng";
                        _response.Status = (int)HttpStatusCode.BadRequest;
                        _response.Data = new object[0];
                    }
                    else
                    {
                        int check = await _userRepository.RegisterUser(userDto);
                        if (check > 0)
                        {
                            _response.Data = userDto;
                            _response.Status = (int)HttpStatusCode.OK;
                            _response.Message = "OK";
                        }
                    }
                }            
            }
            catch(Exception ex)
            {
                _response.Status = (int)HttpStatusCode.BadRequest;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost("login")]
        public async Task<object> Login([FromBody] UserDtoLogin userDtoLogin)
        {
            if(userDtoLogin != null)
            {
                var user = await _userRepository.GetUserToCheckAsync(userDtoLogin.user_name, userDtoLogin.user_password);
                if (user != null)
                {                    
                    List<string> roles = new List<string>
                    {
                        
                    };

                    user.jwt_token = _tokenRepository.CreateJWTToken(user, roles);
                    _response.Data = user;
                    _response.Status = (int)HttpStatusCode.OK;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Message = "Tài khoản hoặc mật khẩu không đúng";
                    _response.Status = (int)HttpStatusCode.BadRequest;
                    _response.Data = new object[0];
                }               
            }
            else
            {
                _response.Message = "Chưa nhập thông tin đăng nhập";
                _response.Status = (int)HttpStatusCode.BadRequest;
                _response.Data = new object[0];
            }
            return _response;
        }
    }
}
