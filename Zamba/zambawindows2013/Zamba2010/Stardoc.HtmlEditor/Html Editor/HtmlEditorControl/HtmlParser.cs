using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.HtmlControls;
using System.Reflection;
using System.ComponentModel;
using System.Text;

namespace Stardoc.HtmlEditor
{
    internal static class HtmlParser
    {

        private static StringBuilder _htmlBuilder = null;

        #region Constantes
        private const string TEXT_AREA_TAG = "textarea";
        private const string SELECT_TAG = "select";
        private const string OPTION_TAG = "option";
        private const string INPUT_TAG = "input";
        private const string INPUT_RADIO_TYPE = "radio";
        private const string INPUT_CHECKBOX_TYPE = "checkbox";
        private const string INPUT_TEXT_TYPE = "text";
        private const string INPUT_READONLY_ATRIBUTE = "readonly";
        private const string SELECT_TYPE_TEXT = "text";
        private const string ZAMBA_INDEXES_PREFIX = "zamba_index_";
        private const string TEXTAREA_ROWS_TAG = "rows";
        private const string TEXTAREA_COLUMNS_TAG = "cols";
        private const string HTML_TYPE_TAG = "type";
        private const string HTML_CHECKED_TAG = "checked";
        private const string HTML_SELECTED_TAG = "selected";

        private const string REQUIRED_FIELD_MESSAGE = "<strong style=\"display: none;\">Requerido</strong>";
        #endregion

        #region Atributos
        private static WebBrowser _editor = null;
        private static HtmlDocument _document = null;
        #endregion

        #region Propiedades
        private static HtmlDocument Document
        {
            get
            {
                if (null == _document)
                {

                    _document = Editor.Document;
                }

                return _document;
            }
        }
        private static WebBrowser Editor
        {
            get
            {
                if (null == _editor)
                {
                    _editor = new WebBrowser();
                    _editor.Navigate("about:blank");
                }

                return _editor;
            }
        }
        private static StringBuilder GetHtmlBuilder()
        {
            if (null == _htmlBuilder)
                _htmlBuilder = new StringBuilder();

            _htmlBuilder.Remove(0, _htmlBuilder.Length);

            return _htmlBuilder;
        }
        #endregion

        internal static String ParseTextArea(TextAreaItem item)
        {
            HtmlElement InputElement = Document.CreateElement(TEXT_AREA_TAG);
            InputElement.SetAttribute("rows", item.RowCount.ToString());
            InputElement.SetAttribute("cols", item.ColumnCount.ToString());

            if (item.Required)
                InputElement.SetAttribute("required", "true");

            InputElement.Enabled = item.Enabled;
            InputElement.InnerText = item.InnerText;

            String ParsedValue = null;

            if (item.Required)
                ParsedValue = InputElement.OuterHtml + REQUIRED_FIELD_MESSAGE;
            else
                ParsedValue = InputElement.OuterHtml;

            InputElement = null; 

            return ParsedValue;
        }
        internal static String ParseTextArea(ZTextAreaItem item)
        {
            StringBuilder NodeBuilder = GetHtmlBuilder();
            NodeBuilder.Append("<textarea ");
            NodeBuilder.Append(BuildAttribute(TEXTAREA_ROWS_TAG, item.RowCount.ToString()));
            NodeBuilder.Append(BuildAttribute(TEXTAREA_COLUMNS_TAG, item.ColumnCount.ToString()));
            NodeBuilder.Append(BuildAttribute("id", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString()));
            NodeBuilder.Append(BuildAttribute("name", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString()));

            if (!item.Enabled)
                NodeBuilder.Append(BuildAttribute("disabled", "disabled"));

            if (item.Required)
                NodeBuilder.AppendLine(BuildAttribute("required", "true"));

            NodeBuilder.Append(">");
            NodeBuilder.Append(item.InnerText);
            NodeBuilder.Append("</textarea>");

            if (item.Required)
                NodeBuilder.Append(REQUIRED_FIELD_MESSAGE);

            return NodeBuilder.ToString();
        }

        internal static String ParseCheckBox(CheckBoxItem item)
        {
            StringBuilder NodeBuilder = GetHtmlBuilder();
            NodeBuilder.Append("<input type=\"checkbox\" ");

            if (!item.Enabled)
                NodeBuilder.Append(BuildAttribute("disabled", "disabled"));
            if (item.Checked)
                NodeBuilder.Append(BuildAttribute("checked", "checked"));
            if (item.Required)
                NodeBuilder.Append(BuildAttribute("required", "true"));

            NodeBuilder.Append(">");
            NodeBuilder.Append(item.Text);
            NodeBuilder.Append("</input>");

            if (item.Required)
                NodeBuilder.Append(REQUIRED_FIELD_MESSAGE);

            return NodeBuilder.ToString();
        }
        internal static String ParseCheckBox(ZCheckBoxItem item)
        {
            StringBuilder NodeBuilder = GetHtmlBuilder();
            NodeBuilder.Append("<input type=\"checkbox\" ");
            NodeBuilder.Append(BuildAttribute("id", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString()));
            NodeBuilder.Append(BuildAttribute("name", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString()));

            if (!item.Enabled)
                NodeBuilder.Append(BuildAttribute("disabled", "disabled"));
            if (item.Checked)
                NodeBuilder.Append(BuildAttribute("checked", "checked"));
            if (item.Required)
                NodeBuilder.Append(BuildAttribute("required", "true"));

            NodeBuilder.Append(">");
            NodeBuilder.Append(item.Text);
            NodeBuilder.Append("</input>");


            if (item.Required)
                NodeBuilder.Append(REQUIRED_FIELD_MESSAGE);

            return NodeBuilder.ToString();
        }

        internal static String ParseRadioButton(RadioButtonItem item)
        {
            StringBuilder NodeBuilder = GetHtmlBuilder();
            NodeBuilder.Append("<input type=\"radio\" ");

            if (!item.Enabled)
                NodeBuilder.Append(BuildAttribute("disabled", "disabled"));
            if (item.Checked)
                NodeBuilder.Append(BuildAttribute("checked", "checked"));
            if (item.Required)
                NodeBuilder.Append(BuildAttribute("required", "true"));

            NodeBuilder.Append(">");

            if (!String.IsNullOrEmpty(item.Name))
                NodeBuilder.Append(item.Name);

            NodeBuilder.Append("</input>");

            if (item.Required)
                NodeBuilder.Append(REQUIRED_FIELD_MESSAGE);

            return NodeBuilder.ToString();
        }
        internal static String ParseRadioButton(ZRadioButtonItem item)
        {
            StringBuilder NodeBuilder = GetHtmlBuilder();
            NodeBuilder.Append("<input type=\"radio\" ");

            if (item.SelectionValue)
                NodeBuilder.Append(BuildAttribute("id", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString() + "S"));
            else
                NodeBuilder.Append(BuildAttribute("id", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString() + "N"));

            NodeBuilder.Append(BuildAttribute("name", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString()));

            if (!item.Enabled)
                NodeBuilder.Append(BuildAttribute("disabled", "disabled"));
            if (item.Checked)
                NodeBuilder.Append(BuildAttribute("checked", "checked"));
            if (item.Required)
                NodeBuilder.Append(BuildAttribute("required", "true"));

            NodeBuilder.Append(">");

            if (!String.IsNullOrEmpty(item.Name))
                NodeBuilder.Append(item.Name);

            NodeBuilder.Append("</input>");
            
            if (item.Required)
                NodeBuilder.Append(REQUIRED_FIELD_MESSAGE);

            return NodeBuilder.ToString();
        }

        internal static String ParseSelect(SelectItem item)
        {
            StringBuilder SelectBuilder = GetHtmlBuilder();
            SelectBuilder.Append("<select ");

            if (!item.Enabled)
                SelectBuilder.Append(BuildAttribute("disabled", "disabled"));
            if (item.Required)
                SelectBuilder.Append(BuildAttribute("required", "true"));

            SelectBuilder.Append(">");

            HtmlElement Option = Document.CreateElement(OPTION_TAG);
            foreach (OptionItem CurrentOption in item.Options)
            {
                Option.SetAttribute("value", CurrentOption.Value);
                Option.SetAttribute("innerText", CurrentOption.Name);

                if (CurrentOption.Default)
                    Option.SetAttribute(HTML_SELECTED_TAG, HTML_SELECTED_TAG);

                SelectBuilder.Append(Option.OuterHtml);
            }

            SelectBuilder.Append("</select>");


            if (item.Required)
                SelectBuilder.Append(REQUIRED_FIELD_MESSAGE);

            return SelectBuilder.ToString();
        }
        internal static String ParseSelect(ZSelectItem item)
        {
            StringBuilder SelectBuilder = GetHtmlBuilder();

            SelectBuilder.Append("<select ");
            SelectBuilder.Append(BuildAttribute("id", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString()));
            SelectBuilder.Append(BuildAttribute("name", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString()));

            if (!item.Enabled)
                SelectBuilder.Append(BuildAttribute("disabled", "disabled"));
            if (item.Required)
                SelectBuilder.Append(BuildAttribute("required", "true"));

            SelectBuilder.Append(">");

            HtmlElement Option = Document.CreateElement(OPTION_TAG);
            foreach (OptionItem CurrentOption in item.Options)
            {
                Option.SetAttribute("value", CurrentOption.Value);
                Option.SetAttribute("innerText", CurrentOption.Name);

                if (CurrentOption.Default)
                    Option.SetAttribute(HTML_SELECTED_TAG, HTML_SELECTED_TAG);

                SelectBuilder.Append(Option.OuterHtml);
            }

            SelectBuilder.Append("</select>");

            if (item.Required)
                SelectBuilder.Append(REQUIRED_FIELD_MESSAGE);

            return SelectBuilder.ToString();
        }

        internal static String ParseTextBox(TextBoxItem item)
        {
            StringBuilder TextBoxBuilder = GetHtmlBuilder();
            TextBoxBuilder.Append(item.Name);
            TextBoxBuilder.Append("<input type=\"text\" ");

            if (!item.Enabled)
                TextBoxBuilder.Append(BuildAttribute("disabled", "disabled"));
            if (item.ReadOnly)
                TextBoxBuilder.Append(BuildAttribute("readonly", "readonly"));
            if (item.Required)
                TextBoxBuilder.Append(BuildAttribute("required", "true"));

            switch (item.DataType)
            {
                case DataType.Alfanumerico:
                    //No necesita validacion alguna
                    break;
                case DataType.Numerico:
                    TextBoxBuilder.Append("onfocus=\"SetValueOnFocus(this)\"; ");
                    TextBoxBuilder.Append("onchange=\"ValidateInteger(this);\" ");
                    break;
                case DataType.Fecha:
                    TextBoxBuilder.Append("onfocus=\"showCalendarControl(this);\" ");
                    break;
                case DataType.Email:
                    //TODO: crear validacion por RegEx de por JS
                    break;
                default:
                    break;
            }

            TextBoxBuilder.Append(" />");
            if (item.Required)
                TextBoxBuilder.Append(REQUIRED_FIELD_MESSAGE);


            return TextBoxBuilder.ToString();
        }
        internal static String ParseTextBox(ZTextBoxItem item)
        {
            StringBuilder TextBoxBuilder = GetHtmlBuilder();
            TextBoxBuilder.Append(item.Name);
            TextBoxBuilder.Append("<input type=\"text\" ");
            TextBoxBuilder.Append(BuildAttribute("name", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString()));
            TextBoxBuilder.Append(BuildAttribute("id", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString()));

            if (!item.Enabled)
                TextBoxBuilder.Append(BuildAttribute("disabled", "disabled"));
            if (item.ReadOnly)
                TextBoxBuilder.Append(BuildAttribute("readonly", "readonly"));
            if (item.Required)
                TextBoxBuilder.Append(BuildAttribute("required", "true"));

            switch (item.DataType)
            {
                case DataType.Alfanumerico:
                    //No necesita validacion alguna
                    break;
                case DataType.Numerico:
                    TextBoxBuilder.Append("onfocus=\"SetValueOnFocus(this);\" ");
                    TextBoxBuilder.Append("onchange=\"ValidateInteger(this);\" ");
                    break;
                case DataType.Fecha:
                    TextBoxBuilder.Append("onfocus=\"showCalendarControl(this);\" ");
                    break;
                case DataType.Email:
                    //TODO: crear validacion por RegEx de por JS
                    break;
                default:
                    break;
            }

            TextBoxBuilder.Append(" />");

            if (item.Required)
                TextBoxBuilder.Append(REQUIRED_FIELD_MESSAGE);

            return TextBoxBuilder.ToString();
        }

        /// <summary>
        /// Crea un atributo HTML con su valor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static String BuildAttribute(String name, String value)
        {
            return name + "=\"" + value + "\" ";
        }
    }
}