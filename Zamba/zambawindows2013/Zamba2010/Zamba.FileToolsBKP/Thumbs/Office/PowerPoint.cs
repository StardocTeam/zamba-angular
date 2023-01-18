using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using System.IO;
using Zamba.Core;

namespace Zamba.FileTools
{
    public class PPTThumb
    {
        public PPTThumb()
        {
            ThumbConfig TC = new ThumbConfig();
            tempPath = TC.TempPath;
            Width = TC.Width;
            Height = TC.Height;
        }

        private string tempPath = string.Empty;
        private int Width = 200;
        private int Height = 250;
   
        public string GenerateThumb(string path)
        {
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Generando Img de ppt " + path);
            string base64 = string.Empty;
            try
            {
                Application pptApplication = new Application();
                Presentation pptPresentation = pptApplication.Presentations.Open(path, MsoTriState.msoFalse,
                MsoTriState.msoFalse, MsoTriState.msoFalse);
                var tempLoc = tempPath + Path.GetFileNameWithoutExtension(path) + ".jpg";
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando temporal ppt en " + tempLoc);
                pptPresentation.Slides[1].Export(tempLoc, "jpg", Width, Height);

                pptPresentation.Close();
                pptApplication.Quit();
                pptApplication = null;
                pptPresentation = null;

                if (File.Exists(tempLoc))
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Generando thumb de img ppt " + tempLoc);
                    var img = new Bitmap(tempLoc);
                    base64 = Base64.ResizeImage(img);
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Eliminando temporal img ppt");
                    img.Dispose();
                    File.Delete(tempLoc);
                }
                else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "No se pudo generar " + tempLoc + ", no se genero thumb");
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se produjo un error al procesar el ppt " +ex.ToString());
            }
            return base64;
        }
    }
}

