using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WEB_Page.Models
{
    public class Friends
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
        [StringLength(128)]
        public string IdFriend { get; set; }
        [Required]
        public int Accepted { get; set; }
    
    
    }//<-- friends class end



}//<-- friends namespace end