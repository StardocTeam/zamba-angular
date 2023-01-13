using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zamba.Web.Services;

namespace Zamba.Web.CSharp.Test.Zamba.Web.Test
{
    [TestClass]
    public class FileUploadTest
    {
        [TestMethod]
        public void SeDebedeEnviarContextoNuloCuandoElHttpContextAsAsiLoSea()
        {
            try
            {
                var fileUpload = new FileUpload();
                fileUpload.ProcessRequest(null);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Contexto nulo", ex.Message);                
            }
        }
        [TestMethod]
        public void CuandoInsertRessultSeaNuloSeDebeDevolverMensajeDeObjetoNulo()
        {


        }
    }
}
