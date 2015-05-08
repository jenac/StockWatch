using System;
using System.IO;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net;
using StockWatch.Utility;
using System.Collections.Generic;

namespace StockWatch.Internet
{
	public class EmailServiceSender
	{
		private readonly string _server;
		private readonly int _port;
		private readonly string _user;
		private readonly string _pass;

		private const string _ACCOUNT = "Account";
		private const string _SERVER = "Server";
		private const string _PORT = "Port";
		private const string _USER = "User";
		private const string _PASS = "Pass";
        //load an xml for email account setting:
        //Keep you file in safe place
        /*
        <?xml version="1.0" encoding="utf-8" ?>
        <EmailSetting>
            <Account>
                <Server>smtp.gmail.com</Server>
                <Port>587</Port>
                <User>xxxx@gmail.com</User>
                <Pass>xxxxxx</Pass>
            </Account>
        </EmailSetting>
        */
		public EmailServiceSender(string settingFile)
		{
			XElement elem = XElement.Load(settingFile);
			_server = elem.Element(_ACCOUNT).Element(_SERVER).Value;
			_port = int.Parse(elem.Element(_ACCOUNT).Element(_PORT).Value);
			_user = elem.Element(_ACCOUNT).Element(_USER).Value;
			_pass = elem.Element(_ACCOUNT).Element(_PASS).Value;

		}
		public void SendEmail(string to, List<string> cc, string subject, string body)
		{
			var fromAddress = new MailAddress(_user);
			var toAddress = new MailAddress(to);
			using (var smtp = new SmtpClient{
				Host = _server,
				Port = _port,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(_user, _pass)
			})
			{
				using (var message = new MailMessage(fromAddress, toAddress) {
                    Subject = subject,
					Body = body,
					IsBodyHtml = true,
				})
				{
                    foreach(string address in cc)
                    {
                        message.CC.Add(address);
                    }
					smtp.Send(message);
				}
			}
		}
	}
}

