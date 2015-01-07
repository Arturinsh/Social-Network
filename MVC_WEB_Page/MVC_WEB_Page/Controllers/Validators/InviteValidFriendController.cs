using MVC_WEB_Page.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace MVC_WEB_Page.Controllers.Validators
{
    [Authorize]
    public class InviteValidFriendController : Controller
    {
        //
        // GET: /InviteValidFriend/
        public ActionResult Index()
        {
            return View();
        }//<-- index end

        public JsonResult inviteFriend(FormCollection form)
        {
            string _idFriend = form["ID"];
            //0- valid
            //1- invalid
            var context = new ApplicationDbContext();
            //check if invited user exists
            string match = User.Identity.GetUserId();
            var querry = from a in context.Users where a.Id==_idFriend && a.Id!= match select a;
            if (querry.Count() == 0) return Json(1);
            //check if already invted

            var querry2 = from a in context.Friends where a.IdFriend == match && a.IdUser == _idFriend select a;
            if (querry2.Count() == 1) return Json(1);
            //reverse look up if other invite
            querry2 = from a in context.Friends where a.IdFriend == _idFriend && a.IdUser == match select a;
            if (querry2.Count() == 1)return Json(1);
            //string email = idReceiver;

            var friendInvite = new Friends { IdUser=match,date=DateTime.Now,IdFriend=_idFriend,Accepted=0 };
            context.Friends.Add(friendInvite);
            context.SaveChanges();
            return Json(0);

        }//<-- inviteFriend end 
        public JsonResult acceptFriend(FormCollection form)
        {
            //0- valid
            //1- invalid
            var context = new ApplicationDbContext();
            int _id =Convert.ToInt32(form["ID"]);
            string match = User.Identity.GetUserId();
            var querry = from a in context.Friends where a.Id == _id && a.IdFriend==match select a;
            //if (querry.Count() == 1) return Json(0);
            //else return Json(1);

            foreach (var item in querry)
            {
                item.Accepted = 1;
                break;
               
            }

            // Submit the changes to the database. 
            try
            {

                context.SaveChanges();
                return Json(0);
            }
            catch (Exception e)
            {
                return Json(1);
                
                // Provide for exceptions.
            }
            

            
        }//<-- accept friend end
        public JsonResult denieFriend(FormCollection form)
        {
            //0- valid
            //1- invalid
            var context = new ApplicationDbContext();
            int _id = Convert.ToInt32(form["ID"]);
            string match = User.Identity.GetUserId();
            var querry = from a in context.Friends where a.Id == _id && a.IdFriend == match select a;
            //if (querry.Count() == 1) return Json(0);
            //else return Json(1);

            foreach (var item in querry)
            {
                item.Accepted = -1;
                break;

            }

            // Submit the changes to the database. 
            try
            {

                context.SaveChanges();
                return Json(0);
            }
            catch (Exception e)
            {
                return Json(1);

                // Provide for exceptions.
            }



        }//<-- denie friend end
	}//<--class end
}//<-- namespace end