namespace Zamba.FormBrowser.Helpers
{
	using System;
	using System.Text;
	using Zamba.Core;

	internal static class AtributesHelper
	{
		#region AppendHelpers
		private static void AppendClass(StringBuilder sbClasses, string cssClass)
		{
			if (sbClasses.Length > 0 && sbClasses.ToString()[sbClasses.Length - 1] != HTML.SPACE_CHAR)
				sbClasses.Append(HTML.SPACE_CHAR);

			sbClasses.Append(cssClass);
		}

		private static void AppendAttribute(StringBuilder sbAttributes, string attributeFormat, object attributeValue)
		{
			if (sbAttributes.Length > 0 && sbAttributes.ToString()[sbAttributes.Length - 1] != HTML.SPACE_CHAR)
				sbAttributes.Append(HTML.SPACE_CHAR);

			sbAttributes.AppendFormat(attributeFormat, attributeValue);
		}
		#endregion

		internal static string SetTagAttributes(IIndex index, string tag, ITaskResult taskResult)
		{
			string tagToReturn = tag;
			StringBuilder sbClasses = new StringBuilder();
			StringBuilder sbAttributes = new StringBuilder();

			if (tag.StartsWith(HTML.START_INPUT) || tag.StartsWith(HTML.START_TEXTAREA))
				GetInputAndTextareaDecorators(index,tagToReturn, sbClasses, sbAttributes, taskResult);
			else
				if (tag.StartsWith(HTML.START_SELECT))
					GetSelectDecorators(index,tagToReturn, sbClasses, sbAttributes);
				else
					throw new Exception("El elemento no inicia correctamente. Elemento: " + tag);

			int classIndex = tagToReturn.LastIndexOf(HTML.START_CLASS_ATTRIBUTE);

			if (classIndex < 0)
				tagToReturn = InsertClassAndAtributes(tagToReturn, sbClasses, sbAttributes);
			else
				tagToReturn = UpdateClassesAndInsertAtributes(tagToReturn, sbClasses, sbAttributes);

			return tagToReturn;
		}

		private static string UpdateClassesAndInsertAtributes(string tag, StringBuilder sbClasses, StringBuilder sbAttributes)
		{
            if(sbClasses.Length > 0)
                sbClasses.Append(" ");

            sbClasses.Append(HTML.GetAttributeValue(tag, "class").Trim());

            //Limpiamos las clases anteriores
            StringBuilder tempSb = new StringBuilder(tag);
            string temporalTag = tag;
            int classIndex = temporalTag.IndexOf(HTML.START_CLASS_ATTRIBUTE);
            while (classIndex > -1)
            {
                tempSb.Remove(classIndex, temporalTag.IndexOf(HTML.QUOTE, classIndex + HTML.START_CLASS_ATTRIBUTE.Length) - classIndex + 1);
                temporalTag = tempSb.ToString();
                classIndex = temporalTag.IndexOf(HTML.START_CLASS_ATTRIBUTE, classIndex);
            }

            return InsertClassAndAtributes(temporalTag, sbClasses, sbAttributes);
		}

		private static string InsertClassAndAtributes(string tag, StringBuilder sbClasses, StringBuilder sbAttributes)
		{
			StringBuilder sbToReturn = new StringBuilder();
			StringBuilder sb = new StringBuilder(tag);

			if (tag.StartsWith(HTML.START_INPUT))
				sb.Insert(tag.IndexOf(HTML.CLOSE_CHAR_TAG), " {0}");
			else
				sb.Insert(tag.IndexOf(HTML.END_CHAR_TAG), " {0}");

			tag = sb.ToString();
			sb.Clear();
			sb.AppendFormat(HTML.CLASS_ATTRIBUTE_FORMAT, sbClasses);
			if (sbAttributes.ToString()[0] != HTML.SPACE_CHAR)
				sb.Append(HTML.SPACE_CHAR);

			sb.Append(sbAttributes);

			sbToReturn.AppendFormat(tag, sb);

			return sbToReturn.ToString();
		}

		private static void GetSelectDecorators(IIndex index, string tagToReturn, StringBuilder sbClasses, StringBuilder sbAttributes)
		{
			GetRequiredDecorator(index, tagToReturn, sbClasses, sbAttributes);
			GetDefaultDecorator(index, tagToReturn, sbClasses, sbAttributes);
			GetIndexNameDecorator(index, tagToReturn, sbClasses, sbAttributes);
			GetOriginalValueDecorator(index, tagToReturn, sbClasses, sbAttributes);
			GetHierarchyFunctionally(index, tagToReturn, sbClasses, sbAttributes);
		}

		private static void GetInputAndTextareaDecorators(IIndex index, string tagToReturn, 
			StringBuilder sbClasses, StringBuilder sbAttributes, ITaskResult taskResult)
		{
			bool isTextTag = HTML.IsTextTag(tagToReturn);

			GetRequiredDecorator(index, tagToReturn, sbClasses, sbAttributes);
			GetDefaultDecorator(index, tagToReturn, sbClasses, sbAttributes);
			GetIndexNameDecorator(index, tagToReturn, sbClasses, sbAttributes);
			GetOriginalValueDecorator(index, tagToReturn, sbClasses, sbAttributes);

			if (isTextTag)
			{
				GetLengthDecorator(index, tagToReturn, sbClasses, sbAttributes);
				GetDataTypeDecorator(index, tagToReturn, sbClasses, sbAttributes);
				GetMaxValueDecorator(index, tagToReturn, sbClasses, sbAttributes, taskResult);
				GetMinValueDecorator(index, tagToReturn, sbClasses, sbAttributes, taskResult);
			}
		}

		private static void GetHierarchyFunctionally(IIndex index, string tagToReturn, StringBuilder sbClasses, StringBuilder sbAttributes)
		{
			if (index.HierarchicalChildID != null && index.HierarchicalChildID.Count > 0)
			{
				AppendClass(sbClasses, "HierarchicalIndex");
				AppendAttribute(sbAttributes, HTML.HIERARCHYCHILD_ATTRIBUTE_FORMAT, HTML.FormatMultipleChildAttribute(index.HierarchicalChildID));
			}
		}

		private static void GetOriginalValueDecorator(IIndex index, string tagToReturn, StringBuilder sbClasses, StringBuilder sbAttributes)
		{
			AppendAttribute(sbAttributes, HTML.ORIGINALVALUE_ATTRIBUTE_FORMAT, index.Data);
		}

		private static void GetIndexNameDecorator(IIndex index, string tagToReturn, StringBuilder sbClasses, StringBuilder sbAttributes)
		{
			AppendAttribute(sbAttributes, HTML.INDEXNAME_ATTRIBUTE_FORMAT, index.Name);
		}

		private static void GetDefaultDecorator(IIndex index, string tagToReturn, StringBuilder sbClasses, StringBuilder sbAttributes)
		{
			if (tagToReturn.Contains("DefaultValue="))
				return;

			if (!string.IsNullOrEmpty(index.DefaultValue))
			{
				AppendClass(sbClasses, "haveDefaultValue");
				AppendAttribute(sbAttributes, HTML.DEFAULTVALUE_ATTRIBUTE_FORMAT, index.DefaultValue);
			}
		}

		private static void GetRequiredDecorator(IIndex index, string tagToReturn, StringBuilder sbClasses, StringBuilder sbAttributes)
		{
			if (tagToReturn.Contains("isRequired"))
				return;

			if (index.Required)
			{
				AppendClass(sbClasses, "isRequired");
			}
		}

		private static void GetMinValueDecorator(IIndex index, string tagToReturn,
			StringBuilder sbClasses, StringBuilder sbAttributes, ITaskResult taskResult)
		{
			if (!string.IsNullOrEmpty(index.MinValue))
			{
				string minValue = TextoInteligente.GetValueFromZvarOrSmartText(index.MinValue, taskResult).Trim();
				GetMinValueDecorator(index, tagToReturn, sbClasses, sbAttributes, minValue);
			}
		}

		private static void GetMinValueDecorator(IIndex index, string tagToReturn, 
			StringBuilder sbClasses, StringBuilder sbAttributes, string decodedMinValue)
		{
			if (tagToReturn.Contains("ZMinValue="))
				return;

			string[] splitedValues = decodedMinValue.Split(null);

			if (splitedValues.Length > 0)
			{
				decodedMinValue = splitedValues[0];
			}

			//Si hay valor maximo
			if (!string.IsNullOrEmpty(decodedMinValue))
			{
				//Parseo el maximo
				ParsedValue parsedMin = ParsedValue.Parse(decodedMinValue);

				//Si el maximo es un numero, lo pasamos
				if (parsedMin != null && (parsedMin.ValueType == typeof(long) || parsedMin.ValueType == typeof(double)
					|| parsedMin.ValueType == typeof(DateTime)))
				{
					AppendClass(sbClasses, "haveMinValue");
					AppendAttribute(sbAttributes, HTML.ZMINVALUE_ATTRIBUTE_FORMAT, decodedMinValue);
				}

				//Si es zgetdate
				if (decodedMinValue.ToLower().Contains("zgetdate") || decodedMinValue.StartsWith("="))
				{
					AppendClass(sbClasses, "haveMinValue");
					AppendAttribute(sbAttributes, HTML.ZMINVALUE_ATTRIBUTE_FORMAT, decodedMinValue);
				}
			}
		}

		private static void GetMaxValueDecorator(IIndex index, string tagToReturn, 
			StringBuilder sbClasses, StringBuilder sbAttributes, ITaskResult taskResult)
		{
			if (!string.IsNullOrEmpty(index.MaxValue))
			{
				string maxValue = TextoInteligente.GetValueFromZvarOrSmartText(index.MaxValue, taskResult).Trim();
				GetMaxValueDecorator(index, tagToReturn, sbClasses, sbAttributes, maxValue);
			}
		}

		private static void GetMaxValueDecorator(IIndex index, string tagToReturn, 
			StringBuilder sbClasses, StringBuilder sbAttributes, string decodedMaxValue)
		{
			if (tagToReturn.Contains("ZMaxValue="))
				return;

			string[] splitedValues = decodedMaxValue.Split(null);

			if (splitedValues.Length > 0)
			{
				decodedMaxValue = splitedValues[0];
			}

			//Si hay valor maximo
			if (!string.IsNullOrEmpty(decodedMaxValue))
			{
				//Parseo el maximo
				ParsedValue parsedMax = ParsedValue.Parse(decodedMaxValue);

				//Si el maximo es un numero, lo pasamos
				if (parsedMax != null && (parsedMax.ValueType == typeof(long) || parsedMax.ValueType == typeof(double)
					|| parsedMax.ValueType == typeof(DateTime)))
				{
					AppendClass(sbClasses, "haveMaxValue");
					AppendAttribute(sbAttributes, HTML.ZMAXVALUE_ATTRIBUTE_FORMAT, decodedMaxValue);
				}

				//Si es zgetdate
				if (decodedMaxValue.ToLower().Contains("zgetdate") || decodedMaxValue.StartsWith("="))
				{
					AppendClass(sbClasses, "haveMaxValue");
					AppendAttribute(sbAttributes, HTML.ZMAXVALUE_ATTRIBUTE_FORMAT, decodedMaxValue);
				}
			}
		}

		private static void GetDataTypeDecorator(IIndex index, string tagToReturn, StringBuilder sbClasses, StringBuilder sbAttributes)
		{
			if (tagToReturn.Contains("dataType="))
				return;

			string dataType = string.Empty;

			switch (index.Type)
			{
				case IndexDataType.Fecha:
				case IndexDataType.Fecha_Hora:
					dataType = "date";
					break;
				case IndexDataType.Moneda:
				case IndexDataType.Numerico_Decimales:
					dataType = "decimal_2_16";
					break;
				case IndexDataType.Numerico_Largo:
				case IndexDataType.Numerico:
					dataType = "numeric";
					break;
				case IndexDataType.Alfanumerico:
				case IndexDataType.Alfanumerico_Largo:
				case IndexDataType.None:
				case IndexDataType.Si_No:
				default:
					break;
			}

			if (!string.IsNullOrEmpty(dataType))
			{
				AppendClass(sbClasses, "dataType");
				AppendAttribute(sbAttributes, HTML.DATATYPE_ATTRIBUTE_FORMAT, dataType);
			}
		}

		private static void GetLengthDecorator(IIndex index, string tagToReturn, StringBuilder sbClasses, StringBuilder sbAttributes)
		{
			if (tagToReturn.Contains("length="))
				return;

			if (index.Len > 0)
			{
				AppendClass(sbClasses, "length");
				switch (index.Type)
				{
					case IndexDataType.Fecha:
						AppendAttribute(sbAttributes, HTML.LENGTH_ATTRIBUTE_FORMAT, HTML.simpleDateLenght);
						break;
					case IndexDataType.Fecha_Hora:
						AppendAttribute(sbAttributes, HTML.LENGTH_ATTRIBUTE_FORMAT, HTML.fullDateLenght);
						break;
					default:
						AppendAttribute(sbAttributes, HTML.LENGTH_ATTRIBUTE_FORMAT, index.Len);
						break;
				}
			}
		}
	}
}
