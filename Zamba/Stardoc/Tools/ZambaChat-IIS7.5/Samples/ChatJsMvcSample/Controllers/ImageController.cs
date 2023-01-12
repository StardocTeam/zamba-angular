using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Web.Http.Cors;
using System.Drawing.Imaging;

namespace ChatJsMvcSample.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ImageController
    {
        public static Image GetDefaultAvatarImage()
        {
            try
            {
                var bd = AppDomain.CurrentDomain.BaseDirectory;
                var da = "ChatJs/images/defaultAvatar.png";
                var path = File.Exists(bd + da) ? bd + da : bd + "bin/" + da;
                Image img = Image.FromFile(path);

                return img;
            }
            catch (Exception ex) { return null; }

        }
        public static string GetDefaultAvatarImageB64()
        {
            try
            {
                return GetBase64FromImage(GetDefaultAvatarImage(), "png");
            }
            catch (Exception ex) { return string.Empty; }

        }

        public static string GetBase64FromImage(Image img, string ext = "")
        {
            string imgstr;
            try
            {
                ImageConverter _imageConverter = new ImageConverter();
                byte[] imageBytes = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                imgstr = Convert.ToBase64String(imageBytes);
                img.Dispose();
                return imgstr;

            }
            catch (Exception ex) { return string.Empty; }

        }
        [EnableCors("*", "*", "*")]
        public static string ResizeImage(Image image)
        {
            //Reduzo imagen a 250x250 porque pueden pasar imagenes muy grandes y se cuelga el chat
            string img;
            var imageSize = (Image)(new Bitmap(image, new Size(250, 250)));
            using (MemoryStream m = new MemoryStream())
            {
                imageSize.Save(m, image.RawFormat);
                byte[] imageBytes = m.ToArray();
                img = Convert.ToBase64String(imageBytes);
            }
            return img;
        }
        [EnableCors("*", "*", "*")]
        public static string ResizeBase64(string image)
        {            
            byte[] imageBytes = Convert.FromBase64String(image);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            image = ResizeImage(Image.FromStream(ms, true));

            return image;
        }
        [EnableCors("*", "*", "*")]
        public static string PathToBase64(string path)
        {
            string base64 = string.Empty;
            try
            {
                if (File.Exists(path))
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
                }
                return base64;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}