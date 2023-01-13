using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Zamba.Core;

namespace Zamba.FileTools
{
    public class WordThumb
    {
        public Image GenerateThumb(string path)
        {
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Abriendo word " + path + " para generacion de thumb");
            Image image = null;
            try
            {
                Document doc = new Document();
                doc.LoadFromFile(path);
                var i = 1;
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Imagen para thumb word generada");
                image = doc.SaveToImages(i, Spire.Doc.Documents.ImageType.Metafile);//.Metafile); // image.Save(string.Format("c:\\result-{0}.jpeg", i), ImageFormat.Jpeg);

                if (image == null)
                {
                    image = doc.SaveToImages(Spire.Doc.Documents.ImageType.Bitmap)[0];
                }
                                                                                         }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al generar thumb word " +ex.ToString());
            }
            return image;
        }
    }
}

