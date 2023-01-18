using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba.Core;
using Zamba.Services;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using Zamba.Membership;
using Zamba;

public static class HTML
{
    public enum HTMLSection
    {
        HEAD,
        FORM,
        BODY
    }

    //Constante utilizada para armar el id del atributo
    public const string ENTITY_ATTRIBUTE_FORMAT = "zamba_index_{0}";
    //Funcion de javascript que para agregar las condiciones
    public const string FUNCTIONAL_INJECT_FORMAT = "InjectCondition({0},{1},{2},{3},{4},{5});";
    public const string JAVASCRIPT_STRING_FORMAT = "'{0}'";
    //Constante para no hardcodear constantemente el document ready.
    public const string DOCUMENT_READY_FORMAT = "$(document).ready(function() {{  {0} }} );";

    //Operadores utilizados en javascript
    public const string COMPARATOR_EQUAL = "==";
    public const string COMPARATOR_DIFFERENT = "!=";
    public const string COMPARATOR_CONTAINS = "in";
    public const string COMPARATOR_STARTS = "starts";
    public const string COMPARATOR_ENDS = "ends";
    public const string COMPARATOR_LOWER = "<";
    public const string COMPARATOR_GREATER = ">";
    public const string COMPARATOR_EQUALLOWER = "<=";
    public const string COMPARATOR_EQUALGREATER = ">=";
    public const string COMPARATOR_INTO = "into";
    public const string COMPARATOR_NOTINTO = "notInto";

    private const string ATTRIBUTETEMPLATE = "{0}=\"";

    public static string getHtmlSection(string HTML, HTMLSection Section)
    {
        string sec = string.Empty;

        if (Section == HTMLSection.HEAD)
            sec = "head";
        else if (Section == HTMLSection.FORM)
            sec = "form";
        else if (Section == HTMLSection.BODY)
            sec = "body";

        String strHtmlLower = HTML.ToLower();
        if (strHtmlLower.Contains(sec))
        {
            Int32 iniSectionTag = strHtmlLower.IndexOf("<" + sec);
            Int32 iniSectionTagClose = strHtmlLower.Substring(iniSectionTag).IndexOf(">") + 1;
            Int32 totIndexOfSectionTag = iniSectionTag + iniSectionTagClose;
            Int32 endSectionTab = HTML.ToLower().IndexOf("</" + sec + ">");
            String strContent = HTML.Substring(totIndexOfSectionTag, endSectionTab - totIndexOfSectionTag);

            ZOptBusiness zoptb = new ZOptBusiness();
            String CurrentTheme = zoptb.GetValue("CurrentTheme");
            zoptb = null;
            //Reemplaza las referencias en el formulario
            strContent = GenerateCorrectlyReferences(strContent, CurrentTheme);

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
    /// Método de ayuda para reeplazar las referencias de html(src,href)
    /// </summary>
    /// <param name="HTML"></param>
    /// <returns></returns>
    public static string GenerateCorrectlyReferences(string HTML, String CurrentTheme)
    {
        string strToReturn = string.Empty;

        //Hacemos un path relativo hacia el path de los formularios
        string curentClientPath = GetRelativeFormPath(CurrentTheme);

        strToReturn = ReplaceValuesWithDirectory(HTML, "src", curentClientPath);
        strToReturn = ReplaceValuesWithDirectory(strToReturn, "href", curentClientPath);

        return strToReturn;
    }

    public static string GetRelativeFormPath(String CurrentTheme)
    {
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
    private static string ReplaceValuesWithDirectory(string Text, string Attribute, string Directory)
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

                //Si el atributo inicia con un path relativo o la llamada a un servicio, entonces no lo cambiaramos
                if (!(strToReplaceValue.StartsWith("..") || strToReplaceValue.StartsWith(MembershipHelper.Protocol)))
                {
                    strToReplaceValue = Directory + '/' + strToReplaceValue;

                    //Remosvemos el viejo valor del atributo
                    strToReturn = strToReturn.Remove(attributeStartIndex + attributeToFindLenght,
                        attributeEndIndex - (attributeStartIndex + attributeToFindLenght));

                    //Insertamos el nuevo valor
                    strToReturn = strToReturn.Insert(attributeStartIndex + attributeToFindLenght, strToReplaceValue);
                }
            }

            //Buscamos el siguiente atributo
            attributeStartIndex = strToReturn.IndexOf(strAttributeToFind, attributeStartIndex + attributeToFindLenght);
        }

        return strToReturn;
    }

    private static string ReplaceValuesWithOutDirectory(string Text, string Attribute, FormTypes FormType,String CurrentTheme)
    {
        string strToReturn = Text;
        string strAttributeToFind = string.Format(ATTRIBUTETEMPLATE, Attribute);
        string strToReplaceValue;
        string formPath = GetRelativeFormPath(CurrentTheme);
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

                //Si el atributo inicia con un path relativo 
                if (strToReplaceValue.StartsWith(".."))
                {
                    //Si el formulario no es web, o si es web, pero no contiene una referencia relativa a content o a scripts
                    if (!FormType.ToString().Contains("Web") ||
                        !(strToReplaceValue.Contains("../Content") || strToReplaceValue.Contains("../Scripts")))
                    {
                        //Reeplazamos la parte relativa por vacio, para que solo queden las carpetas "puras"
                        strToReplaceValue = strToReplaceValue.Replace(formPath + '/', string.Empty);

                        //Remosvemos el viejo valor del atributo
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

    public static string CleanFormReferences(string HTML, FormTypes FormType,String CurrentTheme)
    {
        string strToReturn = HTML;

        strToReturn = ReplaceValuesWithOutDirectory(strToReturn, "src", FormType, CurrentTheme);
        strToReturn = ReplaceValuesWithOutDirectory(strToReturn, "href", FormType, CurrentTheme);

        return strToReturn;
    }

    private static bool ContainsCaseInsensitive(ref string source, ref string value)
    {
        int results = source.IndexOf(value, StringComparison.CurrentCultureIgnoreCase);
        return results == -1 ? false : true;
    }

    /// <summary>
    /// Metodo que en base al body de un form genera las condiciones dinamicas configuradas en el admin.
    /// </summary>
    /// <param name="HTML"></param>
    /// <returns></returns>
    public static string MakeDynamicConditions(string HTMLBody, long formId, List<IIndex> indexs)
    {
        List<IZFormCondition> conditions = new SForms().GetFormConditions(formId);
        //En este SB se guardaran todas las llamadas a las funciones.
        StringBuilder sbScript = new StringBuilder();

        if (conditions != null && conditions.Count > 0)
        {
            int max = conditions.Count;
            IZFormCondition currCondition;
            string currValidateIndexID;
            string currTargetIndexID;
            StringBuilder sbFormater = new StringBuilder();

            //Recorremos las condiciones
            for (int i = 0; i < max; i++)
            {
                currCondition = conditions[i];

                ClearStringBuilder(ref sbFormater);

                currValidateIndexID = sbFormater.AppendFormat(ENTITY_ATTRIBUTE_FORMAT, currCondition.IndexToValidate).ToString();
                ClearStringBuilder(ref sbFormater);

                currTargetIndexID = sbFormater.AppendFormat(ENTITY_ATTRIBUTE_FORMAT, currCondition.TargetIndex).ToString();

                //Si existen los atributos en el formulario, creo la condicion
                if (ContainsCaseInsensitive(ref HTMLBody, ref currTargetIndexID) && ContainsCaseInsensitive(ref HTMLBody, ref currValidateIndexID))
                {
                    sbScript.AppendLine(BuildConditionScript(ref currCondition, ref sbFormater));
                }
            }
        }

        return sbScript.ToString();
    }

    /// <summary>
    /// Arma en un string la invocacion de la funcion javascript para injectar la condicion.
    /// La injeccion de la condicion se hace mediante javascript para balancear el trabajo.
    /// </summary>
    /// <param name="currCondition"></param>
    /// <param name="sbFormater"></param>
    /// <returns></returns>
    private static string BuildConditionScript(ref IZFormCondition currCondition, ref StringBuilder sbFormater)
    {
        ClearStringBuilder(ref sbFormater);

        //FUNCTIONAL_INJECT_FORMAT = "InjectCondition(idSource,idTarget,action,rollbak,value,comparator)"
        sbFormater.AppendFormat(FUNCTIONAL_INJECT_FORMAT,
            FormatValue(JAVASCRIPT_STRING_FORMAT, FormatValue(ENTITY_ATTRIBUTE_FORMAT, currCondition.IndexToValidate)),
            FormatValue(JAVASCRIPT_STRING_FORMAT, FormatValue(ENTITY_ATTRIBUTE_FORMAT, currCondition.TargetIndex)),
            FormatValue(JAVASCRIPT_STRING_FORMAT, currCondition.TargetAction.ToString()),
            FormatValue(JAVASCRIPT_STRING_FORMAT, GetRollbackAction(currCondition.TargetAction).ToString()),
            FormatValue(JAVASCRIPT_STRING_FORMAT, currCondition.ComparateValue),
            FormatValue(JAVASCRIPT_STRING_FORMAT, ConvertToCharCondition(currCondition.Comparator)));

        return sbFormater.ToString();
    }

    /// <summary>
    /// Devuelve un string en base a otro string de formato.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private static string FormatValue(string format, object value)
    {
        StringBuilder sbFormater = new StringBuilder();

        return sbFormater.AppendFormat(format, value).ToString();
    }

    /// <summary>
    /// Obtiene la accion a realizar si no se cumple la condicion.
    /// </summary>
    /// <param name="formAction"></param>
    /// <returns></returns>
    private static FormActions GetRollbackAction(FormActions formAction)
    {
        switch (formAction)
        {
            case FormActions.Disable:
                return FormActions.Enable;
            case FormActions.Enable:
                return FormActions.Disable;
            case FormActions.MakeNonRequired:
                return FormActions.MakeRequired;
            case FormActions.MakeRequired:
                return FormActions.MakeNonRequired;
            case FormActions.NonAction:
                return formAction;
            case FormActions.Hidden:
                return FormActions.Visible;
            case FormActions.Visible:
                return FormActions.Hidden;
            default:
                return FormActions.NonAction;
        }
    }

    /// <summary>
    /// En base al enumerador de comparadores, devuelve un string con el operador
    /// </summary>
    /// <param name="comparators"></param>
    /// <returns></returns>
    private static string ConvertToCharCondition(Comparators comparators)
    {
        switch (comparators)
        {
            case Comparators.Contents:
                return COMPARATOR_CONTAINS;
            case Comparators.Different:
                return COMPARATOR_DIFFERENT;
            case Comparators.Ends:
                return COMPARATOR_ENDS;
            case Comparators.Equal:
                return COMPARATOR_EQUAL;
            case Comparators.EqualLower:
                return COMPARATOR_EQUALLOWER;
            case Comparators.EqualUpper:
                return COMPARATOR_EQUALGREATER;
            case Comparators.Lower:
                return COMPARATOR_LOWER;
            case Comparators.Starts:
                return COMPARATOR_STARTS;
            case Comparators.Upper:
                return COMPARATOR_GREATER;
            case Comparators.Into:
                return COMPARATOR_INTO;
            case Comparators.NotInto:
                return COMPARATOR_NOTINTO;
            default:
                return string.Empty;
        }
    }

    /// <summary>
    /// Limpia un string builder, ya que hasta framework 4.0 no existe este metodo
    /// </summary>
    /// <param name="sbToClear"></param>
    private static void ClearStringBuilder(ref StringBuilder sbToClear)
    {
        sbToClear.Length = 0;
        sbToClear.Capacity = 0;
    }

    public const string ATTRIBUTE_SELECTOR_REGEX_FORMAT = "[\\?\\s]{0}=\\\"([^\\\"]+)";
    public static string GetAttributeValue(string tag, string attributeName)
    {
        Regex reg = new Regex(string.Format(ATTRIBUTE_SELECTOR_REGEX_FORMAT, attributeName));
        if (reg.IsMatch(tag))
        {
            return reg.Match(tag).Value.Replace(attributeName, string.Empty).Replace("=", string.Empty).Replace("\"", string.Empty).Trim();
        }
        else
        {
            return string.Empty;
        }
    }

    public static string[] GetMinValueAttributes(IIndex index, ITaskResult taskResult)
    {
        string minValue = TextoInteligente.GetValueFromZvarOrSmartText(index.MinValue, taskResult);

        string[] splitedValues = minValue.Split(null);

        if (splitedValues.Length > 0)
        {
            minValue = splitedValues[0];
        }

        //Si hay valor maximo
        if (!string.IsNullOrEmpty(minValue))
        {
            //Parseo el maximo
            ParsedValue parsedMin = ParsedValue.Parse(minValue);

            //Si el maximo es un numero, lo pasamos
            if (parsedMin != null && (parsedMin.ValueType == typeof(long) || parsedMin.ValueType == typeof(double) || parsedMin.ValueType == typeof(DateTime)))
            {
                return new string[] { "haveMinValue", "ZMinValue=\"" + minValue + "\"" };
            }

            //Si es zgetdate
            if (minValue.ToLower().Contains("zgetdate") || minValue.StartsWith("="))
            {
                return new string[] { "haveMinValue", "ZMinValue=\"" + minValue + "\"" };
            }
        }

        return null;
    }


    public static string[] GetMaxValueAttributes(IIndex index, ITaskResult taskResult)
    {
        string maxValue = TextoInteligente.GetValueFromZvarOrSmartText(index.MaxValue, taskResult).Trim();

        string[] splitedValues = maxValue.Split(null);

        if (splitedValues.Length > 0)

            maxValue = splitedValues[0];

        //Si hay valor maximo
        if (!string.IsNullOrEmpty(maxValue))
        {
            //Parseo el maximo
            ParsedValue parsedMax = ParsedValue.Parse(maxValue);

            //Si el maximo es un numero, lo pasamos
            if (parsedMax != null && (parsedMax.ValueType == typeof(long) || parsedMax.ValueType == typeof(double) || parsedMax.ValueType == typeof(DateTime)))
            {
                return new string[] { "haveMaxValue", "ZMaxValue=\"" + maxValue + "\"" };
            }

            //Si es zgetdate
            if (maxValue.ToLower().Contains("zgetdate") || maxValue.StartsWith("="))
            {
                return new string[] { "haveMaxValue", "ZMaxValue=\"" + maxValue + "\"" };
            }
        }

        return null;
    }

    public static string[] GetOriginalValueAttribute(IIndex index)
    {
        return new string[] { string.Empty, "ZOriginalValue=\"" + index.Data + "\"" };
    }

    public static string FormatMultipleChildAttribute(List<long> list)
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
