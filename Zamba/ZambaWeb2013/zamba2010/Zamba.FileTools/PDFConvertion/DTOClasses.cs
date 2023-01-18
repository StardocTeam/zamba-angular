using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spire.Pdf;

namespace Zamba.HTMLToPDFConverter
{
    public class HTMLPDFConvertedPage
    {
        PageType _pageType;
        PdfDocument _pdfFile;
        int _pageOrder;

        public HTMLPDFConvertedPage(PageType type, PdfDocument doc, int order) 
        {
            _pageType = type;
            _pdfFile = doc;
            _pageOrder = order;
        }

        public PageType PageType
        {
            get
            {
                return _pageType;
            }
            set
            {
                _pageType = value;
            }
        }

        public PdfDocument PDFDocument
        {
            get
            {
                return _pdfFile;
            }
            set
            {
                _pdfFile = value;
            }
        }

        public int PageOrder
        {
            get
            {
                return _pageOrder;
            }
            set
            {
                _pageOrder = value;
            }
        }
    }

    internal struct ConverterTaskData
    {
        public string folderName;
        public string header;
        public string htmlString;
        public int pageNumber;
        public PdfPageOrientation orientation;
    }

    public enum PageType
    {
        SinglePage,
        AutoAdjustPage
    }

    public enum PageOrientation
    {
        Portrait = 0,
        Landscape = 1,
    }
}
