using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;

namespace ChatJsMvcSample.Controllers
{
    public class ImageController
    {
        public string ResizeImage(Image image)
        {
            //Reduzo imagen a 50x50 porque pueden pasar imagenes muy grandes y se cuelga el chat
            string img;
            var imageSize = (Image)(new Bitmap(image, new Size(50, 50)));
            using (MemoryStream m = new MemoryStream())
            {
                imageSize.Save(m, image.RawFormat);
                byte[] imageBytes = m.ToArray();
                img = Convert.ToBase64String(imageBytes);
            }
            return img;
        }

        public string ResizeBase64(string image)
        {
            //Reduzo imagen a 50x50 porque pueden pasar imagenes muy grandes y se cuelga el chat
            byte[] imageBytes = Convert.FromBase64String(image);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            image = ResizeImage(Image.FromStream(ms, true));

            return image;
        }

                public string PathToBase64(string path)
        {
              string base64;
            try{
             
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
            catch(Exception ex)
            {
                return string.Empty;
            }
        }

    }
}