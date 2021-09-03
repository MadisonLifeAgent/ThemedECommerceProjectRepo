using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eCommerceStarterCode.Controllers
{
    [Route("api/product")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/product
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            // get all products
            var products = _context.Products;
            return Ok(products);
        }

        // POST api/addnewproduct
        [HttpPost("add")]
        public IActionResult Post([FromBody]Product value)
        {
            _context.Products.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }

        // GET api/searchresults/searchterm
        [HttpGet("searchresults/{searchTerm}")]
        public IActionResult GetSearchResults(string searchTerm)
        {
            // get all products with search term in name
            var products = _context.Products.ToList().Where(p => p.ProductName.ToLower().Contains(searchTerm.ToLower()));
            return Ok(products);
        }
    }
}
