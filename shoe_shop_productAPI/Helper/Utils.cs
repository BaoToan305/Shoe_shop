using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;

namespace shoe_shop_productAPI.Helper
{
    public class Utils
    {
        public static bool IsBase64String(string input)
        {
            if (Regex.IsMatch(input, @"[^a-zA-Z0-9\s]"))
            {
                return true; // Chuỗi có thể đã được mã hóa
            }

            // Kiểm tra tần suất xuất hiện của các ký tự
            var characterFrequency = new Dictionary<char, int>();
            foreach (var c in input)
            {
                if (characterFrequency.ContainsKey(c))
                {
                    characterFrequency[c]++;
                }
                else
                {
                    characterFrequency[c] = 1;
                }
            }

            // Nếu không có ký tự nào xuất hiện quá nhiều lần, chuỗi có thể đã được mã hóa
            foreach (var freq in characterFrequency.Values)
            {
                if (freq > input.Length / 4) // Ví dụ: nếu một ký tự chiếm hơn 25% độ dài của chuỗi
                {
                    return false; // Chuỗi có thể là văn bản thuần túy
                }
            }

            return true; // Chuỗi có thể đã được mã hóa
        }

        public static bool IsTokenExpired(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            if (!jwtHandler.CanReadToken(token))
            {
                throw new ArgumentException("Token không hợp lệ.");
            }

            var jwtToken = jwtHandler.ReadJwtToken(token);
            var expirationDate = jwtToken.ValidTo; // Thời gian hết hạn của token

            // So sánh với thời gian hiện tại (UTC)
            return expirationDate < DateTime.UtcNow;
        }
    }
}
