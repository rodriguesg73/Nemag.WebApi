using System.Collections.Generic;
using System.IO;
using System.Net.Mime;

namespace Nemag.Auxiliar.Email.Smtp
{
    public class SmtpClient
    {
        private string _servidor { get; set; }

        public SmtpClient()
            : this("192.168.2.11")
        { }

        public SmtpClient(string servidor)
        {
            _servidor = servidor;
        }

        public void SendMail(System.Net.Mail.MailMessage mailMessage)
        {
            System.Net.Mail.SmtpClient itemSmtp = new System.Net.Mail.SmtpClient
            {
                Host = _servidor
            };

            itemSmtp.Send(mailMessage);
        }

        public void SendMail(string sender, List<string> to, bool confirmation, string subject, string content, List<string> attachmentsUrl, List<string> toCC)
        {
            SendMail(sender, to, confirmation, subject, content, attachmentsUrl, toCC, null, false);
        }

        public void SendMail(string sender, List<string> to, bool confirmation, string subject, string content, List<string> attachmentsUrl, List<string> toCC, List<string> toBCC, bool imagemVisivel)
        {
            using System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            System.Net.Mail.MailAddress mailAddress = new System.Net.Mail.MailAddress(sender);

            foreach (var email in to)
                mailMessage.To.Add(email);

            if (toCC != null && toCC.Count > 0)
                foreach (var email in toCC)
                    mailMessage.CC.Add(email);

            if (toBCC != null && toBCC.Count > 0)
                foreach (var email in toBCC)
                    mailMessage.Bcc.Add(email);

            mailMessage.From = mailAddress;

            mailMessage.Priority = System.Net.Mail.MailPriority.Normal;

            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

            mailMessage.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnFailure;

            if (confirmation)
                mailMessage.Headers.Add("Disposition-Notification-To", "<" + mailAddress.Address + ">");

            mailMessage.IsBodyHtml = true;

            mailMessage.Subject = subject;

            mailMessage.Body = content;

            if (attachmentsUrl != null && attachmentsUrl.Count > 0)
                foreach (var item in attachmentsUrl)
                {
                    var fileInfo = new FileInfo(item);

                    if (!fileInfo.Exists)
                        continue;

                    if (fileInfo.Length == 0)
                        continue;

                    var attachment = new System.Net.Mail.Attachment(item);

                    if (new List<string> { ".gif", ".png", ".jpg", ".bmp", ".jpeg" }.Contains(Path.GetExtension(item)) && string.IsNullOrEmpty(mailMessage.Body))
                    {
                        attachment.ContentDisposition.Inline = true;

                        attachment.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

                        content += "<img src=\"cid:" + attachment.ContentId + "\"><br />";
                    }

                    mailMessage.Attachments.Add(attachment);
                }

            mailMessage.Body = "<html><body>" + content + "</body></html>";

            SendMail(mailMessage);
        }
    }
}
