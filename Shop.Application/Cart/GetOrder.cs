using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Shop.DataBase;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    public class GetOrder
    {
        private ISession _session;
        private ApplicationDBContext _context;

        public GetOrder(ISession session, ApplicationDBContext context)
        {
            _session = session;
            _context = context;
        }

        public Response Do()
        {
            var cart = _session.GetString("cart");

            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cart);

            var customerInfoString = _session.GetString("customer-info");
            var customerInformation = JsonConvert.DeserializeObject<Domain.Models.CustomerInformation>(customerInfoString);

            var listOfProduct = _context.Stock
                .Include(x => x.Product)
                .ToList()
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new Product
                {
                    StockId = x.Id,
                    ProductId = x.ProductId,
                    Qty = cartList.FirstOrDefault(y => y.StockId == x.Id).Qty,
                    Price = (int) x.Product.Price,
                }).ToList();

            return new Response 
            { 
                Products = listOfProduct,
                CustomerInformation = new CustomerInformation
                {
                    Name = customerInformation.Name,
                    LastName = customerInformation.LastName,
                    Email = customerInformation.Email,
                    PhoneNumber = customerInformation.PhoneNumber,

                    Address1 = customerInformation.Address1,
                    Address2 = customerInformation.Address2,
                    City = customerInformation.City,
                    PostCode = customerInformation.PostCode,
                }
            };
        }
        public class Response
        {
            public IEnumerable<Product> Products { get; set; }
            public CustomerInformation CustomerInformation { get; set; }

            public int GetTotalChange() => Products.Sum(x => x.Qty * x.Price); 
        }

        public class Product
        {
            public int ProductId { get; set; }
            public int StockId { get; set; }
            public int Qty { get; set; }
            public int Price { get; set; }
        }

        public class CustomerInformation
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
        }
    }
}
