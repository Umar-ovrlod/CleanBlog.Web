using CleanBlog.Core.Controllers;
using CleanBlog.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web;
using Umbraco.Core.Logging;
using Umbraco.Web.Mvc;
using System.Web;
using System.Web.Mvc;





namespace CleanBlog.Core.Services
{
    public class SmtpService:ISmtpService
    {
        private readonly ILogger _logger;
        public SmtpService(ILogger logger)
        {
            _logger = logger;
        }
        public bool SendEmail(ContactViewModel model)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient client = new SmtpClient();

                string toAddress = model.Email;
                //string toAddress = WebConfigurationManager.AppSettings["EmailToAddress"];
                string fromAddress = "muddassirpasta@gmail.com";
                message.Subject = $"Enquiry from: {model.Name}";
                message.Body = model.Message;
                message.To.Add(new MailAddress(toAddress, toAddress));
                message.From = new MailAddress(fromAddress, fromAddress);
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(typeof(ContactSurfaceController), ex, "Error sending contact form");
                return false;
            }
        }
    }
}
