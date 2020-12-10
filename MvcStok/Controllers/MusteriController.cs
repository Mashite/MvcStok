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
    public class MusteriController : Controller
    {
        MvcStokDbEntities db = new MvcStokDbEntities();

        // GET: Musteri
        public ActionResult Index(string p, int page=1)
        {
            var degerler = db.TBLMUSTERILER.ToList().ToPagedList(page, 4);
            if (!String.IsNullOrEmpty(p))
            {
                degerler = db.TBLMUSTERILER.Where(m=>m.MUSTERIAD.Contains(p)).ToList().ToPagedList(page, 4);
                return View(degerler);
            }            
            return View(degerler);
        }


        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TBLMUSTERILER p)
        {
            if(!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TBLMUSTERILER.Add(p);
            db.SaveChanges();
            return View();
        }

        public ActionResult Sil(int id)
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            return View("MusteriGetir", musteri);            
        }

        public ActionResult Guncelle(TBLMUSTERILER p)
        {
            var musteri = db.TBLMUSTERILER.Find(p.MUSTERIID);
            musteri.MUSTERIAD = p.MUSTERIAD;
            musteri.MUSTERISOYAD = p.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}