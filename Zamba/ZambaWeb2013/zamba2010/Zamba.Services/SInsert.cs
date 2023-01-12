using System;
using Zamba.Core;

namespace Zamba.Services
{
    public class SInsert : IService
    {
        #region Singleton
        private static SInsert _insert = null;

        private SInsert()
        {
        }

        public static IService GetInstance()
        {
            if (_insert == null)
                _insert = new SInsert();

            return _insert;
        }
        #endregion
        #region IService Members
        ServicesTypes IService.ServiceType()
        {
            return ServicesTypes.InsertDocument;
        }
        #endregion
        public static void Insert(String folderPath)
        {
            ///TODO:
        }

        public static void Insert(IResult result)
        {
            if (result.ISVIRTUAL)
            {
                ///TODO: Document Virtual
            }
            else
            {
                ///TODO: Documento normal

                if (result.IsOffice)
                {
                    if (result.IsWord)
                    {
                    }
                    else if (result.IsExcel)
                    {
                    }
                    else if (result.IsPowerpoint)
                    {
                    }
                }
                else
                {
                }
            }
        }

        public static void Insert(Object workflow)
        {
            ///TODO: sacar interfaz de la clase loca de marcelo.	
        }
    }
}