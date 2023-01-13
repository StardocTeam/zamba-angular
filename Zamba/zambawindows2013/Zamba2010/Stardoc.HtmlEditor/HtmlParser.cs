using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.HtmlControls;
using System.Reflection;
using System.ComponentModel;
using System.Text;
using Stardoc.HtmlEditor.Enumerators;
using System.Configuration;
using System.Web;

namespace Stardoc.HtmlEditor
{
    internal static class HtmlParser
    {
        #region Constantes
        private const String CONFIG_FUNCTION_NAME_ASSOCIATED_DOC = "SetAsociatedDocumentFunctionName";
        private const String CONFIG_FUNCTION_NAME_RULE_ID = "SetRuleIdFunctionName";
        private const String CONFIG_SCRIPT_SET_ASOCIATED_DOC = "ScriptSetAsociatedDocument";
        private const String CONFIG_SCRIPT_SET_RULE_ID = "ScriptSetRule";
        private const String CONFIG_ELEMENT_RULE_ID = "ScriptSetRule";
        private const String CONFIG_ELEMENT_ASOCIATED_DOC = "ScriptSetRule";
        private const String TABLE_TAG = "table";
        private const String TEXT_AREA_TAG = "textarea";
        private const String SELECT_TAG = "select";
        private const String OPTION_TAG = "option";
        private const String INPUT_TAG = "input";
        private const String INPUT_RADIO_TYPE = "radio";
        private const String INPUT_CHECKBOX_TYPE = "checkbox";
        private const String INPUT_TEXT_TYPE = "text";
        private const String INPUT_READONLY_ATRIBUTE = "readonly";
        private const String SELECT_TYPE_TEXT = "text";
        private const String ZAMBA_INDEXES_PREFIX = "zamba_index_";
        private const String ZAMBA_RULES_PREFIX = "zamba_rule_";
        private const String ZAMBA_ASSOCIATED_DOCUMENTS = "zamba_associated_documents";
        private const String SCRIPT_SET_RULE_ID = "SetRuleId(this);";
        private const String TEXTAREA_ROWS_TAG = "rows";
        private const String TEXTAREA_COLUMNS_TAG = "cols";
        private const String HTML_TYPE_TAG = "type";
        private const String HTML_CHECKED_TAG = "checked";
        private const String HTML_SELECTED_TAG = "selected";
        #endregion

        #region Atributos
        private static WebBrowser _editor = null;
        private static HtmlDocument _document = null;
        private static StringBuilder _htmlBuilder = null;
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
        /// <summary>
        /// Devuelve el contenido del script para guardar el documento seleccionado
        /// </summary>
        /// <returns></returns>
        private static String ScriptAsociatedRule()
        {
            return ConfigurationManager.ScriptSetAsociatedDocument;
        }
        /// <summary>
        /// Devuelve el contenido del script para guardar el ruleId seleccionado
        /// </summary>
        /// <returns></returns>
        private static String ScriptRuleId()
        {
            return ConfigurationManager.ScriptSetRule;
        }
        /// <summary>
        /// Devuelve el contenido HTML del elemento donde se guarda el id de la regla ejecutada
        /// </summary>
        /// <returns></returns>
        private static String RuleIdElement()
        {
            return ConfigurationManager.HiddenFieldRuleId;
        }
        /// <summary>
        /// Devuelve el contenido HTML del elemento donde se guarda el id del documento asociado seleccionado
        /// </summary>
        /// <returns></returns>
        private static String SetAsociatedElement()
        {
            return ConfigurationManager.HiddenFieldAsociatedDocumentId  ;
        }
        /// <summary>
        /// Devuelve el nombre de la function que establece el id de la regla ejecutada
        /// </summary>
        /// <returns></returns>
        private static String SetRuleIdFunctionName()
        {
            return ConfigurationManager.SetRuleIdFunctionName;
        }
        /// <summary>
        /// Devuelve el nombre de la funcion que estable el id del documento asociado seleccionado
        /// </summary>
        /// <returns></returns>
        private static String SetAsociatedDocFunctionName()
        {
            return ConfigurationManager.SetAsociatedDocumentFunctionName;
        }
        #endregion

        #region Button
        /// <summary>
        /// Convierte un boton a Html
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static String ParseButton(ButtonItem item)
        {
            StringBuilder NodeBuilder = GetHtmlBuilder();
            if (item.IsFormButton)
            {
                NodeBuilder.Append("<button ");
                NodeBuilder.Append(BuildAttribute("type", item.InputType));
                NodeBuilder.Append(BuildAttribute("id", item.Id.ToString()));
                NodeBuilder.Append(BuildAttribute("name", item.Id.ToString()));             
                if (!item.Enabled)
                    NodeBuilder.Append(BuildAttribute("disabled", "disabled"));
                NodeBuilder.Append(">");
                NodeBuilder.Append(item.Name);
                NodeBuilder.Append("</button>");                
            }            
            else
            {
                NodeBuilder.Append("<input ");
                NodeBuilder.Append(BuildAttribute("type", item.InputType));
                NodeBuilder.Append(BuildAttribute("id", item.Id.ToString()));
                NodeBuilder.Append(BuildAttribute("name", item.Name));
                NodeBuilder.Append(BuildAttribute("value", item.Name));

                if (!item.Enabled)
                    NodeBuilder.Append(BuildAttribute("disabled", "disabled"));

                NodeBuilder.Append("/>");
            }


            return NodeBuilder.ToString();
        }
        /// <summary>
        /// Convierte un boton con informacion de una regla de Zamba a Html
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static String ParseButton(RuleButtonItem item)
        {
            StringBuilder NodeBuilder = GetHtmlBuilder();
            NodeBuilder.Append("<input ");
            NodeBuilder.Append(BuildAttribute("type", "button"));
            NodeBuilder.Append(BuildAttribute("id", ZAMBA_RULES_PREFIX + item.Id.ToString()));
            NodeBuilder.Append(BuildAttribute("name", ZAMBA_RULES_PREFIX + item.Id.ToString()));
            NodeBuilder.Append(BuildAttribute("value", item.Name));
            NodeBuilder.Append(BuildAttribute("onclick", SCRIPT_SET_RULE_ID));

            if (!item.Enabled)
                NodeBuilder.Append(BuildAttribute("disabled", "disabled"));

            NodeBuilder.Append("/>");


            return NodeBuilder.ToString();
        }

        #endregion

        #region TextArea
        /// <summary>
        /// Convierte un textarea a Html
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static String ParseTextArea(TextAreaItem item)
        {
            HtmlElement InputElement = Document.CreateElement(TEXT_AREA_TAG);
            InputElement.SetAttribute("rows", item.RowCount.ToString());
            InputElement.SetAttribute("cols", item.ColumnCount.ToString());
            InputElement.Enabled = item.Enabled;
            InputElement.InnerText = item.InnerText;

            return item.Name + InputElement.OuterHtml;
        }
        /// <summary>
        /// Convierte un textarea con informacion de un indice de Zamba a Html
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static String ParseTextArea(ZTextAreaItem item)
        {
            StringBuilder NodeBuilder = GetHtmlBuilder();
            NodeBuilder.Append(BuildLabel(item.IndexName,item.IndexId));
            NodeBuilder.Append("<textarea ");
            NodeBuilder.Append(BuildAttribute(TEXTAREA_ROWS_TAG, item.RowCount.ToString()));
            NodeBuilder.Append(BuildAttribute(TEXTAREA_COLUMNS_TAG, item.ColumnCount.ToString()));
            NodeBuilder.Append(BuildAttribute("id", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString()));
            NodeBuilder.Append(BuildAttribute("name", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString()));

            if (!item.Enabled)
                NodeBuilder.Append(BuildAttribute("disabled", "disabled"));

            NodeBuilder.Append(">");
            NodeBuilder.Append(BuildLabel(item.InnerText, item.IndexId));

            return NodeBuilder.ToString();
        }
        #endregion

        #region CheckBox
        /// <summary>
        /// Convierte un checkbox a Html
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static String ParseCheckBox(CheckBoxItem item)
        {
            StringBuilder NodeBuilder = GetHtmlBuilder();
            NodeBuilder.Append("<input type=\"checkbox\" ");

            if (!item.Enabled)
                NodeBuilder.Append(BuildAttribute("disabled", "disabled"));
            if (item.Checked)
                NodeBuilder.Append(BuildAttribute("checked", "checked"));

            NodeBuilder.Append(">");
            NodeBuilder.Append(BuildLabel(item.Name, item.Id));
            NodeBuilder.Append("</input>");

            return NodeBuilder.ToString();
        }
        /// <summary>
        /// Convierte un checkbox con informacion de un indice de Zamba a Html
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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

            NodeBuilder.Append(">");
            NodeBuilder.Append(BuildLabel(item.Name, item.IndexId));
            NodeBuilder.Append("</input>");

            return NodeBuilder.ToString();
        }
        #endregion

        #region RadioButton
        /// <summary>
        /// Convierte un radiobutton a Html
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static String ParseRadioButton(RadioButtonItem item)
        {
            StringBuilder NodeBuilder = GetHtmlBuilder();
            NodeBuilder.Append("<input type=\"radio\" ");

            if (!item.Enabled)
                NodeBuilder.Append(BuildAttribute("disabled", "disabled"));

            if (item.Checked)
                NodeBuilder.Append(BuildAttribute("checked", "checked"));

            NodeBuilder.Append(">");
            NodeBuilder.Append(BuildLabel(item.Name, item.Id));
            NodeBuilder.Append("</input>");

            return NodeBuilder.ToString();
        }
        /// <summary>
        /// Convierte un radiibutton con informacion de un indice de Zamba a Html
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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

            NodeBuilder.Append(">");
            NodeBuilder.Append(BuildLabel(item.Name, item.IndexId));
            NodeBuilder.Append("</input>");

            return NodeBuilder.ToString();
        }

        #endregion

        #region Select
        /// <summary>
        /// Convierte un Select a Html
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static String ParseSelect(SelectItem item)
        {
            StringBuilder SelectBuilder = GetHtmlBuilder();
            SelectBuilder.Append(BuildLabel(item.Name, item.Id));
            SelectBuilder.Append("<select ");

            if (!item.Enabled)
                SelectBuilder.Append(BuildAttribute("disabled", "disabled"));

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

            return SelectBuilder.ToString();
        }
        /// <summary>
        /// Convierte un select con informacion de un indice de Zamba a Html
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static String ParseSelect(ZSelectItem item)
        {
            StringBuilder SelectBuilder = GetHtmlBuilder();
            SelectBuilder.Append(BuildLabel(item.IndexName, item.IndexId));
            SelectBuilder.Append("<select ");
            SelectBuilder.Append(BuildAttribute("id", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString()));
            SelectBuilder.Append(BuildAttribute("name", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString()));

            if (!item.Enabled)
                SelectBuilder.Append(BuildAttribute("disabled", "disabled"));

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

            return SelectBuilder.ToString();
        }

        #endregion

        #region TextBox
        /// <summary>
        /// Convierte un Textbox a Html
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static String ParseTextBox(TextBoxItem item)
        {
            StringBuilder TextBoxBuilder = GetHtmlBuilder();
            TextBoxBuilder.Append(BuildLabel(item.Name, item.Id));
            TextBoxBuilder.Append("<input type=\"text\" ");

            if (item.Lenght != 0)
                TextBoxBuilder.Append(BuildAttribute("maxlenght", item.Lenght.ToString()));
            if (!item.Enabled)
                TextBoxBuilder.Append(BuildAttribute("disabled", "disabled"));
            if (item.ReadOnly)
                TextBoxBuilder.Append(BuildAttribute("readonly", "readonly"));

            TextBoxBuilder.Append(" />");

            return TextBoxBuilder.ToString();
        }
        /// <summary>
        /// Convierte un textbox con informacion de un indice de Zamba a Html
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static String ParseTextBox(ZTextBoxItem item)
        {
            StringBuilder TextBoxBuilder = GetHtmlBuilder();
            TextBoxBuilder.Append(BuildLabel(item.Name,item.IndexId));
            TextBoxBuilder.Append("<input type=\"text\" ");
            TextBoxBuilder.Append(BuildAttribute("name", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString()));
            TextBoxBuilder.Append(BuildAttribute("id", ZAMBA_INDEXES_PREFIX + item.IndexId.ToString()));
            if (item.Lenght != 0)
                TextBoxBuilder.Append(BuildAttribute("maxlenght", item.Lenght.ToString()));

            if (!item.Enabled)
                TextBoxBuilder.Append(BuildAttribute("disabled", "disabled"));
            if (item.ReadOnly)
                TextBoxBuilder.Append(BuildAttribute("readonly", "readonly"));

            TextBoxBuilder.Append(" />");

            return TextBoxBuilder.ToString();
        }
        #endregion

        /// <summary>
        /// Convierte una tabla a Html
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static String ParseTable(TableItem item)
        {
            StringBuilder NodeBuilder = GetHtmlBuilder();
            NodeBuilder.Append("<");
            NodeBuilder.Append(TABLE_TAG);
            NodeBuilder.Append(BuildAttribute("id", item.Id));

            if (!item.Enabled)
                NodeBuilder.Append(BuildAttribute("disabled", "disabled"));


            NodeBuilder.Append(" ><caption>");
            NodeBuilder.Append(BuildLabel(item.Name, item.Id));
            NodeBuilder.Append("</caption></table>");

            return NodeBuilder.ToString();
        }


        /// <summary>
        /// Construye un atributo HTML
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static String BuildAttribute(String name, String value)
        {
            return " " + name + "=\"" + value + "\" ";
        }


        /// <summary>
        /// Construye el label para el nombre de un control
        /// </summary>
        /// <param name="name"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static String BuildLabel(String name, Int64 indexId)
        {
            return "<Label" + BuildAttribute("id", ZAMBA_INDEXES_PREFIX + indexId.ToString() + "_lbl") + "> " + name + " </Label>";
        }


        /// <summary>
        /// Construye el label para el nombre de un control
        /// </summary>
        /// <param name="name"></param>
        /// <param name="indexId"></param>
        /// <returns></returns>
        private static String BuildLabel(String name, String indexId)
        {
            return " <Label" + BuildAttribute("id", ZAMBA_INDEXES_PREFIX + indexId + "_lbl") + "> " + name + " </Label>";
        }


        /// <summary>
        /// Devuelve una ID parseada con la logica de zamba
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static String GetParsedId(String id)
        {
            return ZAMBA_INDEXES_PREFIX + id;
        }


        /// <summary>
        /// Valida que un body html contenga al menos 1 regla.
        /// </summary>
        /// <param name="htmlBody"></param>
        /// <returns></returns>
        internal static Boolean HasRules(String htmlBody)
        {
            if (String.IsNullOrEmpty(htmlBody))
                return false;

            return htmlBody.Contains(ZAMBA_RULES_PREFIX);
        }

        /// <summary>
        /// Valida que un body html contenga una tabla para documentos asociados.
        /// </summary>
        /// <param name="htmlBody"></param>
        /// <returns></returns>
        internal static Boolean HasAsociatedDocuments(String htmlBody)
        {
            if (String.IsNullOrEmpty(htmlBody))
                return false;

            return htmlBody.Contains(ZAMBA_ASSOCIATED_DOCUMENTS);
        }

        internal static void AppendRuleScript(ref String head)
        {
            if (String.IsNullOrEmpty(head) || head.Contains(CONFIG_FUNCTION_NAME_RULE_ID))
                return;

            if (!head.ToUpper().Contains("<HEAD>"))
                head += "<HEAD>";

            if (!head.ToUpper().Contains("</HEAD>"))
                head += "</HEAD>";

            head = head.Insert(head.ToUpper().LastIndexOf("</HEAD>"), ScriptRuleId());
        }
        internal static void AppendRuleIdContainer(ref string body)
        {
            body = body.Insert(body.ToUpper().LastIndexOf("</BODY>"), RuleIdElement());
        }
        internal static void AppendAsociatedDocContainer(ref string body)
        {
            body = body.Insert(body.ToUpper().LastIndexOf("</BODY>"), SetAsociatedElement());
        }


        internal static void AppendAsociatedScript(ref String head)
        {
            try
            {
                if (String.IsNullOrEmpty(head) || head.Contains(CONFIG_FUNCTION_NAME_ASSOCIATED_DOC))
                    return;

                if (!head.ToUpper().Contains("<HEAD>"))
                    head += "<HEAD>";

                if (!head.ToUpper().Contains("</HEAD>"))
                    head += "</HEAD>";

                head = head.Insert(head.ToUpper().LastIndexOf("</HEAD>"), ScriptAsociatedRule());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}