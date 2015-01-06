using MVC_WEB_Page.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WEB_Page.Anotations
{
    public class MsgValidReceiver : ValidationAttribute
    {

   

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string email = (string)value;
                string name = "";
                string surname = "";

                //support - (support@nova.com)
                try
                {
                    var context = new ApplicationDbContext();
                    var ifEmail = from a in context.Users where a.Email == email select a;
                    if (ifEmail.Count() == 0)
                    {
                        name = email.Substring(0, email.IndexOf("-"));
                        surname = email.Substring(email.IndexOf("-") + 1, email.IndexOf(" ") - email.IndexOf("-") - 1);
                        email = email.Substring(email.IndexOf("(") + 1, email.IndexOf(")") - email.IndexOf("(") - 1);
                        ifEmail = from a in context.Users where ((a.Name == name) && (a.Surname == surname) && (a.Email == email)) select a;

                        //if (ifEmail.Count() == 0) return RedirectToAction("Home");
                    }//<--
                }
                catch { return false; }
            }
            return true;
        }
    }
}