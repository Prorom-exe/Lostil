using Lostil.Data;
using Lostil.Data.Models;
using Lostil.Data.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lostil.Controllers
{
    public class TovarController:Controller
    {
       
        private AppDbContext db;
        IWebHostEnvironment _appEnvironment;
        public TovarController(IWebHostEnvironment appEnvironment,AppDbContext appDb)
        {
            
            _appEnvironment = appEnvironment;
            db = appDb;
        }
       
        // ADMIN____________________________________________________________________________________________
        //add new item in categorys

        [HttpGet]
        public IActionResult Search(string search)
        {
            
                var result = db.Products.Where(x => x.ProductName.ToUpper().Contains(search.ToUpper())).ToList();

            return View("ConclusionItem", result);
        }

        [HttpGet]
        public ViewResult AddItem() => View();
        
        [HttpPost]
        public async Task<IActionResult> AddItem(AddItemViewModel model, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/img/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                Product product = new Product {
                    ImagePath = path,
                    Description = model.Description,
                    CategoryId = model.CategoryId,
                    ProductName = model.ProductName,
                    Price=model.Price
                };
                db.Products.Add(product);
                await db.SaveChangesAsync();
                
                return RedirectToAction("Index", "Home");
            }
           return RedirectToAction("AddItem");
          
        }

        //вывод товаров категорий

        public ViewResult ConclusionItem(int categoryId)
        {
            var items = db.Products.Where(p => p.CategoryId == categoryId).ToList();
            return View(items);
        }

        public IActionResult ConclusionItemDelete(int productId)
        {
            var del = db.Products.FirstOrDefault(d => d.ProductId == productId);
            db.Products.Remove(del);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ViewResult ConclusionItemEdit(int productId)
        {
            Product product = db.Products.Find(productId);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> ConclusionItemEdit(Product product, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/img/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                Product product1 = new Product
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    
                    Description = product.Description,
                    ImagePath = path
                };
                db.Products.Update(product1);
                await db.SaveChangesAsync();
            }
            else
            {
                Product product1 = new Product
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    CategoryId = product.CategoryId,

                    Description = product.Description

                };
                db.Products.Update(product1);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index","Home");
            
        }


    }
}
