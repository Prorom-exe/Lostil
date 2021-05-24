using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lostil.Data.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public string ImagePath { get; set; }
        public string ProductName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

    }
}
