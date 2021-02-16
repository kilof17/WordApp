using System.IO;
using System.Web.Mvc;
using WordApp.DataAccessLayer;
using WordApp.Models;

namespace WordApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        // Unity.Mvc5 neded. Added by NuGet package
        private IUsersRepository _usersRepositories;
        public AdminController(IUsersRepository repo)
        {
            _usersRepositories = repo;
        }      
      

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            ViewBag.Message = TempData["mydata"] as string;
            var allUsers = _usersRepositories.AllUsers();

            return View(allUsers); 
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            string userNick = _usersRepositories.GetUser(id).Nickname;
            bool status = _usersRepositories.DeleteUser(id);            

            if (status)
            {
                foreach (var path in Directory.GetFiles(Request.MapPath("~/TargetDOCS/")))
                {
                    string filename = Path.GetFileNameWithoutExtension(path);
                    if (filename[0] != 126 && filename.Contains(userNick))
                    {
                        System.IO.File.Delete(path);
                    }
                }
                TempData["mydata"] = "Usunięto użytkownika " + userNick + " wraz z jego danymi";
            }
            else
            {
                TempData["mydata"] = "Błąd!!";
            }

            return RedirectToAction("GetAllUsers");
        }
        [HttpGet]
        public ActionResult Edit( int? id )
        {
            string[] roles = { "Admin" , "User" };
            ViewBag.Roles = roles;          
            var editUser = _usersRepositories.GetUser( id );
            if ( editUser != null )
            {               
                return View( editUser );
            }
            return RedirectToAction( "GetAllUsers" );
        }
        [HttpPost]
        public ActionResult Edit(User editUser)
        {
            var userData = _usersRepositories.GetUser(editUser.Nickname);
            userData.Email = editUser.Email;
            userData.Forename = editUser.Forename;
            userData.Surname = editUser.Surname;
            userData.Role = editUser.Role;

            bool status = _usersRepositories.EditUser(userData);

            if (status) { TempData["mydata"] = "Zmieniono dane użytkownika " + userData.Nickname; }
            else { TempData["mydata"] = "Błąd!!"; }
          
            return RedirectToAction("GetAllUsers");
        }

       
    }
}


