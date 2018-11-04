using GuestBook.Models;
using GuestBook.Models.DAL.EF;
using System;
using System.Web.Mvc;

namespace GuestBook.Controllers
{
    public class HomeController : Controller
    {       
        IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View(_repository.GetRecords());
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
                record.CreationDate = DateTime.Now;
                record.UpdationDate = DateTime.Now;
                _repository.Create(record);
                return RedirectToAction("Index");
            }
            return View(record);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Record record = _repository.Find(id);
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
                record.UpdationDate = DateTime.Now;
                _repository.Update(record);
                return RedirectToAction("Index");
            }
            return View(record);
        }        
        
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Record record = _repository.Find(id);
            if (record != null)
            {
                return View(record);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(Record record)
        {            
            if (_repository.Find(record.Id) != null)
            {
                _repository.Delete(record.Id);                
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Record record = _repository.Find(id);
            if (record != null)
            {
                return View(record);
            }
            return RedirectToAction("Index");
        }       
    }
}