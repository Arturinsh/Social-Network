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
        public ActionResult SearchUsers(string SearchName="")
        {            
            List<ApplicationUser> allUsers = new List<ApplicationUser>();            
            var context1 = new ApplicationDbContext();

            if(SearchName=="")
            {
                var user = from a in context1.Users select a;
                foreach (var x in user)
                {
                    allUsers.Add(new ApplicationUser { Id = x.Id, Name = x.Name, Surname = x.Surname, Image = x.Image });
                }

                return View(allUsers);
            }
            else
            {
                var user = from a in context1.Users where a.Name.StartsWith(SearchName) || a.Surname.StartsWith(SearchName) select a;

                foreach (var x in user)
                {
                    allUsers.Add(new ApplicationUser { Id = x.Id, Name = x.Name, Surname = x.Surname, Image = x.Image });
                }

                return View(allUsers);  
            }
            
        }//<-- AllUsers


         [Authorize]
        public ActionResult Friends()
        {
            var currentUserId = User.Identity.GetUserId();
            // var db = new DefaultConnection();

            var context = new ApplicationDbContext();
            string match = User.Identity.GetUserId();
            var friends = from a in context.Friends where a.IdUser == match select a;
            //var allUsers = context.Users.ToList();
            List<ApplicationUser> allUsers = new List<ApplicationUser>();
            foreach (var item in friends)
            {
                var context1 = new ApplicationDbContext();
                var user = from a in context1.Users where a.Id == item.IdFriend select a;
                foreach (var x in user)
                {
                    allUsers.Add(new ApplicationUser { Id = x.Id, Name = x.Name, Surname = x.Surname, Image = x.Image });

                }
            }

            return View(allUsers);
        }//<-- friends end
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