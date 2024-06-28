using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_cors_mediatr.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
