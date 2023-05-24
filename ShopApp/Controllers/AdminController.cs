using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using Shop.DataBase;
using Shop.Domain.Models;

namespace ShopApp.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        public ApplicationDBContext _context;
        public AdminController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("products")]
        public IActionResult GetProducts() => Ok(new GetProducts(_context).Do());

        [HttpGet("products/{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_context).Do(id));
        [HttpPost("products")]
        public IActionResult CreatProduct(CreatProduct.ProductViewModel vm) => Ok(new CreatProduct(_context).Do(vm));

        [HttpDelete("products/{id}")]
        public IActionResult DeleteProduct(int id) => Ok(new DeleteProduct(_context).Do(id));
        [HttpPut("products/{id}")]
        public IActionResult UpDateProduct(int id, UpdateProduct.ProductViewModel vm) => Ok(new UpdateProduct(_context).Do(id, vm));
    }
}
