using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.DataBase;

namespace ShopApp.Pages
{
    public class CartModel : PageModel
    {
        public IEnumerable<GetFromCart.Response> Cart { get; set; }
        private ApplicationDBContext _context;

        public CartModel(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            Cart = new GetFromCart(HttpContext.Session, _context).Do();
            return Page();

        }
    }
}
