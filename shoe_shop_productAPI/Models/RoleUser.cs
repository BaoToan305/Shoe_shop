using System.ComponentModel.DataAnnotations;

namespace shoe_shop_productAPI.Models
{
    public class RoleUser
    {
        [Key]
        public int role_id { get; set; }
        public string? role_name { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
