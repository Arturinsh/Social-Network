﻿using MVC_WEB_Page.Anotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MVC_WEB_Page.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
             
        /*Modifications */
        //Add user birthdate
        [Required]
        [DataType(DataType.DateTime)]
        [DateValidator(ErrorMessage = "Wrong birth date")]
        public DateTime BirthDate { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "The name must be at least 3 characters long.", MinimumLength = 3)]
        public String Name { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "The surname must be at least 3 characters long.", MinimumLength = 3)]
        public String Surname { get; set; }
     
        [Required]
        [GenderValidator(ErrorMessage = "Wrong gender")]
        public int Gender { get; set; }
        [Required]
        public HttpPostedFileBase Image { get; set; }
        //<-- modifications end

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class EditUserViewmodel
    {
        [Required]
        [DataType(DataType.DateTime)]
        [DateValidator(ErrorMessage = "Wrong birth date")]
        public DateTime BirthDate { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "The name must be at least 3 characters long.", MinimumLength = 3)]
        public String Name { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "The surname must be at least 3 characters long.", MinimumLength = 3)]
        public String Surname { get; set; }
        [GenderValidator(ErrorMessage = "Wrong gender")]
        public int Gender { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public String ImageoOld { get; set; }
    }
}//<-- namespace end
