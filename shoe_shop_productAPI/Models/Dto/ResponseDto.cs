using Microsoft.AspNetCore.Http;

namespace shoe_shop_productAPI.Models.Dto
{
    public class ResponseDto
    {
        public int Status { get; set; }
        public string Message { get; set; } = "";
        public object? Data { get; set; }
    }

    public class ResponseDtoPagin
    {
        public int Status { get; set; }
        public string Message { get; set; } = "";
        public DataPaginTion? Data { get; set; }
    }

    public class DataPaginTion
    {
        public long limit { get; set; }
        public long total_recore { get; set; }
        public List<object>? List { get; set; }
    }
}
