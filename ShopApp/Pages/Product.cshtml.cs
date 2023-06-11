using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Application.Products;
using Shop.DataBase;

namespace ShopApp.Pages
{
    public class ProductModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        public GetProduct.ProductViewModel Product { get; set; }
        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }

        public ProductModel(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult OnPost()
        {
            new AddToCart(HttpContext.Session).Do(CartViewModel);
            return RedirectToPage("Cart");
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
