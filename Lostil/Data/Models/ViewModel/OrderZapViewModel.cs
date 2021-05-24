using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lostil.Data.Models.ViewModel
{
    public class OrderZapViewModel
    {
        public DateTime Date { get; set; }
        
        public string Time { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
    }
}
