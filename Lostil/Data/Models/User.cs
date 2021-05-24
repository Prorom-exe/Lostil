using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lostil.Data.Models
{
    public class User:IdentityUser
    {
        [ForeignKey("Orders")]
        public int OrderId { get; set; }
    }
}
