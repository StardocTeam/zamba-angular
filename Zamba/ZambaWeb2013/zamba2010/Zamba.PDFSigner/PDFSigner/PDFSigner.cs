using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Collections;

using System.IO;
using iTextSharp.text.xml.xmp;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Crypto;
using iTextSharp.text;

namespace Zamba.PDFSigner// 
{
    /// <summary>
    /// This class hold the certificate and extract private key needed for e-signature 
    /// </summary>
    class Cert
    {
        #region Attributes

        private string path = "";
        private string password = "";
        private AsymmetricKeyParameter akp;
        private Org.BouncyCastle.X509.X509Certificate[] chain;

        #endregion

        #region Accessors
        public Org.BouncyCastle.X509.X509Certificate[] Chain
        {
            get { return chain; }
        }
        public AsymmetricKeyParameter Akp
        {
            get { return akp; }
        }

        public string Path
        {
            get { return path; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        #endregion

        #region Helpers

        private void processCert()
        {
            string alias = null;
            Pkcs12Store pk12;

            //First we'll read the certificate file
            pk12 = new Pkcs12Store(new FileStream(this.Path, FileMode.Open, FileAccess.Read), this.password.ToCharArray());

            //then Iterate throught certificate entries to find the private key entry
            IEnumerator i = pk12.Aliases.GetEnumerator();
            while (i.MoveNext())
            {
                alias = ((string)i.Current);
                if (pk12.IsKeyEntry(alias))
                    break;
            }

            this.akp = pk12.GetKey(alias).Key;
            X509CertificateEntry[] ce = pk12.GetCertificateChain(alias);
            this.chain = new Org.BouncyCastle.X509.X509Certificate[ce.Length];
            for (int k = 0; k < ce.Length; ++k)
                chain[k] = ce[k].Certificate;

        }
        #endregion

        #region Constructors
        public Cert()
        { }
        public Cert(string cpath)
        {
            this.path = cpath;
            this.processCert();
        }
        public Cert(string cpath, string cpassword)
        {
            this.path = cpath;
            this.Password = cpassword;
            this.processCert();
        }
        #endregion
    }

    /// <summary>
    /// This is a holder class for PDF metadata
    /// </summary>
    public class MetaData
    {
        private Hashtable info = new Hashtable();

        public Hashtable Info
        {
            get { return info; }
            set { info = value; }
        }

        public string Author
        {
            get { return (string)info["Author"]; }
            set { info.Add("Author", value); }
        }
        public string Title
        {
            get { return (string)info["Title"]; }
            set { info.Add("Title", value); }
        }
        public string Subject
        {
            get { return (string)info["Subject"]; }
            set { info.Add("Subject", value); }
        }
        public string Keywords
        {
            get { return (string)info["Keywords"]; }
            set { info.Add("Keywords", value); }
        }
        public string Producer
        {
            get { return (string)info["Producer"]; }
            set { info.Add("Producer", value); }
        }

        public string Creator
        {
            get { return (string)info["Creator"]; }
            set { info.Add("Creator", value); }
        }

        public Hashtable getMetaData()
        {
            return this.info;
        }
        public byte[] getStreamedMetaData()
        {
            MemoryStream os = new System.IO.MemoryStream();
            //  XmpWriter xmp = new XmpWriter(os, this.info);            
            XmpWriter xmp = new XmpWriter(os);
            xmp.Close();
            return os.ToArray();
        }
    }

    /// <summary>
    /// this is the most important class
    /// it uses iTextSharp library to sign a PDF document
    /// </summary>
    //public class PDFSigner
    //{
    //    private string inputPDF = "";
    //    private string outputPDF = "";
    //    private Cert myCert;
    //    private MetaData metadata;

    //    public PDFSigner(string input, string output)
    //    {
    //        this.inputPDF = input;
    //        this.outputPDF = output;
    //    }

    //    private PDFSigner(string input, string output, Cert cert)
    //    {
    //        this.inputPDF = input;
    //        this.outputPDF = output;
    //        this.myCert = cert;
    //    }
    //    private PDFSigner(string input, string output, MetaData md)
    //    {
    //        this.inputPDF = input;
    //        this.outputPDF = output;
    //        this.metadata = md;
    //    }
    //    private PDFSigner(string input, string output, Cert cert, MetaData md)
    //    {
    //        this.inputPDF = input;
    //        this.outputPDF = output;
    //        this.myCert = cert;
    //        this.metadata = md;
    //    }

    //    public void Verify()
    //    {
    //    }
    //}

    public class PDFSign
    {
        private X509Certificate2 cert;
        public void Sign(string fullPath, string fileName, string author, string title, string subject, string keywords,
            string creator, string producer, string certificate, string password, string reason, string contact,
            string location, bool writePDF)
        {

            X509Store store = new X509Store(StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            for (var i = 0; i <= store.Certificates.Count - 1; i++)
            {
                if (store.Certificates[i].SerialNumber == certificate)
                {
                    cert = store.Certificates[i];
                    break;
                }
            }
            //X509Certificate2Collection sel = X509Certificate2UI.SelectFromCollection(store.Certificates, null, null, X509SelectionFlag.SingleSelection);
            //X509Certificate2 cert = sel[0];
            //X509Certificate2 cert = new X509Certificate2();
            //cert.Import(this.myCert.Chain[0].GetEncoded());

            Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] {
            cp.ReadCertificate(cert.RawData)};
            IExternalSignature externalSignature = new X509Certificate2Signature(cert, "SHA-1");
            PdfReader pdfReader = new PdfReader(fullPath);
            var signedPdf = new FileStream(fileName, FileMode.Create);
            var pdfStamper = PdfStamper.CreateSignature(pdfReader, signedPdf, '1');

            var newInfo = pdfReader.Info;
            newInfo["Author"] = author;
            newInfo["Title"] = title;
            newInfo["Subject"] = subject;
            newInfo["Keywords"] = keywords;
            newInfo["Creator"] = creator;
            newInfo["Producer"] = producer;
            pdfStamper.MoreInfo = newInfo;

            PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;
            if (writePDF)
            {
                signatureAppearance.Reason = reason;
                signatureAppearance.Contact = contact;
                signatureAppearance.Location = location;
                signatureAppearance.SignatureGraphic = Image.GetInstance("http://www.veryicon.com/icon/64/System/Phuzion/Sign%20Select.png");
                signatureAppearance.SetVisibleSignature((new Rectangle(100, 100, 250, 150)), pdfReader.NumberOfPages, "Signature");
                signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION;
            }
            MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, null, null, null, 0, CryptoStandard.CMS);

            //  PdfReader reader = new PdfReader(this.inputPDF);
            //  //Activate MultiSignatures
            //  PdfStamper st = PdfStamper.CreateSignature(reader, new FileStream(this.outputPDF, FileMode.Create, FileAccess.Write), '\0', null, true);
            //  //To disable Multi signatures uncomment this line : every new signature will invalidate older ones !
            //  //PdfStamper st = PdfStamper.CreateSignature(reader, new FileStream(this.outputPDF, FileMode.Create, FileAccess.Write), '\0'); 
            ////  st.MoreInfo = this.metadata.getMetaData();
            //  st.XmpMetadata = this.metadata.getStreamedMetaData();
            //  PdfSignatureAppearance sap = st.SignatureAppearance;
            //  IExternalSignature externalSignature = new X509Certificate2Signature(this.myCert.Chain, "SHA-1");

            //  MakeSignature.SignDetached(sap, null, this.myCert.Chain, null, null, null, 0, CryptoStandard.CMS);
            //  //  sap.SetCrypto(this.myCert.Akp, this.myCert.Chain, null, PdfSignatureAppearance.WINCER_SIGNED);
            //  sap.Reason = SigReason;
            //  sap.Contact = SigContact;
            //  sap.Location = SigLocation;            
            //  if (visible)
            //      sap.SetVisibleSignature(new iTextSharp.text.Rectangle(100, 100, 250, 150), 1, null);
            //  st.Close();           
        }

        public void NewSign(string fullPath, string fileName, string author, string title, string subject, string keywords,
            string creator, string producer, string certificate, string password, string reason, string contact,
            string location, bool writePDF)
        {
            if (fullPath == string.Empty)
                throw new Exception("La ruta del documento es vacia.");

            if (fullPath == null)
                throw new Exception("La ruta del documento es nula.");

            if (fileName == string.Empty)
                throw new Exception("El nombre del documento es vacia.");

            if (fileName == null)
                throw new Exception("El nombre del documento es nulo.");





            X509Store store = new X509Store(StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            for (var i = 0; i <= store.Certificates.Count; i++)
            {
                if (store.Certificates[i].SerialNumber == certificate)
                {
                    cert = store.Certificates[i];
                    break;
                }
            }
            //X509Certificate2Collection sel = X509Certificate2UI.SelectFromCollection(store.Certificates, null, null, X509SelectionFlag.SingleSelection);
            //X509Certificate2 cert = sel[0];
            //X509Certificate2 cert = new X509Certificate2();
            //cert.Import(this.myCert.Chain[0].GetEncoded());

            Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] {
            cp.ReadCertificate(cert.RawData)};
            IExternalSignature externalSignature = new X509Certificate2Signature(cert, "SHA-1");
            PdfReader pdfReader = new PdfReader(fullPath);
            var signedPdf = new FileStream(fileName, FileMode.Create);
            var pdfStamper = PdfStamper.CreateSignature(pdfReader, signedPdf, '1');

            var newInfo = pdfReader.Info;
            newInfo["Author"] = author;
            newInfo["Title"] = title;
            newInfo["Subject"] = subject;
            newInfo["Keywords"] = keywords;
            newInfo["Creator"] = creator;
            newInfo["Producer"] = producer;
            pdfStamper.MoreInfo = newInfo;

            PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;
            if (writePDF)
            {
                signatureAppearance.Reason = reason;
                signatureAppearance.Contact = contact;
                signatureAppearance.Location = location;
                signatureAppearance.SignatureGraphic = Image.GetInstance("http://www.veryicon.com/icon/64/System/Phuzion/Sign%20Select.png");
                signatureAppearance.SetVisibleSignature((new Rectangle(100, 100, 250, 150)), pdfReader.NumberOfPages, "Signature");
                signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION;
            }
            MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, null, null, null, 0, CryptoStandard.CMS);

            //  PdfReader reader = new PdfReader(this.inputPDF);
            //  //Activate MultiSignatures
            //  PdfStamper st = PdfStamper.CreateSignature(reader, new FileStream(this.outputPDF, FileMode.Create, FileAccess.Write), '\0', null, true);
            //  //To disable Multi signatures uncomment this line : every new signature will invalidate older ones !
            //  //PdfStamper st = PdfStamper.CreateSignature(reader, new FileStream(this.outputPDF, FileMode.Create, FileAccess.Write), '\0'); 
            ////  st.MoreInfo = this.metadata.getMetaData();
            //  st.XmpMetadata = this.metadata.getStreamedMetaData();
            //  PdfSignatureAppearance sap = st.SignatureAppearance;
            //  IExternalSignature externalSignature = new X509Certificate2Signature(this.myCert.Chain, "SHA-1");

            //  MakeSignature.SignDetached(sap, null, this.myCert.Chain, null, null, null, 0, CryptoStandard.CMS);
            //  //  sap.SetCrypto(this.myCert.Akp, this.myCert.Chain, null, PdfSignatureAppearance.WINCER_SIGNED);
            //  sap.Reason = SigReason;
            //  sap.Contact = SigContact;
            //  sap.Location = SigLocation;            
            //  if (visible)
            //      sap.SetVisibleSignature(new iTextSharp.text.Rectangle(100, 100, 250, 150), 1, null);
            //  st.Close();           
        }
    }
}
