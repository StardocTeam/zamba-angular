using System;
using GhostscriptSharp;
using System.IO;
using System.Drawing;
using Zamba.Core;

namespace Zamba.FileTools
{
    public class PDFThumb
    {
//        private readonly string tempPath = ThumbConfig.TempPath;
        private readonly int Width = ThumbConfig.Width;
        private readonly int Height = ThumbConfig.Height;
        public string GenerateThumb(string path, string tempPath)
        {
            string base64 = string.Empty;
            bool deleteTemp = false;
            var tempLoc = string.Empty;
            if (tempPath == string.Empty)
            {
                tempPath = ThumbConfig.TempPath;
                tempLoc = tempPath + Path.GetFileName(path);
                deleteTemp = true;
            }
            else
            {
                tempLoc = tempPath;
            }

            if (File.Exists(tempLoc))
            {
                var img = new Bitmap(tempLoc);
                base64 = Base64.ResizeImage(img);
                img.Dispose();
                return base64;
            }
            else
            {

                GhostscriptWrapper.GeneratePageThumb(path, tempLoc, 1, 100, 100, Width, Height);

                if (File.Exists(tempLoc))
                {
                    var img = new Bitmap(tempLoc);
                    base64 = Base64.ResizeImage(img);
                    img.Dispose();
                    if (deleteTemp) File.Delete(tempLoc);
                }

                return base64;
            }
        }
    }
}
