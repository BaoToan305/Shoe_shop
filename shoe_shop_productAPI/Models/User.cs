using System.ComponentModel.DataAnnotations;
using System.Data;

namespace shoe_shop_productAPI.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }
        public string? user_name { get; set; }
        public string? user_password { get; set; }
        public string? user_email { get; set; }
        public string? user_phone { get; set; }
        public int user_role_id { get; set; }
        public string? user_role_name { get; set; }
        public string? jwt_token { get; set; }

        public ICollection<RoleUser> Roles { get; set; }
    }
}
