using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_WEB_Page.Models
{
    public class Comments
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string IdUser { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime date { get; set; }
        [Required]
        public int IdImage { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string SenderName { get; set; } 
    }
}