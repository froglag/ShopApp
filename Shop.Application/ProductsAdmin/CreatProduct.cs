﻿using Shop.DataBase;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    public class CreatProduct
    {
        private ApplicationDBContext _context;

        public CreatProduct(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Response> Do(Request request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new Response() { 
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public class Request
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public float Price { get; set; }
        }
        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public float Price { get; set; }
        }

    }
    
}
