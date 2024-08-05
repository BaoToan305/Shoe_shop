using System.ComponentModel.DataAnnotations;

namespace shoe_shop_productAPI.Models.Dto
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile? File { get; set; }

    }
}
