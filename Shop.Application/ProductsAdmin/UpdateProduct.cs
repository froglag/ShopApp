using Shop.DataBase;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    public class UpdateProduct
    {
        private ApplicationDBContext _context;

        public UpdateProduct(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task Do(int Id, ProductViewModel vm)
        {
            var Product = _context.Products.FirstOrDefault(x => x.Id == Id);
            Product.Name = vm.Name;
            Product.Description = vm.Description;
            Product.Price = vm.Price;
            await _context.SaveChangesAsync();
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public float Price { get; set; }
        }
    }
}
