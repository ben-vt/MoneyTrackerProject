using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTrackerProject.Utilities
{
    public class SendEmail
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("benvargheset@gmail.com", "Ben");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }
    }
}