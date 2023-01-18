using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba;
using Zamba.Core;
//using Zamba.Core.WF.WF;

namespace Helpers
{
    /// <summary>
    /// Clase utilizada para generar validaciones en la UI
    /// </summary>
    public class Validators
    {
        /// <summary>
        /// Completa en un control dado, las validaciones necesarias(como hace formbrowser).
        /// </summary>
        /// <param name="Index">Atributo base para validaciones.</param>
        /// <param name="ValidateControl">Control base para volcar los datos</param>
        /// <returns></returns>
        public static Control GetControlWithValidations(IIndex Index, Control ValidateControl, WebModuleMode indexLocation, ITaskResult taskResult, ZFieldType fieldType)
        {
            TextBox tbControl;
            DropDownList ddControl;
            StringBuilder sbClasses = new StringBuilder();
            AttributeCollection acAttributes = new AttributeCollection(new StateBag());

            string[] types = GetTypeToValidateFromIndex(Index.Type);
            if (types != null)
            {
                sbClasses.Append(" dataType");
                acAttributes.Add("dataType", types[1].Split('=')[1].Replace("\"",string.Empty));
            }

            if (Index.Required && indexLocation != WebModuleMode.Search)
                sbClasses.Append(" isRequired");

            if (fieldType != ZFieldType.None)
            {
                sbClasses.Append(" ");
                sbClasses.Append(fieldType.ToString());
            }

            sbClasses.Append(" length");
            acAttributes.Add("length", Index.Len.ToString());

            if (!string.IsNullOrEmpty(Index.DefaultValue) && indexLocation != WebModuleMode.Search)
            {
                sbClasses.Append(" haveDefaultValue");
                acAttributes.Add("DefaultValue", Index.DefaultValue);
            }

            if (taskResult != null) 
            {
                string maxValue = TextoInteligente.GetValueFromZvarOrSmartText(Index.MaxValue, taskResult);
                string[] splitedValues = maxValue.Split(null);

                if (splitedValues.Length > 0)
                {
                    maxValue = splitedValues[0];
                }
                if (!string.IsNullOrEmpty(maxValue) && indexLocation != WebModuleMode.Search)
                {
                    sbClasses.Append(" haveMaxValue");
                    acAttributes.Add("ZMaxValue", maxValue);
                }

                string minValue = TextoInteligente.GetValueFromZvarOrSmartText(Index.MinValue, taskResult);
                splitedValues = minValue.Split(null);

                if (splitedValues.Length > 0)
                {
                    minValue = splitedValues[0];
                }
                if (!string.IsNullOrEmpty(minValue) && indexLocation != WebModuleMode.Search)
                {
                    sbClasses.Append(" haveMinValue");
                    acAttributes.Add("ZMinValue", minValue);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Index.MaxValue) && indexLocation != WebModuleMode.Search)
                {
                    sbClasses.Append(" haveMaxValue");
                    acAttributes.Add("ZMaxValue", Index.MaxValue);
                }
                if (!string.IsNullOrEmpty(Index.MinValue) && indexLocation != WebModuleMode.Search)
                {
                    sbClasses.Append(" haveMinValue");
                    acAttributes.Add("ZMinValue", Index.MaxValue);
                }
            }
            
            IEnumerator keys = acAttributes.Keys.GetEnumerator();
            string key;
            if (Index.DropDown != IndexAdditionalType.DropDown &&
                Index.DropDown != IndexAdditionalType.DropDownJerarquico)
            {
                tbControl = (TextBox)ValidateControl;
                tbControl.CssClass += sbClasses.ToString();

                while (keys.MoveNext())
                {
                    key = (string)keys.Current;
                    tbControl.Attributes.Add(key, acAttributes[key]);
                }

                return tbControl;
            }

            ddControl = (DropDownList)ValidateControl;
            ddControl.CssClass += sbClasses.ToString();

            while (keys.MoveNext())
            {
                key = (string)keys.Current;
                ddControl.Attributes.Add(key, acAttributes[key]);
            }
            return ddControl;
        }

        //Todos los metodos que se encuentran aca devolveran los datos en formato:
        //<NombreClase> | <Atributo>="Valor"
        #region Attributes & Classes To Append

        private static string[] GetLengthFromIndex(IIndex index)
        {
            if (index.Len > 0)
            {
                switch (index.Type)
                {
                    case IndexDataType.Fecha:
                        return new[] {"length", "length=\"" + DateTime.Now.ToString("dd/mm/yyyy").Length + "\""};
                    case IndexDataType.Fecha_Hora:
                        return new[]
                                   {
                                       "length",
                                       "length=\"" + (DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss tt").Length + 2) + "\""
                                   };
                    default:
                        return new[] {"length", "length=\"" + index.Len};
                }
            }
            return null;
        }

        private static string[] GetTypeToValidateFromIndex(IndexDataType indexType)
        {
            switch (indexType)
            {
                case IndexDataType.Fecha:
                case IndexDataType.Fecha_Hora:
                    return new[] {"dataType", "dataType=\"date\""};
                case IndexDataType.Moneda:
                case IndexDataType.Numerico_Decimales:
                    return new[] {"dataType", "dataType=\"decimal_2_16\""};
                case IndexDataType.Numerico_Largo:
                case IndexDataType.Numerico:
                    return new[] {"dataType", "dataType=\"numeric\""};
                default:
                    return null;
            }
        }

        private static string[] GetHierarchyFunctionally(IIndex index)
        {
            if (index.HierarchicalChildID !=  null)
            {
                return new[] {"HierarchicalIndex", "ChildIndexId=\"" + HTML.FormatMultipleChildAttribute( index.HierarchicalChildID) + "\""};
            }
            return null;
        }

        private static string[] GetDefaultValue(IIndex index)
        {
            if (!string.IsNullOrEmpty(index.DefaultValue))
            {
                return new[] {"haveDefaultValue", "DefaultValue=\"" + index.DefaultValue + "\""};
            }
            return null;
        }

        private static string[] GetRequiredFromIndex(IIndex index)
        {
            if (index.Required)
            {
                return new[] {"isRequired", string.Empty};
            }
            return null;
        }

        #endregion
    }
}