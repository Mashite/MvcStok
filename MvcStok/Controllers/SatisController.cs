using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MvcStok.Controllers
{
    public class SatisController : Controller
    {
        MvcStokDbEntities db = new MvcStokDbEntities();
        // GET: Satis
        public ActionResult Index(int page = 1)
        {
            var degerler = db.TBLSATISLAR.ToList().ToPagedList(page, 4);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> urun = (from i in db.TBLURUNLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.URUNADI,
                                                 Value = i.URUNID.ToString()
                                             }).ToList();
            ViewBag.urn = urun;
            List<SelectListItem> mstr = (from i in db.TBLMUSTERILER.ToList()
                                         select new SelectListItem
                                         {
                                             Text = i.MUSTERIAD + i.MUSTERISOYAD,
                                             Value = i.MUSTERIID.ToString()
                                         }).ToList();
            ViewBag.mstr = mstr;
            return View();
        }

        public ActionResult YeniSatis(TBLSATISLAR p)
        {
            var urun = db.TBLURUNLER.Where(m => m.URUNID == p.TBLURUNLER.URUNID).FirstOrDefault();
            p.TBLURUNLER = urun;
            var mstr = db.TBLMUSTERILER.Where(m => m.MUSTERIID == p.TBLMUSTERILER.MUSTERIID).FirstOrDefault();
            p.TBLMUSTERILER = mstr;
            db.TBLSATISLAR.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = 1 });
        }
    }
}