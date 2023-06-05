using Shop.DataBase;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    public class DeleteStock
    {
        private ApplicationDBContext _context;

        public DeleteStock(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Do(int Id)
        {
            var stock = _context.Stock.FirstOrDefault(x => x.Id == Id);
            _context.Remove(stock);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
