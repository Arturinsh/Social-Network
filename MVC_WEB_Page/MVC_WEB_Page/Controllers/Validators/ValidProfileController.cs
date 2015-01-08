using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_WEB_Page.Controllers.Validators
{
    [Authorize]
    public class ValidProfileController : Controller
    {
        //
        // GET: /ValidProfile/
        public ActionResult Index()
        {
            return View();
        }//<-- index end

        public JsonResult ChangeuserInfo(FormCollection form)
        {
            return Json(0);

            
        }//<-- change user info end

	}//<-- controller end
}//<-- namespace ned