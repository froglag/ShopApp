using Shop.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    public class GetStocks
    {
        private ApplicationDBContext _context;

        public GetStocks(ApplicationDBContext context)
        {
            _context = context;
        }
        public IEnumerable<StockViewModel> Do(int ProductId)
        {
            var stock = _context.Stock
                .Where(x=>x.ProductId==ProductId)
                .Select(x=> new StockViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Qty = x.Qty
                })
                .ToList();
            return stock;
        }
        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
    }
}
