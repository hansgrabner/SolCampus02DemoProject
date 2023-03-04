using Campus02DemoProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Campus02DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> meineProdukte = new List<Product>();

        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            meineProdukte.Add(product);
            return Ok("Erzeugt");
        }

        [HttpGet]
        public List<Product> GetAlleProukte()
        {
            return meineProdukte;
        }

    }
}
