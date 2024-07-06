using Microsoft.AspNetCore.Http;

namespace shoe_shop_productAPI.Models.Dto
{
    public class ResponseDto
    {
        public int Status { get; set; }
        public string Message { get; set; } = "";
        public object? Data { get; set; }

    }
}
