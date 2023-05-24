using Shop.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    public class GetProduct
    {
        private ApplicationDBContext _context;

        public GetProduct(ApplicationDBContext context)
        {
            _context = context;
        }

        public ProductViewModel Do(int Id)
        {
            return _context.Products.Where(x=> x.Id == Id).Select(x => new ProductViewModel {Id = x.Id, Name = x.Name, Description = x.Description, Price = x.Price }).FirstOrDefault();
        }
        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public float Price { get; set; }
        }
    }
}
