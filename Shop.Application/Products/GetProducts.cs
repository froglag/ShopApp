using Shop.DataBase;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.GetProducts
{
    public class GetProducts
    {
        private ApplicationDBContext _context;

        public GetProducts(ApplicationDBContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductViewModel> Do()
        {
            return _context.Products.ToList().Select(x => new ProductViewModel { Name = x.Name, Description = x.Description, Price ="$ "+ x.Price.ToString("N2") });
        }
    }
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
}
