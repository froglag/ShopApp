using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Cart;
using Shop.Application.Customer;
using Shop.Application.Orders;
using Shop.DataBase;
using Stripe.Checkout;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ShopApp.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        private ApplicationDBContext _context;
        private IHostingEnvironment _env;

        public CustomerInformationModel(ApplicationDBContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            new AddCustomerInformation(HttpContext.Session).Do(CustomerInformation);
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

            new CreateOrder(_context).Do(new CreateOrder.Request
            {
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
            return new StatusCodeResult(303);
        }

        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Do();

            if (information == null)
            {
                if (_env.IsDevelopment())
                {
                    CustomerInformation = new AddCustomerInformation.Request
                    {
                        Name = "a",
                        LastName = "a",
                        Address1 = "a",
                        Address2 = "a",
                        City = "a",
                        Email = "a@a.com",
                        PostCode = "a",
                        PhoneNumber = "123",
                    };
                }
                return Page();

            }
            else
            {
                return RedirectToPage("/Cart");
            }
        }
    }
}