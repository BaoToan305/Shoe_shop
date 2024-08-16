namespace shoe_shop_productAPI.Models.Dto
{
    public class UserDto
    {
        public string user_name { get; set; } = string.Empty;
        public string user_password { get; set; } = string.Empty;
        public string user_email { get; set; } = string.Empty;
        public string user_phone { get; set; } = string.Empty;
    }

    public class UserDtoLogin
    {
        public string user_name { get; set; } = string.Empty;
        public string user_password { get; set; } = string.Empty;
    }
}
