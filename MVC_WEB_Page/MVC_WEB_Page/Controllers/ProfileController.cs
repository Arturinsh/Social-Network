using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_WEB_Page.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/
        public ActionResult Index()
        {
            return View();
        }//<-- index view end

        public ActionResult ChangePassword()
        {

            return RedirectToAction( "ChangePassword","Manage");
        }//<-- change password
    
    
    
    
    }//<-- class end
}//<-- namespace end