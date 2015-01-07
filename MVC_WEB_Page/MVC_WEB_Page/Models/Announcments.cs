using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WEB_Page.Models
{
    public class Announcments
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string IdReceiver { get; set; }
        [Required] 
        [StringLength(64)]
        public string Content { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public int read { get; set; }
        public int state { get; set; }
        /*
         * state =0 regular , state 1 good, state 2 bad
         * */
    }//<-- class end
}//<-- anmespace end