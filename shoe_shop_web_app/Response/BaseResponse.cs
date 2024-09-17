using Newtonsoft.Json;

namespace shoe_shop_web_app.Response
{
    public class BaseResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
