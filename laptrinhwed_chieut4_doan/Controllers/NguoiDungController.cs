using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace laptrinhwed_chieut4_doan.Controllers
{
    public class NguoiDungController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: NguoiDung
        [HttpGet]
        public bool CheckUser   (string UserName, string Email)
        {
            var check = data.KhachHangs.Any(u => u.tendangnhap == UserName || u.email == Email);
            if (check == true)
            {
                return true;
            }
            return false;
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var hoten = collection["hoten"];
            var tendangnhap = collection["tendangnhap"];
            var matkhau = collection["matkhau"];
            var matkhauxacnhan = collection["matkhauxacnhan"];
            var email = collection["email"];
            var diachi = collection["diachi"];
            var dienthoai = collection["dienthoai"];
            var ngaysinh = string.Format("{0:MM/dd/yyyy}", collection["ngaysinh"]);
            if (CheckUser(tendangnhap, email) == true)
            {
                ViewData["Usertontai"] = "Tên đăng nhập đã tồn tại";
                ViewData["Emailtontai"] = "Email đã tồn tại";
                return this.DangKy();
            }
            if (string.IsNullOrEmpty(matkhauxacnhan))
            {
                ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận !!!";
            }
            else
            {
                if (!matkhau.Equals(matkhauxacnhan))
                {
                    ViewData["matkhaugiongnhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau !!!";
                }
                else
                {
                    kh.hoten = hoten;
                    kh.tendangnhap = tendangnhap;
                    kh.matkhau = matkhau;
                    kh.email = email;
                    kh.diachi = diachi;
                    kh.dienthoai = dienthoai;
                    kh.ngaysinh = DateTime.Parse(ngaysinh);

                    data.KhachHangs.InsertOnSubmit(kh);
                    data.SubmitChanges();

                    return RedirectToAction("DangNhap");
                }
            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendangnhap = collection["tendangnhap"];
            var matkhau = collection["matkhau"];
            KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.tendangnhap == tendangnhap && n.matkhau == matkhau);
            if (kh != null)
            {
                ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                Session["Taikhoan"] = kh;
            }
            else
            {
                ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}