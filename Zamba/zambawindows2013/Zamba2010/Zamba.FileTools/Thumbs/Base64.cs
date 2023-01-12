using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack;
using Zamba.FileTools;
using Zamba.Core;
using System.IO;
using System.Drawing.Imaging;
using Zamba.Tools;

namespace Zamba.Thumbs
{
    public class Base64
    {

        public static string Image(Image img, Int64 entityId, Int64 docId)
        {
            string base64 = string.Empty;
            try
            {
                Image FirstImage = img;
                ThumbConfig TC = new ThumbConfig();
                Image.GetThumbnailImageAbort GCA = new Image.GetThumbnailImageAbort(ThumbAbort);
                IntPtr CBData = default(IntPtr);
                Image ThumbImage = FirstImage.GetThumbnailImage(TC.Width, TC.Height, GCA, CBData);

                //Ruta directorio
                string dirPath = Path.Combine(TC.ThumbDirectory, entityId.ToString(), docId.ToString());

                //Se crea si no existe
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);

                //Ruta img a crear
                string ThumbPath = Path.Combine(dirPath, docId.ToString() + ".Jpeg");// FileBusiness.GetUniqueFileName(dirPath, docId.ToString() + ".Jpeg"); //Path.Combine(dirPath, thumbName);

                //Se guarda imagen
                ThumbImage.Save(ThumbPath, ImageFormat.Jpeg);

                if (TC.StoreInDB)
                {
                    base64 = FileTools.Base64.ResizeImage(FirstImage);
                    //Borrar img
                    if (File.Exists(ThumbPath))
                        File.Delete(ThumbPath);
                }

                FirstImage = null;
                ThumbImage = null;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return base64;
        }

        public static string Image(string path, Int64 entityId, Int64 docId)
        {
            string base64 = string.Empty;
            try
            {
                FileInfo file = new FileInfo(path);
                Image FirstImage = null;

                if (path.EndsWith("__1"))
                {
                    var img = new Bitmap(path);
                    Image[] images = SpireTools.SplitImages(img, ImageFormat.Png);

                    if (images.Count() > 0)
                    {
                        FirstImage = images[0];
                        base64 = Image(FirstImage, entityId, docId);
                    }
                    else
                    {
                        ZClass.raiseerror(new Exception("Archivo del DocId = " + docId + " No tiene imagenes"));
                    }

                    FirstImage = null;
                    file = null;

                }
                else if(path.EndsWith("tif") || path.EndsWith("tiff"))
                {
                    var img = new Bitmap(path);
                    Image[] images = SpireTools.SplitImages(img, ImageFormat.Png);

                    if (images.Count() > 0)
                    {
                        FirstImage = images[0];
                        base64 = Image(FirstImage, entityId, docId);
                    }
                    else
                    {
                        ZClass.raiseerror(new Exception("Archivo del DocId = " + docId + " No tiene imagenes"));
                    }

                    FirstImage = null;
                    file = null;
                }
                else
                {
                    var img = new Bitmap(path);
                    base64 = Image(img, entityId, docId);

                    FirstImage = null;
                    file = null;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return base64;
        }

        //public static string Image(string path,Int64 entityId, Int64 docId)
        //{
        //    string base64 = string.Empty;
        //    try
        //    {
        //        FileInfo file = new FileInfo(path);
        //        Image FirstImage = null;

        //        if (path.EndsWith("__1"))
        //        {
        //            var img = new Bitmap(path);
        //            Image[] images = SpireTools.SplitImages(img, ImageFormat.Png);
        //            if (images.Count() > 0)
        //                FirstImage = images[0];
        //        }

        //        ThumbConfig TC = new ThumbConfig();
        //        Image.GetThumbnailImageAbort GCA = new Image.GetThumbnailImageAbort(ThumbAbort);

        //        IntPtr CBData = default(IntPtr);
        //        Image ThumbImage = FirstImage.GetThumbnailImage(TC.Width, TC.Height, GCA, CBData);

        //        //Ruta directorio
        //        string dirPath = Path.Combine(TC.ThumbDirectory, entityId.ToString(), docId.ToString());
        //        //Se crea si no existe
        //        if (!Directory.Exists(dirPath))
        //            Directory.CreateDirectory(dirPath);
        //        //Ruta img a crear
        //        string ThumbPath = Path.Combine(dirPath, file.Name).Replace(".__1", ".Jpeg");
        //        //Se guarda imagen
        //        ThumbImage.Save(ThumbPath, ImageFormat.Jpeg);
        //        if (TC.StoreInDB)
        //        {
        //            base64 = FileTools.Base64.ResizeImage(FirstImage);
        //            //Borrar img
        //            if (File.Exists(ThumbPath))
        //                File.Delete(ThumbPath);
        //        }

        //        FirstImage = null;
        //        ThumbImage = null;
        //        file = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        ZClass.raiseerror(ex);
        //    }
        //    return base64;
        //}

        private static bool ThumbAbort()
        {
            return false;
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static string Excel(string filePath, Int64 entityId, Int64 docId)
        {
            string base64 = string.Empty;
            try
            {
                var ec = new ExcelThumb();
                var image = ec.GenerateThumb(filePath);
                base64 = FileTools.Base64.ResizeImage(image);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return base64;
        }
        public static string PowerPoint(string path, Int64 entityId, Int64 docId)
        {
            string base64 = string.Empty;
            try
            {
                var zfp = new Zamba.FileTools.PPTThumb();
                base64 = zfp.GenerateThumb(path);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return base64;
        }
        public static string Word(string path, Int64 entityId, Int64 docId)
        {
            string base64 = string.Empty;
            try
            {
                var zfw = new WordThumb();
                var image = zfw.GenerateThumb(path);
                if (image != null)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Generando thumb de imagen de word");
                    base64 = FileTools.Base64.ResizeImage(image);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return base64;
        }
        public static string PDF(string path, Int64 entityId, Int64 docId)
        {
            string base64 = string.Empty;
            try
            {
                var zfp = new PDFThumb();
                base64 = zfp.GenerateThumb(path, entityId, docId);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return base64;
        }
        public static string TXT(string path, Int64 entityId, Int64 docId)
        {
            string base64 = string.Empty;
            try
            {
                var zft = new TXT();
                base64 = zft.GenerateThumb(path);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return base64;
        }
        public static string ConvertFromPath(string path, Int64 entityId, Int64 docId)
        {
            var str = string.Empty;
            if (File.Exists(path))
            {
                switch (Path.GetExtension(path).ToLower())
                {
                    case ".img":
                    case ".jpg":
                    case ".jpeg":
                    case ".bmp":
                    case ".dib":
                    case ".gif":
                    case ".tif":
                    case ".tiff":
                    case ".png":
                        str = Image(path, entityId, docId);
                        break;
                    case ".__1":
                        str = Image(path, entityId, docId);
                        break;
                    case ".xls":
                    case ".xlsx":
                    case ".xlsm":
                    case ".csv":
                        str = Excel(path, entityId, docId);
                        break;
                    case ".ppt":
                    case ".pptx":
                        str = PowerPoint(path, entityId, docId);
                        break;
                    case ".doc":
                    case ".docx":
                        str = Word(path, entityId, docId);
                        break;
                    case ".pdf":
                        str = PDF(path, entityId, docId);
                        break;
                    case ".txt":
                        str = TXT(path, entityId, docId);
                        break;
                    default:
                        //ZTrace.WriteLineIf(ZTrace.IsVerbose, "No hay funcion para obtener thumb de" + Path.GetExtension(path).ToLower());
                        break;
                }
            }
            return str;
        }
    }
}
