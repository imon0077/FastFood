using FastFood.Models;
using FastFood.Repository;
using FastFood.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public ItemsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var items = _context.Items.Include(x => x.Category).Include(y => y.SubCategory)
                //.Select(model => new ItemViewModel()
                //{
                //    Id = model.Id,
                //    Title = model.Title,
                //    Description = model.Description,
                //    Price = model.Price,
                //    CategoryId = model.CategoryId,
                //    SubCategoryId = model.SubCategoryId
                //})
                .ToList();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ItemViewModel vm = new();
            ViewBag.Category = new SelectList(_context.Categories, "Id", "Title").ToList();
            ViewBag.SubCategory = new SelectList(_context.SubCategories, "Id", "Title").ToList();

            return View(vm);
        }

        [HttpGet]
        public IActionResult GetSubCategory(int categoryId)
        {
            var subCategory = _context.SubCategories.Where(x => x.CategoryId == categoryId).FirstOrDefault();
            return Json(subCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemViewModel vm)
        {
            Item model = new();
            if (ModelState.IsValid)
            {
                if (vm.ImageUrl != null && vm.ImageUrl.Length > 0)
                {
                    var uploadDir = @"Images/Items";
                    var fileName = Guid.NewGuid().ToString() + "-" + vm.ImageUrl.FileName;
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, uploadDir, fileName);

                    await vm.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    model.Image = "/" + uploadDir + "/" + fileName;
                }
                model.Title = vm.Title;
                model.Price = vm.Price;
                model.Description = vm.Description;
                model.CategoryId = vm.CategoryId;
                model.SubCategoryId = vm.SubCategoryId;
                _context.Items.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _context.Items.Where(x => x.Id == id).FirstOrDefault();
            if (item != null)
            {
                //vm.Id = item.Id;
                //vm.Title = item.Title;
                //vm.CategoryId = item.CategoryId;
                //vm.SubCategoryId = item.SubCategoryId;

                ViewBag.Category = new SelectList(_context.Categories, "Id", "Title").ToList();
                ViewBag.SubCategory = new SelectList(_context.SubCategories, "Id", "Title", item.SubCategoryId).ToList();
            }
            return View(item);
        }


    }
}
