using Newtonsoft.Json;
using shoe_shop_web_app.Models;

namespace shoe_shop_web_app.Response
{
    public class ProductsResponse
    {
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string? Message { get; set; }
        [JsonProperty(PropertyName = "data")]
        public ProductsData Data { get; set; }
    }
    public class ProductsData
    {
        public long limit { get; set; }
        public long total_recore { get; set; }
        public List<ProductsModel>? list { get; set; }
    }
    public class ProductsModel
    {
        public int product_id { get; set; } = 0;
        public string product_name { get; set; } = string.Empty;
        public string product_image { get; set; } = string.Empty;
        public decimal product_price { get; set; } = 0;
        public long product_cate_id { get; set; } = 0;
    }
}
