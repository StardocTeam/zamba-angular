using System;
using GhostscriptSharp;
using System.IO;
using System.Drawing;
using Zamba.Core;
using System.Linq;
using System.Collections.Generic;

namespace Zamba.FileTools
{
    public class TXT
    {    
        public string GenerateThumb(string path)
        {
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Procesando txt " + path + " para generacion de thumb");
            string base64 = string.Empty;
            if (File.Exists(path))
            {
                try
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Abriendo y procesando txt para generacion de thumb");
                    List<string> text = File.ReadLines(path).Take(25).ToList();
                    var txt = String.Join("\n ", text.ToArray());
                    FontFamily fontFamily = new FontFamily("Arial");
                    Font font = new Font(fontFamily, 8, FontStyle.Regular);//, GraphicsUnit.Pixel
                    var img = DrawText(txt, font, Color.Black, Color.White);
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Image txt thumb correcta");
                    base64 = Base64.ResizeImage(img);
                }
                catch (Exception ex)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se produjo un error al procesar el txt " + ex.ToString());
                }
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
