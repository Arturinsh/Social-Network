using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_WEB_Page.Models;
using Microsoft.AspNet.Identity;
using System.IO;

namespace MVC_WEB_Page.Controllers
{
    public class GalleryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Gallery/
        [Authorize]
        public ActionResult Index()
        {
            var currentUsersID = User.Identity.GetUserId();
            var gallery = from c in db.Galleries
                          where c.UserId == currentUsersID
                          select c;
            List<ImageView> images = new List<ImageView>();
            List<UsersGalleries> gal = gallery.ToList();
            foreach (var x in gal)
            {
                var cmm = db.Comments.Where(d => d.IdImage == x.Id).ToList();
                images.Add(new ImageView() { image = x, comments = cmm });
            }
            return View(images);
        }

        // GET: /Gallery/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersGalleries usersgallery = db.Galleries.Find(id);
            if (usersgallery == null)
            {
                return HttpNotFound();
            }
            return View(usersgallery);
        }

        // GET: /Gallery/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Gallery/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(UsersGalleries model, HttpPostedFileBase picture)
        {
            var currentUsersID = User.Identity.GetUserId();
            var currentUsersName = User.Identity.Name;
            var image = new UsersGalleries
            {
                UserId = currentUsersID,
                Active = true,
            };
            if (picture != null && image.UserId != null)
            {
                image.Image = currentUsersName + DateTime.Now.Day + "-"
                       + DateTime.Now.Month + "-" + DateTime.Now.Year + picture.FileName;
                if (model.Name != null)
                    image.Name = model.Name;
                else model.Name = "";
                if (picture != null)
                {
                    var fileName = Path.GetFileName(picture.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images/" + currentUsersName + "/"), image.Image);
                    if (Directory.Exists(path))
                    {
                        picture.SaveAs(path);
                    }
                    else
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/Images/" + currentUsersName + "/"));
                        picture.SaveAs(path);
                    }

                }
                db.Galleries.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: /Gallery/Edit/5\ 
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersGalleries usersgallery = db.Galleries.Find(id);
            if (usersgallery == null)
            {
                return HttpNotFound();
            }
            if (usersgallery.UserId == User.Identity.GetUserId())
                return View(usersgallery);
            else return HttpNotFound();
        }

        // POST: /Gallery/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,UserId,Image")] UsersGalleries usersgallery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usersgallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (usersgallery.UserId == User.Identity.GetUserId())
                return View(usersgallery);
            else return HttpNotFound();
        }

        // GET: /Gallery/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersGalleries usersgallery = db.Galleries.Find(id);
            if (usersgallery == null)
            {
                return HttpNotFound();
            }
            if (usersgallery.UserId == User.Identity.GetUserId())
                return View(usersgallery);
            else return HttpNotFound();
        }

        // POST: /Gallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            UsersGalleries usersgallery = db.Galleries.Find(id);
            var path = Path.Combine(Server.MapPath("~/Content/Images/" + User.Identity.Name + "/"), usersgallery.Image);
            db.Galleries.Remove(usersgallery);
            db.SaveChanges();
            try
            {
                System.IO.File.Delete(path);
            }
            catch { }
            return RedirectToAction("Index");
        }
        [Authorize]
        public PartialViewResult deleteImage(int id)
        {
            var context = new ApplicationDbContext();
            UsersGalleries usersgallery = context.Galleries.Find(id);
            if (User.Identity.GetUserId() == usersgallery.UserId)
            {
                var comments = context.Comments.Where(d => d.IdImage == usersgallery.Id);
                var path = Path.Combine(Server.MapPath("~/Content/Images/" + User.Identity.Name + "/"), usersgallery.Image);
                context.Galleries.Remove(usersgallery);
                context.Comments.RemoveRange(comments);
                context.SaveChanges();
                try
                {
                    System.IO.File.Delete(path);
                }
                catch { }
            }
            var currentUsersID = User.Identity.GetUserId();
            var gallery = from c in db.Galleries
                          where c.UserId == currentUsersID
                          select c;
            List<UsersGalleries> imgs = gallery.ToList();
            List<ImageView> images = new List<ImageView>();
            foreach (var x in imgs)
            {
                var cmm = db.Comments.Where(d => d.IdImage == x.Id).ToList();
                images.Add(new ImageView() { image = x, comments = cmm });
            }
            return PartialView("_GalleryView", images);
        }
        [Authorize]
        public PartialViewResult returnImage(int id)
        {
            var context = new ApplicationDbContext();
            UsersGalleries usersgallery = context.Galleries.Find(id);
            List<ImageView> image = new List<ImageView>();
            var cmm = db.Comments.Where(d => d.IdImage == id).ToList();
            image.Add(new ImageView() { image = usersgallery,comments=cmm});
            return PartialView("_ImageView", image);
        }
        [Authorize]
        public PartialViewResult addComment(int id, string commentText)
        {
            var context = new ApplicationDbContext();
            Comments comment = new Comments() 
            { IdImage = id, IdUser = User.Identity.GetUserId(), 
                date = DateTime.Now,Content=commentText };
            var user = context.Users.Find(User.Identity.GetUserId());
            comment.SenderName = user.Name + " " +user.Surname;
            context.Comments.Add(comment);
            context.SaveChanges();
            UsersGalleries usersgallery = context.Galleries.Find(id);
            List<ImageView> image = new List<ImageView>();
            var cmm = db.Comments.Where(d => d.IdImage == id).ToList();
            image.Add(new ImageView() { image = usersgallery, comments = cmm });
            return PartialView("_ImageView", image);
        }
        [HttpPost]
        [Authorize]
        public ActionResult uploadImage(String name, HttpPostedFileBase picture, bool imgActive)
        {
            var currentUsersID = User.Identity.GetUserId();
            var currentUsersName = User.Identity.Name;
            var image = new UsersGalleries
            {
                UserId = currentUsersID,
                Active = imgActive,
            };
            if (picture != null && image.UserId != null)
            {
                image.Image = currentUsersName + DateTime.Now.Day + "-"
                       + DateTime.Now.Month + "-" + DateTime.Now.Year + picture.FileName;
                if (name!=null)
                    image.Name = name;
                else name = "";
                if (picture != null)
                {
                    var fileName = Path.GetFileName(picture.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images/" + currentUsersName + "/"), image.Image);
                    if (Directory.Exists(path))
                    {
                        picture.SaveAs(path);
                    }
                    else
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/Images/" + currentUsersName + "/"));
                        picture.SaveAs(path);
                    }

                }
                db.Galleries.Add(image);
                db.SaveChanges();
            }
            var gallery = from c in db.Galleries
                          where c.UserId == currentUsersID
                          select c;
            List<UsersGalleries> imgs = gallery.ToList();
            List<ImageView> images = new List<ImageView>();
            foreach (var x in imgs)
            {
                var cmm = db.Comments.Where(d => d.IdImage == x.Id).ToList();
                images.Add(new ImageView() { image = x, comments = cmm });
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize]
        public PartialViewResult activeImage(int id)
        {
            var context = new ApplicationDbContext();
            UsersGalleries usersgallery = context.Galleries.Find(id);
            if (usersgallery.Active)
                usersgallery.Active = false;
            else
                usersgallery.Active = true;
            context.Entry(usersgallery).State = EntityState.Modified;
            context.SaveChanges();
            var currentUsersID = User.Identity.GetUserId();
            var gallery = from c in db.Galleries
                          where c.UserId == currentUsersID
                          select c;
            List<UsersGalleries> imgs = gallery.ToList();
            List<ImageView> images = new List<ImageView>();
            foreach (var x in imgs)
            {
                var cmm = db.Comments.Where(d => d.IdImage == x.Id).ToList();
                images.Add(new ImageView() { image = x, comments = cmm });
            }
            return PartialView("_GalleryView", images);
        }
        [HttpPost]
        [Authorize]
        public PartialViewResult editImage(int id,string name)
        {
            var context = new ApplicationDbContext();
            UsersGalleries usersgallery = context.Galleries.Find(id);
            usersgallery.Name = name;
            context.Entry(usersgallery).State = EntityState.Modified;
            context.SaveChanges();
            var currentUsersID = User.Identity.GetUserId();
            var gallery = from c in db.Galleries
                          where c.UserId == currentUsersID
                          select c;
            List<UsersGalleries> imgs = gallery.ToList();
            List<ImageView> images = new List<ImageView>();
            foreach (var x in imgs)
            {
                var cmm = db.Comments.Where(d => d.IdImage == x.Id).ToList();
                images.Add(new ImageView() { image = x, comments = cmm });
            }
            return PartialView("_GalleryView", images);
        }
        public PartialViewResult nextImage(int id,string UserName)
        {
            var context = new ApplicationDbContext();
            string userName=context.Users.Where(d => d.UserName == UserName).ToList().First().Id;
            UsersGalleries nextImg = new UsersGalleries();
            List<UsersGalleries> gal = context.Galleries.Where(d => d.UserId == userName).ToList();
            int ListStatus =0;
            int currentId = gal.FindIndex(d =>d.Id==id);
            if (currentId + 1 < gal.Count)
                nextImg = gal[currentId + 1];
            else
                nextImg = gal[currentId];
            if (currentId + 2 > gal.Count)
                ListStatus = 1;
            List<ImageView> image = new List<ImageView>();
            var cmm = db.Comments.Where(d => d.IdImage == id).ToList();
            image.Add(new ImageView() { image = nextImg, comments = cmm, listStatus=ListStatus });
            return PartialView("_ImageView", image);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}