using Lostil.Data;
using Lostil.Data.Models;
using Lostil.Data.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lostil.Controllers
{
    public class OrderController:Controller
    {
        private AppDbContext db;
        private readonly UserManager<User> _userManager;
        public OrderController(AppDbContext appDb, UserManager<User> userManager)
        {
            db = appDb;
            _userManager = userManager;
        }
        [HttpGet]
        public ViewResult OrderZap(int productId)
        {
            Product product = db.Products.Find(productId);

            ViewBag.Message = product.ProductName;
            OrderZapViewModel model = new OrderZapViewModel {ProductId = product.ProductId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> OrderZap(OrderZapViewModel model)
        {
            var _user = await _userManager.GetUserAsync(User);
            var name = db.Products.FirstOrDefault(p => p.ProductId == model.ProductId);
            Orders order = new Orders {
                UserName= _user.UserName,
                ProductId = model.ProductId,
                Description = model.Description,
                Status = "В обработке",
                OrderName =name.ProductName,
                Date = model.Date,
                Time = model.Time
            };
            db.Orders.Add(order);
            await db.SaveChangesAsync();
            return RedirectToAction("LK", "Account");
        }
        //public async Task<IActionResult> OrderAdd(int productId)
        //{
        //    var _user = await _userManager.GetUserAsync(User);
        //    var name = db.Products.FirstOrDefault(p => p.ProductId == productId);
        //    Orders order = new Orders
        //    {
        //        ProductId = productId,
        //        UserName = _user.UserName,
        //        OrderName = name.ProductName,
        //        Status = "В обработке"
        //    };
        //    db.Orders.Add(order);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("LK", "Account");
        //}
    }
}
