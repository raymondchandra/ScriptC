using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyek_Informatika.Models;
using Telerik.Web.Mvc;

namespace Proyek_Informatika.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return PartialView();
        }

        public ActionResult Topik()
        {
            return PartialView();
        }

        public ActionResult TopikAdmin()
        {
            return PartialView();
        }

        protected ViewResult bindingTopik(int id)
        {

            List<Topik> temp = new List<Topik>();

            Topik x = new Topik()
            {
                id = 0,
                Nama = "Sistem Informasi Rumah Makan",
                Deskripsi = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed malesuada magna neque, quis accumsan tellus euismod et. Nulla sollicitudin, magna ac lobortis venenatis, orci lacus pulvinar arcu, id auctor sapien lectus ac metus. Nulla porttitor imperdiet augue ut ornare. Praesent a imperdiet nisi, vel suscipit orci. Nullam pulvinar sem dui, porta vehicula augue accumsan sed. Nulla facilisi. Etiam tincidunt, tortor eget sagittis tempus, eros nisi eleifend urna, nec auctor dui dolor in orci. Fusce adipiscing lectus ac imperdiet dapibus. Etiam ipsum arcu, elementum a risus ac, laoreet faucibus dolor",
                Pembimbing = "Verliyantina",
                Keterangan = "tersedia",
            };
            temp.Add(x);
            x = new Topik()
            {
                id = 1,
                Nama = "Sistem Pendukung Keputusan Perusahaan Asuransi",
                Deskripsi = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed malesuada magna neque, quis accumsan tellus euismod et. Nulla sollicitudin, magna ac lobortis venenatis, orci lacus pulvinar arcu, id auctor sapien lectus ac metus. Nulla porttitor imperdiet augue ut ornare. Praesent a imperdiet nisi, vel suscipit orci. Nullam pulvinar sem dui, porta vehicula augue accumsan sed. Nulla facilisi. Etiam tincidunt, tortor eget sagittis tempus, eros nisi eleifend urna, nec auctor dui dolor in orci. Fusce adipiscing lectus ac imperdiet dapibus. Etiam ipsum arcu, elementum a risus ac, laoreet faucibus dolor",
                Pembimbing = "Verliyantina",
                Keterangan = "tersedia",

            };
            temp.Add(x);

            return View(new GridModel<Topik>
            {
                Data = temp
            });
        }

        [GridAction]
        public ActionResult _SelectTopik()
        {
            return bindingTopik(0);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _SaveTopik(int id)
        {

            return bindingTopik(id);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _InsertTopik()
        {

            return bindingTopik(2);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult _DeleteTopik(int id)
        {

            return bindingTopik(id);
        }

    }
}
