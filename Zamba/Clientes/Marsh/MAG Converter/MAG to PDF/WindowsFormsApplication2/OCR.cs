using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication2
{
    public class OCR
    {
        public OCR() {
                      
        }
   

            #region Methods

            /// <summary>
            ///  Extract Image from PDF file and Store in Image Object
            /// </summary>
            /// <param name="PDFSourcePath">Specify PDF Source Path</param>
            /// <returns>List</returns>
            public static List<System.Drawing.Image> ExtractImages(String PDFSourcePath)
            {
                List<System.Drawing.Image> ImgList = new List<System.Drawing.Image>();

                iTextSharp.text.pdf.RandomAccessFileOrArray RAFObj = null;
                iTextSharp.text.pdf.PdfReader PDFReaderObj = null;
                iTextSharp.text.pdf.PdfObject PDFObj = null;
                iTextSharp.text.pdf.PdfStream PDFStremObj = null;

                try
                {
                    RAFObj = new iTextSharp.text.pdf.RandomAccessFileOrArray(PDFSourcePath);
                    PDFReaderObj = new iTextSharp.text.pdf.PdfReader(RAFObj, null);

                    for (int i = 0; i <= PDFReaderObj.XrefSize - 1; i++)
                    {
                        PDFObj = PDFReaderObj.GetPdfObject(i);

                        if ((PDFObj != null) && PDFObj.IsStream())
                        {
                            PDFStremObj = (iTextSharp.text.pdf.PdfStream)PDFObj;
                            iTextSharp.text.pdf.PdfObject subtype = PDFStremObj.Get(iTextSharp.text.pdf.PdfName.SUBTYPE);

                            if ((subtype != null) && subtype.ToString() == iTextSharp.text.pdf.PdfName.IMAGE.ToString())
                            {
                                try
                                {

                                    iTextSharp.text.pdf.parser.PdfImageObject PdfImageObj =
                             new iTextSharp.text.pdf.parser.PdfImageObject((iTextSharp.text.pdf.PRStream)PDFStremObj);

                                    System.Drawing.Image ImgPDF = PdfImageObj.GetDrawingImage();


                                    ImgList.Add(ImgPDF);

                                }
                                catch (Exception)
                                {

                                }
                            }
                        }
                    }
                    PDFReaderObj.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return ImgList;
            }


            /// <summary>
            ///  Write Image File
            /// </summary>
            private static void WriteImageFile()
            {
                try
                {
                    System.Console.WriteLine("Wait for extracting image from PDF file....");

                    // Get a List of Image
                    List<System.Drawing.Image> ListImage = ExtractImages(@"C:\Users\Kishor\Desktop\TuterPDF\ASP.net\ASP.NET 3.5 Unleashed.pdf");

                    for (int i = 0; i < ListImage.Count; i++)
                    {
                        try
                        {
                            // Write Image File
                            ListImage[i].Save(AppDomain.CurrentDomain.BaseDirectory + "ImageStore\\Image" + i + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                            System.Console.WriteLine("Image" + i + ".jpeg write sucessfully");
                        }
                        catch (Exception)
                        { }
                    }

                }
                catch (Exception ex)
                {
                Console.WriteLine(ex.Message);

            }
        }
            #endregion
        }
} 
