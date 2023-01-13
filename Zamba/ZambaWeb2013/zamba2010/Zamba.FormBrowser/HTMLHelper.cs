namespace Zamba.FormBrowser.Helpers
{
    using System;
    using System.Web;
    using System.Text.RegularExpressions;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Zamba.Membership;
    using Core;
    public enum HTMLSection
	{
		HEAD,
		FORM,
		BODY,
        ALL
	}

	public static class HTML
	{
		internal const string ATTRIBUTETEMPLATE = "{0}=\"";
		internal const string START_CLASS_ATTRIBUTE = "class=\"";
		internal const string CLASS_ATTRIBUTE_FORMAT = "class=\"{0}\"";
		internal const char END_CHAR_TAG = '>';
		internal const string CLOSE_CHAR_TAG = "/>";
		internal const string START_INPUT = "<input";
		internal const string START_TEXTAREA = "<textarea";
		internal const string START_SELECT = "<select";
		internal const string START_TABLE = "<table";
		internal const string QUOTE = "\"";
		internal const char SPACE_CHAR = ' ';
		internal static int simpleDateLenght = DateTime.Now.ToString("dd/mm/yyyy").Length;
		internal static int fullDateLenght = (DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss tt").Length + 2);
		internal const string LENGTH_ATTRIBUTE_FORMAT = "length=\"{0}\"";
		internal const string ZMINVALUE_ATTRIBUTE_FORMAT = "ZMinValue=\"{0}\"";
		internal const string ZMAXVALUE_ATTRIBUTE_FORMAT = "ZMaxValue=\"{0}\"";
		internal const string DEFAULTVALUE_ATTRIBUTE_FORMAT = "DefaultValue=\"{0}\"";
		internal const string INDEXNAME_ATTRIBUTE_FORMAT = "indexName=\"{0}\"";
		internal const string ORIGINALVALUE_ATTRIBUTE_FORMAT = "ZOriginalValue=\"{0}\"";
		internal const string DATATYPE_ATTRIBUTE_FORMAT = "dataType=\"{0}\"";
		internal const string HIERARCHYCHILD_ATTRIBUTE_FORMAT = "ChildIndexId=\"{0}\"";
		internal const string TABLE_HEADER_FORMAT = "<th>{0}</th>";
		internal const string TABLE_HEADER_INVISIBLE_FORMAT = "<th style=\"display:none\">{0}</th>";
		internal const string SELECT_ENDTAG = "</select>";
		internal const string TEXTAREA_ENDTAG = "</textarea>";
		internal const string TABLE_ENDTAG = "</table>";

		private const string ATTRIBUTE_SELECTOR_REGEX_FORMAT = "(?i){0}=[\"']?((?:.(?![\"']?\\s+(?:\\S+)=|[>\"']))+.)[\"']?";
		private const string ZVAR_SELECTOR_REGEX_FORMAT = "zamba_zvar\\([\"']?((?:.(?![\"']?\\s+(?:\\S+)=|[>\"']))+.)[\"']?\\)";
		private const string HTMLITEM_SELECTOR_REGEX_FORMAT = "(?i)<{0}\\b[^>]*>[\\s]?[\\r]?[\\n]?[\\t]?(.*?)?[\\r]?[\\s]?[\\n]?[\\t]?</{0}>";
		private const string HTML_SELFCLOSED_ITEM_SELECTOR_REGEX_FORMAT = "(?i)<{0}[\\s]?[\\r]?[\\n]?[\\t]?(.*?)?[\\r]?[\\s]?[\\n]?[\\t]?/>";
		private static string HTMLITEM_SELECTOR_ADDITIONAL_PARAM_REGEX_FORMAT = "(?i)<{0}[^<>]*{1}[^<>]*>[\\s]?[\\r]?[\\n]?[\\t]?(.*?)?[\\r]?[\\s]?[\\n]?[\\t]?</{0}>";
		private static string HTML_SELFCLOSED_ITEM_SELECTOR_ADDITIONAL_PARAM_REGEX_FORMAT = "(?i)<{0}[^</>]*{1}[^</>]*/>";

        private const string TEMP_HTML_PAGE_FORMAT = "tempReportPage_{0}_{1}.html";
        private const string PAGE_BREAK_TAG = "<zpageBreak";

		static string _taskSelectorURL = null;
		public static string GetTaskSelectorURL(string CurrentTheme)
		{
			if (string.IsNullOrEmpty(_taskSelectorURL))
			{
				StringBuilder sb = new StringBuilder();

				if (HttpContext.Current == null)
					sb.Append(MembershipHelper.AppFormPath(CurrentTheme));
				else
				{
					HttpRequest req = HttpContext.Current.Request;
					sb.Append("http://");
					sb.Append(req.ServerVariables["HTTP_HOST"]);
					sb.Append(req.ApplicationPath);
				}

				sb.Append("/Views/WF/TaskSelector.ashx?docid={0}&doctypeid={1}");
                sb.Append("&userId=");
                sb.Append(Zamba.Membership.MembershipHelper.CurrentUser.ID);

                _taskSelectorURL = sb.ToString();
			}

			return _taskSelectorURL;
		}

		static string _executeImgURL = null;
		public static string GetExecuteImgURL(string CurrentTheme)
		{
			if (string.IsNullOrEmpty(_executeImgURL))
			{
				StringBuilder sb = new StringBuilder();

				if (HttpContext.Current == null)
					sb.Append(MembershipHelper.AppFormPath(CurrentTheme));
				else
				{
					HttpRequest req = HttpContext.Current.Request;
					sb.Append("http://");
					sb.Append(req.ServerVariables["HTTP_HOST"]);
					sb.Append(req.ApplicationPath);
				}

				sb.Append("/Content/Images/Toolbars/play.png");
				_executeImgURL = sb.ToString();
			}

			return _executeImgURL;
		}

		public static string getHtmlSection(string HTML, HTMLSection Section)
		{
			string sec = Section.ToString().ToLower();

			String strHtmlLower = HTML.ToLower();
			if (strHtmlLower.Contains(sec))
			{
				Int32 iniSectionTag = strHtmlLower.IndexOf("<" + sec);
				Int32 iniSectionTagClose = strHtmlLower.Substring(iniSectionTag).IndexOf(">") + 1;
				Int32 totIndexOfSectionTag = iniSectionTag + iniSectionTagClose;
				Int32 endSectionTab = HTML.ToLower().IndexOf("</" + sec + ">");
				String strContent = HTML.Substring(totIndexOfSectionTag, endSectionTab - totIndexOfSectionTag);

				//Reemplaza las referencias en el formulario
                strContent = GenerateRelativeReferences(strContent);

				//se reemplaza este caracter ya que provoca error en el formulario
				strContent = strContent.Replace(char.ConvertFromUtf32(65533), string.Empty);

				return strContent;
			}
			else
			{
				if (Section == HTMLSection.FORM)
				{
					return getHtmlSection(HTML, HTMLSection.BODY);
				}
				else
					return HTML;
			}
		}

		/// <summary>
		/// Método de ayuda para reeplazar las referencias de html(src,href) a rutas Relativas
		/// </summary>
		/// <param name="HTML"></param>
		/// <returns></returns>
		public static string GenerateRelativeReferences(string HTML)
		{
            return FormatReferences(HTML, GetRelativeFormPath(), false);
		}

        /// <summary>
        /// Método de ayuda para reeplazar las referencias de html(src,href) a rutas Absolutas
        /// </summary>
        /// <param name="HTML"></param>
        /// <returns></returns>
        public static string GenerateAbsoluteReferences(string HTML)
        {
            return FormatReferences(HTML, MembershipHelper.AppUrl, true);
        }

        /// <summary>
        /// Método de ayuda para reeplazar las referencias de html(src,href) 
        /// </summary>
        /// <param name="HTML"></param>
        /// <returns></returns>
        private static string FormatReferences(string HTML, string path, bool replaceRelativePaths)
        {
            string strToReturn = string.Empty;

            strToReturn = ReplaceValuesWithDirectory(HTML, "src", path, replaceRelativePaths);
            strToReturn = ReplaceValuesWithDirectory(strToReturn, "href", path, replaceRelativePaths);

            return strToReturn;
        }

		public static string GetRelativeFormPath()
		{
            ZOptBusiness zoptb = new ZOptBusiness();
            String CurrentTheme = zoptb.GetValue("CurrentTheme");
            zoptb = null;
            if (HttpContext.Current == null)
				return Zamba.Membership.MembershipHelper.AppFormPath(CurrentTheme);

			Uri dirUri;
			Uri toReplaceUri;
			//Obtenemos el path actual
			string executionPath = HttpContext.Current.Request.ServerVariables["PATH_INFO"];
			string currDirectory = HttpContext.Current.Server.MapPath(executionPath.Substring(0, executionPath.LastIndexOf('/') + 1));
			dirUri = new Uri(currDirectory);
			//Generamos un uri para elpath de los formularios
			toReplaceUri = new Uri(Zamba.Membership.MembershipHelper.AppFormPath(CurrentTheme));

			return dirUri.MakeRelativeUri(toReplaceUri).ToString();
		}

		/// <summary>
		/// Agrega a las referencias en el atributo dado, el directorio pasado por parámetro
		/// </summary>
		/// <param name="Text"></param>
		/// <param name="Attribute"></param>
		/// <param name="Directory"></param>
		/// <returns></returns>
		private static string ReplaceValuesWithDirectory(string Text, string Attribute, string Directory, bool replaceRelativePaths)
		{
			string strToReturn = Text;
			string strAttributeToFind = string.Format(ATTRIBUTETEMPLATE, Attribute);
			string strToReplaceValue;
			int attributeToFindLenght = strAttributeToFind.Length;
			int attributeStartIndex = strToReturn.IndexOf(strAttributeToFind);
			int attributeEndIndex;

			//Busca todos los atributos
			while (attributeStartIndex != -1)
			{
				//Obtiene la comilla de cierre del atributo
				attributeEndIndex = strToReturn.IndexOf('"', attributeStartIndex + attributeToFindLenght + 1);

				//Si el inicio de la cadena es menor al cierre de las comillas,entonces la cadena debería ser válida
				if (attributeStartIndex < attributeEndIndex)
				{
					//Obtenemos el valor del atributo
					strToReplaceValue = strToReturn.Substring(attributeStartIndex + attributeToFindLenght, attributeEndIndex - (attributeStartIndex + attributeToFindLenght));

					//Si el atributo inicia con la llamada a un servicio, entonces no lo cambiamos
                    if (!strToReplaceValue.StartsWith("http://"))
                    {
                        //Si el atributo inicia con un path relativo no es procesado
                        if (!strToReplaceValue.StartsWith(".."))
                        {
                            strToReplaceValue = Directory + '/' + strToReplaceValue;

                            //Removemos el viejo valor del atributo
                            strToReturn = strToReturn.Remove(attributeStartIndex + attributeToFindLenght,
                                attributeEndIndex - (attributeStartIndex + attributeToFindLenght));

                            //Insertamos el nuevo valor
                            strToReturn = strToReturn.Insert(attributeStartIndex + attributeToFindLenght, strToReplaceValue);
                        }
                        else if (replaceRelativePaths)
                        {
                            // Se verifica que termine con una barra
                            if (!Directory.EndsWith("/"))
                                Directory += "/";

                            // Se remueven los accesos al directorio base
                            string searchValue = "../";
                            while (strToReplaceValue.Contains(searchValue))
                            {
                                strToReplaceValue = strToReplaceValue.Replace(searchValue, string.Empty);
                            }

                            strToReplaceValue = Directory + strToReplaceValue;

                            //Removemos el viejo valor del atributo
                            strToReturn = strToReturn.Remove(attributeStartIndex + attributeToFindLenght,
                                attributeEndIndex - (attributeStartIndex + attributeToFindLenght));

                            //Insertamos el nuevo valor
                            strToReturn = strToReturn.Insert(attributeStartIndex + attributeToFindLenght, strToReplaceValue);
                        }
                    }
				}

				//Buscamos el siguiente atributo
				attributeStartIndex = strToReturn.IndexOf(strAttributeToFind, attributeStartIndex + attributeToFindLenght);
			}

			return strToReturn;
		}

		internal static bool IsTextTag(string tagToReturn)
		{
			bool isTextTag = tagToReturn.StartsWith(HTML.START_TEXTAREA);
			if (!isTextTag)
			{
				string sInputType = HTML.GetAttributeValue(tagToReturn, "type");
				if (string.IsNullOrEmpty(sInputType))
					sInputType = "text";
				isTextTag = string.Compare(sInputType.Trim().ToLower(), "text") == 0;
			}
			return isTextTag;
		}

		public static string GetAttributeValue(string tag, string attributeName)
		{
			return GetRegularEzpressionValue(tag, string.Format(ATTRIBUTE_SELECTOR_REGEX_FORMAT, attributeName));
		}

		public static string GetZambaZvarName(string tag)
		{
			return GetRegularEzpressionValue(tag, ZVAR_SELECTOR_REGEX_FORMAT);
		}

		private static string GetRegularEzpressionValue(string item, string regex)
		{
			Regex reg = new Regex(regex);
			StringBuilder sb = new StringBuilder();
			MatchCollection matchList = reg.Matches(item);

			for (int i = 0; i < matchList.Count; i++)
			{
				if (i > 0)
				{
					sb.Append(SPACE_CHAR);
				}

				sb.Append(matchList[i].Groups[1].Value);
			}

			return sb.ToString();
		}

		public static object FormatMultipleChildAttribute(System.Collections.Generic.List<long> list)
		{
			StringBuilder sb = new StringBuilder();
			int max = list.Count;
			for (int i = 0; i < max; i++)
			{
				sb.Append(list[i]);
				sb.Append('|');
			}
			sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
		}

		internal static string GetStarTag(string tag)
		{
			string item = tag.Trim();
			int delimeterIndex = item.IndexOf(END_CHAR_TAG);
			return item.Substring(0, delimeterIndex + 1);
		}

		private static string GetEndDelimeterTag(string tag)
		{
			string endDelimeter;
			if (tag.StartsWith(START_SELECT))
				endDelimeter = SELECT_ENDTAG;
			else
				if (tag.StartsWith(START_TABLE))
					endDelimeter = TABLE_ENDTAG;
				else
					if (tag.StartsWith(START_TEXTAREA))
						endDelimeter = TEXTAREA_ENDTAG;
					else
						endDelimeter = string.Empty;
			return endDelimeter;
		}

		public static string GetInnerHTML(string tag)
		{
			string endDelimeter;

			endDelimeter = GetEndDelimeterTag(tag);
			if (string.IsNullOrEmpty(endDelimeter))
				return string.Empty;

			string startDelimeter = GetStarTag(tag);
			string[] splittedValues = tag.Split(new string[] { startDelimeter }, StringSplitOptions.RemoveEmptyEntries);
			string htmlToReturn = splittedValues[splittedValues.Length - 1];
			splittedValues = htmlToReturn.Split(new string[] { endDelimeter }, StringSplitOptions.RemoveEmptyEntries);
			if (splittedValues == null || splittedValues.Length == 0)
				return string.Empty;
			else
				return splittedValues[0];
		}

		public static List<string> GetHTMLItem(string html, string itemName)
		{
			Regex reg;

			if (string.Compare(itemName, "input", true) == 0)
				reg = new Regex(string.Format(HTML_SELFCLOSED_ITEM_SELECTOR_REGEX_FORMAT, itemName));
			else
				reg = new Regex(string.Format(HTMLITEM_SELECTOR_REGEX_FORMAT, itemName));

			List<string> listToReturn = new List<string>();

			foreach (Match m in reg.Matches(html))
			{
				listToReturn.Add(m.Groups[0].Value);
			}

			return listToReturn;
		}

		public static List<string> GetHTMLItem(string html, string itemName, string additionalParam)
		{
			if (string.IsNullOrEmpty(additionalParam))
				return GetHTMLItem(html, itemName);

			Regex reg;

			if (string.Compare(itemName, "input", true) == 0)
				reg = new Regex(string.Format(HTML_SELFCLOSED_ITEM_SELECTOR_ADDITIONAL_PARAM_REGEX_FORMAT, itemName, additionalParam));
			else
				reg = new Regex(string.Format(HTMLITEM_SELECTOR_ADDITIONAL_PARAM_REGEX_FORMAT, itemName, additionalParam));

			List<string> listToReturn = new List<string>();

			foreach (Match m in reg.Matches(html))
			{
				listToReturn.Add(m.Groups[0].Value);
			}

			return listToReturn;
		}

		public static string ClearWhiteSpaces(string str)
		{
			StringBuilder sb = new StringBuilder(str);
			sb.Replace("\r\n", string.Empty);
			sb.Replace("\r", string.Empty);
			sb.Replace("\t", string.Empty);
			return sb.ToString();
		}

        public static List<string> GetPageBySeparatorTag(string htmlText)
        {
            List<string> pagesToReturn = new List<string>();

            int pageBreakIndex = htmlText.IndexOf(PAGE_BREAK_TAG);
            int lastIndex = 0;
            do
            {
                if (pageBreakIndex == -1)
                {
                    pagesToReturn.Add(htmlText.Substring(lastIndex, htmlText.Length - lastIndex));
                    lastIndex = pageBreakIndex;
                }
                else
                {
                    pagesToReturn.Add(htmlText.Substring(lastIndex, pageBreakIndex - lastIndex));
                    lastIndex = pageBreakIndex;
                    pageBreakIndex = htmlText.IndexOf(PAGE_BREAK_TAG, lastIndex + 1);
                }
            } while (lastIndex > -1);

            return pagesToReturn;
        }

        /// <summary>
        /// Reemplaza los caracteres por su equivalente en HTML
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ReplaceSpecialCharacters(string text)
        {
            text = text.Replace(" & ", " &amp; ");
            text = text.Replace("´", "&acute;");
            text = text.Replace("ñ", "&ntilde;");
            text = text.Replace("Ñ", "&Ntilde;");

            text = text.Replace("á", "&aacute;");
            text = text.Replace("é", "&eacute;");
            text = text.Replace("í", "&iacute;");
            text = text.Replace("ó", "&oacute;");
            text = text.Replace("ú", "&uacute;");

            text = text.Replace("Á", "&Aacute;");
            text = text.Replace("É", "&Eacute;");
            text = text.Replace("Í", "&Iacute;");
            text = text.Replace("Ó", "&Oacute;");
            text = text.Replace("Ú", "&Uacute;");

            return text;
        }

        /// <summary>
        /// Recupera los caracteres especiales HTML
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string RecoverSpecialCharacters(string text)
        {
            text = text.Replace(" &amp; "," & ");
            text = text.Replace("&acute;", "´");
            text = text.Replace("&ntilde;","ñ");
            text = text.Replace("&Ntilde;", "Ñ");
            text = text.Replace("&acute", "´");
            text = text.Replace("&ntilde", "ñ");
            text = text.Replace("&Ntilde", "Ñ");

            text = text.Replace("&aacute;", "á");
            text = text.Replace("&eacute;", "é");
            text = text.Replace("&iacute;", "í");
            text = text.Replace("&oacute;", "ó");
            text = text.Replace("&uacute;", "ú");
            text = text.Replace("&aacute", "á");
            text = text.Replace("&eacute", "é");
            text = text.Replace("&iacute", "í");
            text = text.Replace("&oacute", "ó");
            text = text.Replace("&uacute", "ú");

            text = text.Replace("&Aacute;", "Á");
            text = text.Replace("&Eacute;", "É");
            text = text.Replace("&Iacute;", "Í");
            text = text.Replace("&Oacute;", "Ó");
            text = text.Replace("&Uacute;", "Ú");
            text = text.Replace("&Aacute", "Á");
            text = text.Replace("&Eacute", "É");
            text = text.Replace("&Iacute", "Í");
            text = text.Replace("&Oacute", "Ó");
            text = text.Replace("&Uacute", "Ú");

            return text;
        }
	}

	/// <summary>
	/// Clase utilizada para parsear un string a double, long o date
	/// Guarda en value el valor parseado y el tipo en otra propiedad
	/// </summary>
	public class ParsedValue
	{
		public object Value
		{
			get;
			set;
		}

		public Type ValueType
		{
			get;
			set;
		}

		public ParsedValue(object value, Type valueType)
		{
			Value = value;
			ValueType = valueType;
		}

		public ParsedValue()
		{
		}

		public static ParsedValue Parse(string input)
		{
			long tryLong;
			DateTime tryDate;
			double tryDouble;

			if (long.TryParse(input, out tryLong))
			{
				return new ParsedValue(tryLong, typeof(long));
			}

			if (DateTime.TryParse(input, out tryDate))
			{
				return new ParsedValue(tryDate, typeof(DateTime));
			}

			if (double.TryParse(input, out  tryDouble))
			{
				return new ParsedValue(tryDouble, typeof(double));
			}

			return null;
		}
	}
}