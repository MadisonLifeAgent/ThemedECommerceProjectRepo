using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eCommerceStarterCode.Controllers
{
    [Route("api/shoppingcart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("{UserId}")]
        public IActionResult Get(string UserId)
        {
            var shoppingCart = _context.ShoppingCart;
            var specificCart = shoppingCart.Include(sc => sc.Product).Include(sc => sc.Product.Category).ToList().Where(sc => sc.Id == UserId);
            return Ok(specificCart);
        }

        [HttpPost("add")]
        public IActionResult Post([FromBody] ShoppingCart value)
        {
            _context.ShoppingCart.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }

        [HttpDelete("{ProudctId}/{UserId}")]
        public IActionResult Remove(int ProudctId, string UserId)
        {
            var deleteProduct = _context.ShoppingCart.Where(dp => dp.Id == UserId && dp.ProductId == ProudctId).SingleOrDefault();
            if (deleteProduct == null)
            {
                return NotFound();
            }
            _context.ShoppingCart.Remove(deleteProduct);
            _context.SaveChanges();
            return Ok(deleteProduct);
        }
    }
}
