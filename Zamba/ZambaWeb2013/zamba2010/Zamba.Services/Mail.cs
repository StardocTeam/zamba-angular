using System;
using System.Collections.Generic;
using Zamba.Core;

namespace Zamba.Services
{
    public class Mail : IService
    {
        #region Singleton
        private static Mail _mail;

        private Mail()
        {
        }

        public static IService GetInstance()
        {
            if (_mail == null)
                _mail = new Mail();

            return _mail;
        }
        #endregion
        #region IService Members
        ServicesTypes IService.ServiceType()
        {
            return ServicesTypes.Mail;
        }
        #endregion
        public List<IMailMessage> GetMessages(Int64 userId)
        {
            List<IMailMessage> MessagesList = null;
            ///TODO:
            return MessagesList;
        }

        public IMailMessage GetMessage(Int64 messageId)
        {
            IMailMessage Message = null;
            ///TODO:
            return Message;
        }

        public void SendMessage(IMailMessage message)
        {
            ///TODO
        }
    }
}