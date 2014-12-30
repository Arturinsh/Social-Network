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

        public ActionResult Friends()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }//<-- friends end
        public ActionResult Interests()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }//<-- interests end
        public ActionResult Gallery()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }//<-- GAllery end
        public ActionResult Messages()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }//<-- messages end
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }//<-- contact end

    }//<-- controller end
}//<-- namespace end