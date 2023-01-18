using System;
using GhostscriptSharp;
using System.IO;
using System.Drawing;
using Zamba.Core;

namespace Zamba.FileTools
{
    public class TXT
    {
        private readonly string tempPath = ThumbConfig.TempPath;
        private readonly int Width = ThumbConfig.Width;
        private readonly int Height = ThumbConfig.Height;
        public string GenerateThumb(string path)
        {
            string base64 = string.Empty;            
            if (File.Exists(path))
            {
                var txt = File.ReadAllText(path);
                FontFamily fontFamily = new FontFamily("Arial");
                Font font = new Font(fontFamily, 8, FontStyle.Regular);//, GraphicsUnit.Pixel
                var img = DrawText(txt, font, Color.Black,  Color.White);
                base64 = Base64.ResizeImage(img);
            }

            return base64;
        }
        private Image DrawText(String text, Font font, Color textColor, Color backColor)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;

        }
    }
}
