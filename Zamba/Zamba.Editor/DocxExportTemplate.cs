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
	internal class TelerikDocxExportTemplate: RadEditorExportTemplate
	{
		public TelerikDocxExportTemplate(RadEditor radEditor)
			: base(radEditor)
		{
		}

		protected override void InitializeXmlContent()
		{
		}

		protected override string GenerateOutput()
		{
			string output = "";

			HtmlFormatProvider htmlProvider = new HtmlFormatProvider();
			RadFlowDocument flowDocument = htmlProvider.Import(editor.Content);

			DocxFormatProvider docxProvider = new DocxFormatProvider();
			byte[] docxBinaryData = docxProvider.Export(flowDocument);

			Stream stream = new MemoryStream(docxBinaryData);

			using (StreamReader reader = new StreamReader(stream, Encoding.Default))
			{
				output = reader.ReadToEnd();
			}

			return output;
		}

		protected override string ContentType
		{
			get { return "application/vnd.openxmlformats-officedocument.wordprocessingml.document"; }
		}

		protected override string FileExtension
		{
			get { return ".docx"; }
		}

		protected override ExportType ExportType
		{
			get { return ExportType.Word; }
		}
	}
}