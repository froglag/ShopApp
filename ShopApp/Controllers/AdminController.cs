using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using Shop.DataBase;
using Shop.Domain.Models;

namespace ShopApp.Controllers
{
    [Route("Admin")]
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
        public async Task<IActionResult> CreatProduct([FromBody] CreatProduct.Request request) => Ok((await new CreatProduct(_context).Do(request)));

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id) => Ok((await new DeleteProduct(_context).Do(id)));

        [HttpPut("products")]
        public async Task<IActionResult> UpDateProduct([FromBody] UpdateProduct.Request request) => Ok((await new UpdateProduct(_context).Do(request)));
    }
}
