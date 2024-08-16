namespace shoe_shop_productAPI.Models.Dto
{
    public class OrderDetailDto
    {

        public long orders_detail_id { get; set; }

        public long orders_id { get; set; }
        public long product_id { get; set; }
        public string product_name { get; set; } = string.Empty;
        public decimal price { get; set; }
        public long quantity { get; set; }
    }
}
