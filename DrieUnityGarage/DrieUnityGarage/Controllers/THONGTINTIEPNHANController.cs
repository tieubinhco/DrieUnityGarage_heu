﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DrieUnityGarage.Models;

namespace DrieUnityGarage.Controllers
{
    public class THONGTINTIEPNHANController : Controller
    {
        private DrieUnityGarageEntities db = new DrieUnityGarageEntities();


        public TIEPNHAN LayThongTinTiepNhan()
        {
            TIEPNHAN lstTN = Session["TiepNhan"] as TIEPNHAN;
            if (lstTN == null)
            {
                Session["TiepNhan"] = lstTN;
            }
            return lstTN;
        }
        public ActionResult ThemThongTinTiepNhan(String id)
        {
            TIEPNHAN lstTN = LayThongTinTiepNhan();
            TIEPNHAN newTN;
            if (lstTN == null)
            {
                newTN = new TIEPNHAN(id);
            }
            return RedirectToAction("Create");
        }

        //Lấy danh sách khách hàng
        public List<THONGTINKHACHHANG> LayDanhSachKhachHangDB()
        {
            var newlstKH= new List<THONGTINKHACHHANG>();
            var khDB = db.KHACHHANGs.ToList();
            int c = khDB.Count();
            for (int i = 0;i< c; i++)
            {

                newlstKH.Add(new THONGTINKHACHHANG(khDB[i].MaKH));
            }
            Session.Remove("lstKH");
            Session["lstKH"]= newlstKH;
            return newlstKH;
        }

        //Lấy danh sách xe
        public List<THONGTINPHUONGTIEN> LayDanhSachXeDB()
        {
            var newlstKH = new List<THONGTINPHUONGTIEN>();
            var khDB = db.PHUONGTIENs.ToList();
            int c = khDB.Count();
            for (int i = 0; i < c; i++)
            {
                newlstKH.Add(new THONGTINPHUONGTIEN(khDB[i].BienSoXe));
            }
            Session.Remove("lstXe");
            Session["lstXe"] = newlstKH;
            return newlstKH;
        }



        // GET: THONGTINTIEPNHAN
        public ActionResult Index()
        {
            return View(db.THONGTINTIEPNHANs.ToList());
        }

        // GET: THONGTINTIEPNHAN/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THONGTINTIEPNHAN tHONGTINTIEPNHAN = db.THONGTINTIEPNHANs.Find(id);
            if (tHONGTINTIEPNHAN == null)
            {
                return HttpNotFound();
            }
            return View(tHONGTINTIEPNHAN);
        }

        // GET: THONGTINTIEPNHAN/Create
        public ActionResult Create()
        {
            String date = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.ThoiGianTiepNhan = date;
            String idTN = TaoMaTiepNhan();
            ViewBag.MaTN = idTN;

            List<THONGTINKHACHHANG> lstKhachHang = LayDanhSachKhachHangDB();
            ViewBag.lstMaKH = new SelectList(lstKhachHang, "MaKH", "ThongTin");
            if (Session["DaLayThongTinXe"] != null)
            {
                List<THONGTINPHUONGTIEN> a = Session["lstXe"] as List<THONGTINPHUONGTIEN>;
                ViewBag.lstXe = new SelectList(a, "BienSoXe", "BienSoXe");
                ViewBag.selectedKhachHang = Session["selectedKhachHang"];
                ViewBag.ThoiGianGiaoXe = Session["ThoiGianGiaoXe"];
            }
            else if (Session["KhongCoThongTinXe"] != null) {
                ViewBag.ThongBao = "Khách hàng chưa được tạo thông tin phương tiện!";
            }
            return View();
        }

        //Hàm được sử dụng sau khi nhấn nút lấy danh sách xe
        public ActionResult TaoTTTN_LayThongTinXe([Bind(Include = "MaTN,TN_MaKH,TN_BienSoXe,TN_MaNV,ThoiGianTiepNhan,ThoiGianGiaoDuKien,GhiChuKH")] THONGTINTIEPNHAN tHONGTINTIEPNHAN, String lstMaKH/*, DateTime thoiGianGiaoXe*/)
        {
            //Lấy danh sách xe từ database sau đó lọc ra những xe của khách hàng
            var xe = LayDanhSachXeDB();
            List<THONGTINPHUONGTIEN> lstXe = new List<THONGTINPHUONGTIEN>();
            int c = xe.Count();
            for (int i = 0; i < c; i++)
            {
                if (xe[i].TTPT_MaKH.Equals(lstMaKH))
                    lstXe.Add(new THONGTINPHUONGTIEN(xe[i].BienSoXe));
            }

            //Lấy ra thông tin của khách hàng có mã khách hàng là từ dropdown
            THONGTINKHACHHANG TTKH = new THONGTINKHACHHANG(lstMaKH);

            //Tạo 1 String chứa các thông tin của khách hàng để hiển thị
            String selectedKH = TTKH.MaKH + " - " + TTKH.DienThoaiKH + " - " + TTKH.HoTenKH;


            //String date = thoiGianGiaoXe.ToString("dd/MMM/yyyy");

            

            Session["selectedKhachHang"] = selectedKH;

            //Thông tin cần lưu của tiếp nhận
            //Session["ThoiGianGiaoXe"] = date;
            Session["MaTN"] = TaoMaTiepNhan();
            Session["MaKH"] = lstMaKH;
            
            Session.Remove("lstXe");
            Session["lstXe"] = lstXe;

            //Check đã lấy thông tin xe hay chưa, có null không
            if (lstXe.Count()==0)
                Session["KhongCoThongTinXe"] = 3;
            else 
                Session["DaLayThongTinXe"] = 3;


            return RedirectToAction("Create");
        }

        //Lưu thông tin tiếp nhận vào database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(String GhiChuKH, String lstXe)
        {
            THONGTINTIEPNHAN tHONGTINTIEPNHAN = new THONGTINTIEPNHAN();
            if (ModelState.IsValid)
            {
                String id = TaoMaTiepNhan();
                tHONGTINTIEPNHAN.MaTN = id;
                /* tHONGTINTIEPNHAN.TN_MaNV = Session["MaTaiKhoanNV"].ToString();*/
                tHONGTINTIEPNHAN.TN_MaNV = "NV001";
                tHONGTINTIEPNHAN.TN_MaKH = Session["MaKH"].ToString();
                tHONGTINTIEPNHAN.TN_BienSoXe = lstXe;
                tHONGTINTIEPNHAN.ThoiGianTiepNhan = DateTime.Now;
                tHONGTINTIEPNHAN.ThoiGianGiaoDuKien = null;
                tHONGTINTIEPNHAN.GhiChuKH=GhiChuKH;
                tHONGTINTIEPNHAN.TrangThai = "Chưa hoàn thành";
                db.THONGTINTIEPNHANs.Add(tHONGTINTIEPNHAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tHONGTINTIEPNHAN);
        }
        // GET: THONGTINTIEPNHAN/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THONGTINTIEPNHAN tHONGTINTIEPNHAN = db.THONGTINTIEPNHANs.Find(id);
            if (tHONGTINTIEPNHAN == null)
            {
                return HttpNotFound();
            }
            return View(tHONGTINTIEPNHAN);
        }

        // POST: THONGTINTIEPNHAN/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTN,TN_MaKH,TN_BienSoXe,TN_MaNV,ThoiGianTiepNhan,ThoiGianGiaoDuKien,GhiChuKH")] THONGTINTIEPNHAN tHONGTINTIEPNHAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHONGTINTIEPNHAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tHONGTINTIEPNHAN);
        }

        // GET: THONGTINTIEPNHAN/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THONGTINTIEPNHAN tHONGTINTIEPNHAN = db.THONGTINTIEPNHANs.Find(id);
            if (tHONGTINTIEPNHAN == null)
            {
                return HttpNotFound();
            }
            return View(tHONGTINTIEPNHAN);
        }

        // POST: THONGTINTIEPNHAN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            THONGTINTIEPNHAN tHONGTINTIEPNHAN = db.THONGTINTIEPNHANs.Find(id);
            db.THONGTINTIEPNHANs.Remove(tHONGTINTIEPNHAN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private String TaoMaTiepNhan()
        {
            String idHD = "";
            //Tạo mã nhà cung cấp String
            List<THONGTINTIEPNHAN> lstHD = db.THONGTINTIEPNHANs.ToList();
            int countLst = lstHD.Count();
            if (countLst == 0)
            {
                idHD = "TN001";
            }
            else
            {
                THONGTINTIEPNHAN lastHD = lstHD[countLst - 1];
                String lastMaHD = lastHD.MaTN;
                int lastMaHDNum = int.Parse(lastMaHD.Substring(2));
                int newMaHD = lastMaHDNum + 1;
                if (newMaHD < 10)
                {
                    idHD = "TN00" + newMaHD.ToString();
                }
                else { idHD = "TN0" + newMaHD.ToString(); }
            }
            return (idHD);
        }

    }
}
