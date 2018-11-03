using GuestBook.Models;
using System;
using System.Web.Mvc;

namespace GuestBook.Controllers
{
    public class HomeController : Controller
    {
        static IRepository repository = new AdoRepository();

        // GET: Home
        public ActionResult Index()
        {
            return View(repository.GetRecords());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Record record)
        {
            if(ModelState.IsValid)
            {
                record.RecordDate = DateTime.Now.Date;
                repository.Create(record);
                return RedirectToAction("Index");
            }
            return View(record);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Record record = repository.Find(id);
            if (record != null)
            {
                return View(record);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(Record record)
        {            
            if (ModelState.IsValid)
            {
                record.RecordDate = DateTime.Now.Date;
                repository.Update(record);
                return RedirectToAction("Index");
            }
            return View(record);
        }        
        
        public ActionResult Delete(int id)
        {
            Record record = repository.Find(id);
            if (record != null)
            {
                repository.Delete(id);
            }
            return RedirectToAction("Index");
        }        
        
        [HttpGet]
        public ActionResult Details(int id)
        {
            Record record = repository.Find(id);
            if (record != null)
            {
                return View(record);
            }
            return RedirectToAction("Index");
        }
    }
}