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

        public async Task<IActionResult> OnPost()
        {
            var result = await new AddToCart(HttpContext.Session, _context).Do(CartViewModel);
            if(result)
                return RedirectToPage("Cart");
            else
                //TODO: add a warning
                return Page();
        }
        public async Task<IActionResult> OnGet(string name)
        {
            Product = await new GetProduct(_context).Do(name.Replace('-', ' '));
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
