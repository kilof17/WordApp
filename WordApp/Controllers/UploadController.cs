using System.IO;
using System.Web;
using System.Web.Mvc;

namespace WordApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UploadController : Controller
    {
        [HttpGet]        
        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase UploadedFile)
        {
            try
            {
                if (UploadedFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(UploadedFile.FileName);
                    string extension = System.IO.Path.GetExtension(fileName);

                    if ( (extension == ".docx") || (extension == ".dotx"))
                    {
                        string path = Path.Combine(Server.MapPath("~/TemplateFiles"), fileName);
                        UploadedFile.SaveAs(path);

                        ViewBag.Message = "Plik zapisany na serwerze";
                        ViewBag.MessageType = "alert alert-success";
                    }
                    else
                    {
                        ViewBag.MessageType = "alert alert-warning";
                        ViewBag.Message = "Zły format danych, wybierz: .docx lub .dotx !!";
                    }
                }
            }            
            catch
            {
                ViewBag.Message = "Wybierz plik";
                ViewBag.MessageType = "alert alert-info";
            }
            return View();
        }
    }
}