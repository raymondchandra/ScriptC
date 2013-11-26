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
using System.Data;

namespace Proyek_Informatika.Controllers
{
    public class AccountController : Controller
    {
        private SkripsiAutoContainer db = new SkripsiAutoContainer();
        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return PartialView();
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
                    ModelState.AddModelError("", "Username atau password tidak sesuai.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            DateTime d = DateTime.Now;
            string username = (string)Session["username"];
            var akun = db.akuns.Where(akunTemp => akunTemp.username == username).SingleOrDefault();
            if (akun != null)
            {
                akun.last_login = d;
                TryUpdateModel(akun);
                db.Entry(akun).State = EntityState.Modified;
                db.SaveChanges();
            }
                Session["role"] = null;
                Session["username"] = null;
                Session["id-skripsi"] = null;
                FormsAuthentication.SignOut();
            
            return RedirectToAction("Index", "Home");
        }

        private bool ValidateUser(string username, string password)
        {
            password = EncodePassword(password);
            akun a = db.akuns.Where(akunTemp => akunTemp.username == username && akunTemp.aktif == 1).SingleOrDefault();
            if (a != null)
            {

                if (a.password == password)
                {
                    peran p = db.perans.Where(peranTemp => peranTemp.id == a.peran).SingleOrDefault();
                    Session["role"] = p.nama_peran;
                    Session["username"] = a.username;
                    if(p.nama_peran == "mahasiswa"){
                        var idSemester = int.Parse(Session["id-semester"].ToString());
                        var temp = (from table in db.skripsis
                                    //join table2 in db.mahasiswas on table.NPM_mahasiswa equals table2.NPM
                                    where (table.id_semester_pengambilan == idSemester && table.mahasiswa.username == a.username)
                                    select table.id).ToList();
                        if (temp.Count == 1)
                        {
                            Session["id-skripsi"] = temp.SingleOrDefault();
                        }
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        public string EncodePassword(string originalPassword)
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

        #region ubah password
        [HttpPost]
        public string ChangePassword(ChangePasswordModel model)
        {
            if (model.OldPassword == null || model.OldPassword == "")
            {
                return "Field password lama harus diisi!";
            }
            if (model.NewPassword == null || model.NewPassword == "")
            {
                return "Field password baru harus diisi!";
            }
            if (model.ConfirmPassword == null || model.ConfirmPassword == "")
            {
                return "Field konfirmasi password baru harus diisi!";
            }
            string username = (string)Session["username"];
            akun a = db.akuns.Where(akunTemp => akunTemp.username == username).First();
            if (EncodePassword(model.OldPassword) != a.password)
            {
                return "Password lama salah!";
            }
            if (model.NewPassword.Length < 6)
            {
                return "Password baru minimal 6 karakter!";
            }
            if (model.NewPassword != model.ConfirmPassword)
            {
                return "Password baru dan konfirmasi password baru tidak sesuai!";
            }
            a.password = EncodePassword(model.NewPassword);
            if (TryUpdateModel(a))
            {
                db.SaveChanges();
            }
            return "Password berhasil diubah.";
        }
        #endregion
      
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

        public ViewResult bindingAkun(int id)
        {
            List<akun> list_akun = (from a in db.akuns where (a.peran != 1 && a.aktif != 3) select a).ToList();
            List<AlbertContainer.AkunForGrid> list_akun2 = new List<AlbertContainer.AkunForGrid>();

            foreach (akun a in list_akun)
            {
                list_akun2.Add(new AlbertContainer.AkunForGrid
                {
                    username = a.username,
                    password = a.password,
                    aktif = (a.aktif == 1 ? "Aktif" : "Non-aktif"),
                    last_login = a.last_login,
                    peran = db.perans.Where(p => p.id == a.peran).FirstOrDefault().nama_peran
                });
            }

            return View(new GridModel<AlbertContainer.AkunForGrid> { 
                Data = list_akun2
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

        public ActionResult Change_Status(string username)
        {
            akun a = db.akuns.FirstOrDefault(o => o.username == username);
            if(a.aktif == 1) a.aktif = 0;
            else a.aktif = 1;

            if (TryUpdateModel(a))
            {
                db.SaveChanges();
                return Json(new { success = true });
            }
            else return Json(new { success = false });            
        }

        public ActionResult Reset_Password(string username)
        {
            akun a = db.akuns.FirstOrDefault(o => o.username == username);
            a.password = this.EncodePassword(a.username);

            if (TryUpdateModel(a))
            {
                db.SaveChanges();
                return Json(new { success = true });
            }
            else return Json(new { success = false });
        }


    }
}
