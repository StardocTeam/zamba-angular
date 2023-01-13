using System;
using System.Collections.Generic;
using System.Text;
using AdsBusiness;

namespace Ads
{
    class Program
    {
        static void Main(string[] args)
        {

            LoadImages();

           // var a = AdLogic.GetImages(2);
        }

        private static void LoadImages()
        {
            foreach (String file in System.IO.Directory.GetFiles(@"D:\Zamba2008\Proyectos\Upload Photos 0.5\Upload Photos 0.5\temp"))
            {
                AdImage a = new AdImage(file);
                a.LoadImages(file);
                a.AdId = 2;
                a.CreationDate = DateTime.Now;
                    AdsBusiness.AdLogic.Insert(a);
            }
        }
    }
}
