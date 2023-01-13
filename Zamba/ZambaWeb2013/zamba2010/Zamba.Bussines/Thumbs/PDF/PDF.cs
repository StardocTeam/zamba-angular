using System;
using GhostscriptSharp;
using System.IO;
using System.Drawing;
using Zamba.Core;

namespace Zamba.FileTools
{
    public class PDFThumb
    {
        private readonly string tempPath = ThumbConfig.TempPath;
        private readonly int Width = ThumbConfig.Width;
        private readonly int Height = ThumbConfig.Height;
        public string GenerateThumb(string path)
        {
            string base64 = string.Empty;

            var tempLoc = tempPath + Path.GetFileName(path);
            GhostscriptWrapper.GeneratePageThumb(path, tempLoc, 1, 100, 100, Width, Height);

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
