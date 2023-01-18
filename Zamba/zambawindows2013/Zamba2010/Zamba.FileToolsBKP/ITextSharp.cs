using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zamba.FileTools
{
    public class ITextSharp
    {
        public string GetTextFromPDF(string path)
        {
            var text = new StringBuilder();

            // The PdfReader object implements IDisposable.Dispose, so you can
            // wrap it in the using keyword to automatically dispose of it
            using (var pdfReader = new iTextSharp.text.pdf.PdfReader(path))
            {
                // Loop through each page of the document
                for (var page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    iTextSharp.text.pdf.parser.ITextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();

                    var currentText = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(
                        pdfReader,
                        page,
                        strategy);

                    currentText =
                        Encoding.UTF8.GetString(Encoding.Convert(
                            Encoding.Default,
                            Encoding.UTF8,
                            Encoding.Default.GetBytes(currentText)));

                    text.Append(currentText);
                    text.Append(" ");

                    strategy = null;
                    currentText = null;
                }
            }

            return text.ToString();
        }
    }
}
