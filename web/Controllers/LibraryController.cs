using Microsoft.AspNetCore.Mvc;
using web.db;
using web.Models;

namespace web.Controllers
{
    public class libraryController : Controller
    {
        private readonly Database _db;

        public libraryController(Database db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Book> bookList = _db.Book;
            return View(bookList);
        }

        //get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book obj)
        {
            if (ModelState.IsValid)
                _db.Book.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
