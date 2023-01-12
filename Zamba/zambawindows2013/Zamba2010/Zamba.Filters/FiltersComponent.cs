#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Zamba.AppBlock;
using Zamba.Core;
using Zamba.Core.Cache;
using Zamba.Data;


#endregion

namespace Zamba.Filters
{
    public class FiltersComponent : IFiltersComponent
    {

        /// <summary>
        /// Remueve un filtro en específico de un entidad.
        /// </summary>
        /// <param name="fe"></param>
        public void RemoveFilter(IFilterElem fe, FilterTypes filterType)
        {
            if (filterType == FilterTypes.Document || filterType == FilterTypes.Task)
            {
                if (Search.HsFilters.Contains(fe.DocTypeId + "-" + filterType))
                    ((List<IFilterElem>)Search.HsFilters[fe.DocTypeId + "-" + filterType]).Remove((FilterElem)fe);

                RightFactory.RemoveFilter(fe.Id);
            }
            else
            {
                if (Search.FiltersCache.Contains(fe.Id))
                    Search.FiltersCache.Remove(fe.Id);
            }
        }

        /// <summary>
        /// Remueve los filtros de un entidad.
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <param name="userId"></param>
        public void ClearFilters(Int64 docTypeId, Int64 userId, FilterTypes filterType, DataTable dataTable, Boolean removeDefaultFilters)
        {
            if (filterType == FilterTypes.Task || filterType == FilterTypes.Document)
            {
                List<IFilterElem> KeepDefaultFilters = new List<IFilterElem>();
                List<IFilterElem> filters;
                string filType = "-" + filterType.ToString();

                if (docTypeId == 0 && !Search.HsFilters.ContainsKey("0" + filType))
                {
                    char[] separator = { '-' };
                    List<Int64> docTypeIds = new List<Int64>();
                    filters = new List<IFilterElem>();

                    foreach (DictionaryEntry de in Search.HsFilters)
                    {
                        if (de.Key.ToString().Contains(filType))
                        {
                            filters.AddRange((List<IFilterElem>)de.Value);
                            docTypeIds.Add(Int64.Parse(de.Key.ToString().Split(separator)[0]));
                        }
                    }

                    //si tiene permisos de remover todos los filtros quitarlos a todos
                    if (removeDefaultFilters || filters[0].Type.ToString() != "defecto")
                    {
                        foreach (Int64 dtId in docTypeIds)
                            ((List<IFilterElem>)Search.HsFilters[dtId + filType]).Clear();
                    }
                    else
                    {
                        //si existen ambos tipos guardo los por defecto
                        foreach (FilterElem elemento in filters)
                        {
                            if (string.Compare(elemento.Type.ToLower(), "defecto") == 0)
                            {
                                KeepDefaultFilters.Add(elemento);
                            }
                        }

                        foreach (Int64 dtId in docTypeIds)
                            Search.HsFilters[dtId + filType] = KeepDefaultFilters;
                    }

                    foreach (Int64 dtId in docTypeIds)
                        RightFactory.ClearFilters(dtId, userId, removeDefaultFilters);
                }
                else
                {
                    filters = (List<IFilterElem>)Search.HsFilters[docTypeId + filType];

                    //si tiene permisos de remover todos los filtros quitarlos a todos
                    if (removeDefaultFilters || filters[0].Type.ToString() != "defecto")
                    {
                        ((List<IFilterElem>)Search.HsFilters[docTypeId + filType]).Clear();
                    }
                    else
                    {
                        //si existen ambos tipos guardo los por defecto
                        foreach (FilterElem elemento in filters)
                        {
                            if (string.Compare(elemento.Type.ToLower(), "defecto") == 0)
                            {
                                KeepDefaultFilters.Add(elemento);
                            }
                        }

                        Search.HsFilters[docTypeId + filType] = KeepDefaultFilters;
                    }

                    RightFactory.ClearFilters(docTypeId, userId, removeDefaultFilters);
                }
            }
            else
                Search.FiltersCache.Clear();
        }


        /// <summary>
        /// Obtiene la cantidad de filtros aplicados sobre un entidad.
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <returns></returns>
        public int GetDocumentFiltersCount(Int64 docTypeId, FilterTypes filterType)
        {
            if (filterType == FilterTypes.Task || filterType == FilterTypes.Document)
            {
                return Search.HsFilters.Contains(docTypeId + "-" + filterType) ? ((List<IFilterElem>)Search.HsFilters[docTypeId + "-" + filterType]).Count : 0;
            }
            else
                return Search.FiltersCache.Count;
        }

        /// <summary>
        /// Obtiene los filtros de un entidad.
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public List<IFilterElem> GetLastUsedFilters(Int64 docTypeId, Int64 currentUserId, FilterTypes filterType)
        {
            Int64 attribute;
            lock (Search.HsFilters)
            {
                if (filterType == FilterTypes.Task || filterType == FilterTypes.Document)
                {
                    if (!Search.HsFilters.Contains(docTypeId + "-" + filterType))
                    {
                        var filters = new List<IFilterElem>();
                        var lastFilters = RightFactory.GetFilters(docTypeId, currentUserId);

                        foreach (DataRow row in lastFilters.Tables[0].Rows)
                        {
                            if (((filterType == FilterTypes.Task) && (String.Compare(row["FilterType"].ToString(), "search") != 0)) || ((filterType == FilterTypes.Document) && (String.Compare(row["FilterType"].ToString(), "search") == 0)) || ((filterType == FilterTypes.Document) && (String.Compare(row["FilterType"].ToString(), "defecto") == 0 && Int64.TryParse(row["attribute"].ToString(), out attribute))))
                            {
                                Int64 filterAttributeId;
                                Int64.TryParse(row["Attribute"].ToString(), out filterAttributeId);

                                string value = row["Value"].ToString();
                                if (value.StartsWith("(") && value.EndsWith(")"))
                                    value = value.Remove(value.Length - 1).Remove(0, 1);

                                var fe = new FilterElem(Int64.Parse(row["Id"].ToString()),
                                                        filterAttributeId,
                                                        row["Attribute"].ToString(),
                                                        value,
                                                        row["Comparator"].ToString().Trim(),
                                                        row["FilterType"].ToString(),
                                                        (IndexDataType)Int32.Parse(row["DataType"].ToString()),
                                                        currentUserId,
                                                        Int64.Parse(row["DocTypeId"].ToString()),
                                                        row["Description"].ToString(),
                                                        (IndexAdditionalType)Int32.Parse(row["IndexDropDown"].ToString()));

                                if (row["Enabled"].ToString() == "1")
                                    fe.Enabled = true;
                                else
                                {
                                    fe.Enabled = false;
                                }
                                filters.Add(fe);
                            }
                        }

                        if (!Search.HsFilters.ContainsKey(docTypeId + "-" + filterType)) Search.HsFilters.Add(docTypeId + "-" + filterType, filters);
                        else
                            Search.HsFilters[docTypeId + "-" + filterType] = filters;
                    }
                }
                else
                {
                    List<IFilterElem> elements = new List<IFilterElem>();

                    foreach (IFilterElem elem in Search.FiltersCache.Values)
                    {
                        elements.Add(elem);
                    }

                    return elements;
                }

                string filterIndex = docTypeId + "-" + filterType;

                return (List<IFilterElem>)Search.HsFilters[filterIndex];
            }
        }

        /// <summary>
        /// Agrega un nuevo filtro.
        /// </summary>
        /// <param name="indexId"></param>
        /// <param name="attribute"></param>
        /// <param name="dataType"></param>
        /// <param name="userId"></param>
        /// <param name="comparator"></param>
        /// <param name="filterValue"></param>
        /// <param name="docTypeId"></param>
        /// <param name="save"></param>
        /// <param name="description"></param>
        /// <param name="additionalType">Tipo de sustitucion del indice</param>
        /// <returns></returns>
        public IFilterElem SetNewFilter(Int64 indexId,
                                        String attribute,
                                        IndexDataType dataType,
                                        Int64 userId,
                                        String comparator,
                                        String filterValue,
                                        Int64 docTypeId,
                                        Boolean save,
                                        String description,
                                        IndexAdditionalType additionalType,
                                        String FilterType,
                                        FilterTypes FType)
        {
            var compareString = comparator;


            var fe = new FilterElem(0,
                                    indexId,
                                    attribute,
                                    filterValue,
                                    compareString,
                                    FilterType,
                                    dataType,
                                    userId,
                                    docTypeId,
                                    description,
                                    additionalType);

            if (FType != FilterTypes.Document && FType != FilterTypes.Task)
                fe.Id = Search.FiltersCache.Keys.Count + 1;

            if (fe.Type.Equals("search"))
                save = false;

            if (FType == FilterTypes.History)
                save = false;

            // Verifica si existe el filtro en la cache antes de insertarlo
            if (!ExistFilter(fe, FType))
            {
                if (FType == FilterTypes.Document || FType == FilterTypes.Task)
                {
                    //Se guarda el filtro en cache.
                    if (Search.HsFilters.Contains(docTypeId + "-" + FType))
                    {
                        ((List<IFilterElem>)Search.HsFilters[docTypeId + "-" + FType]).Add(fe);
                    }
                    else
                    {
                        var filters = new List<IFilterElem> { fe };
                        Search.HsFilters.Add(docTypeId + "-" + FType, filters);
                    }

                    //Se guarda el filtro en la base.
                    if (save)
                    {
                        var attrib = fe.Filter;

                        //Verifica si debe remover la I del Atributo.
                        if (indexId > 0)
                            attrib = attrib.Remove(0, 1);

                        //Se agrega el Atributo a la base.
                        fe.Id = RightFactory.InsertFilter(attrib, fe.FormatValue, (Int32)fe.DataType,
                                                                  fe.Comparator, fe.Type, fe.DocTypeId, fe.UserId, fe.Description, (Int32)fe.IndexSubsType);
                    }
                }
                else
                {
                    //Se guarda el filtro en cache.
                    Search.FiltersCache.Add(fe.Id, fe);
                    if (save)
                    {
                        var attrib = fe.Filter;

                        //Verifica si debe remover la I del Atributo.
                        if (indexId > 0)
                            attrib = attrib.Remove(0, 1);

                        //Se agrega el Atributo a la base.
                        fe.Id = RightFactory.InsertFilter(attrib, fe.FormatValue, (Int32)fe.DataType,
                                                                  fe.Comparator, fe.Type, fe.DocTypeId, fe.UserId, fe.Description, (Int32)fe.IndexSubsType);
                    }
                }
            }

            return fe;
        }

        /// <summary>
        /// Habilita o deshabilita un filtro dependiendo de la propiedad Enabled del filtro.
        /// </summary>
        /// <param name="fe"></param>
        public void SetEnabledFilter(IFilterElem fe, FilterTypes filterType)
        {
            //Se actualiza la cache.
            var index = -1;

            if (filterType == FilterTypes.Task || filterType == FilterTypes.Document)
            {
                if (Search.HsFilters.ContainsKey(fe.DocTypeId + "-" + filterType))
                {
                    index = ((List<IFilterElem>)Search.HsFilters[fe.DocTypeId + "-" + filterType]).IndexOf((FilterElem)fe);
                }

                if (index != -1)
                    ((List<IFilterElem>)Search.HsFilters[fe.DocTypeId + "-" + filterType])[index] = (FilterElem)fe;
            }
            else
            {
                if (Search.FiltersCache.Contains(fe.Id))
                    Search.FiltersCache[fe.Id] = fe;
            }
        }

        /// <summary>
        /// Actualiza los datos de un filtro en la base.
        /// </summary>
        /// <param name="fe"></param>
        public void SaveFilterInDatabase(IFilterElem fe)
        {
            //Se actualiza los datos en la base.
            RightFactory.UpdateFilterEnabled(fe.Id, fe.Enabled);
        }

        /// <summary>
        ///   Método que sirve para filtrar la grilla
        /// </summary>
        /// <exception cref="ArgumentNullException">Cuando el objeto filters no se enucentra instanciado.</exception>
        /// <history> 
        ///   [Gaston]    16/10/2008   Modified    Filtros acumulables 
        ///   [Gaston]    17/10/2008   Modified    Mejora de filtros
        ///   [Gaston]    17/10/2008   Modified    ListView
        ///   [Sebastián] 04-05-09     Modified    Agregado de IF para evitar exception en busqueda de documentos
        /// </history>
        public static string GetFiltersString(IEnumerable<IFilterElem> filters, bool defaultFiltersOnly, bool isCaseSensitive, ref List<long> slstIndexsIds)
        {
            if (filters == null) throw new ArgumentNullException("filters", @"El objeto filters debe encontrare instanciado.");
            var filterstring = new StringBuilder();
            string findOperator;
            bool useLower = isCaseSensitive && Servers.Server.isOracle;
            string caseString = useLower ? "LOWER(" : string.Empty;

            try
            {
                //Para cada filtro, armo la consulta
                foreach (FilterElem fElem in filters)
                {
                    if (fElem.Enabled)
                    {
                        if (defaultFiltersOnly && fElem.Type != "defecto")
                        {
                            fElem.Enabled = false;
                            continue;
                        }

                        if (!filterstring.ToString().StartsWith(" AND ") || !filterstring.ToString().EndsWith(" AND "))
                            filterstring.Append(" AND ");

                        filterstring.Append("(");
                        string columnToFilter = fElem.Filter.Trim();
                        //string Value = fElem.Value.Trim().ToLower();
                        string Value = fElem.Value.Trim();

                        if (fElem.IndexSubsType == IndexAdditionalType.NoIndex)
                        {
                            //GridColumns gc = new GridColumns();
                            if (GridColumns.ZambaColumns != null)
                            {
                                //if (GridColumns.ZambaColumns.ContainsValue(columnToFilter))
                                //{
                                columnToFilter = columnToFilter.Replace(columnToFilter, GridColumns.GetColumnNameByAliasName(columnToFilter));
                                if (fElem.Value.ToLower().Contains(" or ") && fElem.Value.ToLower().Contains(" like "))
                                {
                                    string valor = fElem.Value.Replace(" like ", "§");
                                    valor = valor.Replace(" or ", "¶");
                                    foreach (String value in valor.Split(char.Parse("¶")))
                                    {
                                        String newString = value.Split(char.Parse("§"))[0].Replace("(", string.Empty).Replace(")", string.Empty);
                                        String newString2 = newString.Replace(fElem.Filter.Trim(), columnToFilter);

                                        newString2 = FormatColumnName(newString2);
                                        Value = Value.Replace(newString, newString2);
                                    }
                                }
                                //}
                            }
                        }


                        Int64 numericvalue = 0;
                        //elemento = FormatColumnName(elemento);
                        if (fElem.IndexSubsType == IndexAdditionalType.NoIndex || fElem.IndexSubsType == IndexAdditionalType.LineText)
                        {
                            columnToFilter = FormatColumnName(columnToFilter);
                        }
                        else if (fElem.IndexSubsType == IndexAdditionalType.DropDown || fElem.IndexSubsType == IndexAdditionalType.DropDownJerarquico)
                        {
                            columnToFilter = FormatColumnName(columnToFilter);
                        }
                        else if ((fElem.IndexSubsType == IndexAdditionalType.AutoSustitución || fElem.IndexSubsType == IndexAdditionalType.AutoSustituciónJerarquico) && (Int64.TryParse(fElem.FormatValue, out numericvalue) == false && fElem.Comparator != "=" && fElem.Comparator != "<>"))
                        {
                            if (slstIndexsIds != null)
                                slstIndexsIds.Add(long.Parse(columnToFilter.Replace("I", string.Empty)));

                            columnToFilter = FormatColumnName("slst_s" + columnToFilter.Replace("I", string.Empty) + ".descripcion");
                        }
                        else if ((fElem.IndexSubsType == IndexAdditionalType.AutoSustitución || fElem.IndexSubsType == IndexAdditionalType.AutoSustituciónJerarquico) && fElem.Comparator == "=" && fElem.Comparator == "<>")
                        {
                            if (slstIndexsIds != null)
                                slstIndexsIds.Add(long.Parse(columnToFilter.Replace("I", string.Empty)));

                            columnToFilter = FormatColumnName(columnToFilter);
                        }
                        else
                        {
                            columnToFilter = FormatColumnName(columnToFilter);
                        }


                        try
                        {
                            findOperator = fElem.Comparator;
                            if (fElem.IndexSubsType != IndexAdditionalType.AutoSustitución && ((fElem.DataType == IndexDataType.Numerico) ||
                                                           (fElem.DataType == IndexDataType.Numerico_Largo) ||
                                                           (fElem.DataType == IndexDataType.Numerico_Decimales) ||
                                                           (fElem.DataType == IndexDataType.Moneda)))
                            {
                                double retNum;
                                String doubleToStringValue;
                                if (Double.TryParse(Value.Replace("(", string.Empty).Replace(")", string.Empty), out retNum))
                                {

                                    if (string.Compare(fElem.Comparator, "=") == 0)
                                    {
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append("=");
                                        //filterstring.Append(findOperator.Replace("Contiene", "="));
                                        filterstring.Append(Value.Trim().Replace(",", "."));
                                    }
                                    else if (string.Compare(fElem.Comparator, "Contiene") == 0)
                                    {
                                        doubleToStringValue = Value.Trim().ToString();
                                        if (doubleToStringValue.StartsWith("("))
                                            doubleToStringValue = doubleToStringValue.Remove(0, 1);
                                        if (doubleToStringValue.EndsWith(")"))
                                            doubleToStringValue = doubleToStringValue.Remove(doubleToStringValue.Length - 1, 1);
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append(" LIKE '%");
                                        filterstring.Append(doubleToStringValue);
                                        filterstring.Append("%'");
                                    }
                                    else if (string.Compare(fElem.Comparator, ">") == 0 ||
                                             string.Compare(fElem.Comparator, ">=") == 0 ||
                                             string.Compare(fElem.Comparator, "<") == 0 ||
                                             string.Compare(fElem.Comparator, "<=") == 0 ||
                                             string.Compare(fElem.Comparator, "<>") == 0)
                                    {
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append(findOperator);
                                        filterstring.Append(Value.Trim().Replace(",", "."));
                                    }
                                    else if (string.Compare(fElem.Comparator, "No Contiene") == 0)
                                    {
                                        /*filterstring.Append("(");
                                        filterstring.Append(elemento);
                                        filterstring.Append(" Not in ");
                                        filterstring.Append(Value.Trim().Replace(",", "."));
                                        filterstring.Append(" or ");
                                        filterstring.Append(elemento);
                                        filterstring.Append(" is NULL)");*/

                                        doubleToStringValue = Value.Trim().ToString();
                                        if (doubleToStringValue.StartsWith("("))
                                            doubleToStringValue = doubleToStringValue.Remove(0, 1);
                                        if (doubleToStringValue.EndsWith(")"))
                                            doubleToStringValue = doubleToStringValue.Remove(doubleToStringValue.Length - 1, 1);
                                        filterstring.Append("(");
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append(" NOT LIKE '%");
                                        filterstring.Append(doubleToStringValue);
                                        filterstring.Append("%'");
                                        filterstring.Append(" or ");
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append(" is NULL)");
                                    }

                                }
                                else
                                {
                                    if (string.Compare(fElem.Comparator, "Es Nulo") == 0)
                                    {
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append(" is NULL");
                                    }
                                    else if (string.Compare(fElem.Comparator, "No es Nulo") == 0)
                                    {
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append(" is not NULL");
                                    }
                                    if (string.Compare(fElem.Comparator, "Contiene") == 0)
                                    {

                                        if (Value.Trim().ToLower().Contains("%"))
                                        {
                                            //Cambio el % por el pipe
                                            string tempValue = Value.Replace("%", "|");

                                            //Quito los parentesis
                                            tempValue = tempValue.Replace("(", "");
                                            tempValue = tempValue.Replace(")", "");

                                            StringBuilder tempString = new StringBuilder();
                                            tempString.Append("(");

                                            foreach (string filter in tempValue.Split('|'))
                                            {
                                                tempString.Append(columnToFilter);
                                                tempString.Append(" LIKE '%");
                                                tempString.Append(filter);
                                                tempString.Append("%' AND ");
                                            }

                                            tempString.Remove(tempString.ToString().LastIndexOf("AND"), 3);
                                            tempString.Append(")");
                                            filterstring.Append(tempString);
                                        }
                                        else
                                        {
                                            filterstring.Append(Value.Trim().Replace("%", string.Empty).Replace("not like", "<>"));
                                        }

                                    }
                                    else if (string.Compare(fElem.Comparator.ToLower().Trim(), "no contiene") == 0)
                                    {
                                        filterstring.Append(Value.Trim().Replace("%", string.Empty).Replace("not like", "<>"));
                                    }
                                }
                            }
                            else if (fElem.DataType == IndexDataType.Si_No)
                            {
                                bool boolean;
                                if (bool.TryParse(Value, out boolean))
                                {
                                    if (findOperator == "Es Nulo")
                                    {
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append(" = FALSE");
                                    }
                                    if (findOperator == "No es Nulo")
                                    {
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append(" = TRUE");
                                    }
                                    else
                                    {
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append(" ");
                                        filterstring.Append(findOperator);
                                        filterstring.Append("'");
                                        filterstring.Append(Value.Trim());
                                        filterstring.Append("'");
                                    }
                                }


                                if (bool.TryParse(Value, out boolean))
                                {
                                    filterstring.Append(columnToFilter);
                                    filterstring.Append(" ");
                                    filterstring.Append(findOperator);
                                    filterstring.Append("'");
                                    filterstring.Append(Value.Trim());
                                    filterstring.Append("'");
                                }
                            }
                            else if (fElem.DataType == IndexDataType.Fecha || fElem.DataType == IndexDataType.Fecha_Hora)
                            {
                                DateTime fecha;
                                if (findOperator == "Es Nulo")
                                {
                                    filterstring.Append(columnToFilter);
                                    filterstring.Append(" is NULL");
                                }
                                if (findOperator == "No es Nulo")
                                {
                                    filterstring.Append(columnToFilter);
                                    filterstring.Append(" is not NULL");
                                }

                                if (DateTime.TryParse(Value.Replace("(", string.Empty).Replace(")", string.Empty), out fecha))
                                {
                                    var fechahasta = fecha.AddDays(1);

                                    if (findOperator == ">")
                                    {
                                        filterstring.Append(ServersFactory.ConvertDate(columnToFilter, false));

                                        filterstring.Append(findOperator);
                                        filterstring.Append(ServersFactory.ConvertDate(fecha.ToString(), true));
                                    }
                                    else if (findOperator == "<")
                                    {
                                        filterstring.Append(ServersFactory.ConvertDate(columnToFilter, false));

                                        filterstring.Append(findOperator);
                                        filterstring.Append(ServersFactory.ConvertDate(fecha.ToString(), true));
                                    }
                                    else if (findOperator == ">=")
                                    {
                                        filterstring.Append(ServersFactory.ConvertDate(columnToFilter, false));

                                        filterstring.Append(findOperator);
                                        filterstring.Append(ServersFactory.ConvertDate(fecha.ToString(), true));
                                    }
                                    else if (findOperator == "<=")
                                    {
                                        filterstring.Append(ServersFactory.ConvertDate(columnToFilter, false));
                                        findOperator = "<";
                                        filterstring.Append(findOperator);
                                        filterstring.Append(ServersFactory.ConvertDate(fechahasta.ToString(), true));
                                    }
                                    else if (findOperator == "<>" || findOperator == "No Contiene")
                                    {
                                        filterstring.Append(ServersFactory.ConvertDate(columnToFilter, false));
                                        filterstring.Append("<");
                                        filterstring.Append(ServersFactory.ConvertDate(fecha.ToString(), true));
                                        filterstring.Append(" or ");
                                        filterstring.Append(ServersFactory.ConvertDate(columnToFilter, false));
                                        filterstring.Append(">= ");
                                        filterstring.Append(ServersFactory.ConvertDate(fechahasta.ToString(), true));
                                        filterstring.Append(" or ");
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append(" is NULL");

                                    }
                                    else if (findOperator == "=" || findOperator == "Contiene")
                                    {
                                        filterstring.Append(ServersFactory.ConvertDate(columnToFilter, false));
                                        filterstring.Append(">=");
                                        filterstring.Append(ServersFactory.ConvertDate(fecha.ToString(), true));
                                        filterstring.Append(" and ");
                                        filterstring.Append(ServersFactory.ConvertDate(columnToFilter, false));
                                        filterstring.Append("<");
                                        filterstring.Append(ServersFactory.ConvertDate(fechahasta.ToString(), true));
                                    }
                                }
                            }
                            else if (fElem.IndexSubsType == IndexAdditionalType.AutoSustitución || fElem.DataType == IndexDataType.Alfanumerico ||
                                     fElem.DataType == IndexDataType.Alfanumerico_Largo)
                            {


                                if (findOperator.ToLower().Contains("like"))
                                {
                                    filterstring.Append(useLower ? caseString + columnToFilter + ")" : columnToFilter);
                                    filterstring.Append(" ");
                                    filterstring.Append(findOperator);
                                    filterstring.Append(useLower ? caseString + "'%" : "'%");
                                    filterstring.Append(Value.Trim());
                                    filterstring.Append(useLower ? "%')" : "%'");
                                }
                                else if (string.Compare(findOperator.ToLower(), "contiene") == 0)
                                {
                                    if (Value.Replace("(", String.Empty).Replace(")", String.Empty).Trim() != String.Empty)
                                    {
                                        if (!Value.ToLower().Contains("like"))
                                        {
                                            filterstring.Append(useLower ? caseString + columnToFilter + ")" : columnToFilter);
                                            filterstring.Append(" like ");
                                            filterstring.Append(useLower ? caseString + "'%" : "'%");
                                            filterstring.Append(Value.Trim().Replace("(", String.Empty).Replace(")", String.Empty));
                                            filterstring.Append(useLower ? "%')" : "%'");
                                        }
                                        else
                                        {
                                            if (fElem.NullValue)
                                            {
                                                filterstring.Append("(");
                                                filterstring.Append(Value.Trim().Replace(")", String.Empty));
                                                filterstring.Append(") or (");
                                                filterstring.Append(columnToFilter);
                                                filterstring.Append(" is NULL or ");
                                                filterstring.Append(columnToFilter);
                                                filterstring.Append(" = ");
                                                filterstring.Append("''");
                                                filterstring.Append("))");
                                            }
                                            else
                                            {
                                                StringBuilder tempString = null;

                                                if (fElem.IndexSubsType == IndexAdditionalType.AutoSustitución || fElem.IndexSubsType == IndexAdditionalType.AutoSustituciónJerarquico)
                                                {
                                                    Value = Value.Trim().Replace(fElem.Filter, columnToFilter).Replace("[[", "[").Replace("]]", "]");
                                                }

                                                if (useLower)
                                                {
                                                    if (Value.Trim().ToLower().Contains(" or "))
                                                    {
                                                        //Cambio el or por el pipe
                                                        string tempValue = Value.Replace(" or ", " | ");

                                                        //Quito los parentesis
                                                        tempValue = tempValue.Replace("(", "").Replace(")", "");

                                                        tempString = new StringBuilder();

                                                        foreach (string filter in tempValue.Split('|'))
                                                        {
                                                            string columnName = filter.Substring(0, filter.LastIndexOf("]", StringComparison.Ordinal) + 1);
                                                            string valueName = filter.Substring(filter.IndexOf("'", StringComparison.Ordinal));

                                                            tempString.Append(caseString);
                                                            tempString.Append(columnName);
                                                            tempString.Append(")");
                                                            tempString.Append(" LIKE ");
                                                            tempString.Append(caseString);
                                                            tempString.Append(valueName);
                                                            tempString.Append(")");
                                                            tempString.Append(" OR ");
                                                        }

                                                        tempString.Remove(tempString.ToString().LastIndexOf("OR"), 2);
                                                    }
                                                }

                                                filterstring.Append(tempString != null ? tempString.ToString() : Value.Trim());
                                            }
                                        }
                                    }
                                    else
                                    {
                                        filterstring.Append("(");
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append(" is NULL or ");
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append(" = ");
                                        filterstring.Append("''");
                                        filterstring.Append(")");
                                    }
                                }
                                else if (string.Compare(findOperator.ToLower(), "no contiene") == 0)
                                {
                                    if (Value.Replace("(", String.Empty).Replace(")", String.Empty).Trim() != String.Empty)
                                    {
                                        if (!Value.ToLower().Contains("not like"))
                                        {
                                            filterstring.Append("(( ");
                                            filterstring.Append(useLower ? caseString + columnToFilter + ")" : columnToFilter);
                                            filterstring.Append(" not like ");
                                            filterstring.Append(useLower ? caseString + "'%" : "'%");
                                            filterstring.Append(Value.Trim().Replace("(", String.Empty).Replace(")", String.Empty));
                                            filterstring.Append(useLower ? "%'))" : "%')");
                                            filterstring.Append(useLower ? " OR (" + caseString : " OR (");
                                            filterstring.Append(useLower ? columnToFilter + ")" : columnToFilter);
                                            filterstring.Append(" is NULL) OR (");
                                            filterstring.Append(useLower ? caseString + columnToFilter + ")" : columnToFilter);
                                            filterstring.Append(" = ");
                                            filterstring.Append("''");
                                            filterstring.Append("))");
                                        }
                                        else
                                        {
                                            if (fElem.NullValue)
                                            {
                                                filterstring.Append("(");
                                                filterstring.Append(Value.Trim().Replace(")", String.Empty));
                                                filterstring.Append(") and (");
                                                filterstring.Append(columnToFilter);
                                                filterstring.Append(" is NOT NULL or ");
                                                filterstring.Append(columnToFilter);
                                                filterstring.Append(" <> ");
                                                filterstring.Append(Convert.ToChar(39));
                                                filterstring.Append(Convert.ToChar(39));
                                                filterstring.Append("))");
                                            }
                                            else
                                            {
                                                filterstring.Append(Value.Trim());
                                                filterstring.Append(" or (");
                                                filterstring.Append(columnToFilter);
                                                filterstring.Append(" is NULL or ");
                                                filterstring.Append(columnToFilter);
                                                filterstring.Append(" = ");
                                                filterstring.Append(Convert.ToChar(39));
                                                filterstring.Append(Convert.ToChar(39));
                                                filterstring.Append(")");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        filterstring.Append("(");
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append(" is NOT NULL and ");
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append(" <> ");
                                        filterstring.Append(Convert.ToChar(39));
                                        filterstring.Append(Convert.ToChar(39));
                                        filterstring.Append(")");
                                    }
                                }
                                else if (string.Compare(findOperator.ToLower(), "es nulo") == 0)
                                {
                                    filterstring.Append("(");
                                    filterstring.Append(columnToFilter);
                                    filterstring.Append(" is NULL or ");
                                    filterstring.Append(columnToFilter);
                                    filterstring.Append(" = ");
                                    filterstring.Append(Convert.ToChar(39));
                                    filterstring.Append(Convert.ToChar(39));
                                    filterstring.Append(")");
                                }
                                else if (string.Compare(findOperator.ToLower(), "no es nulo") == 0)
                                {
                                    filterstring.Append("(");
                                    filterstring.Append(columnToFilter);
                                    filterstring.Append(" is not NULL");
                                    if (!Servers.Server.isOracle)
                                    {
                                        filterstring.Append(" and ");
                                        filterstring.Append(columnToFilter);
                                        filterstring.Append(" <> ");
                                        filterstring.Append(Convert.ToChar(39));
                                        filterstring.Append(Convert.ToChar(39));
                                    }
                                    filterstring.Append(")");
                                }
                                else
                                {
                                    if (Value.Trim() != "()")
                                    {
                                        if (!Value.ToLower().Contains("like"))
                                        {
                                            DataRow codigo = null;
                                            if (fElem.IndexSubsType == IndexAdditionalType.AutoSustitución || fElem.IndexSubsType == IndexAdditionalType.AutoSustituciónJerarquico)
                                                codigo = AutoSubstitutionDataFactory.getCodeRow(Int64.Parse(fElem.Filter.Trim().Replace("I", string.Empty)), Value.Replace("(", "").Replace(")", ""));

                                            if (codigo != null && codigo["Codigo"].ToString() != string.Empty)
                                            {

                                                filterstring.Append(columnToFilter);
                                                filterstring.Append(" ");
                                                filterstring.Append(findOperator);

                                                if (!Value.Trim().StartsWith("'") && !Value.Trim().StartsWith("('"))
                                                    filterstring.Append("'");

                                                filterstring.Append(codigo["Codigo"].ToString());

                                                if (!Value.Trim().EndsWith("'") && !Value.Trim().EndsWith("')"))
                                                    filterstring.Append("'");
                                            }
                                            else
                                            {


                                                filterstring.Append(useLower ? caseString + columnToFilter + ")" : columnToFilter);
                                                filterstring.Append(" ");
                                                filterstring.Append(findOperator);
                                                filterstring.Append(useLower ? caseString : "");

                                                if (!Value.Trim().StartsWith("'") && !Value.Trim().StartsWith("('"))
                                                    filterstring.Append("'");

                                                filterstring.Append(Value.Trim().Replace("(", String.Empty).Replace(")", String.Empty));

                                                if (!Value.Trim().EndsWith("'") && !Value.Trim().EndsWith("')"))
                                                    filterstring.Append("'");

                                                filterstring.Append(useLower ? ")" : "");

                                                if (findOperator.Equals("<>"))
                                                {
                                                    filterstring.Append(" OR ");
                                                    filterstring.Append(columnToFilter);
                                                    filterstring.Append(" IS NULL");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            StringBuilder tempString = null;

                                            if (useLower)
                                            {
                                                if (Value.Trim().ToLower().Contains(" or "))
                                                {
                                                    //Cambio el or por el pipe
                                                    string tempValue = Value.Replace(" or ", " | ");

                                                    //Quito los parentesis
                                                    tempValue = tempValue.Replace("(", "");
                                                    tempValue = tempValue.Replace(")", "");

                                                    tempString = new StringBuilder();

                                                    foreach (string filter in tempValue.Split('|'))
                                                    {
                                                        string columnName = filter.Substring(0, filter.IndexOf("]", StringComparison.Ordinal) + 1);
                                                        string valueName = filter.Substring(filter.IndexOf("'", StringComparison.Ordinal));

                                                        tempString.Append(caseString);
                                                        tempString.Append(columnName);
                                                        tempString.Append(")");
                                                        tempString.Append(" LIKE ");
                                                        tempString.Append(caseString);
                                                        tempString.Append(valueName);
                                                        tempString.Append(")");
                                                        tempString.Append(" OR ");
                                                    }

                                                    tempString.Remove(tempString.ToString().LastIndexOf("OR"), 2);
                                                    //filterstring.Append(")");
                                                }
                                            }

                                            filterstring.Append(tempString != null ? tempString.ToString() : Value.Trim());
                                        }
                                    }
                                }

                            }

                            //caso de que exista un filtro por defecto
                            //con una consulta que no devuelve registros
                            if (fElem.Type == "defecto" && Value == "()")
                            {
                                filterstring.Append("(");
                                filterstring.Append(columnToFilter);
                                filterstring.Append(" is NULL");
                                filterstring.Append(")");
                            }
                        }
                        catch (System.Threading.ThreadAbortException)
                        {
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                            ZTrace.WriteLineIf(ZTrace.IsInfo,
                                              "Error con el filtro '" + fElem.Text + "'. Dicho Atributo no será filtrado.");
                        }
                    }


                    //Se agregó esta parte ya que si por algún motivo fallaba el filtro
                    //estaba quedando el AND anidado al stringBuilder y generaba error
                    //y al generar error directamente no filtraba, mostrando todos los 
                    //resultados.
                    if (filterstring.ToString().StartsWith(" AND "))
                        filterstring.Remove(0, 4);

                    if (filterstring.ToString().EndsWith(" AND "))
                        filterstring.Remove(filterstring.Length - 4, 4);

                    if (filterstring.ToString().StartsWith(" OR "))
                        filterstring.Remove(0, 3);

                    if (filterstring.Length > 0 && fElem.Enabled)
                        filterstring.Append(")");

                }
            }
            catch (Exception ex)
            {
                ZException.Log(ex);
            }

            return filterstring.ToString();
        }

        public static string FormatColumnName(string v)
        {
            //Se obtienen los objetos a formatear.
            var separator = new string[1];
            separator[0] = ".";
            string[] toFormat = v.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            //Se agregan los corchetes.
            for (int i = 0; i < toFormat.Length; i++)
            {
                //Se agrega el corchete izquierdo.
                if (!toFormat[i].StartsWith("["))
                    toFormat[i] = "[" + toFormat[i];
                //Se agrega el corchete derecho.
                if (!toFormat[i].EndsWith("]"))
                    toFormat[i] += "]";
                //Se agrega el punto.
                if (i != 0)
                    toFormat[0] += "." + toFormat[i];
            }

            return toFormat[0];
        }

        /// <summary>
        /// Verifica si existe ese filtro en la cache y devuelve verdadero o falso de acuerdo al resultado
        /// </summary>
        /// <param name="fe">Filtro a buscar dentro de la cache</param>
        /// <returns></returns>
        /// <history> 
        ///   [Javier]    31/08/2010   Created     
        /// </history>
        public Boolean ExistFilter(FilterElem fe, FilterTypes filterType)
        {
            if (filterType == FilterTypes.Task || filterType == FilterTypes.Document)
            {
                if (Search.HsFilters.Contains(fe.DocTypeId + "-" + filterType))
                {
                    var elementos = (List<IFilterElem>)Search.HsFilters[fe.DocTypeId + "-" + filterType];
                    for (var i = 0; i < elementos.Count; i++)
                    {
                        if (fe.CompareTo(elementos[i]) == 0)
                            return true;
                    }
                }
            }
            else
            {
                return Search.FiltersCache.Contains(fe.Id);
            }

            return false;
        }


        public List<long> GetSlstFiltersIndexsIDs(Int64 docTypeId, Int64 currentUserId, FilterTypes filterType)
        {
            if (filterType == FilterTypes.Task || filterType == FilterTypes.Document)
            {
                List<long> ids = new List<long>();
                List<IFilterElem> filters = GetLastUsedFilters(docTypeId, currentUserId, filterType);
                string columnToFilter;
                foreach (FilterElem filter in filters)
                {
                    if (filter.IndexSubsType == IndexAdditionalType.AutoSustitución || filter.IndexSubsType == IndexAdditionalType.AutoSustituciónJerarquico)
                    {
                        columnToFilter = filter.Filter.Trim();
                        ids.Add(long.Parse(columnToFilter.Replace("I", string.Empty)));
                    }
                }
                return ids;
            }
            return null;
        }

    }
}