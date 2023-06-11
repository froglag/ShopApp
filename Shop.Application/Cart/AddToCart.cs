using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Models;

namespace Shop.Application.Cart
{
    public class AddToCart
    {
        private ISession _session;

        public AddToCart(ISession session)
        {
            _session = session; 
        }

        public void Do(Request request)
        {
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
        }
        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
