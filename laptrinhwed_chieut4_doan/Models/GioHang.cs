using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace laptrinhwed_chieut4_doan.Models
{
    public class GioHang
    {
        MyDataDataContext data = new MyDataDataContext();
        public int macam { get; set; }

        [Display(Name = "Tên Camera")]
        public string tencam { get; set; }

        [Display(Name = "Hình")]
        public string hinh { get; set; }

        [Display(Name = "Giá Bán")]
        public Double giaban { get; set; }

        [Display(Name = "Số Lượng")]
        public int iSoluong { get; set; }

        [Display(Name = "Thành Tiền")]
        public Double dThanhtien
        {
            get { return iSoluong * giaban; }
        }
        public GioHang(int id)
        {
            macam = id;
            Camera camera = data.Cameras.Single(n => n.macam == macam);
            tencam = camera.tencam;
            hinh = camera.hinh;
            giaban = double.Parse(camera.giaban.ToString());
            iSoluong = 1;
        }
    }
}