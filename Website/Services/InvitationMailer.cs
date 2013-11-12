using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Website.Localization;
using Website.Models;

namespace Website.Services
{
    internal class InvitationMailer
    {
        private static readonly string SmtpHost = ConfigurationManager.AppSettings["SmtpHost"];
        private static readonly int SmtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
        private static readonly NetworkCredential Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SmtpUserName"], ConfigurationManager.AppSettings["SmtpPassword"]);

        public async Task SendInvitationAsync(Invitation invitation)
        {
            CultureHelper.SetCurrentCulture(invitation.LanguageCode);

            await SendMailAsync(invitation, Language.InvitationEmailSubject, Language.InvitationEmailBody);
        }

        public async Task NotifyLaurentAsync(Invitation invitation)
        {
            CultureHelper.SetCurrentCulture(invitation.LanguageCode);

            await SendMailAsync(invitation, Language.NotificationSubject, Language.NotificationBody, "Wedding Website");
        }

        public async Task SendConfirmationAsync(Invitation invitation)
        {
            CultureHelper.SetCurrentCulture(invitation.LanguageCode);

            var subject = "RE: " + Language.InvitationEmailSubject;
            var body = invitation.IsAttending == true ? Language.EmailConfirmedYes : Language.EmailConfirmedNo;

            await SendMailAsync(invitation, subject, body);
        }

        private async Task SendMailAsync(Invitation invitation, string subject, string body, string from = null)
        {
            using (var message = BuildMessage(invitation, subject, body, from))
            {
                using (var client = new SmtpClient(SmtpHost, SmtpPort) { Credentials = Credentials })
                {
                    await client.SendMailAsync(message);
                }
            }
        }

        private MailMessage BuildMessage(Invitation invitation, string subject, string body, string from)
        {
            var message = new MailMessage
            {
                From = new MailAddress(Credentials.UserName, from ?? GetFromName(invitation.GuestOf)),
                Subject = subject.Format(invitation),
                Body = body.Format(invitation),
            };
            message.To.Add(invitation.Email);

            return message;
        }

        private string GetFromName(string guestOf)
        {
            if (guestOf.Equals("Laurent", StringComparison.OrdinalIgnoreCase))
            {
                return "Laurent le Beau-Martin";
            }

            return "Sheetal Pathak";
        }
    }
}