using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace BulkyBookWeb.Controllers
{

    


    public class CategoryController : Controller
    {
        public readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db; 
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string? q)
        {
            if (q == "")
            {
                TempData["error"] = "Please enter a text to search";
                return View();
            }
            else
            {
               
                IEnumerable<Category> objCategoryList = _db.Categories.Where(cats => cats.Name.Contains(q));
                return View(objCategoryList);
            }

            
            
        }


        public IActionResult Create()
        {
            
            return View();
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The Name cannot be the same as Display Order");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successful";
                return RedirectToAction("Index");
            }
            return View(obj);
            
        }*/

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);

            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The Name cannot be the same as Display Order");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successful";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        /*public async Task<IActionResult> Update(Category postNin, IFormFile photo)
        {

            if (photo != null && photo.Length > 0)
            {
                var fileName = Path.GetFileName(photo.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                var file_name = "/images/" + fileName;
                postNin.Image = file_name;
                _db.Categories.Update(postNin);
                _db.SaveChanges();
                using (var fileSrteam = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileSrteam);
                    TempData["success"] = postNin;

                }
                TempData["error"] = "An error occurred";
            }
            else
            {
               
                _db.Categories.Update(postNin);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";

            }
            return RedirectToAction("Index");
        }
        */

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {

        

            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
       
                _db.Categories.Remove(obj);
                _db.SaveChanges();
            TempData["success"] = "Category deleted successful";
            return RedirectToAction("Index");
            

        }

        

        public async Task<IActionResult> Save(Category postNin, IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                var fileName = Path.GetFileName(photo.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                var file_name = "/images/"+fileName;
                postNin.Image = file_name;
                _db.Categories.Add(postNin);
                _db.SaveChanges();
                using (var fileSrteam = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileSrteam);
                    TempData["success"] = "Category Saved successfully";
                    return RedirectToAction("Index");

                }
                
            }
            else
            {
                TempData["error"] = "Please select an image";
                return RedirectToAction("Create");
            }
            
        }
    }
}
