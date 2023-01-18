using System;
using Microsoft.Office.Interop.Outlook;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Zamba.Outlook
{
    /// <summary>
    /// This class wraps the Account Logic from the Outlook API
    /// </summary>

    public sealed class Account
    {
        /// <summary>
        /// Returns an Instance of the Outlook Application
        /// </summary>
        /// <returns></returns>
        private static Application GetOutlook()
        {
            //TODO: Validar si existe una sesion de Outlook y tomarla 
            return new Application();
        }

        #region Constantes
        private const String ADDRESSES_SEPARATOR = ";";
        private const String MAPI_NAMESPACE = "MAPI";
        #endregion


        public static void Create(String name)
        {
            //TODO:
        }

        public static void Delete(String name)
        {
            //TODO
        }

        private Account()
        { }
    }
}