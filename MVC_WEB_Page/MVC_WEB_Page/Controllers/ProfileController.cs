using MVC_WEB_Page.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.IO;

namespace MVC_WEB_Page.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/
        public ActionResult Index()
        {
            var context = new ApplicationDbContext();
            string match = User.Identity.GetUserId();
            EditUserViewmodel applicationUser = new EditUserViewmodel();
            var user = (from a in context.Users where a.Id == match select a).Take(1);
            foreach (var item in user)
            {
                applicationUser.Name = item.Name;
                applicationUser.Surname = item.Surname;
                applicationUser.BirthDate = item.BirthDate;
                applicationUser.Gender = item.Gender;
                applicationUser.ImageoOld = item.Image;

            }



            return View(applicationUser);
        }//<-- index view end
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult changeAcoountInfo(EditUserViewmodel model)
        //public ActionResult Create([Bind(Include = "Id,IdAuthor,IdReceiver,Title,Content,Date")] Messages messages)
        {
            var context = new ApplicationDbContext();
            string match = User.Identity.GetUserId();
            var user = (from a in context.Users where a.Id==match select a).Take(1);
            ModelState.Remove("Image");
            if (ModelState.IsValid)
            {
                foreach (var item in user)
                {
                    item.Name = model.Name;
                    item.Surname = model.Surname;
                    item.BirthDate = model.BirthDate;
                    item.Gender = model.Gender;

                    break;
                }
                //db.Entry(user).State = EntityState.Modified;
                //context.Entry(ApplicationUser).State =Ent`
                
                context.SaveChanges();
                return RedirectToAction("Index", "Profile");
            }
            return RedirectToAction("ChangePassword", "Manage");
        }//<-- index post end
        public ActionResult changeAcoountImage(EditUserViewmodel model)
        //public ActionResult Create([Bind(Include = "Id,IdAuthor,IdReceiver,Title,Content,Date")] Messages messages)
        {
            var context = new ApplicationDbContext();
            string match = User.Identity.GetUserId();
            var user = (from a in context.Users where a.Id == match select a).Take(1);
            if (model.Image != null)
            {

                foreach (var item in user) { 
                var fileName = Path.GetFileName(model.Image.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/Profiles/"),item.Image);


                model.Image.SaveAs(path);
                break;
                }

            }
           
                return RedirectToAction("Index", "Profile");
           
        }//<-- index post end


        public ActionResult ChangePassword()
        {

            return RedirectToAction( "ChangePassword","Manage");
        }//<-- change password
        public ActionResult DeclinedInvitations()
        {


            var context = new ApplicationDbContext();
            var context1 = new ApplicationDbContext();
            string match = User.Identity.GetUserId();

            ProfileDeclinedInvations profileDeclinedInvations = new ProfileDeclinedInvations();
            //--> get logged user info
            ApplicationUser LoggedInUser = new ApplicationUser();
            var userLoged = from a in context.Users where a.Id == match select a;
            foreach (var item in userLoged)
            {
                LoggedInUser = new ApplicationUser { Id = item.Id, Name = item.Name, Surname = item.Surname, Image = item.Image };
                break;
            }
            profileDeclinedInvations.LoggedInUser = LoggedInUser;
            //<-- logged user info end
            //--> get friend invaitions
            var friendQuerry = from a in context.Friends where a.IdFriend == match && a.Accepted == -1 select a;
            foreach (var item in friendQuerry)
            {

                var quer = (from a in context1.Users where a.Id == item.IdUser select a).Take(1);
                foreach (var x in quer)
                    profileDeclinedInvations.Insert(item.Id, item.IdUser, item.date, item.IdFriend, item.Accepted, x.Name + " " + x.Surname, x.Image);
            }

            //<--get friend invations end



            return View(profileDeclinedInvations);
        }//<-- declined invitations end


    
    
    }//<-- class end
}//<-- namespace end