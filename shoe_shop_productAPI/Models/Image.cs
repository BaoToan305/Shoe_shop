using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shoe_shop_productAPI.Models
{
    public class Image
    {
        [NotMapped]
        public IFormFile? File { get; set; }
        [Key]
        public long image_id { get; set; }
        public string? image_name { get; set; }
        public string? image_extention { get; set; }
        public string? image_path { get; set;}
        public long image_size { get; set; }
        public long product_image_id { get; set; }

    }
}
