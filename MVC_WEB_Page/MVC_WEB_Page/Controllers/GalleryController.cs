﻿using System;
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
            return View(gallery.ToList());
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
                IdComment = 1,

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
            return View(usersgallery);
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
            return View(usersgallery);
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
            return View(usersgallery);
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