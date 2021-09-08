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
    [Route("api/category")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/product
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            // get all categories
            var categories = _context.Category;
            return Ok(categories);
        }

        // POST api/add new category - backend only
        [HttpPost("add")]
        public IActionResult Post([FromBody]Category value)
        {
            _context.Category.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }

        // GET api/searchresults/searchterm
        [HttpGet("searchresults/{searchTerm}")]
        public IActionResult GetSearchResults(string searchTerm)
        {
            // get all products with search term in name
            var category = _context.Category.Where(p => p.CategoryName.ToLower().Contains(searchTerm.ToLower()));
            var productsAndCategories = from cId in category
                                        join pcId in _context.Products
                                        on cId.CategoryId equals pcId.CategoryId
                                        select new
                                        {
                                            ProductId = pcId.ProductId,
                                            ProductName = pcId.ProductName,
                                            ProductPrice = pcId.ProductPrice,
                                            ProductDescription = pcId.ProductDescription,
                                            CategoryId = cId.CategoryId,
                                            CategoryName = cId.CategoryName
                                        };

            return Ok(productsAndCategories);
        }

    }
}
