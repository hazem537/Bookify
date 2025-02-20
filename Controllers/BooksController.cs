using Bookify.Core.Models;
using Bookify.Core.ViewModels;
using Bookify.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Controllers
{

    public class BooksController : Controller
    {
        private readonly IWebHostEnvironment _WebHostEnviroment;
        private readonly List<string> _allowedExtension = new() { ".jpg", ".jpeg", ".png" };
        private readonly int _maxAllowedSize = 2 * 1024 * 1024;
        private readonly ApplicationDbContext _context;
        public BooksController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _WebHostEnviroment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }
        public IActionResult Details(int id)
        {
            var book = _context.Books
            .Include(b => b.Author)
            .Include(b => b.Copies)
            .Include(b => b.Categories)
            .ThenInclude(bk => bk.Category)
            .FirstOrDefault(b => b.Id == id);
            if (book is null)
            {
                return NotFound();
            }
            var ViewModel = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorName = book.Author?.Name ?? "No Author",
                Publisher = book.Publisher,
                PublishingDate = book.PublishingDate,
                ImageUrl = book.ImageUrl,
                Hall = book.Hall,
                IsAvilabelForRental = book.IsAvilabelForRental,
                CreatedOn = book.CreatedOn,
                UpdatedOn = book.UpdatedOn,
            };
            ViewModel.Categories = book.Categories.Select(c => c.Category?.Name ?? "");
            ViewModel.Copies = book.Copies.Select(c => new BookCopyViewModel
            {
                Id = c.Id,
                BookTitle = c.Book?.Title ?? "no title",
                IsAvilabelForRental = c.IsAvilabelForRental,
                EditionNumber = c.EditionNumber,
                SerialNumber = c.SerialNumber,
                CreatedOn = c.CreatedOn,
                UpdatedOn = c.UpdatedOn,
                IsDeleted = c.IsDeleted

            });

            return View(ViewModel);
        }
        public IActionResult Create()
        {
            return View("Form", PopulateBook());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookFormViewModel model)
        {
            if (!ModelState.IsValid) return View("Form", PopulateBook(model));


            var book = new Book
            {
                Title = model.Title,
                Description = model.Description,
                Publisher = model.Publisher,
                AuthorId = model.AuthorId,
                PublishingDate = model.PublishingDate,
                Hall = model.Hall,
                IsAvilabelForRental = model.IsAvilabelForRental,
            };

            if (model.Image is not null)
            {
                var extension = Path.GetExtension(model.Image.FileName);
                if (!_allowedExtension.Contains(extension))
                {
                    ModelState.AddModelError(nameof(model.Image), "only .png .jpg .jpeg are allowed ");
                    return View("Form", PopulateBook(model));
                }

                if (_maxAllowedSize < model.Image.Length)
                {
                    ModelState.AddModelError(nameof(model.Image), "that is image is too big has max size 2mb");
                    return View("Form", PopulateBook(model));

                }
                var ImageName = $"{Guid.NewGuid()}{extension}";
                var path = Path.Combine($"{_WebHostEnviroment.WebRootPath}/images/books", ImageName);
                using var stream = System.IO.File.Create(path);
                await model.Image.CopyToAsync(stream);
                book.ImageUrl = path;
            }
            _context.Add(book);
            _context.SaveChanges();

            foreach (var category in model.SelectedCategories)
            {
                _context.BookCategories.Add(new BookCategory { CategoryId = category, BookId = book.Id });
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Details), new { id = book.Id });
        }
        private BookFormViewModel PopulateBook(BookFormViewModel? model = null)
        {
            var ViewModel = model is null ? new BookFormViewModel() : model;
            var authors = _context.Authors
             .Where(a => !a.IsDeleted)
             .Select(a => new SelectListItem { Text = a.Name, Value = a.ID.ToString() })
             .OrderBy(a => a.Text).ToList();

            var categories = _context.Categories
                .Where(a => !a.IsDeleated)
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() })
                .OrderBy(a => a.Text).ToList();
            ViewModel.Authors = authors;
            ViewModel.Categories = categories;
            return ViewModel;
        }
        public IActionResult Edit(int id)
        {
            var book = _context.Books
            .Include(b => b.Categories)
            .First(b => b.Id == id);
            if (book is null) return NotFound();
            var ViewModel = new BookFormViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Publisher = book.Publisher,
                AuthorId = book.AuthorId,
                PublishingDate = book.PublishingDate,
                Hall = book.Hall
            };

            ViewModel.SelectedCategories = book.Categories.Select(c => c.CategoryId).ToList();

            //  SelectedCategories = book.Categories

            return View("Form", PopulateBook(ViewModel));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BookFormViewModel model)
        {
            var book = _context.Books
           .Include(b => b.Categories)
           .First(b => b.Id == model.Id);
            if (book is null) return NotFound();
            if (!ModelState.IsValid) return View("Form", PopulateBook(model));
            if (model.Image is not null)
            {

                if (!string.IsNullOrEmpty(book.ImageUrl))
                {
                    // delete image file and in db 
                    // get path
                    var OldImagePath = Path.Combine($"{_WebHostEnviroment.WebRootPath}/images/books", book.ImageUrl);
                    if (System.IO.File.Exists(OldImagePath))
                    {
                        // if image exist  delete it 
                        System.IO.File.Delete(OldImagePath);
                    }

                }
                var extension = Path.GetExtension(model.Image.FileName);
                if (!_allowedExtension.Contains(extension))
                {
                    ModelState.AddModelError(nameof(model.Image), "only .png .jpg .jpeg are allowed ");
                    return View("Form", PopulateBook(model));
                }

                if (_maxAllowedSize < model.Image.Length)
                {
                    ModelState.AddModelError(nameof(model.Image), "that is image is too big has max size 2mb");
                    return View("Form", PopulateBook(model));

                }
                var ImageName = $"{Guid.NewGuid()}{extension}";
                var path = Path.Combine($"{_WebHostEnviroment.WebRootPath}/images/books", ImageName);
                using var stream = System.IO.File.Create(path);
                await model.Image.CopyToAsync(stream);
                book.ImageUrl = path;
            }

            book.Title = model.Title;
            book.Description = model.Description;
            book.Publisher = model.Publisher;
            book.AuthorId = model.AuthorId;
            book.PublishingDate = model.PublishingDate;
            book.Hall = model.Hall;
            book.IsAvilabelForRental = model.IsAvilabelForRental;
            book.UpdatedOn = DateTime.Now;

            _context.SaveChanges();

            _context.BookCategories.RemoveRange(book.Categories.ToList());
            foreach (var category in model.SelectedCategories)
            {
                _context.BookCategories.Add(new BookCategory { CategoryId = category, BookId = book.Id });
            }
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = book.Id });

        }

        public IActionResult AllowItem(BookFormViewModel model)
        {
            var book = _context.Books.SingleOrDefault(b =>
            b.Title.ToLower() == model.Title.ToLower() && b.AuthorId == model.AuthorId);
            var isAllowed = book is null || book.Id.Equals(model.Id);
            return Json(isAllowed);
        }
    }
}