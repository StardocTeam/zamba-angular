using System;
using GhostscriptSharp;
using System.IO;
using System.Drawing;
using Zamba.Core;

namespace Zamba.FileTools
{
    public class PDFThumb
    {
        ThumbConfig TC = new ThumbConfig();
        public PDFThumb()
        {
        }

        public string GenerateThumb(string path, Int64 entityId, Int64 docId)
        {
            string base64 = string.Empty;
            var tempLoc = Path.Combine(TC.ThumbDirectory, entityId.ToString(), docId.ToString(), docId + ".Jpeg");
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Abriendo PDF con GhostScript " + path +
                    " para generacion de thumb y guardandolo en " + tempLoc);

                if (!Directory.Exists(new FileInfo(tempLoc).Directory.FullName))
                    Directory.CreateDirectory(new FileInfo(tempLoc).Directory.FullName);
                {
                    GhostscriptWrapper.GeneratePageThumb(path, tempLoc, 1, TC.Dpix, TC.Dpiy, TC.Width, TC.Height);
                }
                
                if (File.Exists(tempLoc))
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Image de PDF generada, generando thumb");
                    var img = new Bitmap(tempLoc);
                    base64 = Base64.ResizeImage(img);
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Eliminando objeto img y fisico temporal");
                    img.Dispose();

                    bool StoreInDB;
                    Boolean.TryParse(ZOptBusiness.GetValueOrDefault("ThumbStoreInDB", "false"), out StoreInDB);

                    if (StoreInDB)
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
