using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_WEB_Page
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //<-- empty url
            routes.MapRoute(
               name: "Empty url",
               url: "",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            //<-- Login
            routes.MapRoute(
               name: "Login",
               url: "Login",
               defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
            //<-- Login
            routes.MapRoute(
               name: "Register",
               url: "Register",
               defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
            );
            //<-- about
            routes.MapRoute(
               name: "Home",
               url: "Home",
               defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );
            //<-- friends
            routes.MapRoute(
               name: "AllFriends",
               url: "AllFriends",
               defaults: new { controller = "Home", action = "AllFriends", id = UrlParameter.Optional }
            );
            //<-- AllUsers
            routes.MapRoute(
               name: "AllUsers",
               url: "AllUsers",
               defaults: new { controller = "Home", action = "AllUsers", id = UrlParameter.Optional }
            );
            //<-- interests
            routes.MapRoute(
               name: "Interests",
               url: "Interests",
               defaults: new { controller = "Home", action = "Interests", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Gallery",
               url: "Gallery",
               defaults: new { controller = "Gallery", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "AddGallery",
               url: "AddGallery",
               defaults: new { controller = "Gallery", action = "Create", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Messages",
               url: "Inbox/{id}",
               defaults: new { controller = "Messages", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
             name: "MsgOutbox",
             url: "Outbox/{id}",
             defaults: new { controller = "Messages", action = "Outbox", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             name: "ReturnUser",
             url: "ReturnUser/{id}",
             defaults: new { controller = "Home", action = "ReturnUser", id = UrlParameter.Optional }
          );
            routes.MapRoute(
               name: "SendMessage",
               url: "SendMessage",
               defaults: new { controller = "Messages", action = "Create", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );




        }//<-- registered routes end
    }//<-- route config end
}//<-- namespace end
