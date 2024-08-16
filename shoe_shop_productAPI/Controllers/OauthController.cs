using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NuGet.Common;
using shoe_shop_productAPI.Helper;
using shoe_shop_productAPI.Models;
using shoe_shop_productAPI.Models.Dto;
using shoe_shop_productAPI.Repository.Interface;
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
        private IMapper _mapper;
        public OauthController(IUserRepository userRepository, ITokenRepository tokenRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _response = new ResponseDto();
            _tokenRepository = tokenRepository;
            _mapper = mapper;
        }
       

        [HttpPost("register")]
        public async Task<object> RegisterUser([FromBody] UserDto userDto)
        {
            try
            {
                if (Utils.IsBase64String(userDto.user_password))
                {
                    if (userDto != null)
                    {
                        var user = _userRepository.GetUserToRegisterAsync(userDto.user_name);
                        if (user.Result != null)
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
                else
                {
                    _response.Message = "Mật khẩu chưa được mã hóa";
                    _response.Status = (int)HttpStatusCode.BadRequest;
                    _response.Data = new object[0];
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
                //kiểm tra password có encode base64
                if (Utils.IsBase64String(userDtoLogin.user_password))
                {
                    var user = await _userRepository.GetUserToCheckAsync(userDtoLogin.user_name, userDtoLogin.user_password);
                    if (user != null)
                    {                   
                        if (!string.IsNullOrEmpty(user.jwt_token))
                        {
                            //kiểm tra token có hết hạn hay chưa
                            if (Utils.IsTokenExpired(user.jwt_token))
                            {
                                List<string> roles = new List<string>
                                {
                                    user.user_role_name
                                };
                                RoleUser roleUser = new RoleUser
                                {
                                    role_name = user.user_role_name
                                };
                                user.Roles.Add(roleUser);
                                //tạo token mới và update xuống database
                                user.jwt_token = _tokenRepository.CreateJWTToken(user, roles);
                                int tokenExp = await _userRepository.UpdateJwtToken(user.jwt_token, user.user_id);
                                _response.Data = user;
                                _response.Status = (int)HttpStatusCode.OK;
                                _response.Message = "OK";
                            }
                            else
                            {
                                _response.Data = user;
                                _response.Status = (int)HttpStatusCode.OK;
                                _response.Message = "OK";
                            }
                        }
                        else
                        {
                            //token rỗng
                            List<string> roles = new List<string>
                                {
                                    user.user_role_name
                                };
                            RoleUser roleUser = new RoleUser
                            {
                                role_name = user.user_role_name
                            };
                            // tạo mới và update xuống
                            user.Roles.Add(roleUser);
                            user.jwt_token = _tokenRepository.CreateJWTToken(user, roles);
                            int tokenExp = await _userRepository.UpdateJwtToken(user.jwt_token, user.user_id);
                            _response.Data = user;
                            _response.Status = (int)HttpStatusCode.OK;
                            _response.Message = "OK";
                        }

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
                    _response.Message = "Mật khẩu chưa được mã hóa";
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
