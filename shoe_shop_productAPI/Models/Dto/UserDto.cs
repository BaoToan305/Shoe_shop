namespace shoe_shop_productAPI.Models.Dto
{
    public class UserDto
    {
        public string? user_name { get; set; }
        public string? user_password { get; set; }
        public string? user_email { get; set; }
        public string? user_phone { get; set; }
    }

    public class UserDtoLogin
    {
        public string? user_name { get; set; }
        public string? user_password { get; set; }
    }
}
