using ClientSecretnTokenGenerator.AES256Encryption;
using ClientSecretnTokenGenerator.Models;
using ClientSecretnTokenGenerator.Repository;
using System.Web.Mvc;

namespace ClientSecretnTokenGenerator.Controllers
{
    public class LoginController : Controller
    {
        IRegisterUser _IRegisterUser;
        public LoginController()
        {
            _IRegisterUser = new RegisterUserConcrete();
        }

        public ActionResult Login()
        {
            return View(new RegisterUser());
        }

        [HttpPost]
        public ActionResult Login(RegisterUser RegisterUser)
        {
            try
            {

                if (string.IsNullOrEmpty(RegisterUser.Username) && (string.IsNullOrEmpty(RegisterUser.Password)))
                {
                    ModelState.AddModelError("", "Enter Username and Password");
                }
                else if (string.IsNullOrEmpty(RegisterUser.Username))
                {
                    ModelState.AddModelError("", "Enter Username");
                }
                else if (string.IsNullOrEmpty(RegisterUser.Password))
                {
                    ModelState.AddModelError("", "Enter Password");
                }
                else
                {

                    RegisterUser.Password = EncryptionLibrary.EncryptText(RegisterUser.Password);

                    if (_IRegisterUser.ValidateRegisteredUser(RegisterUser))
                    {
                        var UserID = _IRegisterUser.GetLoggedUserID(RegisterUser);
                        Session["UserID"] = UserID;
                        return RedirectToAction("Create", "RegisterCompany");
                    }
                    else if(!_IRegisterUser.ValidateRegisteredUser(RegisterUser))
                    {
                        ModelState.AddModelError("", "Wrong Username or password.");
                        return View("Login", RegisterUser);
                    }
                    else
                    {
                        ModelState.AddModelError("", "User is Already Registered");
                        return View("Create", RegisterUser);
                    }
                }

                return View("Login", RegisterUser);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}
