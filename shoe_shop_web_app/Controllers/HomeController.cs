using Microsoft.AspNetCore.Mvc;
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

        public async Task GetProduct()
        {
            ProductsResquest resquest = new ProductsResquest();
            ProductsResponse response = await Task.Run(() => resquest.GetProducts());
            if (response != null)
            {

            }
        }

        public IActionResult Index()
        {
            _ = GetProduct();
            return View();
        }
     
    }
}
