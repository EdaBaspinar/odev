using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using odev01.Models;
using odev01.ViewModel;

namespace odev01.Controllers
{
   // [Authorize]
    public class ServisController : ApiController
    {
        odevDB01Entities db = new odevDB01Entities();
        SonucModel sonuc = new SonucModel();

        #region Kategori

        [HttpGet]
        [Route("api/kategoriliste")]

        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {
                KategoriId = x.KategoriId,
                KategoriAdi = x.KategoriAdi,
                KatOdevSay = x.Odev.Count
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/kategoribyid/{katId}")]
        public KategoriModel KategoriById(int katId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.KategoriId == katId).Select(x
           => new KategoriModel()
           {
               KategoriId = x.KategoriId,
               KategoriAdi = x.KategoriAdi,
               KatOdevSay = x.Odev.Count
           }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.KategoriAdi == model.KategoriAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Adı Kayıtlıdır!";
                return sonuc;
            }
            Kategori yeni = new Kategori();
            yeni.KategoriAdi = model.KategoriAdi;
            db.Kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.KategoriId == model.KategoriId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.KategoriAdi = model.KategoriAdi;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/kategorisil/{katId}")]
        public SonucModel KategoriSil(int katId)
        {
            Kategori kayit = db.Kategori.Where(s => s.KategoriId == katId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            if (db.Odev.Count(s => s.KategoriId == katId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Ödev Kayıtlı Kategori Silinemez!";
                return sonuc;
            }
            db.Kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";
            return sonuc;
        }



        #endregion

        #region Odev

        [HttpGet]
        [Route("api/odevliste")]
        public List<OdevModel> OdevListe()
        {
            List<OdevModel> liste = db.Odev.Select(x => new OdevModel()
            {
                OdevId = x.OdevId,
                Baslik = x.Baslik,
                Icerik = x.Icerik,
                Foto = x.Foto,
                KategoriId = x.KategoriId,
                KategoriAdi = x.Kategori.KategoriAdi,
                Goruntulenme = x.Goruntulenme,
                Tarih = x.Tarih,
                UyeId = x.UyeId,
                UyeKadi = x.Uye.KullaniciAdi
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/odevlistesoneklenenler/{s}")]
        public List<OdevModel> OdevListeSonEklenenler(int s)
        {
            List<OdevModel> liste = db.Odev.OrderByDescending(o => o.OdevId).Take(
           s).Select(x => new OdevModel()
           {
               OdevId = x.OdevId,
               Baslik = x.Baslik,
               Icerik = x.Icerik,
               Foto = x.Foto,
               KategoriId = x.KategoriId,
               KategoriAdi = x.Kategori.KategoriAdi,
               Goruntulenme = x.Goruntulenme,
               Tarih = x.Tarih,
               UyeId = x.UyeId,
               UyeKadi = x.Uye.KullaniciAdi
           }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/odevlistebykatid/{katId}")]
        public List<OdevModel> OdevListeByKatId(int katId)
        {
            List<OdevModel> liste = db.Odev.Where(s => s.KategoriId == katId).Select
           (x => new OdevModel()
           {
               OdevId = x.OdevId,
               Baslik = x.Baslik,
               Icerik = x.Icerik,
               Foto = x.Foto,
               KategoriId = x.KategoriId,
               KategoriAdi = x.Kategori.KategoriAdi,
               Goruntulenme = x.Goruntulenme,
               Tarih = x.Tarih,
               UyeId = x.UyeId,
               UyeKadi = x.Uye.KullaniciAdi
           }).ToList();
            return liste;
        }


        [HttpGet]
        [Route("api/odevlistebyuyeid/{uyeId}")]
        public List<OdevModel> OdevListeByUyeId(int uyeId)
        {
            List<OdevModel> liste = db.Odev.Where(s => s.UyeId == uyeId).Select(x =>
           new OdevModel()
           {
               OdevId = x.OdevId,
               Baslik = x.Baslik,
               Icerik = x.Icerik,
               Foto = x.Foto,
               KategoriId = x.KategoriId,
               KategoriAdi = x.Kategori.KategoriAdi,
               Goruntulenme = x.Goruntulenme,
               Tarih = x.Tarih,
               UyeId = x.UyeId,
               UyeKadi = x.Uye.KullaniciAdi
           }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/odevbyid/{odevId}")]
        public OdevModel OdevById(int odevId)
        {
            OdevModel kayit = db.Odev.Where(s => s.OdevId == odevId).Select(x =>
           new OdevModel()
           {
               OdevId = x.OdevId,
               Baslik = x.Baslik,
               Icerik = x.Icerik,
               Foto = x.Foto,
               KategoriId = x.KategoriId,
               KategoriAdi = x.Kategori.KategoriAdi,
               Goruntulenme = x.Goruntulenme,
               Tarih = x.Tarih,
               UyeId = x.UyeId,
               UyeKadi = x.Uye.KullaniciAdi
           }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/odevekle")]
        public SonucModel OdevEkle(OdevModel model)
        {
            if (db.Odev.Count(s => s.Baslik == model.Baslik) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ödev Başlığı Kayıtlıdır!";
                return sonuc;
            }
            Odev yeni = new Odev();
            yeni.Baslik = model.Baslik;
            yeni.Icerik = model.Icerik;
            yeni.Tarih = model.Tarih;
            yeni.Goruntulenme = model.Goruntulenme;
            yeni.KategoriId = model.KategoriId;
            yeni.UyeId = model.UyeId;
            yeni.Foto = model.Foto;
            db.Odev.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ödev Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/odevduzenle")]
        public SonucModel OdevDuzenle(OdevModel model)
        {
            Odev kayit = db.Odev.Where(s => s.OdevId == model.OdevId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.Baslik = model.Baslik;
            kayit.Icerik = model.Icerik;
            kayit.Tarih = model.Tarih;
            kayit.Goruntulenme = model.Goruntulenme;
            kayit.KategoriId = model.KategoriId;
            kayit.UyeId = model.UyeId;
            kayit.Foto = model.Foto;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ödev Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/odevsil/{odevId}")]
        public SonucModel OdevSil(int odevId)
        {
            Odev kayit = db.Odev.Where(s => s.OdevId == odevId).SingleOrDefault(
           );
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            if (db.Yorum.Count(s => s.OdevId == odevId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Yorum Kayıtlı Ödev Silinemez!";
                return sonuc;
            }
            db.Odev.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ödev Silindi";
            return sonuc;
        }

        #endregion

        #region Uye

        [HttpGet]
        [Route("api/uyeliste")]
        public List<UyeModel> UyeListe()
        {
            List<UyeModel> liste = db.Uye.Select(x => new UyeModel()
            {
                UyeId = x.UyeId,
                AdSoyad = x.AdSoyad,
                Email = x.Email,
                KullaniciAdi = x.KullaniciAdi,
                Foto = x.Foto,
                Sifre = x.Sifre,
                UyeAdmin = x.UyeAdmin
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyeModel UyeById(int uyeId)
        {
            UyeModel kayit = db.Uye.Where(s => s.UyeId == uyeId).Select(x => new UyeModel()
        {
                UyeId = x.UyeId,
                AdSoyad = x.AdSoyad,
                Email = x.Email,
                KullaniciAdi = x.KullaniciAdi,
                Foto = x.Foto,
                Sifre = x.Sifre,
                UyeAdmin = x.UyeAdmin
                }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyeModel model)
        {
            if (db.Uye.Count(s => s.KullaniciAdi == model.KullaniciAdi || s.Email == model.Email) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kullanıcı Adı veya E-Posta Adresi Kayıtlıdır!";
                return sonuc;
            }
            Uye yeni = new Uye();
            yeni.AdSoyad = model.AdSoyad;
            yeni.Email = model.Email;
            yeni.KullaniciAdi = model.KullaniciAdi;
            yeni.Foto = model.Foto;
            yeni.Sifre = model.Sifre;
            yeni.UyeAdmin = model.UyeAdmin;
            db.Uye.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(UyeModel model)
        {
            Uye kayit = db.Uye.Where(s => s.UyeId == model.UyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            kayit.AdSoyad = model.AdSoyad;
            kayit.Email = model.Email;
            kayit.KullaniciAdi = model.KullaniciAdi;
            kayit.Foto = model.Foto;
            kayit.Sifre = model.Sifre;
            kayit.UyeAdmin = model.UyeAdmin;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public SonucModel UyeSil(int uyeId)
        {
            Uye kayit = db.Uye.Where(s => s.UyeId == uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            if (db.Odev.Count(s => s.UyeId == uyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Ödev Kaydı Olan Üye Silinemez!";
                return sonuc;
            }
            if (db.Yorum.Count(s => s.UyeId == uyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Yorum Kaydı Olan Üye Silinemez!";
                return sonuc;
            }
            db.Uye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi";
            return sonuc;
        }


        #endregion

        #region Yorum

        [HttpGet]
        [Route("api/yorumliste")]
        public List<YorumModel> YorumListe()
        {
            List<YorumModel> liste = db.Yorum.Select(x => new YorumModel()
            {
                YorumId = x.YorumId,
                YorumIcerik = x.YorumIcerik,
                OdevId = x.OdevId,
                UyeId = x.UyeId,
                Tarih = x.Tarih,
                KullaniciAdi = x.Uye.KullaniciAdi,
                OdevBaslik = x.Odev.Baslik,
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/yorumlistebyuyeid/{uyeId}")]
        public List<YorumModel> YorumListeByUyeId(int uyeId)
        {
            List<YorumModel> liste = db.Yorum.Where(s => s.UyeId == uyeId).Select(x => new YorumModel()
            {
                YorumId = x.YorumId,
                YorumIcerik = x.YorumIcerik,
                OdevId = x.OdevId,
                UyeId = x.UyeId,
                Tarih = x.Tarih,
                KullaniciAdi = x.Uye.KullaniciAdi,
                OdevBaslik = x.Odev.Baslik,
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/yorumlistebyodevid/{odevId}")]
        public List<YorumModel> YorumListeByodevId(int odevId)
        {
            List<YorumModel> liste = db.Yorum.Where(s => s.OdevId == odevId).Select(
           x => new YorumModel()
           {
               YorumId = x.YorumId,
               YorumIcerik = x.YorumIcerik,
               OdevId = x.OdevId,
               UyeId = x.UyeId,
               Tarih = x.Tarih,
               KullaniciAdi = x.Uye.KullaniciAdi,
               OdevBaslik = x.Odev.Baslik,
           }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/yorumlistesoneklenenler/{s}")]
        public List<YorumModel> YorumListeSonEklenenler(int s)
        {
            List<YorumModel> liste = db.Yorum.OrderByDescending(o => o.OdevId).Take(s)
           .Select(x => new YorumModel()
           {
               YorumId = x.YorumId,
               YorumIcerik = x.YorumIcerik,
               OdevId = x.OdevId,
               UyeId = x.UyeId,
               Tarih = x.Tarih,
               KullaniciAdi = x.Uye.KullaniciAdi,
               OdevBaslik = x.Odev.Baslik,
           }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/yorumbyid/{yorumId}")]
        public YorumModel YorumById(int yorumId)
        {
            YorumModel kayit = db.Yorum.Where(s => s.YorumId == yorumId).Select(x => new
           YorumModel()
            {
                YorumId = x.YorumId,
                YorumIcerik = x.YorumIcerik,
                OdevId = x.OdevId,
                UyeId = x.UyeId,
                Tarih = x.Tarih,
                KullaniciAdi = x.Uye.KullaniciAdi,
                OdevBaslik = x.Odev.Baslik,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/yorumekle")]
        public SonucModel YorumEkle(YorumModel model)
        {
            if (db.Yorum.Count(s => s.UyeId == model.UyeId && s.OdevId == model.OdevId && s.YorumIcerik == model.YorumIcerik) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Aynı Kişi, Aynı Ödeve Aynı Yorumu Yapamaz!";
                return sonuc;
            }
            Yorum yeni = new Yorum();
            yeni.YorumId = model.YorumId;
            yeni.YorumIcerik = model.YorumIcerik;
            yeni.OdevId = model.OdevId;
            yeni.UyeId = model.UyeId;
            yeni.Tarih = model.Tarih;
            db.Yorum.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yorum Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/yorumduzenle")]
        public SonucModel YorumDuzenle(YorumModel model)
        {
            Yorum kayit = db.Yorum.Where(s => s.YorumId == model.YorumId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.YorumId = model.YorumId;
            kayit.YorumIcerik = model.YorumIcerik;
            kayit.OdevId = model.OdevId;
            kayit.UyeId = model.UyeId;
            kayit.Tarih = model.Tarih;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yorum Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/yorumsil/{yorumId}")]
        public SonucModel YorumSil(int yorumId)
        {
            Yorum kayit = db.Yorum.Where(s => s.YorumId == yorumId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            db.Yorum.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yorum Silindi";
            return sonuc;
        }
        #endregion
    }
    }
