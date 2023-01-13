using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Xml.Xsl;

using Telerik.Web.UI;
using Telerik.Web.UI.Editor.Export;

using Telerik.Windows.Zip;
using Telerik.Windows.Documents;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.FormatProviders.Html;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;

namespace Zamba.Editor
{
    internal class DocxToHtmlConverter
    {

		public string ConvertDocxToHtml(byte[] binaryData)
		{
			DocxFormatProvider docxProvider = new DocxFormatProvider();
			RadFlowDocument flowDoc = docxProvider.Import(binaryData);

			HtmlFormatProvider htmlProvider = new HtmlFormatProvider();
			string result = htmlProvider.Export(flowDoc);

			return result;
		}

	}
}