﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lostil.Data.Models.ViewModel
{
    public class AddItemViewModel
    {
        
        
        public int CategoryId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
