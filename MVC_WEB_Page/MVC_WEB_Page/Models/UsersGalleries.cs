using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_WEB_Page.Models
{
    public class UsersGalleries
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public String UserId { get; set; }
        [Required]
        public String Image { get; set; }

        public String Name { get; set; }
        [Required]
        public bool Active { get; set; }
        public int IdComment { get; set; }
    }
}