using Bookify.Core.ViewModels;
using Bookify.Core.Models;

using Bookify.Data;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Authours = _context.Authors.ToList();
            return View(Authours);
        }

        public IActionResult Create()
        {
            return View("Form");
        }
        [HttpPost]
        public IActionResult Create(AuthorFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var NewAuthour = new Author { Name = model.Name };
                _context.Authors.Add(NewAuthour);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Form");
        }
        public IActionResult Edit(int ID)
        {
            var author = _context.Authors.Find(ID);
            if (author is null)
            {
                return NotFound();
            }
            var viewModel = new AuthorFormViewModel
            {
                ID = author.ID,
                Name = author.Name
            };
            return View("Form", viewModel);
        }

        [HttpPost]
        public IActionResult Edit(AuthorFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var author = _context.Authors.Find(model.ID);
            if (author is null)
            {
                return NotFound();
            }
            author.Name = model.Name;
            author.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult AllowItem (string name  ,int id){
            var IsExist  = _context.Authors.Any(a=>a.Name == name  && a.ID != id) ;
            return Json(!IsExist);
        }
    }
}