using FastFood.Models;
using FastFood.Repository;
using FastFood.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var subCategory = _context.SubCategories.Include(x => x.Category).ToList();
            return View(subCategory);
        }


        [HttpGet]
        public IActionResult Create()
        {
            SubCategoryViewModel vm = new();
            ViewBag.category = new SelectList(_context.Categories, "Id", "Title");
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(SubCategoryViewModel vm)
        {
            if (ModelState.IsValid) { 
                SubCategory model = new()
                {
                    Title = vm.Title,
                    CategoryId = vm.CategoryId
                };
                _context.SubCategories.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            SubCategoryViewModel vm = new();
            var subCategory = _context.SubCategories.Where(x => x.Id == id).FirstOrDefault();
            if (subCategory != null)
            {
                vm.Id = subCategory.Id;
                vm.Title = subCategory.Title;
                vm.CategoryId = subCategory.CategoryId;

                ViewBag.category = new SelectList(_context.Categories, "Id", "Title", subCategory.CategoryId);
            }
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(SubCategoryViewModel vm)
        {
            SubCategory model = _context.SubCategories.Where(x => x.Id == vm.Id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                model.Title = vm.Title;
                model.CategoryId = vm.CategoryId;

                _context.SubCategories.Update(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            SubCategoryViewModel vm = new();
            var subCategory = _context.SubCategories.Where(x => x.Id == id).FirstOrDefault();
            if (subCategory != null)
            {
                _context.SubCategories.Remove(subCategory);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
