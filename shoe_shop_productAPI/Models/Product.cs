using System.ComponentModel.DataAnnotations;

namespace shoe_shop_productAPI.Models
{
    public class Product
    {
        [Key]
        public int product_id { get; set; }
        public string? product_name { get; set; }
        public string? product_image {  get; set; }
        public decimal product_price { get; set; }
        public int product_cate_id { get; set; }
    }
}
