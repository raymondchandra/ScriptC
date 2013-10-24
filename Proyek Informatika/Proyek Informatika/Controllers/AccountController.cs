using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Proyek_Informatika.Controllers
{
    public class AccountController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();
        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //if (Membership.ValidateUser(model.UserName, model.Password))
                if (ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            Session["role"] = null;
            Session["username"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private bool ValidateUser(string username, string password)
        {
            password = EncodePassword(password);
            akun a = db.akuns.Where(akunTemp => akunTemp.username == username).SingleOrDefault();
            if (a != null)
            {
                string pass = EncodePassword(a.password);
                if (pass == password)
                {
                    peran p = db.perans.Where(peranTemp => peranTemp.id == a.peran).SingleOrDefault();
                    Session["role"] = p.nama_peran;
                    Session["username"] = a.username;
                    return true;
                }
                return false;
            }
            return false;
        }

        private string EncodePassword(string originalPassword)
        {
            //Declarations
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
        }



        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return PartialView();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return PartialView(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return PartialView();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

        public ActionResult ListAkun()
        {
            return PartialView();
        }



        public ActionResult DaftarAkun()
        {
            return PartialView();
        }

        protected ViewResult bindingAkun(int id)
        {
            List<Akun> listAkun = new List<Akun>();

            Akun x = new Akun()
            {
                username = "KoLiong19",
                aktif = "Aktif",
                last_login = "12/Sep/2013 17:00",
                peran = "Pembimbing"
            };
            listAkun.Add(x);
            x = new Akun()
            {
                username = "KoLiong20",
                aktif = "Aktif",
                last_login = "15/Oct/2013 13:45",
                peran = "Koordinator"
            };
            listAkun.Add(x);
            x = new Akun()
            {
                username = "RayWuBestStudent",
                aktif = "Aktif",
                last_login = "20/Sep/2013 14:30",
                peran = "Mahasiswa"
            };
            listAkun.Add(x);
            x = new Akun()
            {
                username = "Hanzz",
                aktif = "-",
                last_login = "01/Jan/2013 13:55",
                peran = "Mahasiswa"
            };
            listAkun.Add(x);
            x = new Akun()
            {
                username = "albertusalvin",
                aktif = "Aktif",
                last_login = "01/Oct/2013 16:30",
                peran = "Mahasiswa"
            };
            listAkun.Add(x);

            return View(new GridModel<Akun>
            {
                Data = listAkun
            });
        }
        [GridAction]
        public ActionResult _SelectAkun()
        {
            return bindingAkun(0);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveAkun(int id)
        {

            return bindingAkun(id);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertAkun()
        {
            return bindingAkun(2);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteAkun(int id)
        {

            return bindingAkun(id);
        }


    }
}
