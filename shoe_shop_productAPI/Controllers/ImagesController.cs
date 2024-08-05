using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shoe_shop_productAPI.Models;
using shoe_shop_productAPI.Models.Dto;
using shoe_shop_productAPI.Repository;
using shoe_shop_productAPI.Response;
using System.Net;

namespace shoe_shop_productAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        protected ResponseDto _response;
        private readonly IImageRespository _imageRespository;

        public ImagesController(IImageRespository imageRespository)
        {
            _response = new ResponseDto();
            _imageRespository = imageRespository;
        }

        private void ValidationFileUpload(ImageUploadRequestDto image)
        {
            var allowExtention = new string[] { ".jpg", ".png", ".jpeg" };

            if (!allowExtention.Contains(Path.GetExtension(image.File.FileName)))
            {
                Console.WriteLine("Định dạng file không chính xác");
            }

            if(image.File.Length > 10485760)
            {
                Console.WriteLine("Vui lòng upload ảnh có độ lớn dưới 10MB");
            }
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<object> Upload([FromForm] ImageUploadRequestDto requestDto )
        {
            try
            {
                ValidationFileUpload(requestDto);
                if (ModelState.IsValid)
                {
                    var imageModel = new Image
                    {
                        File = requestDto.File,
                        image_size = requestDto.File.Length,
                        image_name = requestDto.File.FileName,
                        image_path = requestDto.File.FileName,
                        image_extention = Path.GetExtension(requestDto.File.FileName)
                    };

                    await _imageRespository.Upload(imageModel);

                    ImageResponse response = new ImageResponse
                    {
                        image_id = imageModel.image_id,
                        image_name = imageModel.image_name,
                        image_extention = imageModel.image_extention,
                        image_path = imageModel.image_path,
                        image_size = imageModel.image_size
                    };
                    _response.Status = (int)HttpStatusCode.OK;
                    _response.Message = "OK";
                    _response.Data = response;
                }
                else
                {
                    _response.Message = "Có lỗi xảy ra";
                    _response.Status = (int)HttpStatusCode.BadRequest;
                    _response.Data = new object[0];
                }
            }
            catch(Exception ex)
            {
                _response.Message = ex.Message;
                _response.Status = (int)HttpStatusCode.BadRequest;
                _response.Data = new object[0];
            }
            return _response;
           
        }
    }
}
