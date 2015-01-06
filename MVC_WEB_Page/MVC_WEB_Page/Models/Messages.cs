using MVC_WEB_Page.Anotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_WEB_Page.Models
{
    public class Messages
    {

        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string IdAuthor { get; set; }        
        [Required]
        [StringLength(128, MinimumLength = 5, ErrorMessage="Invalid input")]
        public string IdReceiver { get; set; }
        [StringLength(128)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }     
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public int read { get; set; }



        
    }//<-- messages class end




}//<-- namespace end