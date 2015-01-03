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
        public ActionResult Index(int? id)
        {

            // var db = new DefaultConnection();

            var context = new ApplicationDbContext();
            string match = User.Identity.GetUserId();

            var messages = from a in context.Messages where a.IdReceiver == match && a.read != -1 orderby a.Date descending select a;
            //--> pagination variables
            int messagesPerPage = 10;
            int messageCount = messages.Count();
            double xs = messageCount / messagesPerPage;
            int pagecount =Convert.ToInt32(Math.Ceiling(xs));

            if ((xs % 1)> -0.0001) pagecount += 1;
            
             
            if (id > pagecount) id = (int)pagecount;
            else if (id < 1 || id == null) id = 1;
            int startRecord = (Convert.ToInt32(id) - 1) * messagesPerPage;
            //<-- pagination variables end 
            List<Messages> msg = new List<Messages>();
            MsgModelOutput msgModelOutput = new MsgModelOutput();
            int i = 0;
            foreach (var item in messages)
            {
                if (i >= startRecord && i < messageCount && i < startRecord + messagesPerPage) { 
                var context1 = new ApplicationDbContext();
                var getAuthor = from x in context1.Users where x.Id == item.IdAuthor select x;
                foreach (var x in getAuthor)
                    msg.Add(new Messages { Id = item.Id, IdAuthor = x.Name + " " + x.Surname, IdReceiver = item.IdReceiver, Title = item.Title, Content = item.Content, Date = item.Date });
                }
                i++;
                if (i == messageCount || i == startRecord + messagesPerPage) break;
            }
            msgModelOutput.messages = msg;

            List<string>urls= new List<string>();
            if (pagecount <= 10)
            {
                for (i = 1; i <= pagecount; i++)
                {

                    if (i == 1) urls.Add(" <li><a href='/Inbox/" + i + "'>&laquo;</a></li>");

                    if (i == id) urls.Add("<li class='active'><a href='/Inbox/" + i + "'>" + i + "</a></li>");
                    else urls.Add("<li><a href='/Inbox/" + i + "'>" + i + "</a></li>");
                    if (i == pagecount) urls.Add(" <li><a href='/Inbox/" + i + "'>&raquo;</a></li>");
                }//<--
            }
            else
            {
                int subCounter = 0;
                if (id > 3) { 
                for (i = (int)id-3; i <= pagecount; i++)
                {

                    if (i == (int)id - 3) urls.Add(" <li><a href='/Inbox/" + i + "'>&laquo;</a></li>");

                    if (i == id) urls.Add("<li class='active'><a href='/Inbox/" + i + "'>" + i + "</a></li>");
                    else urls.Add("<li><a href='/Inbox/" + i + "'>" + i + "</a></li>");
                    if (i == pagecount) urls.Add(" <li><a href='/Inbox/" + i + "'>&raquo;</a></li>");
                    subCounter++;
                    if (subCounter == 10)
                    {
                         urls.Add(" <li><a href='/Inbox/" + i + "'>&raquo;</a></li>");
                         break;
                    }
                }//<--
                }
                else
                {
                    for (i =(int)id ; i <= pagecount; i++)
                    {

                        if (i == 1) urls.Add(" <li><a href='/Inbox/" + i + "'>&laquo;</a></li>");

                        if (i == id) urls.Add("<li class='active'><a href='/Inbox/" + i + "'>" + i + "</a></li>");
                        else urls.Add("<li><a href='/Inbox/" + i + "'>" + i + "</a></li>");
                        if (i == pagecount) urls.Add(" <li><a href='/Inbox/" + i + "'>&raquo;</a></li>");
                        subCounter++;
                        if (subCounter == 10)
                        {
                            urls.Add(" <li><a href='/Inbox/" + i + "'>&raquo;</a></li>");
                            break;
                        }
                    }//<--
                }
            }
            msgModelOutput.paginationUrls = urls;
            return View(msgModelOutput);
        }

        // GET: outbox
        public ActionResult Outbox(int? id)
        {

            var context = new ApplicationDbContext();
            string match = User.Identity.GetUserId();

            var messages = from a in context.Messages where a.IdAuthor == match && a.read == -1 orderby a.Date descending select a;
            //--> pagination variables
            int messagesPerPage = 10;
            int messageCount = messages.Count();
            double xs = messageCount / messagesPerPage;
            int pagecount = Convert.ToInt32(Math.Ceiling(xs));

            if ((xs % 1) > -0.0001) pagecount += 1;


            if (id > pagecount) id = (int)pagecount;
            else if (id < 1 || id == null) id = 1;
            int startRecord = (Convert.ToInt32(id) - 1) * messagesPerPage;
            //<-- pagination variables end 
            List<Messages> msg = new List<Messages>();
            MsgModelOutput msgModelOutput = new MsgModelOutput();
            int i = 0;
            foreach (var item in messages)
            {
                if (i >= startRecord && i < messageCount && i < startRecord + messagesPerPage)
                {
                    var context1 = new ApplicationDbContext();
                    var getAuthor = from x in context1.Users where x.Id == item.IdReceiver select x;
                    foreach (var x in getAuthor)
                        msg.Add(new Messages { Id = item.Id, IdAuthor = item.IdAuthor, IdReceiver = x.Name + " " + x.Surname, Title = item.Title, Content = item.Content, Date = item.Date });
                     
                }
                i++;
                if (i == messageCount || i == startRecord + messagesPerPage) break;
            }
            msgModelOutput.messages = msg;

            List<string> urls = new List<string>();
            if (pagecount <= 10)
            {
                for (i = 1; i <= pagecount; i++)
                {

                    if (i == 1) urls.Add(" <li><a href='/Inbox/" + i + "'>&laquo;</a></li>");

                    if (i == id) urls.Add("<li class='active'><a href='/Inbox/" + i + "'>" + i + "</a></li>");
                    else urls.Add("<li><a href='/Inbox/" + i + "'>" + i + "</a></li>");
                    if (i == pagecount) urls.Add(" <li><a href='/Inbox/" + i + "'>&raquo;</a></li>");
                }//<--
            }
            else
            {
                int subCounter = 0;
                if (id > 3)
                {
                    for (i = (int)id - 3; i <= pagecount; i++)
                    {

                        if (i == (int)id - 3) urls.Add(" <li><a href='/Inbox/" + i + "'>&laquo;</a></li>");

                        if (i == id) urls.Add("<li class='active'><a href='/Inbox/" + i + "'>" + i + "</a></li>");
                        else urls.Add("<li><a href='/Inbox/" + i + "'>" + i + "</a></li>");
                        if (i == pagecount) urls.Add(" <li><a href='/Inbox/" + i + "'>&raquo;</a></li>");
                        subCounter++;
                        if (subCounter == 10)
                        {
                            urls.Add(" <li><a href='/Inbox/" + i + "'>&raquo;</a></li>");
                            break;
                        }
                    }//<--
                }
                else
                {
                    for (i = (int)id; i <= pagecount; i++)
                    {

                        if (i == 1) urls.Add(" <li><a href='/Inbox/" + i + "'>&laquo;</a></li>");

                        if (i == id) urls.Add("<li class='active'><a href='/Inbox/" + i + "'>" + i + "</a></li>");
                        else urls.Add("<li><a href='/Inbox/" + i + "'>" + i + "</a></li>");
                        if (i == pagecount) urls.Add(" <li><a href='/Inbox/" + i + "'>&raquo;</a></li>");
                        subCounter++;
                        if (subCounter == 10)
                        {
                            urls.Add(" <li><a href='/Inbox/" + i + "'>&raquo;</a></li>");
                            break;
                        }
                    }//<--
                }
            }
            msgModelOutput.paginationUrls = urls;
            return View(msgModelOutput);
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messages.Find(id);
            var context1 = new ApplicationDbContext();
          
            if (messages == null)
            {
                return HttpNotFound();
            }
            
            if (messages.IdReceiver == messages.IdAuthor)
            {

                if (messages.IdReceiver != User.Identity.GetUserId())
                {
                    return RedirectToAction("Index");

                }
            }
            if (messages.IdReceiver != messages.IdAuthor)
            {

                if (messages.IdReceiver != User.Identity.GetUserId()&& messages.read!=-1)
                {
                    return RedirectToAction("Index");

                }
            }
           
                
            
                var getAuthor = from x in context1.Users where x.Id == messages.IdAuthor select x;
                foreach (var x in getAuthor)
                    messages.IdAuthor = x.Name + " " + x.Surname;
                getAuthor = from x in context1.Users where x.Id == messages.IdReceiver select x;
                foreach (var x in getAuthor)
                    messages.IdReceiver = x.Name + " " + x.Surname; 


            return View(messages);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            List<string> users = new List<string>();
            var context = new ApplicationDbContext();
            string match = User.Identity.GetUserId();

            var friends = from a in context.Friends where a.IdUser == match select a;

            foreach (var item in friends)
            {
                var context1 = new ApplicationDbContext();
                var getFriendCredits = from x in context1.Users where x.Id == item.IdFriend select x;
                foreach (var x in getFriendCredits) users.Add(x.Name + "-" + x.Surname + " - (" + x.Email + ")");

            }
            friends = from a in context.Friends where a.IdFriend == match select a;

            foreach (var item in friends)
            {
                var context1 = new ApplicationDbContext();
                var getFriendCredits = from x in context1.Users where x.Id == item.IdUser select x;
                foreach (var x in getFriendCredits) users.Add(x.Name + "-" + x.Surname + " - (" + x.Email + ")");

            }
            MsgModelCreate msgModelCreate = new MsgModelCreate();
            msgModelCreate.users = users;

            return View(msgModelCreate);
        }

        public ActionResult MessageIconClick(string id = "")
        {
            List<string> users = new List<string>();
            var context = new ApplicationDbContext();

            var friends = from a in context.Users where a.Id==id select a;
                       
            foreach (var x in friends)
            {
                users.Add(x.Name + "-" + x.Surname + " - (" + x.Email + ")");
                ViewBag.ReceiverMail = x.Email;
                ViewBag.ReceiverName = x.Name;
                ViewBag.ReceiverSurname = x.Surname;
            } 
            
            MsgModelCreate msgModelCreate = new MsgModelCreate();
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
            string name = "";
            string surname = "";
            var message = new MsgModelCreate();
            var messages = new Messages { IdAuthor = User.Identity.GetUserId(), IdReceiver = "", Title = model.messages.Title, Content = model.messages.Content, Date = DateTime.Now };
            //support - (support@nova.com)
            var context = new ApplicationDbContext();
            var ifEmail = from a in context.Users where a.Email == email select a;
            if (ifEmail.Count() == 0)
            {
                name = email.Substring(0, email.IndexOf("-"));
                surname = email.Substring(email.IndexOf("-") + 1, email.IndexOf(" ") - email.IndexOf("-") - 1);
                email = email.Substring(email.IndexOf("(") + 1, email.IndexOf(")") - email.IndexOf("(") - 1);
                ifEmail = from a in context.Users where ((a.Name == name) && (a.Surname == surname) && (a.Email == email)) select a;

                //if (ifEmail.Count() == 0) return RedirectToAction("Home");
            }//<--

            string receiver = "|" + email + "|";
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
                message.messages.read = -1;//create message for outbox
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
            if (messages.IdReceiver != User.Identity.GetUserId() && messages.IdAuthor != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (messages.IdAuthor != messages.IdReceiver)
            {
                if (messages.IdReceiver == User.Identity.GetUserId() && messages.read == -1)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (messages.IdReceiver == User.Identity.GetUserId() && messages.read == -1)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (messages.IdAuthor == User.Identity.GetUserId() && messages.read != -1)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
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
            Messages messages = db.Messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            if (messages.IdReceiver != User.Identity.GetUserId() && messages.IdAuthor != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (messages.IdAuthor != messages.IdReceiver)
            {
                if (messages.IdReceiver == User.Identity.GetUserId() && messages.read == -1)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (messages.IdReceiver == User.Identity.GetUserId() && messages.read == -1)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (messages.IdAuthor == User.Identity.GetUserId() && messages.read != -1)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            db.Messages.Remove(messages);
            db.SaveChanges();
            return RedirectToAction("Outbox");
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

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
