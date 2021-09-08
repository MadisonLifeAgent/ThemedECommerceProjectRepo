﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceStarterCode.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // get all reviews
        [HttpGet]
        public IActionResult Get()
        {
            var reviews = _context.Reviews;
            return Ok(reviews);
        }

        // add review
        [HttpPost("add")]
        public IActionResult Post([FromBody] Review value)
        {
            _context.Reviews.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }

        [HttpGet("{productId}")]
        public IActionResult GetReviewByProductId(int productId)
        {
            var reviews = _context.Reviews.Include(r => r.User).ToList().Where(r => r.ProductId == productId);

            return Ok(reviews);
        }


        [HttpGet("averagerating{productId}")]
        public IActionResult GetAverageRating(int productId)
        {
            var ratingsSum = _context.Reviews.Where(r => r.ProductId == productId).Select(a => (decimal)a.Rating).Sum();

            var reviewsCount = _context.Reviews.Where(r => r.ProductId == productId).Count();

            var ratingsAverage = (decimal)0;

            if (reviewsCount == 0)
            {
                ratingsAverage = 0;
            }
            else if (reviewsCount > 0)
            {
                ratingsAverage = ratingsSum / reviewsCount;
            }

            return Ok(ratingsAverage);
        }
    }
}
