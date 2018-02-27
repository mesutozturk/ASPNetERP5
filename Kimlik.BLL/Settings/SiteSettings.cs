using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Kimlik.Models.ViewModels;

namespace Kimlik.BLL.Settings
{
    public static class SiteSettings
    {
        public static string SiteMail = "kamilfidil@gmail.com";
        public static string SiteMailPassword = "123456kf";
        public static string SiteMailSmtpHost = "smtp.gmail.com";
        public static int SiteMailSmtpPort = 587;
        public static bool SiteMailEnableSsl = true;

        public static async Task SendMail(MailModel model)
        {
            using (var smtp = new SmtpClient())
            {
                var message = new MailMessage();
                message.To.Add(model.To);
                message.From = new MailAddress(SiteMail);
                message.Subject = model.Subject;
                message.SubjectEncoding = Encoding.UTF8;
                message.Priority = MailPriority.High;
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                message.Body = model.Message;
                if (!string.IsNullOrEmpty(model.Cc))
                    message.CC.Add(model.Cc);
                if (!string.IsNullOrEmpty(model.Bcc))
                    message.Bcc.Add(model.Bcc);

                var credential = new NetworkCredential
                {
                    UserName = SiteMail,
                    Password = SiteMailPassword
                };
                smtp.Credentials = credential;
                smtp.Host = SiteMailSmtpHost;
                smtp.Port = SiteMailSmtpPort;
                smtp.EnableSsl = SiteMailEnableSsl;
                await smtp.SendMailAsync(message);
            }
        }

        public static string UrlFormatConverter(string name)
        {
            string sonuc = name.ToLower();
            sonuc = sonuc.Replace("'", "");
            sonuc = sonuc.Replace(" ", "-");
            sonuc = sonuc.Replace("<", "");
            sonuc = sonuc.Replace(">", "");
            sonuc = sonuc.Replace("&", "");
            sonuc = sonuc.Replace("[", "");
            sonuc = sonuc.Replace("!", "");
            sonuc = sonuc.Replace("]", "");
            sonuc = sonuc.Replace("ı", "i");
            sonuc = sonuc.Replace("ö", "o");
            sonuc = sonuc.Replace("ü", "u");
            sonuc = sonuc.Replace("ş", "s");
            sonuc = sonuc.Replace("ç", "c");
            sonuc = sonuc.Replace("ğ", "g");
            sonuc = sonuc.Replace("İ", "I");
            sonuc = sonuc.Replace("Ö", "O");
            sonuc = sonuc.Replace("Ü", "U");
            sonuc = sonuc.Replace("Ş", "S");
            sonuc = sonuc.Replace("Ç", "C");
            sonuc = sonuc.Replace("Ğ", "G");
            sonuc = sonuc.Replace("|", "");
            sonuc = sonuc.Replace(".", "-");
            sonuc = sonuc.Replace("?", "-");
            sonuc = sonuc.Replace(";", "-");

            return sonuc;
        }
    }
}
