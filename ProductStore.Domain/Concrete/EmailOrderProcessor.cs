using ProductStore.Domain.Abstract;
using ProductStore.Domain.Entities;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ProductStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "andrew_gri@mail.ru";
        public string MailFromAddress = "mosq47@gmail.com";
        public bool UseSsl = true;
        public string Username = "mosq47@gmail.com";
        public string Password = "andrew18ruls";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"D:\product_store_emails";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Новый заказ обработан")
                    .AppendLine("---")
                    .AppendLine("Товары:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Guitar.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2})",
                        line.Quantity, line.Guitar.Name, subtotal);
                }

                body.AppendFormat("Общая стоимость: {0}", cart.ComputeTotalValue())
                    .AppendLine("")
                    .AppendLine("---")
                    .AppendLine("Доставка:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Address)
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.Phone.ToString())
                    .AppendLine("---");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	// От кого
                                       emailSettings.MailToAddress,		// Кому
                                       "Новый заказ",		            // Тема
                                       body.ToString()); 				// Тело письма

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}
