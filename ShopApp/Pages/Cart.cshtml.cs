using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Application.Customer;
using Shop.Application.Orders;
using Shop.DataBase;
using Stripe;
using Stripe.Checkout;

namespace ShopApp.Pages
{
    public class CartModel : PageModel
    {
        public IEnumerable<GetFromCart.Response> Cart { get; set; }
        private ApplicationDBContext _context;
        public string PublicKey { get; }

        public CartModel(IConfiguration conf, ApplicationDBContext context)
        {
            PublicKey = conf["Stripe:PublicKey"].ToString();
            _context = context;
        }

        public IActionResult OnPost()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Do();

            if (information == null)
                return RedirectToPage("/Checkout/CustomerInformation");

            var Order = new Shop.Application.Cart.GetOrder(HttpContext.Session, _context).Do();


            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                      UnitAmount = Order.GetTotalChange(),
                      Currency = "usd",
                      ProductData = new SessionLineItemPriceDataProductDataOptions
                      {
                        Name = "T-shirt",
                      },
                    },
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                SuccessUrl = "http://localhost:5000/Checkout/SuccessfulPayment",
                CancelUrl = "http://localhost:5000/Cart",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            var sessionId = HttpContext.Session.Id;

            new CreateOrder(_context).Do(new CreateOrder.Request
            {
                SessionId = sessionId,
                Name = Order.CustomerInformation.Name,
                LastName = Order.CustomerInformation.LastName,
                Email = Order.CustomerInformation.Email,
                PhoneNumber = Order.CustomerInformation.PhoneNumber,

                Address1 = Order.CustomerInformation.Address1,
                Address2 = Order.CustomerInformation.Address2,
                City = Order.CustomerInformation.City,
                PostCode = Order.CustomerInformation.PostCode,

                Stocks = Order.Products.Select(x => new CreateOrder.Stock
                {
                    StockId = x.StockId,
                    Qty = x.Qty,
                }).ToList(),
            });

            Response.Headers.Add("Location", session.Url);

            return RedirectToPage("/Index");
        }


        public IActionResult OnGet()
        {
            Cart = new GetFromCart(HttpContext.Session, _context).Do();
            return Page();

        }
    }
}
