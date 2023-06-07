using Microsoft.EntityFrameworkCore;
using Shop.DataBase;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products
{
    public class GetProduct
    {
        private ApplicationDBContext _context;

        public GetProduct(ApplicationDBContext context)
        {
            _context = context;
        }

        public ProductViewModel Do(string name)
        {

            return _context.Products
                .Include(x => x.Stock)
                .Where(x => x.Name == name)
                .Select(x => new ProductViewModel { 
                    Name = x.Name, 
                    Description = x.Description, 
                    Price =  x.Price.ToString("N2"),
                    Stock = x.Stock.Select(y => new StockViewModel { 
                        Id = y.Id,
                        Description = y.Description,
                        InStock = y.Qty > 0
                    })
                })
                .FirstOrDefault();
        }
        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public bool InStock { get; set; }
        }
        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }
}

