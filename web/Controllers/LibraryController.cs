using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
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

        [HttpGet]

        public IEnumerable<Book> GetBook()
        {
            var books = from b in _db.Book.Where(b => b.Category == 1)
                         select b;
            return books.ToList();
        }

        private int GetCount()
        {
            var bookcount = (from c in _db.Book.Where(c => c.Category == 1)
                            select c).Count();
            return bookcount;
        }

        public IActionResult Index()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Books = GetBook();
            mymodel.Count = GetCount();
            return View(mymodel);
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
