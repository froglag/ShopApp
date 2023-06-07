using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Products;
using Shop.DataBase;

namespace ShopApp.Pages
{
    public class ProductModel : PageModel
    {
        private readonly ApplicationDBContext _context;
        public GetProduct.ProductViewModel Product { get; set; }
        public ProductModel(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult OnGet(string name)
        {
            Product = new GetProduct(_context).Do(name.Replace('-', ' '));
            if(Product == null)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
