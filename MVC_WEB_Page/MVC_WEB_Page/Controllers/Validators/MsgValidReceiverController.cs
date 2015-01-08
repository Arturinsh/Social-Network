using MVC_WEB_Page.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_WEB_Page.Controllers.Validators
{
    [Authorize]
    public class MsgValidReceiverController : Controller
    {
        //
        // GET: /MsgValidReceiver/
        public ActionResult Index()
        {
            return View();
        }//<-- Index end
        [ValidateAntiForgeryToken]
        public JsonResult CheckName(FormCollection form)
        {
            string idReceiver = form["username"];

            string email = idReceiver;
            string name = "";
            string surname = "";
             try
             {
                 var context = new ApplicationDbContext();
                 var ifEmail = from a in context.Users where a.Email == email select a;
                 if (ifEmail.Count() == 0)
                 {
                     name = email.Substring(0, email.IndexOf("-"));
                     surname = email.Substring(email.IndexOf("-") + 1, email.IndexOf(" ") - email.IndexOf("-") - 1);
                     email = email.Substring(email.IndexOf("(") + 1, email.IndexOf(")") - email.IndexOf("(") - 1);
                     ifEmail = from a in context.Users where ((a.Name == name) && (a.Surname == surname) && (a.Email == email)) select a;

                    if (ifEmail.Count() == 0) return Json(1);
                 }//<--

             }
             catch { return Json(1); }
            
            return Json(0);

        }//<-- check name end
    
    }//<-- controller end




}//<-- namespace end