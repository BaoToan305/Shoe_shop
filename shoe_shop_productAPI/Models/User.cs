using System.ComponentModel.DataAnnotations;

namespace shoe_shop_productAPI.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }
        public string? user_name { get; set; } = string.Empty;
        public string? user_password { get; set; } = string.Empty;
        public string? user_email { get; set; } = string.Empty;
        public string? user_phone { get; set; } = string.Empty;
        public int user_role_id { get; set; }
        public string? user_role_name { get; set; } = string.Empty;
        public string? jwt_token { get; set; } = string.Empty;

        public ICollection<RoleUser> Roles { get; set; } = new HashSet<RoleUser>();
    }
}
