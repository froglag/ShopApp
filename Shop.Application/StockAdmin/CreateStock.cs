using Azure.Core;
using Shop.DataBase;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    public class CreateStock
    {
        private ApplicationDBContext _context;

        public CreateStock(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Response> Do(Request request)
        {
            var stock = new Stock() {
                ProductId = request.ProductId,
                Qty = request.Qty,
                Description = request.Description
            };
            
            _context.Stock.Add(stock);
            await _context.SaveChangesAsync();

            return new Response{
                Id = stock.Id,
                Description = stock.Description,
                Qty = stock.Qty
            };
        }
        public class Request
        {            
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
        public class Response
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
    }
}
