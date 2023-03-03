using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ULDeneme.BLL.Concrete.ResultServiceBLL;
using ULDeneme.DAL.Concrete.Repository;
using ULDeneme.Model.Entities;

namespace ULDeneme.BLL.Concrete.SendMailServiceBLL
{
    public class SendActivationCodeService
    {
        public static bool SendMail(string userName, string userMailAddress, Guid activationCode)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(userMailAddress);
            msg.Subject = "Plak Dükkanı Aktivasyon Maili";
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<!DOCTYPE html>\r\n<html>\r\n    <head>\r\n        <meta charset='utf-8'>\r\n    </head>\r\n<body>\r\n<h1>Aktivasyon maili</h1>\r\n<p>Merhaba {0}</p>\r\n<p>Sistemize kayıt olduğunuz için teşekkür ederiz.</p> </br>\r\n<p>Kayıt işleminizi aktifleştirmek için <a href='http://localhost:port/User/ActivedUser/{1}'> linke</a> tıklayınız.</p>\r\n\r\n</body>\r\n</html>\r\n", userName, activationCode);
            msg.From = new MailAddress("miolingomio@gmail.com");


            SmtpClient smtp = new SmtpClient();
            smtp.Port = 465;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential("miolingomio@gmail.com", "134526Mio..");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                smtp.Send(msg);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
