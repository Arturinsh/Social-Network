using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MVC_WEB_Page.Models;


namespace MVC_WEB_Page.Controllers
{
    [Authorize]
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
            var context = new ApplicationDbContext();
            var context1 = new ApplicationDbContext();
            string match = User.Identity.GetUserId();
            
            HomeDefaultPageOutput homeDefaultPageOutput = new HomeDefaultPageOutput();
            //--> get logged user info
            ApplicationUser LoggedInUser = new ApplicationUser();
            var userLoged = from a in context.Users where a.Id == match select a; 
            foreach (var item in userLoged)
            {
                 LoggedInUser = new ApplicationUser { Id = item.Id, Name = item.Name, Surname = item.Surname, Image = item.Image };
                 break;
             }
             homeDefaultPageOutput.LoggedInUser = LoggedInUser;
            //<-- logged user info end
            //--> get friend invaitions
             var friendQuerry= from a in context.Friends  where a.IdFriend==match && a.Accepted==0 select a;
             foreach (var item in friendQuerry)
             {
                 
                 var quer = (from a in context1.Users where a.Id == item.IdUser select a).Take(1);
                 foreach(var x in quer)
                 homeDefaultPageOutput.Insert(item.Id, item.IdUser, item.date, item.IdFriend, item.Accepted, x.Name+" "+x.Surname,x.Image);
             }

            //<--get friend invations end
            //--> get announcments
             var anounceQuerry = (from a in context.Announcments orderby a.Date descending where a.IdReceiver == match select a).Take(10);
             List<Announcments> announcments = new List<Announcments>();
             foreach (var item in anounceQuerry)
             {
                 announcments.Add(new Announcments {Content= item.Content, IdReceiver=item.IdReceiver, Date= item.Date, read= item.read, Id= item.Id, state=item.state});

             }//<--
             homeDefaultPageOutput.annoucments = announcments;
            //<-- get announcments end
            //--> get 4 friends
             List<ApplicationUser> myFriends = new List<ApplicationUser>();
             var currentUserId = User.Identity.GetUserId();
             var friends = (from a in context.Friends orderby a.date descending where (a.IdUser == match || a.IdFriend == match) && a.Accepted == 1 select a).Take(4);
             foreach (var item in friends)
             {
                 var user = from a in context1.Users
                            where (a.Id == item.IdFriend || a.Id == item.IdUser) && a.Id != match
                            select a;
                 foreach (var x in user)
                 {
                     myFriends.Add(new ApplicationUser { Id = x.Id, Name = x.Name, Surname = x.Surname, Image = x.Image });
                 }
             }
             homeDefaultPageOutput.friendsMyStart = myFriends;
            //<-- get 4 friends end
             return View(homeDefaultPageOutput);
        }//<-- about end
        [Authorize]
        public ActionResult ReturnUser(string user)
        {
            UserView userview = new UserView();
            try
            {
                var context = new ApplicationDbContext();
                var users = from a in context.Users where a.Id == user select a;
                var gallery = from a in context.Galleries where a.UserId == user && a.Active select a;
                userview.friends = false;

                userview.gallery = gallery.ToList();
                userview.user = users.ToList().First();
            }
            catch
            {
                return HttpNotFound();
            }
            return View(userview);
        }
        public ActionResult sendInvite(string ID)
        {
           

            return View();
        }//<-- send invite end
        [Authorize]
        public ActionResult AllUsers(int? id)
        {
            int idStart = Convert.ToInt32(id);
            //--> base variables start
            UsersOutput usersOutput = new UsersOutput();
            List<ApplicationUser> allUsers = new List<ApplicationUser>();
            var context = new ApplicationDbContext();
            string current_user = User.Identity.GetUserId();
            //var user = from a in context.Users where a.Id != current_user select a;
            var user = from a in context.Friends where (a.IdUser == current_user && a.IdFriend != current_user && a.Accepted==1) || (a.IdUser != current_user && a.IdFriend == current_user && a.Accepted==1) select a; 
            //<-- base variables end
            //--> pagination variables
            int userPerPage = 4;
            int userCount = user.Count();
            double xs = userCount / userPerPage;
            int pagecount = Convert.ToInt32(Math.Ceiling(xs));
            if ((xs % 1) > -0.00001) pagecount += 1;
            if (id > pagecount) id = (int)pagecount;
            else if (id < 1 || id == null) id = 1;
            int startRecord = (Convert.ToInt32(id) - 1) * userPerPage;
            usersOutput.pages = pagecount;
            //<-- pagination variables end 
            //--> select required users start
            int i = 0;
            foreach (var x in user)
            {
                if (i >= startRecord && i < userCount && i < startRecord + userPerPage)
                {
                    var context1 = new ApplicationDbContext();
                    var assignUser = (from a in context1.Users  select a).Take(0);
                    if(x.IdUser!= current_user)
                    assignUser = from a in context1.Users where a.Id == x.IdUser select a;
                    if (x.IdFriend != current_user)
                    assignUser = from a in context1.Users where a.Id == x.IdFriend select a;
                    foreach(var item in assignUser)
                        allUsers.Add(new ApplicationUser { Id = item.Id, Name = item.Name, Surname = item.Surname, Image = item.Image });
                }
                i++;
                if (i == userCount || i == startRecord + userPerPage) break;
            }
            //<-- slect required users end
          
            usersOutput.users = allUsers;
            
                return View(usersOutput);
           
        }//<-- AllUsers
        [Authorize]
        public PartialViewResult AllUsersPart(int? id)
        {
            int idStart = Convert.ToInt32(id);
            //--> base variables start
            UsersOutput usersOutput = new UsersOutput();
            List<ApplicationUser> allUsers = new List<ApplicationUser>();
            var context = new ApplicationDbContext();
            string current_user = User.Identity.GetUserId();
            var user = from a in context.Friends where (a.IdUser == current_user && a.IdFriend != current_user && a.Accepted == 1) || (a.IdUser != current_user && a.IdFriend == current_user && a.Accepted == 1) select a; 
            //<-- base variables end
            //--> pagination variables
            int userPerPage = 4;
            int userCount = user.Count();
            double xs = userCount / userPerPage;
            int pagecount = Convert.ToInt32(Math.Ceiling(xs));
            if ((xs % 1) > -0.00001) pagecount += 1;
            if (id > pagecount) id = (int)pagecount;
            else if (id < 1 || id == null) id = 1;
            int startRecord = (Convert.ToInt32(id) - 1) * userPerPage;
            usersOutput.pages = pagecount;
            //<-- pagination variables end 
            //--> select required users start
            int i = 0;
            foreach (var x in user)
            {
                if (i >= startRecord && i < userCount && i < startRecord + userPerPage)
                {
                    var context1 = new ApplicationDbContext();
                    var assignUser = (from a in context1.Users select a).Take(0);
                    if (x.IdUser != current_user)
                        assignUser = from a in context1.Users where a.Id == x.IdUser select a;
                    if (x.IdFriend != current_user)
                        assignUser = from a in context1.Users where a.Id == x.IdFriend select a;
                    foreach (var item in assignUser)
                        allUsers.Add(new ApplicationUser { Id = item.Id, Name = item.Name, Surname = item.Surname, Image = item.Image });
                }
                i++;
                if (i == userCount || i == startRecord + userPerPage) break;
            }
            //<-- slect required users end

            usersOutput.users = allUsers;

            return PartialView("_DisplayUsers", usersOutput);    

        }//<-- AllUsers partial
        [Authorize]
       
        public PartialViewResult SearchUsers(int? id, string SearchName = "")
        {
            UsersOutput usersOutput = new UsersOutput();
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
                   // user = from a in context1.Users where a.Id != current_user && (a.Name.StartsWith(sname) || a.Surname.StartsWith(sname)) select a;
                    user = from a in context1.Users where a.Id != current_user && (a.Name.Contains(fname) || a.Name.Contains(sname)) select a;            
                if (sname != null && fname != null)
                    user = from a in context1.Users where a.Id != current_user && (a.Name.StartsWith(sname) && a.Surname.StartsWith(fname) || a.Name.StartsWith(fname) && a.Surname.StartsWith(sname)) select a;
              
                if (fname != null && sname==null)
                    //user = from a in context1.Users where a.Id != current_user && (a.Name.StartsWith(fname) || a.Surname.StartsWith(fname)) select a;
                    user = from a in context1.Users where a.Id != current_user && (a.Name.Contains(fname) || a.Name.Contains(fname)) select a;  

                int idStart = Convert.ToInt32(id);
                //--> base variables start
                var context = new ApplicationDbContext();
                //<-- base variables end
                //--> pagination variables
                int userPerPage = 1;
                int userCount = user.Count();
                double xs = userCount / userPerPage;
                int pagecount = Convert.ToInt32(Math.Ceiling(xs));
                if ((xs % 1) > -0.0001 && userPerPage!=1) pagecount += 1;
                if (id > pagecount) id = (int)pagecount;
                else if (id < 1 || id == null) id = 1;
                int startRecord = (Convert.ToInt32(id) - 1) * userPerPage;
                usersOutput.pages = pagecount;
                //<-- pagination variables end 
                //--> select required users start
                int i = 0;
                foreach (var x in user)
                {
                    if (i >= startRecord && i < userCount && i < startRecord + userPerPage)
                    {
                        searchUsers.Add(new ApplicationUser { Id = x.Id, Name = x.Name , Surname = x.Surname, Image = x.Image });
                    }
                    i++;
                    if (i == userCount || i == startRecord + userPerPage) break;
                }
                //<-- slect required users end
       
            }
            catch { }
            //<-- assign found users to user output model
            usersOutput.users = searchUsers;
            //--> user output model assignment end





            return PartialView("_DisplayUsers", usersOutput);        
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

             var friends = from a in context.Friends where (a.IdUser == match || a.IdFriend == match)&& a.Accepted==1 select a;

             foreach (var item in friends)
             {
                 var user = from a in context1.Users
                            where (a.Id == item.IdFriend || a.Id == item.IdUser) && a.Id != match select a;
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
                var friends = from a in context.Friends where (a.IdUser == match || a.IdFriend == match) && a.Accepted == 1 select a;
                foreach (var item in friends)
                {
                    if (sname != null && fname == null)
                        user = from a in context1.Users where ((a.Id == item.IdFriend || a.Id == item.IdUser) && a.Id != match) && (a.Name.StartsWith(sname) || a.Surname.StartsWith(sname)) select a;

                    if (sname != null && fname != null)
                        user = from a in context1.Users where ((a.Id == item.IdFriend || a.Id == item.IdUser) && a.Id != match) && (a.Name.StartsWith(sname) && a.Surname.StartsWith(fname) || a.Name.StartsWith(fname) && a.Surname.StartsWith(sname)) select a;

                    if (fname != null && sname == null)
                        user = from a in context1.Users where ((a.Id == item.IdFriend || a.Id == item.IdUser) && a.Id != match) && (a.Name.StartsWith(fname) || a.Surname.StartsWith(fname)) select a;

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