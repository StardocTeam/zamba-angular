#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Zamba.AppBlock;
using Zamba.Core;
using Zamba.Data;
using Zamba.Filters;
using System.Diagnostics;
using Zamba.Servers;
using System.Text.RegularExpressions;
using System.Linq;

#endregion

namespace Zamba.Filters
{
    public class FiltersComponent : IFiltersComponent
    {
        /// <summary>
        /// Remueve un filtro en específico de un entidad.
        /// </summary>
        /// <param name="fe"></param>
        public void RemoveFilter(IFilterElem fe, Boolean IsTask)
        {
            if (Cache.HsFilters.Contains(fe.DocTypeId + "-" + IsTask))
                ((List<IFilterElem>)Cache.HsFilters[fe.DocTypeId + "-" + IsTask]).Remove((FilterElem)fe);

            RightFactory.RemoveFilter(fe.Id);
        }
        /// <summary>
        /// Remueve un filtro en específico de un entidad.
        /// </summary>
        /// <param name="fe"></param>
        public void RemoveFilter(Int64 filterID)
        {
            RightFactory.RemoveFilter(filterID);
        }
        public void RemoveFilterWeb(Int64 docTypeId,Int64 userId,string attribute,string comparator, string val)
        {
            RightFactory.RemoveFilterWeb(docTypeId,userId, attribute,comparator,val);
        }

        public void RemoveFilterWebById(Int64 filterId)
        {
            RightFactory.RemoveFilterWebById(filterId);
        }
        
        public void RemoveZambaColumnsFilterWeb(Int64 docTypeId, Int64 userId, string filterType)
        {
            RightFactory.RemoveZambaColumnsFilterWeb(docTypeId, userId, filterType);
        }
        public void UpdateFilterValue(Int64 zfilterId,string filterValue)
        {
            RightFactory.UpdateFilterValue(zfilterId, filterValue);
        }

        public void SetDisabledAllFiltersByUser(Int64 userId, String FilterType)
        {
            RightFactory.SetDisabledAllFiltersByUser(userId, FilterType);
        }

        public void SetDisabledAllFiltersByUserViewDoctype(Int64 userId, String FilterType, Int64 DocTypeId)
        {
            RightFactory.SetDisabledAllFiltersByUserViewDoctype(userId, FilterType, DocTypeId);
        }
        
        public void SetEnabledAllFiltersByUserViewDoctype(Int64 userId, String FilterType, Int64 DocTypeId)
        {
            RightFactory.SetEnabledAllFiltersByUserViewDoctype(userId, FilterType, DocTypeId);
        }


        public void RemoveAllZambaColumnsFilter(List<Int64> filterIds)
        {
            string filterIdsString = "";
            foreach (var id in filterIds)
            {
                if (filterIdsString == "")
                    filterIdsString += "(" + id;
                else
                    filterIdsString += ", " + id;
            }

            if (filterIdsString != "")
                filterIdsString += ")";


            RightFactory.RemoveAllZambaColumnsFilter(filterIdsString);
        }

        public void RemoveAllFilters(long userId, string FilterType,long DocTypeId)
        {
            RightFactory.RemoveAllFilters(userId,FilterType,DocTypeId);
        }

        

        ///// <summary>
        ///// Remueve los filtros de un entidad.
        ///// </summary>
        ///// <param name="docTypeId"></param>
        ///// <param name="userId"></param>
        //public void ClearFilters(Int64 docTypeId, Int64 userId, Boolean IsTask)
        //{
        //    if (Cache.HsFilters.Contains(docTypeId + "-" + IsTask))
        //        ((List<IFilterElem>)Cache.HsFilters[docTypeId + "-" + IsTask]).Remove() .Clear();

        //    RightFactory.ClearFilters(docTypeId, userId);
        //}

        /// <summary>
        /// Remueve los filtros de un entidad.
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <param name="userId"></param>
        public void ClearFilters(Int64 docTypeId, Int64 userId, Boolean IsTask, DataTable dataTable, Boolean removeDefaultFilters)
        {
            List<IFilterElem> filters = (List<IFilterElem>)Cache.HsFilters[docTypeId + "-" + IsTask];
            List<IFilterElem> KeepDefaultFilters = new List<IFilterElem>();

            if (filters != null && filters.Count > 0)
            {
                //si tiene permisos de remover todos los filtros quitarlos a todos
                if (removeDefaultFilters || filters[0].Type.ToString() != "defecto")
                {
                    ((List<IFilterElem>)Cache.HsFilters[docTypeId + "-" + IsTask]).Clear();
                }
                else
                {
                    //si existen ambos tipos guardo los por defecto
                    foreach (IFilterElem elemento in filters)
                    {
                        if (string.Compare(elemento.Type.ToLower(), "defecto") == 0)
                        {
                            KeepDefaultFilters.Add(elemento);
                        }
                    }

                    Cache.HsFilters[docTypeId + "-" + IsTask] = KeepDefaultFilters;
                }
            }
            RightFactory.ClearFilters(docTypeId, userId, removeDefaultFilters);
        }


        /// <summary>
        /// Obtiene la cantidad de filtros aplicados sobre un entidad.
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <returns></returns>
        public int GetDocumentFiltersCount(Int64 docTypeId, Boolean IsTask)
        {
            return Cache.HsFilters.Contains(docTypeId + "-" + IsTask) ? ((List<IFilterElem>)Cache.HsFilters[docTypeId + "-" + IsTask]).Count : 0;
        }
        /// <summary>
        /// Obtiene los filtros de un entidad en web.
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns
        public List<IFilterElem> GetFiltersWebByDocTypeIdAndUserId(Int64 docTypeId, Int64 currentUserId)
        {

            try
            {
                var filters = new List<IFilterElem>();
                var lastFilters = RightFactory.GetFiltersWeb(docTypeId, currentUserId);

                //mapping row to IFilterElem
                foreach (DataRow row in lastFilters.Tables[0].Rows)
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
                                            (IndexAdditionalType)Int32.Parse(row["IndexDropDown"].ToString()),
                                            row["DataDescription"].ToString().Trim());

                    if (row["Enabled"].ToString() == "1")
                        fe.Enabled = true;
                    else
                    {
                        fe.Enabled = false;
                    }
                    filters.Add(fe);
                }
                return filters;
            }
            catch (Exception)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al obtener los filtros en 'GetFiltersWebByDocTypeIdAndUserId'");
                return null;
            }
        }
        /// <summary>
        /// Obtiene los filtros de un entidad en web.
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns
        public List<IFilterElem> GetFiltersWebByView(Int64 docTypeId, Int64 currentUserId, String filterType)
        {

            try
            {
                var filters = new List<IFilterElem>();

                if (filterType.ToLower() == "myprocess" || filterType.ToLower() == "process" || filterType.ToLower() == "task")
                    filterType = "manual";

                var lastFilters = RightFactory.GetFiltersWebByView(docTypeId, currentUserId, filterType);


                //mapping row to IFilterElem
                foreach (DataRow row in lastFilters.Tables[0].Rows)
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
                                            (IndexAdditionalType)Int32.Parse(row["IndexDropDown"].ToString()),
                                            "");

                    if (row["Enabled"].ToString() == "1")
                        fe.Enabled = true;
                    else
                    {
                        fe.Enabled = false;
                    }
                    filters.Add(fe);
                }
                return filters;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al obtener los filtros en 'GetFiltersWebByView'");
                return null;
            }
        }
        /// <summary>
        /// Obtiene los filtros de un entidad.
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public List<IFilterElem> GetLastUsedFilters(Int64 docTypeId, Int64 currentUserId, bool IsTask)
        {
            Int64 attribute;
            if (!Cache.HsFilters.Contains(docTypeId + "-" + IsTask))
            {
                var filters = new List<IFilterElem>();
                var lastFilters = RightFactory.GetFilters(docTypeId, currentUserId);

                foreach (DataRow row in lastFilters.Tables[0].Rows)
                {
                    if (((IsTask == true) && (String.Compare(row["FilterType"].ToString(), "search") != 0)) || ((IsTask == false) && (String.Compare(row["FilterType"].ToString(), "search") == 0)) || ((IsTask == false) && (String.Compare(row["FilterType"].ToString(), "defecto") == 0 && Int64.TryParse(row["attribute"].ToString(), out attribute))))
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
                if (filters != null)
                {

                    if (!Cache.HsFilters.ContainsKey(docTypeId + "-" + IsTask)) Cache.HsFilters.Add(docTypeId + "-" + IsTask, filters);
                    else
                        Cache.HsFilters[docTypeId + "-" + IsTask] = filters;
                }
            }

            return (List<IFilterElem>)Cache.HsFilters[docTypeId + "-" + IsTask];
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
        public IFilterElem SetNewFilter(Int64 indexId, String attribute, IndexDataType dataType, Int64 userId, String comparator, String filterValue, Int64 docTypeId, Boolean save, String description, IndexAdditionalType additionalType, String FilterType, Boolean IsTask)
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

            // Verifica si existe el filtro en la cache antes de insertarlo
            if (!ExistFilter(fe, IsTask))
            {
                //Se guarda el filtro en cache.
                if (Cache.HsFilters.Contains(docTypeId + "-" + IsTask))
                {
                    ((List<IFilterElem>)Cache.HsFilters[docTypeId + "-" + IsTask]).Add(fe);
                }
                else
                {
                    var filters = new List<IFilterElem> { fe };
                    Cache.HsFilters.Add(docTypeId + "-" + IsTask, filters);
                }

                //Se guarda el filtro en la base.
                if (save)
                {
                    var attrib = fe.Filter;

                    //Verifica si debe remover la I del índice.
                    if (indexId > 0)
                        attrib = attrib.Remove(0, 1);

                    //Se agrega el índice a la base.
                    fe.Id = RightFactory.InsertFilter(attrib, fe.FormatValue, (Int32)fe.DataType,
                                                              fe.Comparator, fe.Type, fe.DocTypeId, fe.UserId, fe.Description, (Int32)fe.IndexSubsType);
                }
            }

            return fe;
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
        public IFilterElem SetNewFilterWeb(Int64 indexId, String attribute, IndexDataType dataType, Int64 userId, String comparator, String filterValue, Int64 docTypeId, Boolean save, String description, IndexAdditionalType additionalType, String FilterType)
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
                                    additionalType
                                    );
            //Traduce el nombre segun la sintaxis de la aplicacion desktop
            SetFilterNameByDesktopName(ref fe);


            //Se guarda el filtro en la base.
            if (save)
            {
                var attrib = fe.Filter;

                //Verifica si debe remover la I del índice.
                if (indexId > 0)
                    attrib = attrib.Remove(0, 1);

                //Se agrega el índice a la base.
                fe.Id = RightFactory.InsertFilterWeb(attrib, fe.FormatValue, (Int32)fe.DataType,
                                                            fe.Comparator, fe.Type, fe.DocTypeId, fe.UserId, fe.Description, (Int32)fe.IndexSubsType);
            }

            return fe;
        }

        public void DeleteUserAssignedFilter(Int64 userId, Int64 docTypeId, String FilterType)
        {
            try
            {
                   RightFactory.DeleteUserAssignedFilter(userId, docTypeId, FilterType);
            }
            catch (Exception)
            {
            }

        }

        public void DeleteStepFilter(Int64 userId, Int64 docTypeId, String FilterType)
        {
            try
            {
                RightFactory.DeleteStepFilter(userId, docTypeId, FilterType);
            }
            catch (Exception)
            {
            }

        }
        



        private void SetFilterNameByDesktopName(ref FilterElem fe) {

            switch (fe.Filter.ToLower())
            {
                case "name":
                    fe.Filter = "WD.NAME";
                    break;
                case "original":
                    fe.Filter = "ORIGINAL_FILENAME";
                    fe.Description = "Nombre Original";
                    break;
                case "i.crdate":
                    fe.Filter = "Fecha Creacion";
                    fe.Description = "Fecha Creacion";
                    break;
                case "wss.name":
                    fe.Filter = "Estado Tarea";
                    fe.Description = "Estado Tarea";
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Habilita o deshabilita un filtro dependiendo de la propiedad Enabled del filtro.
        /// </summary>
        /// <param name="fe"></param>
        public void SetEnabledFilter(IFilterElem fe, Boolean IsTask)
        {
            //Se actualiza la cache.
            var index = -1;

            if (Cache.HsFilters.ContainsKey(fe.DocTypeId + "-" + IsTask))
            {
                index = ((List<IFilterElem>)Cache.HsFilters[fe.DocTypeId + "-" + IsTask]).IndexOf((FilterElem)fe);
            }

            if (index != -1)
                ((List<IFilterElem>)Cache.HsFilters[fe.DocTypeId + "-" + IsTask])[index] = (FilterElem)fe;
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

        public void SetEnabledFilterWeb(Int64 docTypeId, Int64 userId, string attribute, string comparator, string val,int enabled)
        {
            RightFactory.UpdateFilterWebEnabled(docTypeId,userId,attribute,comparator,val,enabled);
        }
        public void SetEnabledFilterWebById(Int64 id, int enabled)
        {
            RightFactory.UpdateFilterWebEnabledById(id, enabled);
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
        public string GetFiltersString(IEnumerable<IFilterElem> filters)
        {
            if (filters == null) throw new ArgumentNullException("filters", @"El objeto filters debe encontrare instanciado.");
            var filterstring = new StringBuilder();
            string findOperator;
            try
            {
                //Para cada filtro, armo la consulta
                foreach (IFilterElem fElem in filters)
                {
                    if (fElem.Enabled)
                    {
                        if (!filterstring.ToString().StartsWith(" AND ") || !filterstring.ToString().EndsWith(" AND "))
                            filterstring.Append(" AND ");

                        string elemento;

                        if (((FilterElem)fElem).IndexSubsType == IndexAdditionalType.NoIndex)
                        {
                            elemento = FormatColumnName(fElem.Filter.Trim());
                        }
                        else
                        {
                            elemento = FormatColumnName("Doc" + fElem.DocTypeId + "." + fElem.Filter.Trim());
                        }

                        try
                        {
                            findOperator = fElem.Comparator;
                            if (((FilterElem)fElem).IndexSubsType != IndexAdditionalType.AutoSustitución && ((fElem.DataType == IndexDataType.Numerico) ||
                                                           (fElem.DataType == IndexDataType.Numerico_Largo) ||
                                                           (fElem.DataType == IndexDataType.Numerico_Decimales) ||
                                                           (fElem.DataType == IndexDataType.Moneda)))
                            {
                                int retNum;
                                if (Int32.TryParse(fElem.Value.Replace("(", "").Replace(")", ""), out retNum))
                                {
                                    if (string.Compare(fElem.Comparator, "Contiene") == 0 ||
                                        string.Compare(fElem.Comparator, "=") == 0)
                                    {
                                        filterstring.Append(elemento);
                                        filterstring.Append(findOperator.Replace("Contiene", "="));
                                        filterstring.Append(fElem.Value.Trim());
                                    }
                                    else if (string.Compare(fElem.Comparator, ">") == 0 ||
                                             string.Compare(fElem.Comparator, ">=") == 0 ||
                                             string.Compare(fElem.Comparator, "<") == 0 ||
                                             string.Compare(fElem.Comparator, "<=") == 0 ||
                                             string.Compare(fElem.Comparator, "<>") == 0)
                                    {
                                        filterstring.Append(elemento);
                                        filterstring.Append(findOperator);
                                        filterstring.Append(fElem.Value.Trim());
                                    }
                                    else if (string.Compare(fElem.Comparator, "No Contiene") == 0)
                                    {
                                        filterstring.Append("(");
                                        filterstring.Append(elemento);
                                        filterstring.Append(" Not in ");
                                        filterstring.Append(fElem.Value.Trim());
                                        filterstring.Append(" or ");
                                        filterstring.Append(elemento);
                                        filterstring.Append(" is NULL)");
                                    }
                                }
                                else
                                {
                                    if (string.Compare(fElem.Comparator, "Es Nulo") == 0)
                                    {
                                        filterstring.Append(elemento);
                                        filterstring.Append(" is NULL");
                                    }
                                    else if (string.Compare(fElem.Comparator, "No es Nulo") == 0)
                                    {
                                        filterstring.Append(elemento);
                                        filterstring.Append(" is not NULL");
                                    }
                                    if (string.Compare(fElem.Comparator, "Contiene") == 0)
                                    {
                                        filterstring.Append(fElem.Value.Trim().Replace("%", "").Replace("like", "="));
                                    }
                                    else if (string.Compare(fElem.Comparator.ToLower().Trim(), "no contiene") == 0)
                                    {
                                        filterstring.Append(fElem.Value.Trim().Replace("%", "").Replace("not like", "<>"));
                                    }
                                }
                            }
                            else if (fElem.DataType == IndexDataType.Si_No)
                            {
                                bool boolean;
                                if (bool.TryParse(fElem.Value, out boolean))
                                {
                                    if (findOperator == "Es Nulo")
                                    {
                                        filterstring.Append(elemento);
                                        filterstring.Append(" = FALSE");
                                    }
                                    if (findOperator == "No es Nulo")
                                    {
                                        filterstring.Append(elemento);
                                        filterstring.Append(" = TRUE");
                                    }
                                    else
                                    {
                                        filterstring.Append(elemento);
                                        filterstring.Append(" ");
                                        filterstring.Append(findOperator);
                                        filterstring.Append("'");
                                        filterstring.Append(fElem.Value.Trim());
                                        filterstring.Append("'");
                                    }
                                }


                                if (bool.TryParse(fElem.Value, out boolean))
                                {
                                    filterstring.Append(elemento);
                                    filterstring.Append(" ");
                                    filterstring.Append(findOperator);
                                    filterstring.Append("'");
                                    filterstring.Append(fElem.Value.Trim());
                                    filterstring.Append("'");
                                }
                            }
                            else if (fElem.DataType == IndexDataType.Fecha || fElem.DataType == IndexDataType.Fecha_Hora)
                            {
                                DateTime fecha;
                                if (findOperator == "Es Nulo")
                                {
                                    filterstring.Append(elemento);
                                    filterstring.Append(" is NULL");
                                }
                                if (findOperator == "No es Nulo")
                                {
                                    filterstring.Append(elemento);
                                    filterstring.Append(" is not NULL");
                                }

                                if (DateTime.TryParse(fElem.Value.Replace("(", "").Replace(")", ""), out fecha))
                                {
                                    var fechahasta = fecha.AddDays(1);

                                    if (findOperator == ">")
                                    {
                                        filterstring.Append("CONVERT(DateTime,");
                                        filterstring.Append(elemento);
                                        filterstring.Append(")");
                                        filterstring.Append(findOperator);
                                        filterstring.Append(ServersFactory.ConvertDate(fecha.ToString()));
                                    }
                                    else if (findOperator == "<")
                                    {
                                        filterstring.Append("CONVERT(DateTime,");
                                        filterstring.Append(elemento);
                                        filterstring.Append(") ");
                                        filterstring.Append(findOperator);
                                        filterstring.Append(ServersFactory.ConvertDate(fecha.ToString()));
                                    }
                                    else if (findOperator == ">=")
                                    {
                                        filterstring.Append("CONVERT(DateTime,");
                                        filterstring.Append(elemento);
                                        filterstring.Append(")");
                                        filterstring.Append(findOperator);
                                        filterstring.Append(ServersFactory.ConvertDate(fecha.ToString()));
                                    }
                                    else if (findOperator == "<=")
                                    {
                                        filterstring.Append("CONVERT(DateTime,");
                                        filterstring.Append(elemento);
                                        filterstring.Append(")");
                                        filterstring.Append(findOperator);
                                        filterstring.Append(ServersFactory.ConvertDate(fechahasta.ToString()));
                                    }
                                    else if (findOperator == "<>" || findOperator == "No Contiene")
                                    {
                                        filterstring.Append("(CONVERT(DateTime,");
                                        filterstring.Append(elemento);
                                        filterstring.Append(")<");
                                        filterstring.Append(ServersFactory.ConvertDate(fecha.ToString()));
                                        filterstring.Append(" or ");
                                        filterstring.Append("CONVERT(DateTime,");
                                        filterstring.Append(elemento);
                                        filterstring.Append(")>= ");
                                        filterstring.Append(ServersFactory.ConvertDate(fechahasta.ToString()));
                                        filterstring.Append(" or ");
                                        filterstring.Append(elemento);
                                        filterstring.Append(" is NULL");
                                        filterstring.Append(")");
                                    }
                                    else if (findOperator == "=" || findOperator == "Contiene")
                                    {
                                        filterstring.Append("CONVERT(DateTime,");
                                        filterstring.Append(elemento);
                                        filterstring.Append(")>=");
                                        filterstring.Append(ServersFactory.ConvertDate(fecha.ToString()));
                                        filterstring.Append(" and ");
                                        filterstring.Append("CONVERT(DateTime,");
                                        filterstring.Append(elemento);
                                        filterstring.Append(")<=");
                                        filterstring.Append(ServersFactory.ConvertDate(fechahasta.ToString()));
                                    }
                                }
                            }
                            else if (((FilterElem)fElem).IndexSubsType == IndexAdditionalType.AutoSustitución || fElem.DataType == IndexDataType.Alfanumerico ||
                                     fElem.DataType == IndexDataType.Alfanumerico_Largo)

                                if (findOperator.ToLower().Contains("like"))
                                {
                                    filterstring.Append(elemento);
                                    filterstring.Append(" ");
                                    filterstring.Append(findOperator);
                                    filterstring.Append("'%");
                                    filterstring.Append(fElem.Value.Trim());
                                    filterstring.Append("%'");
                                }
                                else if (string.Compare(findOperator.ToLower(), "contiene") == 0)
                                {
                                    if (fElem.Value.Replace("(", String.Empty).Replace(")", String.Empty).Trim() != String.Empty)
                                    {
                                        if (!fElem.Value.ToLower().Contains("like"))
                                        {
                                            filterstring.Append(elemento);
                                            filterstring.Append(" like ");
                                            filterstring.Append("'%");
                                            filterstring.Append(fElem.Value.Trim().Replace("(", String.Empty).Replace(")", String.Empty));
                                            filterstring.Append("%'");
                                        }
                                        else
                                        {
                                            if (fElem.NullValue)
                                            {
                                                filterstring.Append("(");
                                                filterstring.Append(fElem.Value.Trim().Replace(")", String.Empty));
                                                filterstring.Append(") or (");
                                                filterstring.Append(elemento);
                                                filterstring.Append(" is NULL or ");
                                                filterstring.Append(elemento);
                                                filterstring.Append(" = ");
                                                filterstring.Append(Convert.ToChar(39));
                                                filterstring.Append(Convert.ToChar(39));
                                                filterstring.Append("))");
                                            }
                                            else
                                                filterstring.Append(fElem.Value.Trim());
                                        }
                                    }
                                    else
                                    {
                                        filterstring.Append("(");
                                        filterstring.Append(elemento);
                                        filterstring.Append(" is NULL or ");
                                        filterstring.Append(elemento);
                                        filterstring.Append(" = ");
                                        filterstring.Append(Convert.ToChar(39));
                                        filterstring.Append(Convert.ToChar(39));
                                        filterstring.Append(")");
                                    }
                                }
                                else if (string.Compare(findOperator.ToLower(), "no contiene") == 0)
                                {
                                    if (fElem.Value.Replace("(", String.Empty).Replace(")", String.Empty).Trim() != String.Empty)
                                    {
                                        if (!fElem.Value.ToLower().Contains("not like"))
                                        {
                                            filterstring.Append("((");
                                            filterstring.Append(elemento);
                                            filterstring.Append(" not like ");
                                            filterstring.Append("'%");
                                            filterstring.Append(fElem.Value.Trim().Replace("(", String.Empty).Replace(")", String.Empty));
                                            filterstring.Append("%') or (");
                                            filterstring.Append(elemento);
                                            filterstring.Append(" is NULL or ");
                                            filterstring.Append(elemento);
                                            filterstring.Append(" = ");
                                            filterstring.Append(Convert.ToChar(39));
                                            filterstring.Append(Convert.ToChar(39));
                                            filterstring.Append("))");
                                        }
                                        else
                                        {
                                            if (fElem.NullValue)
                                            {
                                                filterstring.Append("(");
                                                filterstring.Append(fElem.Value.Trim().Replace(")", String.Empty));
                                                filterstring.Append(") and (");
                                                filterstring.Append(elemento);
                                                filterstring.Append(" is NOT NULL or ");
                                                filterstring.Append(elemento);
                                                filterstring.Append(" <> ");
                                                filterstring.Append(Convert.ToChar(39));
                                                filterstring.Append(Convert.ToChar(39));
                                                filterstring.Append("))");
                                            }
                                            else
                                            {
                                                filterstring.Append(fElem.Value.Trim());
                                                filterstring.Append(" or (");
                                                filterstring.Append(elemento);
                                                filterstring.Append(" is NULL or ");
                                                filterstring.Append(elemento);
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
                                        filterstring.Append(elemento);
                                        filterstring.Append(" is NOT NULL and ");
                                        filterstring.Append(elemento);
                                        filterstring.Append(" <> ");
                                        filterstring.Append(Convert.ToChar(39));
                                        filterstring.Append(Convert.ToChar(39));
                                        filterstring.Append(")");
                                    }
                                }
                                else if (string.Compare(findOperator.ToLower(), "es nulo") == 0)
                                {
                                    filterstring.Append("(");
                                    filterstring.Append(elemento);
                                    filterstring.Append(" is NULL or ");
                                    filterstring.Append(elemento);
                                    filterstring.Append(" = ");
                                    filterstring.Append(Convert.ToChar(39));
                                    filterstring.Append(Convert.ToChar(39));
                                    filterstring.Append(")");
                                }
                                else if (string.Compare(findOperator.ToLower(), "no es nulo") == 0)
                                {
                                    filterstring.Append("(");
                                    filterstring.Append(elemento);
                                    filterstring.Append(" is not NULL and ");
                                    filterstring.Append(elemento);
                                    filterstring.Append(" <> ");
                                    filterstring.Append(Convert.ToChar(39));
                                    filterstring.Append(Convert.ToChar(39));
                                    filterstring.Append(")");
                                }

                                else
                                {
                                    if (fElem.Value.Trim() != "()")
                                    {
                                        if (!fElem.Value.ToLower().Contains("like"))
                                        {
                                            filterstring.Append(elemento);
                                            filterstring.Append(" ");
                                            filterstring.Append(findOperator);

                                            if (!fElem.Value.Trim().StartsWith("'") && !fElem.Value.Trim().StartsWith("('"))
                                                filterstring.Append("'");

                                            filterstring.Append(fElem.Value.Trim().Replace("(", String.Empty).Replace(")", String.Empty));

                                            if (!fElem.Value.Trim().EndsWith("'") && !fElem.Value.Trim().EndsWith("')"))
                                                filterstring.Append("'");
                                        }
                                        else
                                        {
                                            filterstring.Append(fElem.Value.Trim());
                                        }
                                    }
                                }

                            //caso de que exista un filtro por defecto
                            //con una consulta que no devuelve registros
                            if (fElem.Type == "defecto" && fElem.Value == "()")
                            {
                                filterstring.Append("(");
                                filterstring.Append(elemento);
                                filterstring.Append(" is NULL");
                                filterstring.Append(")");
                            }
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                            ZTrace.WriteLineIf(ZTrace.IsInfo,
                                              "Error con el filtro '" + fElem.Text + "'. Dicho índice no será filtrado.");
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
                }

                //if (Server.isOracle)
                //{

                //    string my_String = filterstring.ToString().Replace("[", string.Empty).Replace("]", string.Empty);
                //}

            }
            catch (Exception ex)
            {
                ZException.Log(ex);
            }

            return filterstring.ToString();
        }

        public static string FormatColumnName(string v)
        {
            var separator = new string[1];
            separator[0] = ".";
            List<string> toFormat = v.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (toFormat.Count > 2) //Si viene con tres elementos es que tiene dos tablas, se borra la primera.
                toFormat.RemoveAt(0);

            //Se obtienen los objetos a formatear.

            //Se agregan los corchetes.
            for (int i = 0; i < toFormat.Count; i++)
            {
                if (!Server.isOracle)
                {
                    //Se agrega el corchete izquierdo.
                    if (!toFormat[i].StartsWith("["))
                        toFormat[i] = "[" + toFormat[i];
                    //Se agrega el corchete derecho.
                    if (!toFormat[i].EndsWith("]"))
                        toFormat[i] += "]";
                }
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
        public Boolean ExistFilter(IFilterElem fe, bool IsTask)
        {
            if (Cache.HsFilters.Contains(fe.DocTypeId + "-" + IsTask))
            {
                var elementos = (List<IFilterElem>)Cache.HsFilters[fe.DocTypeId + "-" + IsTask];
                for (var i = 0; i < elementos.Count; i++)
                {
                    if (((FilterElem)fe).CompareTo(elementos[i]) == 0)
                        return true;
                }
            }
            return false;
        }

        public void AddFiltersElements(List<IFilterElem> filtersElements)
        {
            filtersElements.ForEach(x => AddFilterElement(x, true));
        }

        private void AddFilterElement(IFilterElem fe, bool IsTask)
        {
            // Verifica si existe el filtro en la cache antes de insertarlo
            FilterElem f = (FilterElem)fe;
            if (!ExistFilter(fe, IsTask))
            {
                //Se guarda el filtro en cache.
                if (Cache.HsFilters.Contains(f.DocTypeId + "-" + IsTask))
                {
                    ((List<IFilterElem>)Cache.HsFilters[f.DocTypeId + "-" + IsTask]).Add(fe);
                }
                else
                {
                    var filters = new List<IFilterElem> { fe };
                    Cache.HsFilters.Add(f.DocTypeId + "-" + IsTask, filters);
                }
            }
        }
    }
}
