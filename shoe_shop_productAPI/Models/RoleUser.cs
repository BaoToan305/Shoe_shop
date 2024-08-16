using System.ComponentModel.DataAnnotations;

namespace shoe_shop_productAPI.Models
{
    public class RoleUser
    {
        [Key]
        public string? role_name { get; set; }

    }
}
