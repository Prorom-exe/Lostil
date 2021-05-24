using Lostil.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lostil.Data
{
    public class InitialNow
    {
        public static async Task Initial(AppDbContext content, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("Moderator"));
                
            }

            if (!userManager.Users.Any())
            {
                User user = new User { Email = "admin12@mail.ru", UserName = "BigAdmin", };
                string Password = "Admin90+";
                await userManager.CreateAsync(user, Password);
                await userManager.AddToRoleAsync(user, "Admin");
                await userManager.AddToRoleAsync(user, "Moderator");
            }
        }
        }
}
