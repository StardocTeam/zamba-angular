using Spire.XLS;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Spire.Doc;
using Spire.Xls;

namespace Zamba.FileTools
{
    public class ExcelThumb
    {
        public Image  GenerateThumb(string path)
        {
            var workbook = new Workbook();
            workbook.LoadFromFile(path);           
            Worksheet sheet = workbook.Worksheets[0];
            //sheet.SaveToImage("c:\\sample.jpg");
            var image=sheet.SaveToImage(1,1,30,20);
            return image;
        }
    }
}

