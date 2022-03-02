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
            {
                _db.Book.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Информация занесена в базу данных";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //get
        public IActionResult Edit(int? id)
        {
            if (id == null | id == 0) { return NotFound(); }
            var book = _db.Book.Find(id);
            if (book == null) { return NotFound(); }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book obj)
        {
            if (ModelState.IsValid)
            {
                _db.Book.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Информация изменена";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //get
        public IActionResult Delete(int? id)
        {
            if (id == null | id == 0) { return NotFound(); }
            var book = _db.Book.Find(id);
            if (book == null) { return NotFound(); }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Book.Find(id);
            if (obj == null) { return NotFound(); }

            _db.Book.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Информация удалена из базы данных";
            return RedirectToAction("Index");
        }
    }
}
