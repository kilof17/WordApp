using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WordApp.DataAccessLayer;
using WordApp.Models;

namespace WordApp.Controllers
{
    public class UsersController : Controller
    {
        #region Consturctor
        private readonly IUsersRepository _usersRepository;

        public UsersController( IUsersRepository usersRepository )
        {
            _usersRepository = usersRepository;
        }
        #endregion

        #region Registration
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }     
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Registration([Bind(Exclude = "EmailVerification,UniqueCode,Role")] User newUser)
        {
            bool IsRegistered = false; 
            string Mess = "";

            if (ModelState.IsValid) 
            {                
                if (CheckNickNameExist(newUser.Nickname))
                {
                    ModelState.AddModelError("CheckNickNameExist", "Ktoś inny używa juz tego nicku, wybierz inny");
                    return View(newUser);
                }
                if (CheckEmailExist(newUser.Email))
                {
                    ModelState.AddModelError("CheckEmailExist", "Ktoś inny używa juz tego adresu e-mail, wybierz inny");
                    return View(newUser);
                }

                newUser.EmailVerification = false;     // account is not activate yet
                newUser.UniqueCode = Guid.NewGuid(); // activating code

                newUser.Password = HashingFunctions.Hash(newUser.Password);
                newUser.ConfirmPassword = HashingFunctions.Hash(newUser.ConfirmPassword);
                newUser.Role = "User";

                try
                {
                    Mess = _usersRepository.AddNewUser(newUser);
                    IsRegistered = true;
                }
                catch { Mess = "Błąd.W razie problemów skontatkuj się z administratorem witryny."; }
            }
            else { Mess = "Błąd!! Nie utworzono konta, spróbuj ponownie"; }
           
            ViewBag.Message = Mess;
            ViewBag.IsRegistered = IsRegistered;
            return View(newUser);
        }
        #endregion

        #region ChangePassword
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePass chPass)//,string id)
        {
            bool isChanged = false;
            var findUserByEmail = _usersRepository.GetUserByEmail(chPass.Email);

            if (findUserByEmail != null)
            {
                SendEmail email = new SendEmail();
                email.ChangePasswordEmail(findUserByEmail.Email, findUserByEmail.UniqueCode.ToString());
                ViewBag.Message = "Wysłano, sprawdź swoja skrytkę pocztową";
                isChanged = true;
            }
            else ViewBag.Message = "Podany email jest nieprawidłowy";

            ViewBag.IsChanged = isChanged;     
            return View();
        }
        #endregion

        #region Set new password
        [HttpGet]
        public ActionResult SetNewPassword()
        {           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetNewPassword( NewPassword newPass , string id )
        {
            bool isChanged = false;
            var findByUniqueCode = _usersRepository.GetUser(id);
            if (findByUniqueCode != null)
            {
                findByUniqueCode.Password = HashingFunctions.Hash(newPass.Password);          
                findByUniqueCode.UniqueCode = Guid.NewGuid();             
                isChanged=_usersRepository.EditUser(findByUniqueCode);
                ViewBag.Message = "Pomyślnie zminiono hasło.";
            }
            else ViewBag.Message = "Niewłaściwy link";
                
            ViewBag.IsChanged = isChanged;
            return View();
        }
        #endregion

        #region Verification
        [HttpGet]
        public ActionResult Verification(string id)
        {
            bool IsRegistered = _usersRepository.Verification(id);
            if(!IsRegistered) ViewBag.Message = "Użyj właściwego linku";
            ViewBag.IsRegistered = IsRegistered;
            return View();
        }
        #endregion

        #region Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            string Mess = "";
            if (ModelState.IsValid)
            {   
                var findUser = _usersRepository.GetUser(login.Nickname);
                if (findUser != null)
                {
                    if (findUser.EmailVerification) // if the account is verified
                    {
                        string tryLoginHashPassword = HashingFunctions.Hash(login.Password);
                        bool rightPassword = string.Equals(findUser.Password, tryLoginHashPassword);
                        if (rightPassword)
                        {                             
                            var authenticationTicket = new FormsAuthenticationTicket(findUser.Nickname, true, 1440);// 1440m= 24h                    
                            string encryptTicket = FormsAuthentication.Encrypt(authenticationTicket);
                            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket)
                            {
                                Expires = DateTime.Now.AddDays(1),
                                HttpOnly = true
                            };
                            Response.Cookies.Add(cookie);
                            return RedirectToAction("Index", "App");
                        }
                        else { Mess = "Dane logowania nieprawidłowe"; }
                                                                   }
                    else { Mess = "Aktywuj sowje konto, link wysłano emailem na podany adres"; }
                }
                else { Mess = "Dane logowania nieprawidłowe"; }
            }            
            else { Mess = "Błąd!!"; }      

            ViewBag.Message = Mess;
            return View();
        }
        #endregion

        #region Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {       
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Users");
        }
        #endregion

        #region Edit
        [HttpGet]
        [Authorize]
        public ActionResult Edit()
        {
            string username=System.Web.HttpContext.Current.User.Identity.Name;
           
            try
            {
                var user = _usersRepository.GetUser(username);          
                user.Password = "xxxxx";
                return View(user);             
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "App");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit([Bind(Include ="Forename,Surname,Email,Password")]User edit)
        {
            string username = System.Web.HttpContext.Current.User.Identity.Name;
            var user = _usersRepository.GetUser(username);
         
            if (HashingFunctions.Hash(edit.Password)==user.Password)
            {
                user.Forename = edit.Forename;
                user.Surname = edit.Surname;
                user.Email = edit.Email;
                _usersRepository.EditUser(user);
                TempData["IndexMessage"] = "Dane konta zostały zminione";                       
            }
            else
            {
                ViewBag.EditMessage= "Nieprawidłowe hasło";
                return View(edit);
            }
            
            return RedirectToAction("Index", "App");
        }

#endregion

        #region CheckNickExist
        [NonAction]
        public bool CheckNickNameExist(string nick)
        {
            var n = _usersRepository.GetUser(nick);           
            return n != null;            
        }
        #endregion

        #region CheckEmailExist
        [NonAction]
        public bool CheckEmailExist(string email)
        {
            var e = _usersRepository.GetUserByEmail(email);
            return e != null;           
        }
#endregion
    }
}