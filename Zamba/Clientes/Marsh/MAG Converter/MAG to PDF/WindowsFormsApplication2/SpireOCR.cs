using System;
using System.Collections.Generic;
using System.Text;
using Spire.Pdf;
using System.Drawing;
using System.IO;
using Tesseract;
using Zamba.Core;

namespace WindowsFormsApplication2
{
    class SpireOCR
    {

        public static void ExtractImages(String PDFSourcePath)
        {
            FileInfo fi = new FileInfo(PDFSourcePath);
            String Textfile = fi.Directory.FullName + "\\" + fi.Name.Substring(0, fi.Name.LastIndexOf(".")) + "\\" + fi.Name.Substring(0, fi.Name.LastIndexOf(".")) + ".txt";

            FileInfo newfi = new FileInfo(Textfile);
            if (newfi.Directory.Exists == false)
            {
                newfi.Directory.Create();

                //Create a pdf document.
                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(PDFSourcePath);

                StringBuilder buffer = new StringBuilder();
                IList<Image> images = new List<Image>();

                foreach (PdfPageBase page in doc.Pages)
                {
                    buffer.Append(page.ExtractText());
                    foreach (Image image in page.ExtractImages())
                    {
                        images.Add(image);
                    }
                }

                doc.Close();

                //save text
                File.WriteAllText(Textfile, buffer.ToString());


                List<String> newImages = new List<string>();
                //save image
                int index = 0;
                foreach (Image image in images)
                {
                    String imageFileName
                        = String.Format(fi.Name.Substring(0, fi.Name.LastIndexOf(".")) + "-{0}.png", index++);
                    image.Save(imageFileName, System.Drawing.Imaging.ImageFormat.Png);
                    newImages.Add(imageFileName);
                }

                foreach (string imagefile in newImages)
                {
                    ZambaOCR ZOCR = new ZambaOCR();

                    string WorkDirectory = new FileInfo(imagefile).Directory.FullName + "\\Work";
                    if (System.IO.Directory.Exists(WorkDirectory) == false) System.IO.Directory.CreateDirectory(WorkDirectory);

                    ZOCR.ExtractTextToTxt(imagefile, Textfile, null, true, string.Empty, WorkDirectory);
                    ZOCR = null;
                    //ImageOCR.ExtractText(imagefile, Textfile);
                }

            }

        }
    }

}


