using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using kitaabKhana.Data;
using kitaabKhana.Models;
using Microsoft.AspNetCore.Mvc;

namespace kitaabKhana.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;            
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();

            return View(objCategoryList);
        }


        public IActionResult Create() 
        {
            return View();
        }



        [HttpPost]
        public IActionResult Create(Category obj) 
        {
            /*
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name","The Display Order cannot exactly match the Name.");
            }
            */

            // if(obj.Name != null && obj.Name.ToLower() == "test")
            // {
            //     ModelState.AddModelError("",$"{obj.Name} is invalid name for Category");
            // }



            if(ModelState.IsValid){
                _db.Categories.Add(obj);
                _db.SaveChanges(); 
                TempData["success"] = "Category created sucessfully";
                return RedirectToAction("Index","Category");
            }
            return View();
        }




        public IActionResult Edit(int? id ) 
        {
            if( id==null || id == 0){
                return NotFound();
            }

            // multiple ways of retrieving one of the category from the database
            Category? categoryFromDb = _db.Categories.Find(id);           // it only works with primary key 
            /*
            Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.CategoryId==id);       // you can find this with any other column
                                                                                                    // e.g you can do as below
                                                                                                    // _db.Categories.FirstOrDefault(u => u.Name=="Some Novel");
            Category? categoryFromDb2 = _db.Categories.Where( u => u.CategoryId==id).FirstOrDefault();
            */

            if(categoryFromDb ==  null) {
                return NotFound();
            }
            return View(categoryFromDb);
        }



        [HttpPost]
        public IActionResult Edit(Category obj) 
        {
            
            if(ModelState.IsValid){
                _db.Categories.Update(obj);
                _db.SaveChanges(); 
                TempData["success"] = "Category updated sucessfully";
                return RedirectToAction("Index","Category");
            }
            return View();
        }




        public IActionResult Delete(int? id ) 
        {
            if( id==null || id == 0){
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(id);      
            if(categoryFromDb ==  null) {
                return NotFound();
            }
            return View(categoryFromDb);
        }



        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id) 
        {
            Category? obj = _db.Categories.Find(id);
            if(obj == null) {
                return NotFound();
            } 
            _db.Categories.Remove(obj);
            _db.SaveChanges(); 
            TempData["success"] = "Category deleted sucessfully";
            return RedirectToAction("Index","Category");
        }

    }
}