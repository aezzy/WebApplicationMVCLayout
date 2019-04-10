using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationMVCLayout.Models;

namespace WebApplicationMVCLayout.Controllers
{
    public class PhotosController : Controller
    {
        // GET: Photos
        private ModelDb db = new ModelDb();

        // GET: Photo
        public ActionResult Index()
        {
            return View(db.Photos.ToList());
        }

        // GET: Photo/Details/5
        [ActionName("Details")]
        public ActionResult Display(int id)
        {
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }

            return View("Display", photo);
        }


        public ActionResult GetImage(int id)
        {
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return null;
            }

            return File(photo.PhotoFile, photo.ImageMimeType);
        }

        // GET: Photo/Create
        public ActionResult Create()
        {
            Photo newPhoto = new Photo();
            newPhoto.CreatedDate = DateTime.Now;

            return View(newPhoto);
        }

        // POST: Photo/Create
        [HttpPost]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            try
            {
                photo.CreatedDate = DateTime.Now;

                if (!ModelState.IsValid)
                {
                    return View(photo);
                }

                if (image != null)
                {
                    photo.ImageMimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];

                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                }

                db.Photos.Add(photo);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [ChildActionOnly]
        public ActionResult _PhotoGallery(int number = 0)
        {
            List<Photo> photos;

            if (number == 0)
            {
                photos = db.Photos.ToList();
            }
            else
            {
                photos = (from p in db.Photos
                          orderby p.CreatedDate
                          descending
                          select p).Take(number).ToList();
            }

            return PartialView(photos);
        }
    }
}