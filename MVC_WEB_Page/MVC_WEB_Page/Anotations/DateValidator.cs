using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_WEB_Page.Anotations
{
    public class DateValidator : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dataTime = Convert.ToDateTime(value);
            int yearDif=DateTime.Now.Year-dataTime.Year;
            if ( yearDif<12)
            {

                return new ValidationResult(base.ErrorMessage);

            }
            else if (yearDif > 105)
                return new ValidationResult(base.ErrorMessage);

            return ValidationResult.Success;
        }
    }//<-- class end
}//<-- namespace end