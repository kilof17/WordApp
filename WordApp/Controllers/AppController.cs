using System;
using System.Linq;
using System.Web.Mvc;
using WordApp.DataAccessLayer;
using WordApp.ViewModels;
using System.IO;

namespace WordApp.Controllers
{
    [Authorize]
    public class AppController : Controller
    { 
        private readonly IFilesRepository _filesReopository;
        private readonly IControlsRepository _controlsRepository;
        private readonly IFormatsRepository _formatsRepository;

        #region constructor
        public AppController(IFilesRepository filesRepository,IControlsRepository controlsRepository,IFormatsRepository formatsRepository)
        {
            _filesReopository = filesRepository;
            _controlsRepository = controlsRepository;
            _formatsRepository = formatsRepository;
        }
        #endregion

        #region Select template
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.SelectedMessage = TempData["IndexMessage"] as string;
            ViewBag.Templates = _filesReopository.GetFilesNames(Request.MapPath("~/TemplateFiles/")).OrderBy(FileName=> FileName);            
            //ViewBag.Templates = _filesReopository.GetFiles(Request.MapPath("~/TemplateFiles/")).Select(file => file.FileName).OrderBy(FileName => FileName).ToList();
            
            return View();
        }

        #endregion

        #region Display Form    
        public ActionResult NewDocument(string TemplateName)
        {       
            if (String.IsNullOrEmpty(TemplateName))
            {
                TempData["IndexMessage"] = "Wybierz szablon";
                return RedirectToAction("Index");
            }

            var templates = _filesReopository.GetFiles(Request.MapPath("~/TemplateFiles/"));
            object templatePath = templates.First(x => x.FileName == TemplateName).FilePaht;

            AppNewDocumentViewModel viewModel = new AppNewDocumentViewModel();          
            Word word = new Word();

            viewModel.AllControls= word.GetContorlsFromTemplate(templatePath);
            viewModel.AllControls = _controlsRepository.GetControlsFromDb(viewModel.AllControls).ToList();
            viewModel.AllFormats = _filesReopository.GetAllFormats().ToList();            

            ViewBag.Template = TemplateName;
            return View(viewModel);
        }
        #endregion
        
        #region Save Form
        [HttpPost]
        public ActionResult Save(AppNewDocumentViewModel model, string templateName, int selected)
        {          
            model.AllControls.RemoveAll(r => r.Tag.Contains("//comment")); // delete labels from list            
            Models.Format format = _formatsRepository.GetFormat(selected);
            Models.File template= _filesReopository.GetFiles(Request.MapPath("~/TemplateFiles/")).Find(x => x.FileName == templateName);
           
            _controlsRepository.SaveContols(model.AllControls);
            Word word = new Word();
            string fileInfoHash = word.SaveDocument(model.AllControls,template,format);            

            ViewBag.InfoHash = fileInfoHash;
            ViewBag.Type = 2;
            ViewBag.Message = "Zapisano plik ";
            return View();
        }
        #endregion

        //#region Directly Download        
        //public ActionResult Download(string templateName,string format)  //TODO: Download Action
        //{
        //    string UserNick = System.Web.HttpContext.Current.User.Identity.Name;
        //    string fileName = templateName + "-" + UserNick + format;
        //    string path = (Server.MapPath("~/TargetDOCS/") + fileName);
        //    var stream = new FileStream(path, FileMode.Open);
        //    return File(stream, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);          
        //}
        //#endregion
    }
}



