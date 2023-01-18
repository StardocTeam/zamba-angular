using System;
using System.Drawing;
using System.IO;
using Zamba.Core;

namespace Tesseract
{
    public class ZambaOCR
    {
        public class Rectangular
        {
            public Rectangular(Point point1, Point point2, String sectorName)
            {
                Point1 = point1;
                SectorName = sectorName;
                Point2 = point2;
            }
            public Point Point1 { get; set; }
            public Point Point2 { get; set; }
            public String SectorName { get; set; }
            public String Text { get; set; }
        }

        TesseractEngine engine;
        public ZambaOCR()
        {
            if (engine == null)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando TesseractEngine");
                var language = "spa";//eng
                var langPath = (Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\tessdata").Replace("file:\\", "");
                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("langPath: {0}", langPath));
                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("Language: {0}.", language));
                engine = new TesseractEngine(langPath, language, EngineMode.Default);
            }
        }

        public string GetText(string path, Rectangular rect, string SectorName, string WorkDirectory)
        {
            try
            {
                Pix img = Pix.LoadFromFile(path);
                Page page = engine.Process(img,
                    Rect.FromCoords(rect.Point1.X, rect.Point1.Y, rect.Point2.X, rect.Point2.Y),
                    null);

                img.Deskew();
                Pix newimg = page.GetThresholdedImage();

                FileInfo fi = new FileInfo(path);

                string SectorFileImage = WorkDirectory + "\\" + fi.Name.Replace(".", " Sector " + SectorName + ".PNG.");
                SectorFileImage = SectorFileImage.Substring(0, SectorFileImage.LastIndexOf("."));

                if (File.Exists(SectorFileImage))
                    File.Delete(SectorFileImage);
                newimg.Save(SectorFileImage, ImageFormat.Png);

                //GetPageOrientation here
                string text = page.GetText();
                page.Dispose();
                img.Dispose();
                text = text.Replace("\n", "\r\n");
                rect.Text = text;

                return rect.Text;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
                //return string.Empty;
            }
        }
        public string GetText(string path)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando extraccion de texto de imagen " + path);

                if (engine == null)
                    engine = new TesseractEngine(System.Environment.GetEnvironmentVariable("TESSERACT_PREFIX"), "eng", EngineMode.Default);

                    Pix img = Pix.LoadFromFile(path);
                    Page page = engine.Process(img);
                    img.Deskew();
                    //GetPageOrientation here
                    string text = page.GetText();
                    page.Dispose();
                    img.Dispose();
                    text = text.Replace("\n", "\r\n");
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Extraccion finalizada, texto: " + text);
                return text;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
                //return string.Empty;
            }
        }
        public string GetText(Image imga)
        {
            try
            {                
                if (engine == null)
                    engine = new TesseractEngine(System.Environment.GetEnvironmentVariable("TESSERACT_PREFIX"), "eng", EngineMode.Default);

               Byte[] allbytes = null;
                using (var ms = new MemoryStream())
                {
                    imga.Save(ms, System.Drawing.Imaging.ImageFormat.Tiff);
                    allbytes = ms.ToArray();
                }

                Pix img = Pix.LoadTiffFromMemory(allbytes);
                Page page = engine.Process(img);
                img.Deskew();
                //GetPageOrientation here
                string text = page.GetText();
                page.Dispose();
                img.Dispose();
                text = text.Replace("\n", "\r\n");
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Extraccion finalizada, texto: " + text);
                return text;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
                //return string.Empty;
            }
        }

        public string GetTextFromImage(Image image)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando extraccion de texto de imagen ");
                if (engine == null)
                    engine = new TesseractEngine(Environment.GetEnvironmentVariable("TESSERACT_PREFIX"), "eng", EngineMode.Default);

                Page page = engine.Process(new Bitmap(image));
                //GetPageOrientation here
                string text = page.GetText();
                page.Dispose();
                text = text.Replace("\n", "\r\n");
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Extraccion finalizada, texto: " + text);

                return text;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void ExtractTextToTxt(string imgPath, string txtPath, Rectangular rectangular, Boolean regenerate, string SectorName, string WorkDirectory)
        {
            if (regenerate || !File.Exists(txtPath))
            {
                if (VerifyFiles(imgPath, txtPath))
                {
                    try
                    {
                        string text;

                        if (rectangular == null)
                        {
                            text = GetText(imgPath);
                        }
                        else
                        {
                            text = GetText(imgPath, rectangular, SectorName, WorkDirectory);
                        }

                        text = text.Replace("\n", "\r\n");

                        if (File.Exists(txtPath)) File.Delete(txtPath);
                        File.WriteAllText(txtPath, text);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                        //return string.Empty;
                    }
                }
            }
        }

        private static bool VerifyFiles(string imgPath, string txtPath)
        {
            try
            {
                if (Path.GetDirectoryName(txtPath) != string.Empty && File.Exists(imgPath))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
