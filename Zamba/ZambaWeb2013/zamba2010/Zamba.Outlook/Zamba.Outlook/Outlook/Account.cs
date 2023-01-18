using System;
using Zamba.Outlook.Interfaces;

namespace Zamba.Outlook.Outlook
{
    /// <summary>
    /// This class wraps the Account Logic from the Outlook API
    /// </summary>

    public sealed class Account
        : IAccount
    {

        #region Constantes
        private const String ADDRESSES_SEPARATOR = ";";
        private const String MAPI_NAMESPACE = "MAPI";
        #endregion

        /// <summary>
        /// Returns an Instance of the Outlook Application
        /// </summary>
        /// <returns></returns>
        //private static Application GetOutlook()
        //{
        //    //TODO: Validar si existe una sesion de Outlook y tomarla 
        //    return new Application();
        //}

        public void Create(String name)
        {
            //TODO:
        }

        public void Delete(String name)
        {
            //TODO
        }

        private Account()
        { }
    }
}