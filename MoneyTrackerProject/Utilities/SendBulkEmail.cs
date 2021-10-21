using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MoneyTrackerProject.Utilities
{
    public class SendBulkEmail
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(List<String> toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("benvargheset@gmail.com", "MoneyTracker Mail Service");
            var to = new List<EmailAddress>();
            foreach (string email in toEmail)
            {
                to.Add(new EmailAddress(email));
            }
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }
    }
}