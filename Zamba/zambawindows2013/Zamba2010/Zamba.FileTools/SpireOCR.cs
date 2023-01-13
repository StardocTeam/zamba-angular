using System;
using System.Collections.Generic;
using System.Text;
using Spire.Pdf;
using System.Drawing;
using System.IO;
using Tesseract;
using Zamba.Core;

namespace Zamba.FileTools
{
    public class SpireOCR
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
                    if (Directory.Exists(WorkDirectory) == false) Directory.CreateDirectory(WorkDirectory);

                    ZOCR.ExtractTextToTxt(imagefile, Textfile, null, true, string.Empty, WorkDirectory);
                    File.Delete(imagefile);
                    ZOCR = null;
                    //ImageOCR.ExtractText(imagefile, Textfile);
                }

            }

        }

        public string GetTextFromPDF(String PDFSourcePath, int docID)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Comenzando extraccion texto de documento PDF");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("Ruta del documento {0}", PDFSourcePath));
                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(PDFSourcePath);
                StringBuilder pdfText = new StringBuilder();

                StringBuilder buffer = new StringBuilder();
                List<Image> images = new List<Image>();

                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("Cantidad paginas del PDF {0}", doc.Pages.Count));
                foreach (PdfPageBase page in doc.Pages)
                {
                    if (page != null)
                    {
                        buffer.Append(page.ExtractText());
                        Image[] extractedImages = page.ExtractImages();
                        if (extractedImages != null)
                        {
                            foreach (Image image in extractedImages)
                            {
                                if (image != null)
                                    images.Add(image);
                            }
                        }
                    }
                }

                doc.Close();

                pdfText.Append(buffer.ToString());
                buffer = null;

                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("Cantidad de imagenes del PDF {0}", images.Count));
                List<String> newImages = new List<string>();

                //  string indexerImagesDirectory = Path.Combine(Membership.MembershipHelper.AppConfigPath, "indexertemp", docID.ToString());
                //  ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("Carpeta donde se almancenaran las imagenes {0}", indexerImagesDirectory));

                ZambaOCR ZOCR = new ZambaOCR();

                //  Directory.CreateDirectory(indexerImagesDirectory);
                int index = 0;
                foreach (Image image in images)
                {
                //    if (Directory.Exists(indexerImagesDirectory))
                //    {
                //        string imageFileName = Path.Combine(indexerImagesDirectory, String.Format("Image-{0}.png", index++));
                //        ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("Guardando imagen {0}", String.Format("Image-{0}.png", index)));
                //        image.Save(imageFileName, System.Drawing.Imaging.ImageFormat.Png);
                //        newImages.Add(imageFileName);
                //    }
                //}

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Extrayendo texto de las imagenes");

              
                //foreach (string imagefile in newImages)
                //{
                    //ZambaOCR ZOCR = new ZambaOCR();
                    try
                    {
                        pdfText.Append(ZOCR.GetText(image));
                      //  ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("Extrayendo texto imagen {0}", imagefile.ToString()));
                    }
                    catch (Exception e)
                    {
                        ZClass.raiseerror(e);
                    }
                    //ZOCR = null;
                }
                ZOCR = null;

               // ZTrace.WriteLineIf(ZTrace.IsVerbose, "Eliminando carpeta temporal con imagenes");
                //if (Directory.Exists(indexerImagesDirectory))
                //    Directory.Delete(indexerImagesDirectory, true);

                return pdfText.ToString();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return string.Empty;
            }
        }



        public string GetTextFromImage(String PDFSourcePath, int docID)
        {
            StringBuilder ExtractedText = new StringBuilder();

            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Comenzando extraccion texto de documento Imagenes");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("Ruta del documento {0}", PDFSourcePath));

                List<Image> images = new List<Image>();
                if (PDFSourcePath.EndsWith("TIF") || PDFSourcePath.EndsWith("TIFF") || PDFSourcePath.EndsWith("__1"))
                {
                     
                    Image image = Image.FromFile(PDFSourcePath);
                    Image[] img = SpireTools.SplitImages(image, System.Drawing.Imaging.ImageFormat.Png);

                    for (int i = 0; i < img.Length; i++)
                    {
                        images.Add(img[i]);
                    }
                }
                else
             {
                    Image img = Image.FromFile(PDFSourcePath);
                if (img != null)
                    images.Add(img);
            }

                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("Cantidad de imagenes del PDF {0}", images.Count));
                List<String> newImages = new List<string>();

                string indexerImagesDirectory = Path.Combine(Membership.MembershipHelper.AppTempPath, "indexertemp", docID.ToString());
                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("Carpeta donde se almancenaran las imagenes {0}", indexerImagesDirectory));

                Directory.CreateDirectory(indexerImagesDirectory);
                int index = 0;
                foreach (Image image in images)
                {
                    if (Directory.Exists(indexerImagesDirectory))
                    {
                        string imageFileName = Path.Combine(indexerImagesDirectory, String.Format("Image-{0}.png", index++));
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("Guardando imagen {0}", String.Format("Image-{0}.png", index)));
                        image.Save(imageFileName, System.Drawing.Imaging.ImageFormat.Png);
                        newImages.Add(imageFileName);
                    }
                }

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Extrayendo texto de las imagenes");

                ZambaOCR ZOCR = new ZambaOCR();
                foreach (string imagefile in newImages)
                {
                    try
                    {
                        ExtractedText.Append(ZOCR.GetText(imagefile));
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("Extrayendo texto imagen {0}", imagefile.ToString()));
                        File.Delete(imagefile);
                    }
                    catch (Exception e)
                    {
                        ZClass.raiseerror(e);
                    }
                    //ZOCR = null;
                }
                ZOCR = null;

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Eliminando carpeta temporal con imagenes");
                if (Directory.Exists(indexerImagesDirectory))
                    Directory.Delete(indexerImagesDirectory, true);

                return ExtractedText.ToString();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return string.Empty;
            }
        }
    }

}


