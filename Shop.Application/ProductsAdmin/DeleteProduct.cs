using Shop.DataBase;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    public class DeleteProduct
    {
        private ApplicationDBContext _context;

        public DeleteProduct(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Do(int Id)
        {
            var Product = _context.Products.FirstOrDefault(x=>x.Id == Id);
            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
