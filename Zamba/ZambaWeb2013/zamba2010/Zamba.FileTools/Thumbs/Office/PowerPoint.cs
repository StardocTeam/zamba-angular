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

namespace Zamba.FileTools
{
    public class PPTThumb
    {
        private readonly int Width = ThumbConfig.Width;
        private readonly int Height = ThumbConfig.Height;
        private readonly string tempPath = ThumbConfig.TempPath;
        public string GenerateThumb(string path)
        {
            string base64 = string.Empty;
            Application pptApplication = new Application();
            Presentation pptPresentation = pptApplication.Presentations.Open(path, MsoTriState.msoFalse,
            MsoTriState.msoFalse, MsoTriState.msoFalse);
            var tempLoc = tempPath + Path.GetFileNameWithoutExtension(path) + ".jpg";
            pptPresentation.Slides[1].Export(tempLoc, "jpg", Width, Height);

            pptPresentation.Close();
            pptApplication.Quit();
            pptApplication = null;
            pptPresentation = null;

            if (File.Exists(tempLoc))
            {
                var img = new Bitmap(tempLoc);
                base64 = Base64.ResizeImage(img);
                img.Dispose();
                File.Delete(tempLoc);
            }
            return base64;
        }
    }
}

