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
using Zamba.Core;

namespace Zamba.FileTools
{
    public class ExcelThumb
    {
        public Image GenerateThumb(string path)
        {
            Image image = null;
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Generando Img thumb de excel " + path);
            try
            {
                var workbook = new Workbook();
                workbook.LoadFromFile(path);
                Worksheet sheet = workbook.Worksheets[0];
                //sheet.SaveToImage("c:\\sample.jpg");
                image = sheet.SaveToImage(1, 1, 30, 20);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Generacion Img thumb de excel exitosa " + path);
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al generar img thumb excel " + ex.ToString());
            }
            return image;
        }
    }
}

