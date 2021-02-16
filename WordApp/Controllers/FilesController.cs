using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using WordApp.DataAccessLayer;
using WordApp.Models;

namespace WordApp.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        #region consturctor
        private readonly IFilesRepository _filesRepository;

        public FilesController(IFilesRepository filesRepository)
        {
            _filesRepository = filesRepository;
        } 
        #endregion

        #region  MyDocs
        public ActionResult MyDocs()
        {     
            string user = System.Web.HttpContext.Current.User.Identity.Name;
            string path = Request.MapPath("~/TargetDOCS/");
            var userFiles = _filesRepository.GetFiles(path, user);
            ViewBag.Message = TempData["mydata"] as string;
            ViewBag.Status = TempData["status"];
            TempData["redirect"] = "MyDocs";
            ViewBag.Name = "Zapisane dokumenty";
            ViewBag.Type = 2;
            return View(userFiles);            
        }
#endregion

        #region Delete
        [HttpGet]
        public ActionResult Delete(string id, int type)
        {
            string mess = "";
            bool status = false;
            string path;

            switch (type)
            {
                case 1: path  = Request.MapPath("~/TemplateFiles/");
                    break;
                case 2: path = Request.MapPath("~/TargetDOCS/");
                    break;
                default:
                    path = "";
                    break;
            }          
            try
            {
                var file = _filesRepository.GetFiles(path).Find(p => p.Hash == id);
                string filename = file.FileName;
                System.IO.File.Delete(file.FilePaht);
                mess = "Usunięto plik o nazwie: " + filename;
                status = true;
            }
            catch (Exception)
            {
                mess = "Nie znaleziono pliku";
                status = true;
            }          
            TempData["mydata"] = mess;
            TempData["status"] = status;            
            return RedirectToAction(TempData["redirect"] as string);                   
        }
#endregion

        #region Download
        [HttpGet]
        public ActionResult Download(string id, int? type) 
        {
            string path;

            switch (type)
            {
                case 1:
                    path = Request.MapPath("~/TemplateFiles/");
                    break;
                case 2:
                    path = Request.MapPath("~/TargetDOCS/"); 
                    break;
                default:
                    return View();                    
            }           
            
            var file = _filesRepository.GetFiles(path).Find(p => p.Hash == id);
            if(file != null)
            {
                var stream = new FileStream(file.FilePaht, FileMode.Open);
                return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, file.FileName + file.Extension);        
            }
            return View();

                           
        }
        #endregion

        #region Templates

        [Authorize(Roles ="Admin")]
        public ActionResult Templates()
        {
            var templates = _filesRepository.GetFiles(Request.MapPath("~/TemplateFiles/"));
            ViewBag.Message = TempData["mydata"] as string;
            ViewBag.Status = TempData["status"];
            ViewBag.Name = "Szablony zapisane na serwerze";
            ViewBag.Type = 1;
            TempData["redirect"] = "Templates";
            return View("MyDocs",templates);
        }

     
#endregion
    }
}