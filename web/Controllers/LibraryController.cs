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

        private IEnumerable<Book> GetBook()
        {
            IEnumerable<Book> books = _db.Book;
            return books;
        }

        private CountBook GetCount()
        {
            CountBook Count = new();
            CountBook Catcount = Count.GetCount();
            return Catcount;
        }
        private int GetCategory()
        {
            CountBook Cat = new();
            int Category = Cat.GetCategory();
            return Category;
        }

        public IActionResult Index()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Books = GetBook();
            mymodel.Count = GetCount();
            mymodel.Category = GetCategory();
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
