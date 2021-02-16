using System.Net;
using System.Web.Mvc;
using WordApp.DataAccessLayer;
using WordApp.Models;

namespace WordApp.Controllers
{
    public class FormatsController : Controller
    {
        public readonly IFormatsRepository _formatsRepository;

        public FormatsController(IFormatsRepository formatsRepository)
        {
            _formatsRepository = formatsRepository;
        }

        // GET: Formats
        public ActionResult Index()
        {
            return View(_formatsRepository.GetAllFormats());
        }

        // GET: Formats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format format = _formatsRepository.GetFormat(id);
            if (format == null)
            {
                return HttpNotFound();
            }
            return View(format);
        }

        // GET: Formats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Formats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Displayed_Name,Format_String,Format_Int")] Format format)
        {
            if (ModelState.IsValid)
            {
                _formatsRepository.AddFormat(format);
                return RedirectToAction("Index");
            }

            return View(format);
        }

        // GET: Formats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format format = _formatsRepository.GetFormat(id);
            if (format == null)
            {
                return HttpNotFound();
            }
            return View(format);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Displayed_Name,Format_String,Format_Int")] Format format)
        {
            if (ModelState.IsValid)
            {
                _formatsRepository.EditFormat(format);
                return RedirectToAction("Index");
            }
            return View(format);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Format format = _formatsRepository.GetFormat(id);
            if (format == null)
            {
                return HttpNotFound();
            }
            return View(format);
        }
      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _formatsRepository.RemoveFormat(id);
            return RedirectToAction("Index");
        }   
    }
}
