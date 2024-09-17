using RestSharp;
using shoe_shop_web_app.BaseCallApi;
using shoe_shop_web_app.Helper;
using shoe_shop_web_app.Response;

namespace shoe_shop_web_app.Resquest
{
    public class ProductsResquest : BaseClient
    {
        public ProductsResquest() : base()
        {
            
        }

        public ProductsResponse GetProducts()
        {
            RestRequest request = new RestRequest(string.Format("{0}/api/Products/get-product", Constants.DOMAIN_URL), Method.Get);
            return Get<ProductsResponse>(request);
        }
    }
}
