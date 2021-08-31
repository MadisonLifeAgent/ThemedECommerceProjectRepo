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


namespace eCommerceStarterCode.Controllers
{
    [Route("api/product")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/product
        [HttpGet]
        public IActionResult Get()
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
    }
}
