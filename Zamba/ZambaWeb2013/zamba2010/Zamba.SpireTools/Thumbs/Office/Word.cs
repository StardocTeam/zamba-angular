using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Zamba.FileTools
{
    public class WordThumb
    {
        public Image GenerateThumb(string path)
        {
            Document doc = new Document();
            doc.LoadFromFile(path);
            var i = 1;
            Image image = doc.SaveToImages(i, Spire.Doc.Documents.ImageType.Metafile);//.Metafile);
                                                                                      // image.Save(string.Format("c:\\result-{0}.jpeg", i), ImageFormat.Jpeg);

            return image;
        }
    }
}

