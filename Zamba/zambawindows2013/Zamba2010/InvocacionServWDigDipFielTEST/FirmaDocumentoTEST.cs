using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.X509;
using SysX509 = System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.X509Certificates;

namespace InvocacionServWDigDipFielTEST
{
    [TestClass]
    public class FirmaDocumentoTEST
    {
        [TestMethod]
        public void Firmar()
        {
            var serialNumber = "25D87E4BE1A1ED8D";

            X509Store store = new X509Store("MY", StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            var collection = store.Certificates;
            var fcollection =collection.Find(X509FindType.FindByTimeValid, serialNumber, false);


            Mocks.FirmaDocumentoMock.SignHashed(@"C:\WSASS_como_adherirse.pdf",
                @"C:\WSASS_como_adherirse.pdf", fcollection[1],
                "", @"C:\", true);
            
            store.Close(); 

    


        }


    }
}

