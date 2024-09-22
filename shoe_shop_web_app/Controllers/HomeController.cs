using Microsoft.AspNetCore.Mvc;
using PagedList;
using shoe_shop_web_app.Response;
using shoe_shop_web_app.Resquest;
namespace shoe_shop_web_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ProductsData> GetProduct(int page, int limit)
        {
            string keySearch = "";

            ProductsResquest resquest = new ProductsResquest();
            ProductsResponse response = await Task.Run(() => resquest.GetProducts(keySearch, page, limit));
            if (response != null && response.Data != null && response.Data.list != null && response.Data.list.Count > 0)
            {
                return response.Data;
            }

            return null;
        }

        public async Task<IActionResult> IndexAsync(int page)
        {
            page = 1;
            int limit = 10;
            var products = await GetProduct(page, limit);
            var pagedProducts = products.list.ToPagedList(page, limit);
            if (products.total_recore > 0)
            {
                ViewBag.TotalPages = (products.total_recore / products.limit) + 1;
            }
            else
            {
                ViewBag.TotalPages = (products.total_recore / products.limit);
            }
            ViewBag.CurrentPage = pagedProducts.PageNumber;
            return View(pagedProducts);
        }
    }
}
