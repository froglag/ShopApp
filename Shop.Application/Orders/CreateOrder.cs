using Shop.DataBase;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shop.Application.Orders
{
    public class CreateOrder
    {
        private ApplicationDBContext _context;

        public CreateOrder(ApplicationDBContext context)
        {
            _context = context;
        }

        public class Request
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }

            public List<Stock> Stocks { get; set; }
        }

        public class Stock
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            var UpdateStock = _context.Stock.Where(x => request.Stocks.Any(y => y.StockId == x.Id)).ToList();

            foreach(var item in UpdateStock)
            {
                item.Qty = item.Qty - request.Stocks.FirstOrDefault(x => x.StockId == item.Id).Qty;
            }

            var order = new Order
            { 
                OrderRef = OrderReference(),
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,

                Address1 = request.Address1,
                Address2 = request.Address2,
                City = request.City,
                PostCode = request.PostCode,

                OrderStocks = request.Stocks.Select(x => new OrderStock
                {
                    StockId = x.StockId,
                    Qty = x.Qty,
                }).ToList(),
            };
            _context.Add(order);
            return await _context.SaveChangesAsync() > 0;
        }

        public string OrderReference()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdifjhigklmnopqrstuvwxyz1234567890";
            var result = new char[12];
            var random = new Random();
            do
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = chars[random.Next(chars.Length)];
                }
            } while (_context.Order.Any(x => x.OrderRef == new string(result)));
            return new string(result);
        } 
    }
}
