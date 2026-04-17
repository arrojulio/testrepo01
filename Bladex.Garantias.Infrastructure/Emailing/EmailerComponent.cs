using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Bladex.Garantias.Infrastructure.Logging;
using log4net;

namespace Bladex.Garantias.Infrastructure.Emailing
{
    /// <summary>
    /// The emailer component class.
    /// </summary>
    public sealed class EmailerComponent
    {
        private static volatile EmailerComponent instance;
        private static Object syncRoot = new Object();

        private EmailerComponent()
        {

        }

        public static EmailerComponent Instance
        {
            get
            {
                // Use 'Lazy initialization'
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (null == instance)
                            instance = new EmailerComponent();
                    }

                }
                return instance;
            }
        }

        public bool SendEmail(string EmailAddressTo, string EmailSubject, string EmailBody, string EmailReplyTo, string Cc, string Bcc, string[] Attachments)
        {

            bool _toRet = false;
            SmtpClient objClient = null;
            try
            {
                objClient = GetSmtpClient();
            }
            catch (ArgumentNullException anEx)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, string.Format("Error configurando cliente de correo. Parametro: {0} vacio.", anEx.ParamName), new Dictionary<string, object> { { "EmailAddressTo", EmailAddressTo }, { "EmailSubject", EmailSubject}, { "EmailBody", EmailBody}, { "EmailReplyTo", EmailReplyTo}, { "Cc", Cc}, {"Bcc", Bcc }},anEx);
            }
            catch (ArgumentException aEx)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Warning, string.Format("Error configurando cliente de correo. Parametro: {0}", aEx.ParamName), new Dictionary<string, object> { { "EmailAddressTo", EmailAddressTo }, { "EmailSubject", EmailSubject}, { "EmailBody", EmailBody}, { "EmailReplyTo", EmailReplyTo}, { "Cc", Cc}, {"Bcc", Bcc }},aEx);
            }
            catch (Exception ex)
            {
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, "Error inicializando cliente de correo.", new Dictionary<string, object> { { "EmailAddressTo", EmailAddressTo }, { "EmailSubject", EmailSubject}, { "EmailBody", EmailBody}, { "EmailReplyTo", EmailReplyTo}, { "Cc", Cc}, {"Bcc", Bcc }}, ex);
            }

            if (objClient == null || string.IsNullOrEmpty(objClient.Host))
            {
                return _toRet;
            }
            using (objClient)
            {
                MailAddress objFrom;
                string objEmailFromDefault = Constants.InfrastructureConstants._EMAIL_SENDER_ADDRESS;


                string[] objArrEmailTo = string.IsNullOrEmpty(EmailAddressTo) ? new string[0] : EmailAddressTo.Split(';');
                string[] objArrEmailCC = string.IsNullOrEmpty(Cc) ? new string[0] : Cc.Split(';');
                string[] objArrEmailBCC = string.IsNullOrEmpty(Bcc) ? new string[0] : Bcc.Split(';');
                MailMessage objMessage = new MailMessage();

                if (!string.IsNullOrEmpty(objEmailFromDefault))
                {
                    objFrom = new MailAddress(objEmailFromDefault);
                    objMessage.Sender = new MailAddress(objEmailFromDefault);
                    objMessage.From = objFrom;
                }
                if (!string.IsNullOrEmpty(EmailReplyTo))
                {
                    objMessage.ReplyTo = new MailAddress(EmailReplyTo);
                }

                PrepareAttachments(Attachments, objMessage);

                foreach (string s in objArrEmailTo)
                {
                    if (!string.IsNullOrEmpty(s))
                        objMessage.To.Add(new MailAddress(s));
                }

                foreach (string s in objArrEmailCC)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        objMessage.CC.Add(new MailAddress(s));
                    }
                }

                foreach (string s in objArrEmailBCC)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        objMessage.Bcc.Add(new MailAddress(s));
                    }
                }

                objMessage.Subject = EmailSubject;
                if (!string.IsNullOrEmpty(EmailReplyTo))
                    objMessage.ReplyToList.Add(new MailAddress(EmailReplyTo));
                objMessage.IsBodyHtml = true;
                objMessage.Body = EmailBody;

                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Information, "Trying to send an email.", new Dictionary<string, object> {{"EmailAddressTo", EmailAddressTo}, {"EmailSubject", EmailSubject}, {"EmailBody", EmailBody}, {"EmailReplyTo", EmailReplyTo}, {"Cc", Cc}, {"Bcc", Bcc} });
                ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Debug, "Body: " + EmailBody, new Dictionary<string, object> {{"EmailAddressTo", EmailAddressTo}, {"EmailSubject", EmailSubject}, {"EmailReplyTo", EmailReplyTo}, {"Cc", Cc}, {"Bcc", Bcc} });


                try
                {
                    objClient.Send(objMessage);
                    _toRet = true;
                }
                catch (Exception ex)
                {
                    ApplicationLogger.Instance.Log(ApplicationLogger.LogType.Error, "Error trying to send an email.", new Dictionary<string, object> {{"EmailAddressTo", EmailAddressTo}, {"EmailSubject", EmailSubject}, {"EmailBody", EmailBody}, {"EmailReplyTo", EmailReplyTo}, {"Cc", Cc}, {"Bcc", Bcc}}, ex);
                    _toRet = false;
                }
                return _toRet;
            }
        }

        public bool SendEmail(string EmailAddressTo, string EmailSubject, string EmailBody, string EmailReplyTo, string[] Attachments)
        {
            return SendEmail(EmailAddressTo, EmailSubject, EmailBody, EmailReplyTo, string.Empty, string.Empty, Attachments);
        }

        private static void PrepareAttachments(string[] Attachments, MailMessage objMessage)
        {
            if (Attachments == null || Attachments.Length == 0) return;
            for (int i = 0; i < Attachments.Length; i++)
            {
                objMessage.Attachments.Add(new System.Net.Mail.Attachment(Attachments[i].ToString()));
            }
        }

        /// <summary>
        /// Retrieves the SMTP Client object already configured.
        /// </summary>
        /// <returns></returns>
        private static SmtpClient GetSmtpClient()
        {
            SmtpClient objClient = new SmtpClient();
            string objHost = Infrastructure.Constants.InfrastructureConstants.GetEmailHost();
            objClient.EnableSsl = Infrastructure.Constants.InfrastructureConstants.GetEmailUseSsl();
            int objPort = Infrastructure.Constants.InfrastructureConstants.GetEmailPort();
            if (objPort != -1)
                objClient.Port = objPort;
                
            objClient.Host = objHost;

            if (!string.IsNullOrEmpty(Infrastructure.Constants.InfrastructureConstants.GetEmailPassword()) && !string.IsNullOrEmpty(Infrastructure.Constants.InfrastructureConstants.GetEmailUsername()))
            {
                NetworkCredential credential = new NetworkCredential(Infrastructure.Constants.InfrastructureConstants.GetEmailUsername(), Infrastructure.Constants.InfrastructureConstants.GetEmailPassword(), Infrastructure.Constants.InfrastructureConstants.GetEmailDomain());
                objClient.UseDefaultCredentials = false;
                objClient.Credentials = credential;
            }
            else
                objClient.UseDefaultCredentials = true;

            return objClient;
        }
    }
}

