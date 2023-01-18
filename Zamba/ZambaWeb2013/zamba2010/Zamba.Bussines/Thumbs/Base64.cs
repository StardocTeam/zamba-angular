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

namespace Zamba.Thumbs
{
    public class Base64
    {
        public static string Image(string path)
        {
            string base64 = string.Empty;
            try
            {
                var img = new Bitmap(path);
                base64 = FileTools.Base64.ResizeImage(img);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return base64;
        }
        public static string Excel(string filePath)
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
        public static string PowerPoint(string path)
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
        public static string Word(string path)
        {
            string base64 = string.Empty;
            try
            {
                var zfw = new Zamba.FileTools.WordThumb();
                var image = zfw.GenerateThumb(path);
                base64 = FileTools.Base64.ResizeImage(image);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return base64;
        }
        public static string PDF(string path)
        {
            string base64 = string.Empty;
            try
            {
                var zfp = new Zamba.FileTools.PDFThumb();
                base64 = zfp.GenerateThumb(path);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return base64;
        }
        public static string TXT(string path)
        {
            string base64 = string.Empty;
            try
            {
                var zft = new Zamba.FileTools.TXT();
                base64 = zft.GenerateThumb(path);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return base64;
        }
        public static string ConvertFromPath(string path)
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
                    case ".png":
                        str = Image(path);
                        break;
                    case ".xls":
                    case ".xlsx":
                    case ".xlsm":
                    case ".csv":
                        str = Excel(path);
                        break;
                    case ".ppt":
                    case ".pptx":
                        str = PowerPoint(path);
                        break;
                    case ".doc":
                    case ".docx":
                        str = Word(path);
                        break;
                    case ".pdf":
                        str = PDF(path);
                        break;
                    case ".txt":
                        str = TXT(path);
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
