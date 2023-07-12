using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Orders;
using Shop.DataBase;

namespace ShopApp.Pages
{
    public class OrderModel : PageModel
    {
        private ApplicationDBContext _context;

        public OrderModel(ApplicationDBContext context)
        {
            _context = context;
        }

        public GetOrder.Response Order { get; set; }
        public void OnGet(string reference)
        {
            Order = new GetOrder(_context).Do(reference);
        }
    }
}
