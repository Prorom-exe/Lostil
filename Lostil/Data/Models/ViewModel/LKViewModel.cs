using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lostil.Data.Models.ViewModel
{
    public class LKViewModel
    {
       public List<IGrouping<DateTime, Orders>> Orders { get; set; }

        public List<string> Products { get; set; }
        
    }
}
