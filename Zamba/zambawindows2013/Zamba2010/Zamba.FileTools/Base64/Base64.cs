using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Zamba.Core;

namespace Zamba.FileTools
{
    public class Base64
    {
        public static string ResizeImage(Image image)
        {
            ThumbConfig TC = new ThumbConfig();
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Dentro de filetools generando imagen ancho: " + TC.Width +
                " alto: " + TC.Height);
            string img = string.Empty;
            try
            {
                var imageSize = (Image)(new Bitmap(image, new Size(TC.Width, TC.Height)));
                using (var m = new MemoryStream())
                {
                    imageSize.Save(m, ImageFormat.Bmp);
                    byte[] imageBytes = m.ToArray();
                    img = Convert.ToBase64String(imageBytes);
                }
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Procesamiento de Img base 64 exitosa");
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error de procesamiento de Img base 64 " + ex.ToString());
            }
            return img;
        }

        public string ResizeBase64(string image)
        {
            byte[] imageBytes = Convert.FromBase64String(image);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            image = ResizeImage(Image.FromStream(ms, true));

            return image;
        }
        public string PathToBase64(string path)
        {
            string base64;
            try
            {

                using (var fs = System.IO.File.OpenRead(path))
                {
                    using (var image = System.Drawing.Image.FromStream(fs))
                    {
                        using (System.IO.MemoryStream m = new System.IO.MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();
                            base64 = Convert.ToBase64String(imageBytes);
                        }
                    }
                }
                return base64;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
