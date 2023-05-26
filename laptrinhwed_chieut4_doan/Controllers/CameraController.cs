using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace laptrinhwed_chieut4_doan.Controllers
{
    public class CameraController : Controller
    {
        // GET: Camera
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult ListCamera()
        {
            var all_camera = from ss in data.Cameras select ss;
            return View(all_camera);

        }
        public ActionResult Detail(int id)
        {
            var Detail = data.Cameras.Where(m => m.macam == id).First();
            return View(Detail);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Camera ts)
        {
            var ten = collection["tencam"];
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                ts.tencam = ten;
                data.Cameras.InsertOnSubmit(ts);
                data.SubmitChanges();
                return RedirectToAction("ListCamera");
            }
            return this.Create();
        }
        public ActionResult Edit(int id)
        {
            var E_category = data.Cameras.First(m => m.macam == id);
            return View(E_category);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var camera = data.Cameras.First(m => m.macam == id);
            var E_tencam = collection["tencam"];
            camera.macam = id;
            if (string.IsNullOrEmpty(E_tencam))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                camera.tencam = E_tencam;
                UpdateModel(camera);
                data.SubmitChanges();
                return RedirectToAction("ListCamera");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var D_cam = data.Cameras.First(m => m.macam == id);
            return View(D_cam);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_cam = data.Cameras.Where(m => m.macam == id).First();
            data.Cameras.DeleteOnSubmit(D_cam);
            data.SubmitChanges();
            return RedirectToAction("ListCamera ");
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
    }
}