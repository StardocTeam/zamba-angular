using Zamba.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;

namespace Test.Zamba.Bussines
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para Message_FactoryTest y se pretende que
    ///contenga todas las pruebas unitarias Message_FactoryTest.
    ///</summary>
    [TestClass()]
    public class Message_FactoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de la prueba que proporciona
        ///la información y funcionalidad para la ejecución de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        // 
        //Puede utilizar los siguientes atributos adicionales mientras escribe sus pruebas:
        //
        //Use ClassInitialize para ejecutar código antes de ejecutar la primera prueba en la clase 
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup para ejecutar código después de haber ejecutado todas las pruebas en una clase
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize para ejecutar código antes de ejecutar cada prueba
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup para ejecutar código después de que se hayan ejecutado todas las pruebas
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Una prueba de SendMail
        ///</summary>
        [TestMethod()]
        public void SendMailTest()
        {
            MailTypes eMailType = new MailTypes(); // TODO: Inicializar en un valor adecuado
            string from = string.Empty; // TODO: Inicializar en un valor adecuado
            string ProveedorSMTP = string.Empty; // TODO: Inicializar en un valor adecuado
            string Puerto = string.Empty; // TODO: Inicializar en un valor adecuado
            string UserName = string.Empty; // TODO: Inicializar en un valor adecuado
            string Password = string.Empty; // TODO: Inicializar en un valor adecuado
            string para = string.Empty; // TODO: Inicializar en un valor adecuado
            string cc = string.Empty; // TODO: Inicializar en un valor adecuado
            string cco = string.Empty; // TODO: Inicializar en un valor adecuado
            string asunto = string.Empty; // TODO: Inicializar en un valor adecuado
            string body = string.Empty; // TODO: Inicializar en un valor adecuado
            bool isBodyHtml = false; // TODO: Inicializar en un valor adecuado
            string ReplyTo = string.Empty; // TODO: Inicializar en un valor adecuado
            bool ReturnReceipt = false; // TODO: Inicializar en un valor adecuado
            List<string> attachFileNames = null; // TODO: Inicializar en un valor adecuado
            long userId = 0; // TODO: Inicializar en un valor adecuado
            bool SaveOnSend = false; // TODO: Inicializar en un valor adecuado
            List<IResult> _attachsResults = null; // TODO: Inicializar en un valor adecuado
            long DocTypeId = 0; // TODO: Inicializar en un valor adecuado
            ArrayList ArrayLinks = null; // TODO: Inicializar en un valor adecuado
            bool expected = false; // TODO: Inicializar en un valor adecuado
            bool actual;
            actual = Message_Factory.SendMail(eMailType, from, ProveedorSMTP, Puerto, UserName, Password, para, cc, cco, asunto, body, isBodyHtml, ReplyTo, ReturnReceipt, attachFileNames, userId, SaveOnSend, _attachsResults, DocTypeId, ArrayLinks);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
