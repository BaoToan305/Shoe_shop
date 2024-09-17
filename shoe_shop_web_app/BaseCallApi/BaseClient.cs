using Newtonsoft.Json;
using RestSharp;
using shoe_shop_web_app.Helper;
using shoe_shop_web_app.Response;

namespace shoe_shop_web_app.BaseCallApi
{
    public class BaseClient : RestClient
    {
        private readonly RestClient _client = new RestClient();

        public int TimeOut = 30000;


        public async Task<string> GetDataAsync(RestRequest request)
        {
            var response = await _client.GetAsync(request);

            if (response != null && response.IsSuccessful)
            {
                return response.Content;
            }
            else
            {
                throw new Exception("Error retrieving data");
            }
        }
        public async Task<string> PostDataAsync(RestRequest request)
        {
            //request = new RestRequest(endpoint,Method.Post).AddJsonBody(body);
            var response = await _client.ExecutePostAsync(request); 

            if (response.IsSuccessful) 
            {
                return response.Content;
            }
            else
            {
                throw new Exception($"Error posting data: {response.ErrorMessage}");
            }
        }


        public  RestResponse<T> Execute<T>(RestRequest request) where T : new()
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null");
            }

            RestResponse<T> response;

            try
            {
                if (request.Method == Method.Get)
                {
                    response = _client.ExecuteGet<T>(request); 
                }
                else
                {
                    response = _client.ExecutePost<T>(request); 
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("Error executing request: " + ex.Message, ex);
            }

            return response;
        }

        public T Get<T>(RestRequest request) where T : new()
        {
            try
            {

                var response = Execute<T>(request);
                if (response != null && response.ResponseUri != null)
                {
                    WriteLog.logs(response.ResponseUri.ToString());
                    WriteLog.logs(response.Content);
                }
                if (response != null && response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    WriteLog.logs(response.Content);
                }
                dynamic jsonResponseObject = JsonConvert.DeserializeObject(response.Content);
                BaseResponse o = jsonResponseObject.ToObject<BaseResponse>();
                if (o.Status == (int)ResponseEnum.Ambiguous)
                {
                    return (T)Convert.ChangeType(o, typeof(T));
                }
                else
                {
                    return (T)Convert.ChangeType(response.Data, typeof(T));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Error Server call api time out");
                return default;
            }
        }
    }
}
