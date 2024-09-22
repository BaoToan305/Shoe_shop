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

        public ProductsResponse GetProducts(string keySearch,long page,long limit)
        {
            RestRequest request = new RestRequest(string.Format("{0}/api/Products/get-product", Constants.DOMAIN_URL_AZURE), Method.Get);
            request.AddParameter("keySearch", keySearch);
            request.AddParameter("page", page.ToString());
            request.AddParameter("limit", limit.ToString());
            return Get<ProductsResponse>(request);
        }
    }
}
