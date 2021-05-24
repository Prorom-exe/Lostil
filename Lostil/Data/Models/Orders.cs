using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lostil.Data.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        
        public string UserName { get; set; }

        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int ProductId { get; set; }
        public string OrderName { get; set; }
        public string Status { get; set; }
    }
}
