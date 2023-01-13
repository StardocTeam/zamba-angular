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

namespace Zamba.Thumbs
{
    //public class Base64
    //{
    //    public static string Image(string path)
    //    {
    //        string base64 = string.Empty;
    //        try
    //        {
    //            var img = new Bitmap(path);
    //            base64 = FileTools.Base64.ResizeImage(img);
    //        }
    //        catch (Exception ex)
    //        {
    //            ZClass.raiseerror(ex);
    //        }
    //        return base64;
    //    }
    //    public static string Excel(string path)
    //    {
    //        string base64 = string.Empty;
    //        try
    //        {
    //            var et = new ExcelThumb();
    //            var image = et.GenerateThumb(path);
    //            base64 = FileTools.Base64.ResizeImage(image);
    //        }
    //        catch (Exception ex)
    //        {
    //            ZClass.raiseerror(ex);
    //        }
    //        return base64;
    //    }
    //    public static string PowerPoint(string path)
    //    {
    //        string base64 = string.Empty;
    //        try
    //        {
    //            var zfp = new Zamba.FileTools.PPTThumb();
    //            base64 = zfp.GenerateThumb(path);
    //        }
    //        catch (Exception ex)
    //        {
    //            ZClass.raiseerror(ex);
    //        }
    //        return base64;
    //    }
    //    public static string Word(string path)
    //    {
    //        string base64 = string.Empty;
    //        try
    //        {
    //            var zfw = new Zamba.FileTools.WordThumb();
    //            var image = zfw.GenerateThumb(path);
    //            base64 = FileTools.Base64.ResizeImage(image);
    //        }
    //        catch (Exception ex)
    //        {
    //            ZClass.raiseerror(ex);
    //        }
    //        return base64;
    //    }
    //    public static string PDF(string path)
    //    {
    //        string base64 = string.Empty;
    //        try
    //        {
    //            var zfp = new Zamba.FileTools.PDFThumb();
    //            base64 = zfp.GenerateThumb(path);
    //        }
    //        catch (Exception ex)
    //        {
    //            ZClass.raiseerror(ex);
    //        }
    //        return base64;
    //    }
    //}
}
