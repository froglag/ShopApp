﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using Shop.Application.StockAdmin;
using Shop.DataBase;
using Shop.Domain.Models;

namespace ShopApp.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        public ApplicationDBContext _context;

        public AdminController(ApplicationDBContext context)
        {
            _context = context;
        }


        [HttpGet("products")]
        public IActionResult GetProducts() => Ok(new GetProducts(_context).Do());

        [HttpGet("products/{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_context).Do(id));

        [HttpPost("products")]
        public async Task<IActionResult> CreatProduct([FromBody] CreatProduct.Request request) => Ok((await new CreatProduct(_context).Do(request)));

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id) => Ok((await new DeleteProduct(_context).Do(id)));

        [HttpPut("products")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct.Request request) => Ok((await new UpdateProduct(_context).Do(request)));



        [HttpGet("stocks")]
        public IActionResult GetStock() => Ok(new GetStock(_context).Do());

        [HttpPost("stocks")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStock.Request request) => Ok((await new CreateStock(_context).Do(request)));

        [HttpDelete("stocks/{id}")]
        public async Task<IActionResult> DeleteStock(int id) => Ok((await new DeleteStock(_context).Do(id)));

        [HttpPut("stocks")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStock.Request request) => Ok((await new UpdateStock(_context).Do(request)));
    }
}
