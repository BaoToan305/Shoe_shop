using Newtonsoft.Json;

namespace shoe_shop_web_app.Models
{
    public class Products
    {
        [JsonProperty("product_id")]
        public int ProductId { get; set; }
        [JsonProperty("product_name")]
        public string ProductName { get; set; } = string.Empty;
        [JsonProperty("product_image")]
        public string ProductImage { get; set; } = string.Empty;
        [JsonProperty("product_price")]
        public decimal ProductPrice { get; set; } = 0;
        [JsonProperty("product_cate_id")]
        public long ProductCateId { get; set; } = 0;
    }
}
