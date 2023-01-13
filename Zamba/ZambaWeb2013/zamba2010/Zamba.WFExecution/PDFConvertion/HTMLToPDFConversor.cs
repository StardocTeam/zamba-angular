using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
//using Zamba.FormBrowser.Helpers;

namespace Zamba.HTMLToPDFConverter
{
    public class HTMLToPDFConversor
    {
        private const string TEMP_HTML_PAGE_FORMAT = "tempReportPage_{0}_{1}.html";
        private const string PAGE_BREAK_TAG = "<zpageBreak";

        public delegate void WriteLogDelegate(string text);
        public WriteLogDelegate WriteLog;

        static PdfUnitConvertor conv = new PdfUnitConvertor();
        float _pagePixelHeigh = 0;
        Random r = new Random();

        //Dictionary<int, Image> _imgsByPage;

        //public HTMLToPDFConversor() 
        //{
        //    //_imgsByPage = new Dictionary<int, Image>();
        //}

        /// <summary>
        /// Convierte html a pdf, respetando saltos de linea fijos y condicionales
        /// </summary>
        /// <param name="html"></param>
        /// <param name="fileName"></param>
        /// <param name="tempFolder"></param>
        /// <param name="reportOrientation"></param>
        /// <returns></returns>
        //public bool ConvertHTMLToPDF(string htmlHeader,string htmlBody, string fileName, string tempFolder, PageOrientation reportOrientation)
        //{
        //    WriteLog("********************Inicio conversion************************");
        //    //Se divide el body en las paginas dadas
        //    List<string> htmlPages = GetPageBySeparatorTag(htmlBody);

        //    //Se convierten las paginas en html "crudo"
        //    List<HTMLPDFConvertedPage> pages = SavePages(tempFolder, htmlHeader, htmlPages, (PdfPageOrientation)reportOrientation);

        //    //Se unen todas las paginas en html final
        //    PdfDocument doc = MergePages(pages.OrderBy(p => p.PageOrder).ToList(), tempFolder, fileName, (PdfPageOrientation)reportOrientation);

        //    //Se guarda el pdf final
        //    doc.SaveToFile(tempFolder + fileName);

        //    doc.Dispose();

        //    WriteLog("********************Fin conversion************************");

        //    return true;
        //}

        /// <summary>
        /// Combina las paginas ya convertidas a pdf en el archivo pdf final
        /// </summary>
        /// <param name="pages"></param>
        /// <param name="tempFolder"></param>
        /// <param name="fileName"></param>
        /// <param name="reportOrientation"></param>
        /// <returns></returns>
        private PdfDocument MergePages(List<HTMLPDFConvertedPage> pages, string tempFolder, string fileName,
            PdfPageOrientation reportOrientation)
        {
            //Documento final
            PdfDocument doc = GetPDFDocument(reportOrientation);
            //Obtenemos el alto total de la hoja
            _pagePixelHeigh = conv.ConvertToPixels(doc.PageSettings.Height, PdfGraphicsUnit.Point);

            //Path del documento final
            string pdfFileName = tempFolder + fileName;

            //Lista de imagenes por pagina
            Dictionary<int, List<Image>> _imgsByPage = new Dictionary<int, List<Image>>();

            foreach (HTMLPDFConvertedPage page in pages)
            {
                //Si la pagina es de tipo single page solo agregará las imagenes
                //sino se realizara el calculo para ver si la imagen entra en la pagina anterior
                if (page.PageType == PageType.SinglePage)
                {
                    AddSinglePage(doc, page);
                }
                else
                {
                    AddConditionalPage(doc, page, pdfFileName, _imgsByPage);
                }

                page.PDFDocument.Dispose();
            }

            _imgsByPage.Clear();

            return doc;
        }

        /// <summary>
        /// Guarda y vuelve a abrir el documento
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="pathToSave"></param>
        /// <returns></returns>
        private PdfDocument RefreshDocument(PdfDocument doc, string pathToSave)
        {
            doc.SaveToFile(pathToSave);
            doc.Close();
            doc.LoadFromFile(pathToSave);
            doc.PageSettings.Margins.All = 10;
            return doc;
        }

        /// <summary>
        /// Agrega las imagenes de la pagina a la ultima pagina si tiene espacio
        /// Si no lo tiene, las agrega en una nueva pagina
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="page"></param>
        /// <param name="filePath"></param>
        private void AddConditionalPage(PdfDocument doc, HTMLPDFConvertedPage page, string filePath,
            Dictionary<int, List<Image>> imgByPage)
        {
            PdfPageBase currPage = null;
            float y;
            Image[] imgs;
            Image imgToDraw;
            int currPageIndex;

            for (int i = 0; i < page.PDFDocument.Pages.Count; i++)
            {
                imgs = page.PDFDocument.Pages[i].ExtractImages();
                if (imgs != null)
                    for (int j = 0; j < imgs.Length; j++)
                    {
                        currPageIndex = doc.Pages.Count - 1;

                        if (currPage == null)
                            currPage = doc.Pages[currPageIndex];

                        //Se obtienen las imagenes de la ultima pagina
                        List<Image> lastPageImages = GetImagesByPage(currPage, currPageIndex, imgByPage);

                        if (lastPageImages != null && lastPageImages.Count > 0)
                        {
                            //Se hace la sumatoria de sus altos
                            double lastPageImgHeight = lastPageImages.Sum((image) => image.Height);

                            double lastPageFreeSpace;
                            //Si la imagen ocupa mas de una hoja
                            //Se calcula el espacio libre de la ultima pagina de esa imagen
                            if (lastPageImgHeight > _pagePixelHeigh)
                                lastPageFreeSpace = _pagePixelHeigh - lastPageImgHeight % _pagePixelHeigh;
                            else
                                lastPageFreeSpace = _pagePixelHeigh - lastPageImgHeight;

                            //Se hace la sumatoria de esas imagenes
                            double currPageImgHeight = imgs[j].Height;

                            //Si las ultimas imagenes agregadas, entran en la hoja anterior
                            if (currPageImgHeight <= lastPageFreeSpace)
                            {
                                y = conv.ConvertFromPixels(((float)(_pagePixelHeigh - lastPageFreeSpace)), PdfGraphicsUnit.Point);

                                imgToDraw = CropImageMargin(doc.PageSettings.Margins, imgs[j]);
                                DrawImage(currPage, imgToDraw, y);

                                //Se actualizan las imagenes que hay para esa pagina
                                currPageIndex = doc.Pages.Count - 1;
                                lastPageImages.Add(imgToDraw);
                                imgByPage[currPageIndex] = lastPageImages;
                            }
                            else
                            {
                                currPage = doc.AppendPage();

                                imgToDraw = CropImageMargin(doc.PageSettings.Margins, imgs[j]);
                                DrawImage(currPage, imgToDraw, 0);

                                //Se agrega a la coleccion la ultima imagen
                                currPageIndex = doc.Pages.Count - 1;
                                imgByPage[currPageIndex] = new List<Image>(new Image[] { imgToDraw });
                            }

                            //Cambiar currentPage por la nueva en el documento principal
                            currPage = doc.Pages[currPageIndex];
                        }
                    }
            }
        }

        /// <summary>
        /// Busca dentro de la coleccion las imagenes de esa pagina, si no estan las extrae
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageNumber"></param>
        /// <param name="imgByPage"></param>
        /// <returns></returns>
        private List<Image> GetImagesByPage(PdfPageBase page, int pageNumber, Dictionary<int, List<Image>> imgByPage)
        {
            if (!imgByPage.ContainsKey(pageNumber))
            {
                List<Image> imgs = new List<Image>(page.ExtractImages());
                imgByPage.Add(pageNumber, imgs);
            }

            return imgByPage[pageNumber];
        }

        /// <summary>
        /// Agrega las imagenes del documento PDF en una nueva pagina
        /// </summary>
        /// <param name="pdfdoc"></param>
        /// <param name="page"></param>
        private static void AddSinglePage(PdfDocument pdfdoc, HTMLPDFConvertedPage page)
        {
            PdfPageBase currPage;
            Image[] imgs;

            currPage = pdfdoc.AppendPage();
            for (int i = 0; i < page.PDFDocument.Pages.Count; i++)
            {
                imgs = page.PDFDocument.Pages[i].ExtractImages();
                if (imgs != null)
                {
                    for (int j = 0; j < imgs.Length; j++)
                    {
                        DrawImage(currPage, imgs[j], 0);
                    }
                }
            }
        }

        /// <summary>
        /// Dibuja la imagen dada en la pagina y altura determinada
        /// </summary>
        /// <param name="currPage"></param>
        /// <param name="img"></param>
        /// <param name="y"></param>
        private static void DrawImage(PdfPageBase currPage, Image img, float y)
        {
            PdfImage pdfImg = PdfImage.FromImage(img);
            //PdfImage pdfImg = PdfImage.FromImage(img);
            float width = pdfImg.PhysicalDimension.Width;
            float height = pdfImg.PhysicalDimension.Height;

            //Se calcula el eje x para dibujar la imagen centrada
            float x = (currPage.Canvas.ClientSize.Width - width) / 2;

            currPage.Canvas.DrawImage(pdfImg, x, y, width, height);
        }

        /// <summary>
        /// Quita a la imagen los margenes dados
        /// </summary>
        /// <param name="margin"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        private static Image CropImageMargin(PdfMargins margin, Image img)
        {
            float toCrop = conv.ConvertToPixels(margin.Top, PdfGraphicsUnit.Point);
            //Corte margen superior
            Image crpPedImg = cropImage(img, new Rectangle(0, (int)toCrop, img.Width, ((int)(img.Height - toCrop))));
            //Corte margen inferior
            crpPedImg = cropImage(crpPedImg, new Rectangle(0, 0, crpPedImg.Width, ((int)(crpPedImg.Height - toCrop + 2))));

            return crpPedImg;
        }

        /// <summary>
        /// Guarda las paginas html previamente divididas en documentos PDF seprados
        /// </summary>
        /// <param name="tempFolder">Path fisico de la carpeta donde se guardaran los temporales</param>
        /// <param name="htmlHeader">Header de las paginas HTML</param>
        /// <param name="htmlPages">Lista con las paginas a guardar</param>
        /// <param name="reportOrientation">Orientacion de la hoja en el reporte</param>
        /// <returns></returns>
        private List<HTMLPDFConvertedPage> SavePages(string tempFolder, string htmlHeader,
            List<string> htmlPages, PdfPageOrientation reportOrientation)
        {
            List<HTMLPDFConvertedPage> pagesToReturn = new List<HTMLPDFConvertedPage>();

            //ParallelLoopResult res = Parallel.For(0, htmlPages.Count, i => {
            //    pagesToReturn.Add(SavePage(tempFolder, htmlHeader, htmlPages[i], i, reportOrientation));
            //});

            //Se genera un array de parallel tasks
            Task<HTMLPDFConvertedPage>[] taskArray = new Task<HTMLPDFConvertedPage>[htmlPages.Count];
            for (int i = 0; i < taskArray.Length; i++)
            {
                //Se instanciarán e iniciaran una parallel task por cada pagina para hacer su conversion a pdf
                taskArray[i] = Task<HTMLPDFConvertedPage>.Factory.StartNew((objData) =>
                {
                    ConverterTaskData dt = (ConverterTaskData)objData;
                    return SavePage(dt.folderName, dt.header, dt.htmlString, dt.pageNumber, dt.orientation);
                }, new ConverterTaskData
                {
                    folderName = tempFolder,
                    header = htmlHeader,
                    htmlString = htmlPages[i],
                    pageNumber = i,
                    orientation = reportOrientation
                });
            }

            //Se agrega mute para esperar que termine la conversion completa
            Task.WaitAll(taskArray);

            for (int i = 0; i < taskArray.Length; i++)
            {
                //Se agregan las paginas convertidas a la coleccion
                pagesToReturn.Add(taskArray[i].Result);
            }

            return pagesToReturn;
        }

        /// <summary>
        /// Convierte la porcion dada de HTML a PDF
        /// </summary>
        /// <param name="tempFolder"></param>
        /// <param name="htmlHeader"></param>
        /// <param name="htmlPage"></param>
        /// <param name="pageNumber"></param>
        /// <param name="reportOrientation"></param>
        /// <returns></returns>
        private HTMLPDFConvertedPage SavePage(string tempFolder, string htmlHeader, string htmlPage,
            int pageNumber, PdfPageOrientation reportOrientation)
        {
            WriteLog("Inicio guardado pagina:" + pageNumber.ToString());

            //Se genera un nuevo documento pdf
            PdfDocument pdfdoc = GetPDFDocument(reportOrientation);
            string tempHTMLfileName;
            PageType pageType;
            StringBuilder sb = new StringBuilder();

            //Se arma el path para el html temporal
            sb.Append(tempFolder);
            tempHTMLfileName = sb.AppendFormat(TEMP_HTML_PAGE_FORMAT, pageNumber, r.Next()).ToString();
            sb.Clear();

            //Se escribe el html en archivo temporal
            using (StreamWriter sw = new StreamWriter(tempHTMLfileName, false, Encoding.UTF8))
            {
                sw.Write(sb.AppendFormat("<html><head>{0}</head><body>{1}</body></html>", htmlHeader, htmlPage));
                sw.Close();
            }

            WriteLog("Inicio conversion pagina:" + pageNumber.ToString());
            //Se genera un nuevo hilo para que funcione la conversion de spire
            Thread t = new Thread(new System.Threading.ParameterizedThreadStart((parameters) =>
            {
                object[] par = (object[])parameters;
                PdfDocument doc = (PdfDocument)par[0];
                string htmlFilename = (string)par[1];
                string fileName = (string)par[2];

                doc.LoadFromHTML(htmlFilename, true, true, true);
                //doc.SaveToFile(fileName);
            }));
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start(new object[] { pdfdoc, tempHTMLfileName, "tempPDFPage" + pageNumber.ToString() + ".pdf" });

            //Se espera a que termine el hilo
            t.Join();

            WriteLog("Fin conversion pagina:" + pageNumber.ToString());
            pageType = (htmlPage.Contains("<zpageBreakConditional")) ? PageType.AutoAdjustPage : PageType.SinglePage;
            WriteLog("Fin guardado pagina:" + pageNumber.ToString());

            //Borra el temporal
            File.Delete(tempHTMLfileName);

            return new HTMLPDFConvertedPage(pageType, pdfdoc, pageNumber);
        }

        /// <summary>
        /// Instancia un nuevo documento PDF con la orientacion de pagina dada y margenes en 10
        /// </summary>
        /// <param name="pdfPageOrientation"></param>
        /// <returns></returns>
        private PdfDocument GetPDFDocument(PdfPageOrientation pdfPageOrientation)
        {
            PdfDocument pdfdoc = new PdfDocument();
            pdfdoc.PageSettings.Orientation = pdfPageOrientation;
            pdfdoc.PageSettings.Margins.All = 10;
            return pdfdoc;
        }

        //private List<string> GetPageBySeparatorTag(string htmlText)
        //{
        //    return Zamba.FormBrowser.Helpers.HTML.GetPageBySeparatorTag(htmlText);
        //}

        //private static string getHtmlSection(string HTML, HTMLSection Section)
        //{
        //    return Zamba.FormBrowser.Helpers.HTML.getHtmlSection(HTML, Section);
        //}
        
        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }
    }
}
