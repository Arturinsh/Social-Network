using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_WEB_Page.Models
{
    public class UserView
    {
        public List<UsersGalleries> gallery { get; set; }

        public ApplicationUser user { get; set; }
    }
}