using System.ComponentModel.DataAnnotations;

namespace shoe_shop_productAPI.Models
{
    public class OrderDetail
    {
        [Key]
        public long Id { get; set; }

        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public long Quantity { get; set; }

    }
}
