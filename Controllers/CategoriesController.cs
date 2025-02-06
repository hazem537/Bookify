using Bookify.Core.Models;
using Bookify.Core.ViewModels;
using Bookify.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // TODO : using view Model
            var categories = _context.Categories.AsNoTracking().ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View("Form");
        }

        [HttpPost]
        public IActionResult Create(CategoryFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category { Name = model.Name };
                _context.Add(category);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View("Form");

        }
        public IActionResult Edit(int Id)
        {
            var category = _context.Categories.Find(Id);
            if (category is null)
            {
                return NotFound();
            }
            var viewModel = new CategoryFormViewModel
            {
                Id = Id,
                Name = category.Name,
            };
            return View("Form", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Form");
            }
            var category = _context.Categories.Find(model.Id);
            if (category is null)
            {
                return NotFound();
            }
            category.Name = model.Name;
            category.LastUpdatedOn = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AllowItem (string name ,int id){
            var isExist= _context.Categories.Any(c=>c.Name == name  && c.Id != id) ;
            return Json(!isExist);
        }
    }

}