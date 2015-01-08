using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WEB_Page.Anotations
{
    public class GenderValidator:ValidationAttribute{

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int x = Convert.ToInt32(value);
            if (x>1 || x<0)
            {
                
                return new ValidationResult(base.ErrorMessage);

            }
            return ValidationResult.Success;
        }
    }//<-- class end

}//<-- namespace end