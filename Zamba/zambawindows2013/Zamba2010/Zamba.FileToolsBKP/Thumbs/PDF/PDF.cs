using System;
using GhostscriptSharp;
using System.IO;
using System.Drawing;
using Zamba.Core;

namespace Zamba.FileTools
{
    public class PDFThumb
    {
        public PDFThumb()
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
            string base64 = string.Empty;
            var tempLoc = tempPath + Path.GetFileName(path);
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Abriendo PPT con GhostScript " + path +
                    " para generacion de thumb y guardandolo en " + tempLoc);

                GhostscriptWrapper.GeneratePageThumb(path, tempLoc, 1, 100, 100, Width, Height);

                if (File.Exists(tempLoc))
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Image de PDF generada, generando thumb");
                    var img = new Bitmap(tempLoc);
                    base64 = Base64.ResizeImage(img);
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Eliminando objeto img y fisico temporal");
                    img.Dispose();
                    File.Delete(tempLoc);
                }
                else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "No se pudo generar-guardar " + tempLoc + " no se genero thumb");
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se produjo un error al generar thumb PDF " + ex.ToString());
            }
            return base64;
        }
    }
}
