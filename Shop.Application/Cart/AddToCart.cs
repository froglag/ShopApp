using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.DataBase;
using Shop.Domain.Models;

namespace Shop.Application.Cart
{
    public class AddToCart
    {
        private ISession _session;
        private ApplicationDBContext _context;

        public AddToCart(ISession session, ApplicationDBContext context)
        {
            _session = session; 
            _context = context;
        }

        public async Task<bool> Do(Request request)
        {
            var stockOnHold = _context.StockOnHolds.Where(x => x.SessionId == _session.Id).ToList();
            var stockToHold = _context.Stock.Where(x => x.Id == request.StockId).FirstOrDefault();

            if (stockToHold.Qty < request.Qty)
            {
                return false;
            }
            _context.StockOnHolds.Add(new StockOnHold
            {
                StockId = request.StockId,
                SessionId = _session.Id,
                Qty = request.Qty,
                ExpiryDate = DateTime.Now.AddMinutes(20),
            });

            stockToHold.Qty = stockToHold.Qty - request.Qty;

            foreach(var stock in stockOnHold)
            {
                stock.ExpiryDate = DateTime.Now.AddMinutes(20);
            }
            
            await _context.SaveChangesAsync();

            var cartlist = new List<CartProduct>();
            var stringObject = _session.GetString("cart");

            if(!string.IsNullOrEmpty(stringObject))
            {
                cartlist = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }

            if (cartlist.Any(x => x.StockId == request.StockId))
            {
                cartlist.Find(x => x.StockId == request.StockId).Qty += request.Qty;
            }
            else
            {
                cartlist.Add(new CartProduct
                {
                    StockId = request.StockId,
                    Qty = request.Qty
                });
            }

            stringObject = JsonConvert.SerializeObject(cartlist);
            _session.SetString("cart", stringObject);

            return true;
        }
        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
