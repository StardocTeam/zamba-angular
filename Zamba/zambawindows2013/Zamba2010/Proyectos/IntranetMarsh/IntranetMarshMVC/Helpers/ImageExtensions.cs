using System;
using System.Web.Mvc;
using Zamba.Core;

namespace IntranetMarshMVC.Helpers
{
    public static class ImageExtensions
    {
        public static string Image(this HtmlHelper helper, string src, string text, int maxwidth, int maxheight)
        {
            double width = maxwidth;
            double heigth = maxheight;
            double proporcion;
            string img_full_path;
            System.Drawing.Image imagen = null;

            //try
            //{
            //    img_full_path = AppDomain.CurrentDomain.BaseDirectory.ToString() + src;
            //    img_full_path = src;

            //    imagen = System.Drawing.Image.FromFile(img_full_path);

            //    // si el ancho o el alto se pasan del maximo recalcular tamaño
            //    if ((imagen.Height > maxheight) || (imagen.Width > maxwidth))
            //    {
            //        // es mas alta que ancha
            //        if (imagen.Height > imagen.Width)
            //        {
            //            proporcion = (double)imagen.Width / (double)imagen.Height;

            //            heigth = maxheight;
            //            width = heigth * proporcion;
            //        }

            //        // es mas ancha que alta
            //        if (imagen.Width > imagen.Height)
            //        {
            //            proporcion = (double)imagen.Height / (double)imagen.Width;

            //            width = maxwidth;
            //            heigth = width * proporcion;
            //        }
            //    }
            //    else
            //    {
            //        // si no es mas grande que el permitido devolverla con
            //        // el tamaño original
            //        width = imagen.Width;
            //        heigth = imagen.Height;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ZClass.raiseerror(ex);
            //}
            //finally
            //{
            //    if(imagen != null)
            //        imagen.Dispose();
            //}

            //return String.Format("<img src='{0}' alt='{1}' title='{1}' width={2} height={3}>", src, text, (int)width, (int)heigth);

            return String.Format("<img src='{0}' alt='{1}' title='{1}' width={2}>", src, text, (int)width);
        }
    }
}
