﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using SBEU.Gramm.DataLayer.DataBase;
using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.Models.Requests;
using SBEU.Gramm.Models.Requests.Update;

namespace SBEU.Gramm.Middleware.SMTP
{
    public static class ConfirmationEmail
    {
        /* Creating a new instance of the SmtpClient class. */
        private static readonly SmtpClient _mail;
        /* A constant string that is used to send emails. */
        private const string NoReplyMail = "no-reply@sbeusilent.space";

        /* A static constructor. It is called once when the class is first loaded. */
        static ConfirmationEmail()
        {
            var mail = new SmtpClient("sbeusilent.space",25);
            mail.Credentials = new NetworkCredential(NoReplyMail, "1mynewHome1_nrp");
            //mail.EnableSsl = true;
            _mail=mail;
        }

        /// <summary>
        /// It sends a confirmation email to the user with a confirmation code
        /// </summary>
        /// <param name="XIdentityUser">The user model</param>
        /// <param name="ApiDbContext">The database context</param>
        /// <param name="UpdateUserCredDto"></param>
        /// <param name="mailCode">a string that is generated by the server and sent to the
        /// client.</param>
        /// <returns>
        /// The confirmation.Id is being returned.
        /// </returns>
        public static async Task<string> SendConfirmationEmail(this XIdentityUser user, ApiDbContext context, UpdateUserCredDto confirm, string mailCode)
        {
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new Exception("Email not found.");
            }

            var toEmail = user.Email;
            var message = new MailMessage(NoReplyMail, toEmail);
            message.Subject = "Confirmation message";
            var confirmation = new XIdentityUserConfirm()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                Email = confirm.Email,
                UserName = confirm.UserName,
                MailCode = mailCode
            };
            await context.UserConfirmations.AddAsync(confirmation);
            var code = confirmation.ConfirmCode;
            message.Body = $"Confirmation code: \n\n {code}";
            await _mail.SendMailAsync(message);
            await context.SaveChangesAsync();
            return confirmation.Id;
        }
    }
}
