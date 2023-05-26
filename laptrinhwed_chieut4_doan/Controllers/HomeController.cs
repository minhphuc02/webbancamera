using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace laptrinhwed_chieut4_doan.Controllers
{
    public class HomeController : Controller
    {
        MyDataDataContext dt = new MyDataDataContext();

        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var all_camera = (from s in dt.Cameras select s).Where(n => n.soluongton != 0).OrderBy(m => m.macam);
            int pageSize = 3;
            int pageNum = page ?? 1;
            return View(all_camera.ToPagedList(pageNum, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}