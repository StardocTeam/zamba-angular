using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Printing;
using System.Text;
using System.IO;
using System.Diagnostics;
using Pechkin;
using Zamba.FormBrowser.Helpers;

namespace Zamba.HTMLToPDFConverter
{
    public class HTMLToPDFPechkin
    {
        /// <summary>
        /// Convierte el código HTML en un documento PDF
        /// </summary>
        /// <param name="htmlCode">Código HTML</param>
        /// <param name="pdfPath">Ruta completa del archivo PDF a generar</param>
        /// <param name="orientation">Orientación de la impresión</param>
        /// <param name="absolutePath">Ruta absoluta necesaria para darle formato a todas las rutas relativas del html. Ejemplo: <code>MembershipHelper.AppUrl</code></param>
        public void ConvertCode(string htmlCode, string pdfPath, PageOrientation orientation, string absolutePath)
        {
            byte[] buf;

            try
            {
                //Se instancia el conversor
                using (IPechkin sc = Factory.Create(new GlobalConfig()
                     .SetMargins(new Margins(0, 0, 0, 0))
                     .SetDocumentTitle("Stardoc")
                     .SetCopyCount(1)
                     .SetImageQuality(100)
                     .SetLosslessCompression(true)
                     .SetMaxImageDpi(200)
                     .SetPaperOrientation(orientation == PageOrientation.Landscape ? true : false)
                     .SetPaperSize(PaperKind.A4)))
                {
                    //Se transforman todas las rutas relativas a absolutas
                    //Este proceso es necesario para que el conversor pueda interpretarlas correctamente
                    htmlCode = HTML.GenerateAbsoluteReferences(htmlCode);

                    //Obtiene los caracteres reales de los códigos HTML
                    htmlCode = HTML.RecoverSpecialCharacters(htmlCode);

                    //Se realiza la conversión y se obtiene un array de bytes
                    buf = sc.Convert(new ObjectConfig()
                        .SetCreateForms(false)
                        .SetPrintBackground(true), htmlCode);
                }

                //Se guarda el archivo PDF 
                using (FileStream fs = new FileStream(pdfPath, FileMode.Create))
                {
                    fs.Write(buf, 0, buf.Length);
                    fs.Close();
                }
            }
         
            finally
            {
                buf = null;
            }
        }

        public bool ConvertFile(string htmlPath, string pdfPath, PageOrientation orientation)
        {
            return true;
        }
    }
}
