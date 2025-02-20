using Bookify.Core.Models;
using Bookify.Core.ViewModels;
using Bookify.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Controllers
{
    public class BookCopiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookCopiesController(ApplicationDbContext context)
        {

            _context = context;
        }

        public ActionResult Create(int bookID)
        {

            var book = _context.Books.Find(bookID);
            if (book is null)
            {
                return NotFound();
            }

            var ViewModel = new BookCopyFormViewModel { BookID = bookID, BookForRental = book.IsAvilabelForRental };
            return View("Form", ViewModel);
        }
        [HttpPost]
        public ActionResult Create(BookCopyFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", model);
            }

            var book = _context.Books.Find(model.BookID);
            if (book is null)
            {
                return NotFound();
            }

            var copy = new BookCopy
            {
                EditionNumber = model.EditionNumber,
                IsAvilabelForRental = model.IsAvilabelForRental,
                BookId = model.BookID,

            };
            _context.BookCopies.Add(copy);
            _context.SaveChanges();
            return RedirectToAction("Details", "Books", new { id = model.BookID });
            // var ViewModel = new BookCopyFormViewModel { BookID = bookID, BookForRental = book.IsAvilabelForRental };
            // return View("Form", ViewModel);
        }


        public ActionResult Edit(int copyID)
        {

            var copy = _context.BookCopies
            .Include(bc => bc.Book)
            .SingleOrDefault(bc => bc.Id == copyID);
            if (copy is null)
            {
                return NotFound();
            }


            var ViewModel = new BookCopyFormViewModel
            {
                Id=copy.Id,
                BookID = copy.BookId,
                BookForRental = copy.Book?.IsAvilabelForRental ?? false,
                EditionNumber = copy.EditionNumber,
            };
            return View("Form", ViewModel);
        }
        [HttpPost]
        public ActionResult Edit(BookCopyFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", model);
            }

            var book = _context.Books.Find(model.BookID);
            if (book is null)
            {
                return NotFound();
            }
            var copy = _context.BookCopies.Find(model.Id);
            if (copy is null)
            {
                return NotFound();
            }
            // var copy = new BookCopy
            // {
            //     EditionNumber = model.EditionNumber,
            //     IsAvilabelForRental = model.IsAvilabelForRental,
            //     BookId = model.BookID,

            // };
            copy.EditionNumber = model.EditionNumber;
            copy.IsAvilabelForRental = model.IsAvilabelForRental;
            _context.SaveChanges();
            return RedirectToAction("Details", "Books", new { id = model.BookID });
            // var ViewModel = new BookCopyFormViewModel { BookID = bookID, BookForRental = book.IsAvilabelForRental };
            // return View("Form", ViewModel);
        }

    }


}