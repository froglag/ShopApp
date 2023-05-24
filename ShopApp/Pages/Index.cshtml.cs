using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Products;
using Shop.Application.ProductsAdmin;
using Shop.DataBase;
using Shop.Domain.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ShopApp.Pages
{
    public class IndexModel : PageModel
    {        
        [BindProperty]
        public CreatProduct.ProductViewModel Product { get; set; }

        private readonly ApplicationDBContext _context;
        public IndexModel(ApplicationDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Shop.Application.Products.GetProducts.ProductViewModel> Products { get; set; }
        public void OnGet()
        {
            Products = new Shop.Application.Products.GetProducts(_context).Do();
        }
        public async Task<IActionResult> OnPost()
        {
            await new CreatProduct(_context).Do(Product);
            return RedirectToPage("Index");
        }
    }
}