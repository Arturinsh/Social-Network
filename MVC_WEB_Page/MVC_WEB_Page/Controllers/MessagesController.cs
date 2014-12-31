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

namespace MVC_WEB_Page.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Messages
        public ActionResult Index()
        {
           
            // var db = new DefaultConnection();

            var context = new ApplicationDbContext();
            string match = User.Identity.GetUserId();

            var messages = from a in context.Messages where a.IdReceiver == match select a;
            List<Messages> msg = new List<Messages>();
            foreach (var item in messages)
            {
                var context1 = new ApplicationDbContext();
                var getAuthor = from x in context1.Users where x.Id == item.IdAuthor select x;
                foreach(var x in getAuthor)
                msg.Add(new Messages {Id=item.Id,IdAuthor=x.Name+" "+x.Surname,IdReceiver=item.IdReceiver, Title=item.Title, Content=item.Content, Date=item.Date });
                //allUsers.Add(new ApplicationUser { Id = item.Id, Name = item.Name, Surname = item.Surname, Image = item.Image });
            }
            return View(msg);
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            List<string>users = new List<string>();
            var context = new ApplicationDbContext();
            string match = User.Identity.GetUserId();

            var friends= from a in context.Friends where a.IdUser == match select a;
          
            foreach (var item in friends)
            {
                var context1 = new ApplicationDbContext();
                var getFriendCredits = from x in context1.Users where x.Id == item.IdFriend select x;
                foreach (var x in getFriendCredits) users.Add(x.Name + "-" + x.Surname+" - (" +x.Email+")" );
                   
            }
            MsgModelCreate msgModelCreate= new MsgModelCreate();
            msgModelCreate.users = users;
            
            return View(msgModelCreate);
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MsgModelCreate model)
        //public ActionResult Create([Bind(Include = "Id,IdAuthor,IdReceiver,Title,Content,Date")] Messages messages)
        {
            string email = model.messages.IdReceiver;
            string name="";
            string surname="";
            var message = new MsgModelCreate();
            var messages = new Messages { IdAuthor = User.Identity.GetUserId(), IdReceiver = "", Title = model.messages.Title, Content = model.messages.Content, Date = DateTime.Now };
            //support - (support@nova.com)
             var context = new ApplicationDbContext();
             var ifEmail= from a in context.Users where a.Email==email select a;
             if (ifEmail.Count() == 0) {
                 name = email.Substring(0,email.IndexOf("-"));
                 surname = email.Substring(email.IndexOf("-") + 1, email.IndexOf(" ") - email.IndexOf("-") -1);
                 email = email.Substring(email.IndexOf("(") + 1, email.IndexOf(")") - email.IndexOf("(")-1);
                 ifEmail = from a in context.Users where ((a.Name==name) && (a.Surname==surname) && (a.Email == email)) select a;

                 //if (ifEmail.Count() == 0) return RedirectToAction("Home");
             }//<--

            string receiver="|"+email+"|";
            foreach (var x in ifEmail) receiver = x.Id;

            messages.IdReceiver = receiver;
            message.messages = messages;
            ModelState.Remove("messages.IdAuthor");
            ModelState.Remove("messages.Date");
            ModelState.Remove("messages.read");
            ModelState.Remove("users");
             

            if (ModelState.IsValid)
            {
                db.Messages.Add(message.messages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(message);
        }




        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdAuthor,IdReceiver,Title,Content,Date")] Messages messages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(messages);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Messages messages = db.Messages.Find(id);
            db.Messages.Remove(messages);
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






    }
}
