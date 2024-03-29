﻿using EcommerceShop.Core.Model.sendingEmail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EcommerceShop.EF.Repositories
{
    public class MailSendService : IMailSendService
    {
        private readonly MailSetting _mailSettings;

        public MailSendService(IOptions<MailSetting> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendingEmail(string mailTo, string subject, string body)//, IList<IFormFile> attachments = null)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Email),
                Subject = subject
            };

            email.To.Add(MailboxAddress.Parse(mailTo));

            var builder = new BodyBuilder();

            //if (attachments != null)
            //{
            //    byte[] fileBytes;
            //    foreach (var file in attachments)
            //    {
            //        if (file.Length > 0)
            //        {
            //            using var ms = new MemoryStream();
            //            file.CopyTo(ms);
            //            fileBytes = ms.ToArray();

            //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            //        }
            //    }
            //}

            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}
