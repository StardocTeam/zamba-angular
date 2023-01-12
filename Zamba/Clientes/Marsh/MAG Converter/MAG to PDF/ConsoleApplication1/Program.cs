using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using static Tesseract.ZambaOCR;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            var lr = new List<Rectangular>();
           
     

            lr.Add(new Rectangular
            {
                Point1 = new Point(63, 333),
                Point2 = new Point(800, 443)
                //Point2 = new Point(382, 425)
            });

            var asdasdasdasd = ZambaOCR.GetText("c:\\c.jpg", lr).First().Text;
            var asdasfxcvxcv = ZambaOCR.GetText("c:\\c.jpg", lr).Last().Text;

        }
    }
}
