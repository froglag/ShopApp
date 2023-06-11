using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.DataBase;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    public class GetFromCart
    {
        private ISession _session;
        private ApplicationDBContext _context;

        public GetFromCart(ISession session, ApplicationDBContext context)
        {
            _session = session;
            _context = context;
        }

        public IEnumerable<Response> Do()
        {
            var stringObject = _session.GetString("cart");

            if (string.IsNullOrEmpty(stringObject))
            { 
                return new List<Response>();
            }
            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            var products = _context.Stock
                .Include(x => x.Product).AsEnumerable()
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new Response { 
                    Name = x.Product.Name,
                    Price = x.Product.Price.ToString("N2"), 
                    StockId = x.Id, 
                    Qty = cartList.FirstOrDefault(y => y.StockId == x.Id).Qty })
                .ToList();
            return products;
        }
        public class Response
        {
            public string Name { get; set; }
            public string Price { get; set; }
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
