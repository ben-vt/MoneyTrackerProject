﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoneyTrackerProject.Models
{
    public class SendBulkEmailsViewModel
    {
        [Display(Name = "Email address")]
        //[Required(ErrorMessage = "Please enter an email address.")]
        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        public List<String> ToEmails { get; set; }
        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Please enter the email content")]
        public string Contents { get; set; }

        [Required]
        public HttpPostedFileBase PathToFile { get; set; }
    }
}