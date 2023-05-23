using Shop.DataBase;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.CreatProducts
{
    public class CreatProduct
    {
        private ApplicationDBContext _context;

        public CreatProduct(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task Do(ProductViewModel vm)
        {
            _context.Products.Add(new Product { Name = vm.Name, Description = vm.Description, Price = vm.Price });
            await _context.SaveChangesAsync();
        }
    }
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}
