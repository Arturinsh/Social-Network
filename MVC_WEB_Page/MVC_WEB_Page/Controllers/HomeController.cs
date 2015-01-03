using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MVC_WEB_Page.Models;


namespace MVC_WEB_Page.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
          if (Request.IsAuthenticated)
              return RedirectToAction("Home", "Home");
          else 
              return RedirectToAction("Login", "Account");
        }//<-- Index end
        [Authorize]
        public ActionResult Home()
        {
            var currentUserId = User.Identity.GetUserId();
           // var db = new DefaultConnection();
            
            var context = new ApplicationDbContext();
            string match = User.Identity.GetUserId();
            var user = from a in context.Users where a.Id ==match select a; 
            //var allUsers = context.Users.ToList();
            List<ApplicationUser> allUsers= new List<ApplicationUser>(); 
             foreach (var item in user){
                 allUsers.Add(new ApplicationUser {Id=item.Id, Name=item.Name,Surname=item.Surname,Image=item.Image });
             }
           
            ViewBag.Message =  "sss";

            return View(user);
        }//<-- about end
        [Authorize]
        public ActionResult ReturnUser(string user)
        {
            var context = new ApplicationDbContext();
            var users = from a in context.Users where a.Id == user select a;
            var gallery = from a in context.Galleries where a.UserId == user select a;

            UserView userview = new UserView();
            userview.gallery = gallery.ToList();
            userview.user = users.ToList().First();
            return View(userview);
        }
        [Authorize]
        public ActionResult AllUsers()
        {            
            List<ApplicationUser> allUsers = new List<ApplicationUser>();            
            var context1 = new ApplicationDbContext();
            string current_user = User.Identity.GetUserId();
            var user = from a in context1.Users where a.Id != current_user select a;
            foreach (var x in user)
                {
                    allUsers.Add(new ApplicationUser { Id = x.Id, Name = x.Name, Surname = x.Surname, Image = x.Image });
                }
                return View(allUsers);
        }//<-- AllUsers

        [Authorize]
        public PartialViewResult SearchUsers(string SearchName = "")
        {
            List<ApplicationUser> searchUsers = new List<ApplicationUser>();
            var context1 = new ApplicationDbContext();
            string fname=null;
            string sname=null;
            try
            {
                string current_user = User.Identity.GetUserId();
                var user = (System.Linq.IQueryable<MVC_WEB_Page.Models.ApplicationUser>)null;

                string[] words = SearchName.Split(' ');
                
                fname = words[0];
                if(words.Count()==2)
                {                    
                    sname = words[1];
                }                

                if (sname != null && fname == null)
                    user = from a in context1.Users where a.Id != current_user && (a.Name.StartsWith(sname) || a.Surname.StartsWith(sname)) select a;
                               
                if (sname != null && fname != null)
                    user = from a in context1.Users where a.Id != current_user && (a.Name.StartsWith(sname) && a.Surname.StartsWith(fname) || a.Name.StartsWith(fname) && a.Surname.StartsWith(sname)) select a;
              
                if (fname != null && sname==null)
                    user = from a in context1.Users where a.Id != current_user && (a.Name.StartsWith(fname) || a.Surname.StartsWith(fname)) select a;
               
                foreach (var x in user)
                {
                    searchUsers.Add(new ApplicationUser { Id = x.Id, Name = x.Name, Surname = x.Surname, Image = x.Image });
                }         
            }
            catch { }

            return PartialView("_DisplayUsers", searchUsers);        
        }//<-- SearchUsers

        [Authorize]    
        public ActionResult AllFriends()
        {
             List<ApplicationUser> allFriends = new List<ApplicationUser>();
             var context1 = new ApplicationDbContext();
             var currentUserId = User.Identity.GetUserId();
             var context = new ApplicationDbContext();
             string match = User.Identity.GetUserId();
             context1 = new ApplicationDbContext();

             var friends = from a in context.Friends where a.IdUser == match select a;

             foreach (var item in friends)
             {
                 var user = from a in context1.Users where a.Id == item.IdFriend select a;
                 foreach (var x in user)
                 {
                     allFriends.Add(new ApplicationUser { Id = x.Id, Name = x.Name, Surname = x.Surname, Image = x.Image });
                 }
             }
             return View(allFriends);
        }//<-- AllFriends

        [Authorize]
        public PartialViewResult SearchFriends(string SearchName = "")
        {
            List<ApplicationUser> searchFriends = new List<ApplicationUser>();
            var context1 = new ApplicationDbContext();
            string fname = null;
            string sname = null;
            try
            {
                var user = (System.Linq.IQueryable<MVC_WEB_Page.Models.ApplicationUser>)null;

                string[] words = SearchName.Split(' ');

                fname = words[0];
                if (words.Count() == 2)
                {
                    sname = words[1];
                }

                var context = new ApplicationDbContext();
                string match = User.Identity.GetUserId();
                var friends = from a in context.Friends where a.IdUser == match select a;
                foreach (var item in friends)
                {
                    if (sname != null && fname == null)
                        user = from a in context1.Users where a.Id == item.IdFriend && (a.Name.StartsWith(sname) || a.Surname.StartsWith(sname)) select a;

                    if (sname != null && fname != null)
                        user = from a in context1.Users where a.Id == item.IdFriend && (a.Name.StartsWith(sname) && a.Surname.StartsWith(fname) || a.Name.StartsWith(fname) && a.Surname.StartsWith(sname)) select a;

                    if (fname != null && sname == null)
                        user = from a in context1.Users where a.Id == item.IdFriend && (a.Name.StartsWith(fname) || a.Surname.StartsWith(fname)) select a;

                    foreach (var x in user)
                    {
                        searchFriends.Add(new ApplicationUser { Id = x.Id, Name = x.Name, Surname = x.Surname, Image = x.Image });
                    }
                }
            }

            catch { }
            return PartialView("_DisplayFriends", searchFriends);
        }//<-- SearchFriends


         [Authorize]
        public ActionResult Interests()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }//<-- interests end
         [Authorize]
        public ActionResult Gallery()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }//<-- GAllery end
         [Authorize]
        public ActionResult Messages()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }//<-- messages end
         [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }//<-- contact end

    }//<-- controller end
}//<-- namespace end