using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Models
{
    public class ShoppingCart
    {   
        [Key]
        public string Id { get; set; }
        [Key]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
