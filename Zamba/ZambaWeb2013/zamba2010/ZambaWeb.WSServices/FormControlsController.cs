using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba.Core;
using System.Data;
using System.Text;
using Zamba.Services;
using Zamba.Tools;
using Zamba.Membership;
using System.Collections;
using System.Web.Script.Serialization;
using Zamba;

/// <summary>
/// Summary description for FormControlsController
/// </summary>
public class FormControlsController
{
    #region Constantes
    const string STR_OPTION_FORMAT = "<option value=\"{0}\">{1}</option>";
    const string STR_QUERY_EQUAL = " = ";
    const string STR_QUERY_APOSTROPHE = "'";
    const string HTML_TR_FORMAT = "<tr class=\"ZGridRow\">{0}</tr>";
    const string HTML_TH_FORMAT = "<th class=\"ZGridCellHeader\">{0}</th>";
    const string HTML_TD_FORMAT = "<td class=\"ZGridCell\">{0}</td>";
    const string HTML_PARAMBODY = "<tbody class=\"hideopentask\" editablecolumns=\"{0}\">";
    const string HTML_EDITABLE_CELL = "<td class=\"ZGridEditableCell\"><input type=\"text\" ZOriginalValue=\"\" value=\"{0}\" {1} id=\"{2}\" name=\"{3}\"/></td>";
    const string HTML_ATTRIBUTE_TEMPLATE = "{0}=\"{1}\"";
    const string HTML_INPUT_TABLE_ID_TEMPLATE = "{0}-{1}-{2}";
    #endregion

    UserPreferences UP = new UserPreferences();
    public FormControlsController()
	{
	}

	#region SelectOptions
	/// <summary>
	/// Obtiene los options para un select desde una zvar en formato string
	/// </summary>
	/// <param name="controlId"></param>
	/// <param name="dataSourceName">Nombre de zvar para obtener los options</param>
	/// <param name="displayMember">Columna que se usara como caption de los options</param>
	/// <param name="valueMember">Columna que se usara como valor de los options</param>
	/// <param name="filterColumn">Columna para filtrar valores, usar String.Empty si no se quiere filtrar</param>
	/// <param name="filterValue">Valor de filtrado, si filterColumn = string.Empty no se tendra en cuenta</param>
	/// <returns></returns>
	public FieldOptions GetZVarOptions(string controlId,
																		string dataSourceName,
																		string displayMember,
																		string valueMember,
																		string filterColumn,
																		string filterValue)
	{
		
			FieldOptions fo = new FieldOptions();
			fo.ControlId = controlId;

			if (string.IsNullOrEmpty(dataSourceName))
				throw new Exception("No se encuentra nombre de fuente de datos");

			object dataSource;

			if (dataSourceName.ToLower().StartsWith("zfillselect"))
			{
				SZQuery sQuery = new SZQuery();
				//Quitar el fillselect y dejar query | parametros
				string query = dataSourceName.ToLower().Replace("zfillselect(", string.Empty);
				query = query.Remove(query.Length - 1, 1);
				//Obtener los parametros de query
				string[] queryParamValues = filterValue.Split(new char[] { '|' });
				//Obtener la query
				string[] splitedQueryAndParams = query.Split(new char[] { '|' });
				query = splitedQueryAndParams[0];

				//Si hay parametros, los obtengo
				string[] queryParamNames = null;
				if (splitedQueryAndParams.Length > 0)
				{
					queryParamNames = splitedQueryAndParams[1].Split(new char[] { ',' });
				}

				//Llenar el source
				dataSource = sQuery.FillSource(query, queryParamValues, queryParamNames);
				sQuery = null;

				//Convertir el source a las opciones de select
				fo.SelectOptions = SetSelectOptions(dataSource, displayMember, valueMember, string.Empty, string.Empty);
			}
			else
			{
				if (!VariablesInterReglas.ContainsKey(dataSourceName))
					throw new Exception("Variable de datos no encontrada");

				dataSource = VariablesInterReglas.get_Item(dataSourceName);

				if (dataSource == null)
					throw new Exception("Variable de datos vacia");

				fo.SelectOptions = SetSelectOptions(dataSource, displayMember, valueMember, filterColumn, filterValue);
			}

			return fo;
	
	}

	/// <summary>
	/// Convierte el datasource pasado en un string con los options, respetando valor a mostrar, valor de opcion y filtrado
	/// </summary>
	/// <param name="dataSource"></param>
	/// <param name="displayMember"></param>
	/// <param name="valueMember"></param>
	/// <param name="filterColumn"></param>
	/// <param name="filterValue"></param>
	/// <returns></returns>
	private static string SetSelectOptions(object dataSource, string displayMember, string valueMember, string filterColumn, string filterValue)
	{
		string selectOptions;

		DataTable dtSource = dataSource as DataTable;
		if (dtSource == null)
		{
			DataSet ds = dataSource as DataSet;
			if (ds != null)
			{
				dtSource = ds.Tables[0];
			}
		}

		if (dtSource != null)
		{
			//Si no debe filtrar se convierte la tabla a options normalmente, sino se convierte el resultado del filtrado.
			if (string.IsNullOrEmpty(filterColumn))
				selectOptions = TableToOptions(dtSource, displayMember, valueMember);
			else
				selectOptions = TableToOptions(FilterTable(dtSource, filterColumn, filterValue), displayMember, valueMember);
		}
		else
			throw new Exception("Tipo de variable no reconocido");

		return selectOptions;
	}

	private static DataTable FilterTable(DataTable dtSource, string filterColumn, string filterValue)
	{
		if (!dtSource.Columns.Contains(filterColumn))
			throw new Exception("Columna de filtrado no encontrada");

		DataView dv = null;
		StringBuilder sb = null;

		try
		{
			dv = new DataView(dtSource);
			sb = new StringBuilder();
			//Obtengo el tipo de la columna a filtrar
			Type columnType = dtSource.Columns[filterColumn].DataType;
			//armo la condicion de filtro, columna
			sb.Append(filterColumn);
			//operador
			sb.Append(STR_QUERY_EQUAL);
			//valor, dependiendo si agregar comillas o no
			if (columnType == typeof(char) || columnType == typeof(string))
			{
				sb.Append(STR_QUERY_APOSTROPHE);
				sb.Append(filterValue);
				sb.Append(STR_QUERY_APOSTROPHE);
			}
			else
				sb.Append(filterValue);

			dv.RowFilter = sb.ToString();
			return dv.ToTable();
		}
		finally
		{
			if (dv != null)
			{
				dv.Dispose();
				dv = null;
			}
			if (sb == null)
			{
				sb.Capacity = 0;
				sb.Length = 0;
				sb = null;
			}
		}
	}

	private static string TableToOptions(DataTable dt, string displayMember, string valueMember)
	{
		if (dt == null || dt.Rows.Count == 0)
			return string.Empty;
		if (!dt.Columns.Contains(displayMember))
			throw new Exception("No existe columna a mostrar");
		if (!dt.Columns.Contains(valueMember))
			throw new Exception("No existe valor a utilizar");

		StringBuilder sb = null;
		try
		{
			sb = new StringBuilder();
			long rowCount = dt.Rows.Count;
			//Se obtienen los indices de posicion de las columnas a usar
			int displayMemberPosition = dt.Columns[displayMember].Ordinal;
			int valueMemberPosition = dt.Columns[valueMember].Ordinal;

			for (int i = 0; i < rowCount; i++)
			{
				//Por cada fila se va apendeando un nuevo option en base a una constante de formato, se usa item array para dar mas performance
				sb.AppendFormat(STR_OPTION_FORMAT, dt.Rows[i].ItemArray[valueMemberPosition], dt.Rows[i].ItemArray[displayMemberPosition]);
			}

			return sb.ToString();
		}
		finally
		{
			if (sb == null)
			{
				sb.Capacity = 0;
				sb.Length = 0;
				sb = null;
			}
		}
	}
	#endregion

	#region htmlTables
	/// <summary>
	/// Obtiene los options para un select desde una zvar en formato string
	/// </summary>
	/// <param name="controlId"></param>
	/// <param name="dataSourceName">Nombre de zvar para obtener los options</param>
	/// <param name="displayMember">Columna que se usara como caption de los options</param>
	/// <param name="valueMember">Columna que se usara como valor de los options</param>
	/// <param name="filterColumn">Columna para filtrar valores, usar String.Empty si no se quiere filtrar</param>
	/// <param name="filterValue">Valor de filtrado, si filterColumn = string.Empty no se tendra en cuenta</param>
	/// <returns></returns>
	public FieldOptions GetZDynamicTable(string controlId,
																		string dataSourceName,
																		string showColumns,
																		string filterFieldId,
																		string editableColumns,
																		string editableColumnsAttributes,
																		string filterValues,
																		string additionalValidationButton,
																		string postAjaxFuncion,
                                                                        string tbody)
	{
			FieldOptions fo = new FieldOptions();
			fo.ControlId = controlId;
			fo.AdditionalValidationButton = additionalValidationButton;
			fo.PostAjaxFunction = postAjaxFuncion;

			if (string.IsNullOrEmpty(dataSourceName))
				throw new Exception("No se encuentra nombre de fuente de datos");

			object dataSource;
			dataSource = FillSource(dataSourceName, filterValues);

			//Convertir el source a las opciones de select
			fo.SelectOptions = SetHTMLRows(dataSource, showColumns, editableColumns, editableColumnsAttributes, string.Empty, string.Empty, tbody);

			return fo;
		
	}

	private static object FillSource(string dataSourceName, string filterValues)
	{
		object dataSource = null;
		if (dataSourceName.ToLower().StartsWith("zfillselect"))
		{
			SZQuery sQuery = new SZQuery();
			//Quitar el fillselect y dejar query | parametros
			string query = dataSourceName.ToLower().Replace("zfillselect(", string.Empty);
			query = query.Remove(query.Length - 1, 1);
			//Obtener los parametros de query
			string[] queryParamValues = filterValues.Split(new char[] { '|' });
			//Obtener la query
			string[] splitedQueryAndParams = query.Split(new char[] { '|' });
			query = splitedQueryAndParams[0];

			//Si hay parametros, los obtengo
			string[] queryParamNames = null;
			if (splitedQueryAndParams.Length > 0)
			{
				queryParamNames = splitedQueryAndParams[1].Split(new char[] { ',' });
			}

			//Llenar el source
			dataSource = sQuery.FillSource(query, queryParamValues, queryParamNames);
			sQuery = null;
		}
        else if (dataSourceName.ToLower().StartsWith("zamba_associated_documents_importants"))
        {
                                    //DataTable Asociated;
                                    ////Se cargan los asociados marcados como importantes
                                    //    DocAsociatedBusinessExt _DocAsociatedBusinessExt =  new DocAsociatedBusinessExt();
                                    //    Asociated = _DocAsociatedBusinessExt.getImportantAsociatedResults(localResult);
                                    //    _DocAsociatedBusinessExt.Dispose();
                                    //    _DocAsociatedBusinessExt = Nothing;
                                    //    LoadTable(AsociatedTable, Mydoc, False, localResult, Asociated);
       

        }
        else
        {
            //TODO implementar para variables de zamba.

            //if (!VariablesInterReglas.ContainsKey(dataSourceName))
            //    throw new Exception("Variable de datos no encontrada");

            //dataSource = VariablesInterReglas.get_Item(dataSourceName);

            //if (dataSource == null)
            //    throw new Exception("Variable de datos vacia");

            //fo.SelectOptions = SetHTMLRows(dataSource, showColumns, gType, filterColumn, filterValue);
        }
		return dataSource;
	}

    public String TestCompleteFormAsociates(Int64 TaskId, Int64 UserId) { 
    
        //Int64 TaskId = 1;
        //Int64 UserId = 1;

        IResult Task = new Zamba.Services.STasks().GetTask(TaskId,UserId);
        IUser user = new Zamba.Services.SUsers().GetUser(UserId);

        //<table  class="tablesorter" id="zamba_zvar(Facturas_Globales)">
        //                <tbody id="zamba_rule_34438/Factura Global/zvar(GastoTaskID=10)">

         //<table id="zamba_zvar(DocumentacionFaltanteIngreso)" class="tablesorter">
         //               <tbody id="zamba_rule_46281/Ver/zvar(DocFaltante=5)">
					
         //<table id="zamba_associated_documents_26032" class="tablesorter">
         //           <tbody>
         //           </tbody>
         //       </table>
        string TableId = "zamba_associated_documents_26032";
        string TBodyId = ""; //"zamba_rule_0/Previsualizar/zvar(path=length-3)/prev§zamba_rule_6471/Responder/zvar(rutaDocumento=length-3)";
        Int64 AsociatedId = 26032;

        return GetAsociatedResults(TableId, TBodyId, Task, user, AsociatedId);
    }


    public string GetAsociatedResults(String TableId, string TBodyId, IResult Task, IUser user, Int64 AsociatedId)
    {
        Boolean OnlyWF = false;
        if (ContainsCaseInsensitive(TableId, "zamba_associated_documents_WF"))
            OnlyWF = true;

        //SDocAsociated SDocAsociated = new SDocAsociated();
        //DataSet docTypeAsoc = SDocAsociated.getDocTypesAsociated(Task.DocTypeId);
        //SDocAsociated = null;

        //List<Int64> docTypeAsocIDs = new List<long>();

        //foreach (DataRow drRow in docTypeAsoc.Tables[0].Rows)
        //{
        //    Int64 dtid = Int64.Parse(drRow[1].ToString());
        //    if (!docTypeAsocIDs.Contains(dtid))
        //        docTypeAsocIDs.Add(dtid);
        //}

        //docTypeAsoc.Dispose();
        //docTypeAsoc = null;

        SDocAsociated sda = new SDocAsociated();
        DataTable AsociatedResults = null;

        //foreach (Int64 DocTypeId in docTypeAsocIDs)
        //{
        AsociatedResults = sda.getAsociatedResultsFromResultAsList(AsociatedId, Task, user.ID, true);
        //}

        DataTable dt = AsociatedResults;
        if (string.IsNullOrEmpty(TBodyId) == false)
                {
                   // dt = ParseResult(AsociatedResults, OnlyWF, TBodyId, Task, user);
        return               DataTableToJSON(dt);
                }
                else
                {
                   // dt = ParseResult(AsociatedResults, OnlyWF, string.Empty, Task,user);
                    return DataTableToJSON(dt);
                }

                //if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                //{
                //    itemToReturn = LoadTableHeader(itemToReturn, dt.Columns);
                //    itemToReturn = LoadTableBody(itemToReturn, dt.Rows);
                //}

//        return itemToReturn;
    }



    /// <summary>
    /// Obtiene los atributos asociados de un entidad 
    /// </summary>
    /// <param name="docTypeId">Acepta el formato "zamba_asoc_[doctypeid]_" o directamente el docTypeId en formato string</param>
    /// <param name="body">Body donde se obtendrán los atributos</param>
    /// <returns>Un listado de atributos asociados al entidad solicitado. 
    ///         Puede que devuelva un id de tipo string o el nombre del índice.</returns>
    private List<string> getIndexItems(string docTypeId, string body)
    {
        List<string> elements = new List<string>();
        Int64 index;
        Int32 lastPosition;
        string elem;

        //Valida que existan datos a procesar
        if (!string.IsNullOrEmpty(body) && !string.IsNullOrEmpty(docTypeId))
        {
            //Verifica el formato de docTypeId
            if (Int64.TryParse(docTypeId, out index))
                docTypeId = "zamba_asoc_" + docTypeId + "_";
            else
                docTypeId = docTypeId.ToLower();

            //Verifica que en el body exista algún índice asociado del entidad a buscar
            body = body.ToLower();
            while (body.Contains(docTypeId))
            {
                //Obtiene el id del índice y lo agrega a una lista
                lastPosition = body.IndexOf(docTypeId);
                elem = body.Substring(lastPosition);
                elem = elem.Substring(0, elem.IndexOf(" ")).Replace("\"", String.Empty);
                elements.Add(elem.Replace(docTypeId + "index_", String.Empty));

                //Modifica el body para no encontrar el último índice agregado
                body = body.Substring(lastPosition).Replace(elem, String.Empty);
            }
        }

        return elements;
    }



    private static bool ContainsCaseInsensitive(string source, string value)
    {
        int results = source.IndexOf(value, StringComparison.CurrentCultureIgnoreCase);
        return results == -1 ? false : true;
    }

    private static bool IsNumeric(object expression)
    {
        double retNum;

        bool isNum = Double.TryParse(Convert.ToString(expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        return isNum;
    }

    public static string DataTableToJSON(DataTable table)
    {
        var list = new List<Dictionary<string, object>>();

        foreach (DataRow row in table.Rows)
        {
            var dict = new Dictionary<string, object>();

            foreach (DataColumn col in table.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(list);
    }

    /// <summary>
    /// Convierte el contenido de un listado de results en un Datatable
    /// </summary>
    /// <param name="results"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    /// <history> 
    ///     [Gaston]    19/11/2008     Modified     Se agrego la columna "Estado" y verificación del documento asociado para ver si es una tarea
    ///     [Gaston]    20/11/2008     Modified     Se agrego la columna "Usuario Asignado" 
    ///     [Gaston]    05/01/2009     Modified     Verificación de la columna "Nombre del Documento" en el UserPreferences para mostrar o ocultar 
    ///                                             dicha columna
    ///     [Gaston]    06/01/2009     Modified     Validación del valor de "Nombre del Documento" y código comentado en donde se intenta colocar
    ///                                             un String.Empty en una columna que no existe
    ///     Marcelo     05/02/2009     Modified     Se modifico la carga de los indices para mejorar la performance 
    ///     Marcelo     06/01/2010     Modified     Se agrego variable para cargar solo las tareas que esten en WF
    /// </history>
    public DataTable ParseResult(List<IResult> results, bool onlyWF, string TBodyId, IResult Task, IUser user)
    {
        //En caso de que el contendido sea vacio retornamos una tabla vacia
        if (results.Count == 0)
            return new DataTable();

        DataTable Dt = new DataTable();
        Dictionary<Int64, Int64> taskids = new Dictionary<Int64, Int64>();

        STasks STasks = new STasks();
        SRights SRights = new SRights();
        UserPreferences UserPreferences = new UserPreferences();

        Char mander = Char.Parse("§");
        Char pipe = Char.Parse("/");
        Char equal = Char.Parse("=");

        //Dt.Columns.Add(new DataColumn("Ver"));

        if (string.IsNullOrEmpty(TBodyId) == false)
        {
            if (TBodyId.Split(mander).Length <= 1)
                TBodyId = string.Empty;
            else
                foreach (string btn in TBodyId.Split(mander))
                {
                    String[] items = btn.Split(pipe);
                    Dt.Columns.Add(items[1].ToString());
                }
        }

            Dt.Columns.Add(new DataColumn("Nombre"));
            Dt.Columns.Add(new DataColumn("Estado"));
     
            Dt.Columns.Add(new DataColumn("Usuario Asignado"));
      
        try
        {
            Type CurrentIndexType = null;

            //Cargo todos los indices de todos los results , como pueden ser diferentes tipos de documento recorro todos
            //Solo visualizo en la tabla los indices sobre los cuales tiene permiso el documento. Mariela
            Int64 lastDocTypeId = 0;
            Hashtable IRI = null;

            foreach (IResult CurrentResult in results)
            {
                // Se verifica si el documento es una tarea
                Int64 TaskId = -1;

                List<Int64> taskIdList = STasks.GetTaskIDsByDocId(CurrentResult.ID);

                if ((taskIdList != null) && taskIdList.Count == 1)
                {
                    TaskId = taskIdList[0];
                    taskids.Add(CurrentResult.ID, TaskId);
                }

                if (onlyWF == false | TaskId != -1)
                {
                    //Guardo el entidad anterior asi no recargo los indices mas veces de las necesarias
                    if (lastDocTypeId != CurrentResult.DocTypeId)
                    {
                        UserBusiness UB = new UserBusiness();
                        lastDocTypeId = CurrentResult.DocTypeId;
                        //26/10/2011: Se cambia la forma de obtener los permisos por indices a la misma que en windows. Dado que no los obtenia en forma correcta
                        IRI = UB.GetAssociatedIndexsRightsCombined(Task.DocTypeId, lastDocTypeId, user.ID);
                        UB = null;
                    }

                    RightsBusiness RiB = new RightsBusiness();
                    foreach (IIndex CurrentIndex in CurrentResult.Indexs)
                    {
                        bool ShowIndex = false;
                        if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewAssociateRightsByIndex, Int32.Parse(Task.DocTypeId.ToString())))
                        {
                            AssociatedIndexsRightsInfo IR = (AssociatedIndexsRightsInfo)IRI[CurrentIndex.ID];

                            foreach (Int64 indexid in IRI.Keys)
                            {
                                if (indexid == CurrentIndex.ID)
                                {
                                    //26/10/2011: Se cambia el tipo de permiso a buscar.
                                    if (IR.GetIndexRightValue(RightsType.AssociateIndexView))
                                        ShowIndex = true;

                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                        }
                        else
                        {
                            ShowIndex = true;
                        }

                        if (ShowIndex)
                        {
                            if (!Dt.Columns.Contains(CurrentIndex.Name))
                            {
                                CurrentIndexType = typeof(string);

                                if (CurrentIndex.DropDown == IndexAdditionalType.LineText)
                                {
                                    CurrentIndexType = GetIndexType(CurrentIndex.Type);
                                }
                                else
                                {
                                    CurrentIndexType = typeof(string);
                                }

                                Dt.Columns.Add(CurrentIndex.Name.Trim(), CurrentIndexType);
                            }
                        }
                    }
                    RiB = null;
                }
            }

         
                Dt.Columns.Add(new DataColumn("Creado"));
          
                Dt.Columns.Add(new DataColumn("Entidad"));
          
                Dt.Columns.Add(new DataColumn("Modificado"));
          
            if (bool.Parse(UP.getValue("NombreOriginal", UPSections.FormPreferences, "True")) == true)
            {
                Dt.Columns.Add(new DataColumn("Original"));
            }
            if (bool.Parse(UP.getValue("NumerodeVersion", UPSections.FormPreferences, "True")) == true)
            {
                Dt.Columns.Add(new DataColumn("Numero de Version"));
            }
            if (bool.Parse(UP.getValue("ParentId", UPSections.FormPreferences, "True")) == true)
            {
                Dt.Columns.Add(new DataColumn("ParentId"));
            }
          
            Dt.Columns.Add(new DataColumn("Ruta Documento"));
            Dt.Columns.Add(new DataColumn("DoctypeId"));
            Dt.Columns.Add(new DataColumn("IdDoc", typeof(Int64)));
            Dt.AcceptChanges();

            DataRow CurrentRow = null;

            foreach (IResult CurrentResult in results)
            {
                // Se verifica si el documento es una tarea
                if (onlyWF == false | taskids.ContainsKey(CurrentResult.ID))
                {
                    CurrentRow = Dt.NewRow();

                    CurrentRow["Nombre"] = CurrentResult.Name;
                  
                    CurrentRow["IdDoc"] = CurrentResult.ID;
                    CurrentRow["Ruta Documento"] = CurrentResult.FullPath;

                    if (bool.Parse(UP.getValue("FechaCreacion", UPSections.FormPreferences, "True")) == true)
                    {
                        CurrentRow["Creado"] = (DateTime)CurrentResult.CreateDate;
                    }
                    if (bool.Parse(UP.getValue("TipodeDocumento", UPSections.FormPreferences, "True")) == true)
                    {
                        CurrentRow["Entidad"] = CurrentResult.Parent.Name;
                    }
                    if (bool.Parse(UP.getValue("FechaModificacion", UPSections.FormPreferences, "True")) == true)
                    {
                        CurrentRow["Modificado"] = (DateTime)CurrentResult.EditDate;
                    }
                    if (bool.Parse(UP.getValue("NumerodeVersion", UPSections.FormPreferences, "True")) == true)
                    {
                        CurrentRow["Numero de Version"] = CurrentResult.VersionNumber;
                    }
                    if (bool.Parse(UP.getValue("ParentId", UPSections.FormPreferences, "True")) == true)
                    {
                        CurrentRow["ParentId"] = CurrentResult.ParentVerId;
                    }

                    CurrentRow["DoctypeId"] = CurrentResult.DocType.ID;

                    foreach (IIndex CurrentIndex in CurrentResult.Indexs)
                    {
                        Type IndexType = GetIndexType(CurrentIndex.Type);

                        try
                        {
                            //Si Data tiene un valor que se le asigne al Item
                            if (!string.IsNullOrEmpty(CurrentIndex.Data) && CurrentRow.Table.Columns.Contains(CurrentIndex.Name))
                            {
                                if (CurrentIndex.DropDown == IndexAdditionalType.LineText)
                                {
                                    if (CurrentIndex.Type == IndexDataType.Si_No)
                                    {
                                        if (int.Parse(CurrentIndex.Data) == 0)
                                        {
                                            CurrentRow[CurrentIndex.Name] = "No";
                                        }
                                        else
                                        {
                                            CurrentRow[CurrentIndex.Name] = "Si";
                                        }
                                    }
                                    else
                                    {
                                        CurrentRow[CurrentIndex.Name] = CurrentIndex.Data;
                                    }

                                }
                                else
                                {
                                    if (string.Compare(string.Empty, CurrentIndex.dataDescription) != 0)
                                    {
                                        CurrentRow[CurrentIndex.Name] = CurrentIndex.dataDescription;
                                    }
                                    else
                                    {
                                        CurrentRow[CurrentIndex.Name] = CurrentIndex.Data;
                                    }
                                }

                                //Si Data no tiene valor se le asigna el de DataDescription
                                //(si es que no esta vacío)
                            }
                            else if (!string.IsNullOrEmpty(CurrentIndex.dataDescription) && CurrentRow.Table.Columns.Contains(CurrentIndex.Name))
                            {
                                CurrentRow[CurrentIndex.Name] = CurrentIndex.dataDescription;
                                //Si no hay valor para asignar no hace nada
                            }
                        }
                        catch (Exception ex)
                        {
                           ZClass.raiseerror(ex);
                        }
                    }

                    if (taskids.ContainsKey(CurrentResult.ID))
                    {
                        DataSet dsTask = STasks.GetTaskDs(taskids[CurrentResult.ID]);

                        // Si el documento es una tarea entonces se coloca el estado actual de la tarea, sino, el estado se coloca como vacío
                        if (dsTask != null && dsTask.Tables.Count == 1 && dsTask.Tables[0].Rows.Count >= 1)
                        {
                                SStepStates SStepStates = new SStepStates(ref user);
                                CurrentRow["Estado"] = SStepStates.GetStateName(Int32.Parse(dsTask.Tables[0].Rows[0]["Do_State_Id"].ToString()));
                          
                                SUserGroup SUserGroup = new SUserGroup(ref user);
                            Boolean IsGroup = false;
                                CurrentRow["Usuario Asignado"] = SUserGroup.GetUserorGroupNamebyId(Int32.Parse(dsTask.Tables[0].Rows[0]["User_Asigned"].ToString()),ref IsGroup);
                          
                            dsTask.Dispose();
                            dsTask = null;
                        }
                    }

                    //Nombre del documento
                    if (bool.Parse(UP.getValue("NombreOriginal", UPSections.FormPreferences, "True")) == true)
                    {
                        string FileName = CurrentResult.OriginalName;

                        if (FileName == null)
                            FileName = CurrentResult.Name;

                        Int32 indexpath = FileName.LastIndexOf("\\");

                        if (indexpath == -1 || FileName.Length - 1 == -1)
                        {
                        }
                        else
                        {
                            if (indexpath == -1)
                                indexpath = 0;
                            try
                            {
                                FileName = FileName.Substring(indexpath + 1, FileName.Length - indexpath - 1);
                            }
                            catch
                            {
                                FileName = CurrentResult.OriginalName;
                            }
                        }
                        CurrentRow["Original"] = FileName;
                    }

                    StringBuilder InnerHtml = new StringBuilder();

                    if (string.IsNullOrEmpty(TBodyId) == false)
                    {
                        string textItem2 = null;
                        string textAux = null;

                        if (TBodyId.Split(mander).Length <= 1)
                            TBodyId = string.Empty;
                        else
                            foreach (string btn in TBodyId.Split(mander))
                            {
                                InnerHtml.Remove(0, InnerHtml.Length);
                                String[] items = btn.Split(pipe);
                                Int32 itemNum = default(Int32);
                                String[] zvarItems = null;
                                string @params = null;

                                InnerHtml.Append("&nbsp;<INPUT id=");
                                InnerHtml.Append(Convert.ToChar(34));

                                //Si tiene zvar
                                if (items.Length > 2)
                                {
                                    textItem2 = items[2].ToString();
                                    InnerHtml.Append(items[0] + "_");

                                    while (string.IsNullOrEmpty(textItem2) == false)
                                    {
                                        textAux = textItem2.Remove(0, 5);
                                        zvarItems = textAux.Remove(textAux.IndexOf(")")).Split(equal);
                                        textItem2 = textItem2.Remove(0, textItem2.IndexOf(")") + 1);

                                        if (Int32.TryParse(zvarItems[1].ToString(), out itemNum) == false)
                                        {
                                            if (zvarItems[1].ToString().ToLower().Contains("length"))
                                            {
                                                itemNum = Dt.Columns.Count - Int32.Parse(zvarItems[1].ToString().Split(Char.Parse("-"))[1]);
                                            }
                                        }
                                        InnerHtml.Append("zvar(" + zvarItems[0].ToString() + "=" + CurrentRow[itemNum].ToString() + ")");

                                        @params = @params + "'" + CurrentRow.ItemArray[itemNum].ToString() + "',";
                                    }
                                }
                                else
                                {
                                    InnerHtml.Append(items[0]);
                                }

                                InnerHtml.Append(Convert.ToChar(34));
                                InnerHtml.Append(" type=button onclick=");

                                //Si hay un cuarto parametro es el nombre de la funcion JS que hay que llamar,
                                //sino se llama a SetRuleId por default
                                if (items.Length > 3)
                                {
                                    InnerHtml.Append(Convert.ToChar(34));
                                    InnerHtml.Append(items[3] + "(this, ");
                                    InnerHtml.Append(@params.Substring(0, @params.Length - 1).Replace("\\", "\\\\"));
                                    InnerHtml.Append(");");
                                    InnerHtml.Append(Convert.ToChar(34));
                                }
                                else
                                {
                                    InnerHtml.Append(Convert.ToChar(34));
                                    InnerHtml.Append("SetRuleId(this);");
                                    InnerHtml.Append(Convert.ToChar(34));
                                }

                                InnerHtml.Append(" value = ");
                                InnerHtml.Append(Convert.ToChar(34));
                                InnerHtml.Append(items[1]);
                                InnerHtml.Append(Convert.ToChar(34));
                                InnerHtml.Append(" Name = ");
                                InnerHtml.Append(Convert.ToChar(34));
                                InnerHtml.Append(items[0]);
                                InnerHtml.Append(Convert.ToChar(34));
                                InnerHtml.Append(" >");

                                CurrentRow[items[1]] = InnerHtml.ToString();
                                @params = string.Empty;
                            }
                    }

                    InnerHtml = null;
                    Dt.Rows.Add(CurrentRow);
                }
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);

        }

        Dt.AcceptChanges();
        Dt.DefaultView.Sort = "IdDoc DESC";

        return Dt.DefaultView.ToTable();
    }

    /// <summary>
    /// Castea el tipo de un índice a Type
    /// </summary>
    /// <param name="indexType"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    private Type GetIndexType(IndexDataType indexType)
    {
        Type ParsedIndexType = null;

        switch (indexType)
        {
            case IndexDataType.Alfanumerico:
                ParsedIndexType = typeof(string);
                break;
            case IndexDataType.Alfanumerico_Largo:
                ParsedIndexType = typeof(string);
                break;
            case IndexDataType.Fecha:
                ParsedIndexType = typeof(System.DateTime);
                break;
            case IndexDataType.Fecha_Hora:
                ParsedIndexType = typeof(DateTime);
                break;
            case IndexDataType.Moneda:
                ParsedIndexType = typeof(decimal);
                break;
            case IndexDataType.None:
                ParsedIndexType = typeof(string);
                break;
            case IndexDataType.Numerico:
                ParsedIndexType = typeof(Int64);
                break;
            case IndexDataType.Numerico_Decimales:
                ParsedIndexType = typeof(decimal);
                break;
            case IndexDataType.Numerico_Largo:
                ParsedIndexType = typeof(decimal);
                break;
            case IndexDataType.Si_No:
                ParsedIndexType = typeof(string);
                break;
            default:
                ParsedIndexType = typeof(string);
                break;
        }

        return ParsedIndexType;
    }

    /// <summary>
    /// Carga las columnas header de un DataTable en el una tabla Html del documento
    /// </summary>
    /// <param name="table"></param>
    /// <param name="dcs"></param>
    /// <remarks></remarks>
    private String LoadTableHeader(String table, DataColumnCollection dcs)
    {
        UserPreferences UserPreferences = new UserPreferences();

        //se agrega una columna para el link de abrir la tarea
        String HeaderRow = "<tr><th width='20'></th>";

        String HeaderColumn = string.Empty;

        ////Agrego columnas de atributos
        foreach (DataColumn Column in dcs)
        {
            if (string.Compare(Column.ColumnName.ToLower(), "iddoc") == 0 && bool.Parse(UP.getValue("ResultId", UPSections.FormPreferences, "True")) == false)
            {

                HeaderColumn = "<th style=\"display:none\">" + Column.ColumnName + "</th>";
                HeaderRow = HeaderRow + HeaderColumn;
                continue;
            }

            if (string.Compare(Column.ColumnName.ToLower(), "nombre") == 0 )
            {

                HeaderColumn = "<th style=\"display:none\">" + Column.ColumnName + "</th>";
                HeaderRow = HeaderRow + HeaderColumn;
            }
            else
                if (string.Compare(Column.ColumnName.ToLower(), "doctypeid") == 0 && bool.Parse(UP.getValue("DoctypeId", UPSections.FormPreferences, "True")) == false)
                {
                }
                else
                    if (string.Compare(Column.ColumnName.ToLower(), "ruta documento") == 0 && bool.Parse(UP.getValue("RutaDocumento", UPSections.FormPreferences, "False")) == false)
                    {
                    }
                    else
                    {
                        HeaderColumn = "<th>" + Column.ColumnName + "</th>";

                        HeaderRow = HeaderRow + HeaderColumn;
                    }
        }

        return table + HeaderRow + "</tr>";
    }

    /// <summary>
    /// Carga el contenido de un DataTabla en el body de una tabla Html del documento
    /// </summary>
    /// <param name="table">Tabla HTML donde se cargaran los datos</param>
    /// <param name="drs">Rows que van a ser cargadas</param>
    /// <param name="mydoc">Documento HTML que contiene la tabla</param>
    /// <remarks></remarks>
    private string LoadTableBody(String table, DataRowCollection drs)
    {
        String CurrentRow = String.Empty;
        Int32 i;

        UserPreferences UserPreferences = new UserPreferences();

        foreach (DataRow dr in drs)
        {
            CurrentRow = CurrentRow + "<tr>";
            i = 0;

            String DocTypeId = String.Empty;
            String DocId = String.Empty;
          

            foreach (object CellValue in dr.ItemArray)
            {
                if (i == dr.ItemArray.Length - 1)
                {
                    DocId = CellValue.ToString();
                }
                else if (i == dr.ItemArray.Length - 2)
                {
                    DocTypeId = CellValue.ToString();
                }

                if (i == dr.ItemArray.Length - 1 && bool.Parse(UP.getValue("ResultId", UPSections.FormPreferences, "True")) == false)
                {
                    //Ezequiel: Si la columna docid esta para que no se vea en la grilla pongo la columna como no visible.
                    CurrentRow = CurrentRow + "<td style=\"display:none\">" + CellValue.ToString() + "</td>";
                }
                else if (i == dr.ItemArray.Length - 2 && bool.Parse(UP.getValue("DoctypeId", UPSections.FormPreferences, "True")) == false)
                {
                  
                    DocTypeId =CellValue.ToString();
                }
                else if (i == dr.ItemArray.Length - 3 && bool.Parse(UP.getValue("RutaDocumento", UPSections.FormPreferences, "False")) == false)
                {
                }
                else
                {
                    if (CellValue is DateTime)
                    {
                        string date = CellValue.ToString();
                        if (date.Length > 10)
                            date = date.Substring(0, 10);
                        CurrentRow = CurrentRow + "<td>" + date + "</td>";
                    }
                    else
                         
                        CurrentRow = CurrentRow + "<td>" + CellValue.ToString() + "</td>";
                }

                i = i + 1;
            }

            //se agrega una columna mas con el link.
            CurrentRow = CurrentRow + "</tr>";
            StringBuilder Link = new StringBuilder();
            Link.Append("<tr class=\"FormRowStyle\"><td style='text-decoration: none'  width='20'><a href=" + Convert.ToChar(34));
            Link.Append(MembershipHelper.Protocol);
           // Link.Append(Request.ServerVariables["HTTP_HOST"]);
          //  Link.Append(Request.ApplicationPath);
            Link.Append("/Views/WF/TaskSelector.ashx?docid=");
            Link.Append(DocId);
            Link.Append("&doctypeid=");
            Link.Append(DocTypeId);
            Link.Append("&userId=");
            Link.Append(Zamba.Membership.MembershipHelper.CurrentUser.ID);
            Link.Append(Convert.ToChar(34));
            Link.Append(" style=" + Convert.ToChar(34) + "text-decoration: none" + Convert.ToChar(34) + " ><img height='20' src='");
            Link.Append(Tools.GetProtocol(HttpContext.Current.Request));
          //  Link.Append(Request.ServerVariables["HTTP_HOST"]);
           // Link.Append(Request.ApplicationPath);
            Link.Append("/Content/Images/Toolbars/play.png' border='0'/> </a></td><td>");
            CurrentRow = CurrentRow.Replace("<tr><td>", Link.ToString());
        }
        return table + CurrentRow;
    }



    public string GetDocFileUrl
    {
        get
        {
            return @"http://" +  @"/Services/GetDocFile.ashx?DocTypeId={0}&DocId={1}&UserID={2}";
        }
    }

	/// <summary>
	/// Obtiene en un string todos los tr y td del cuerpo de una tabla para el datasource
	/// </summary>
	/// <param name="dataSource"></param>
	/// <param name="showColumns"></param>
	/// <param name="editableColumns"></param>
	/// <param name="editableColumnsAttributes"></param>
	/// <param name="filterColumn"></param>
	/// <param name="filterValue"></param>
	/// <returns></returns>
	private static string SetHTMLRows(object dataSource, string showColumns, string editableColumns,
																		string editableColumnsAttributes, string filterColumn, string filterValue, string tbody)
	{
		string selectOptions;

		DataTable dtSource = dataSource as DataTable;
		if (dtSource == null)
		{
			DataSet ds = dataSource as DataSet;
			if (ds != null)
			{
				dtSource = ds.Tables[0];
			}
		}

		if (dtSource != null)
		{
			//Si no debe filtrar se convierte la tabla a options normalmente, sino se convierte el resultado del filtrado.
			if (string.IsNullOrEmpty(filterColumn))
				selectOptions = TableHTMLRows(dtSource, showColumns, editableColumns, tbody, editableColumnsAttributes);
			else
				selectOptions = TableHTMLRows(FilterTable(dtSource, filterColumn, filterValue), showColumns, editableColumns, tbody, editableColumnsAttributes);
		}
		else
			throw new Exception("Tipo de variable no reconocido");

		return selectOptions;
	}

	/// <summary>
	/// Obtiene en un string todos los tr y td del cuerpo de una tabla para el datasource
	/// </summary>
	/// <param name="dtSource"></param>
	/// <param name="showColumns"></param>
	/// <param name="editableColumns"></param>
	/// <param name="editableColumnsAttributes"></param>
	/// <returns></returns>
	private static string TableHTMLRows(DataTable dtSource, string showColumns, string editableColumns,string tbody, string editableColumnsAttributes)
	{
		string a = string.Empty;
		a = LoadZVarTableHeader(a, dtSource.Columns, tbody);
		//Se crea un tbody para la parametrizacion de valores como columnas editables
		a = LoadZVarTableBody(a, dtSource.Rows, string.Format(HTML_PARAMBODY, editableColumns), tbody ,editableColumnsAttributes);
		return a;
	}

	/// <summary>
	/// Carga el contenido de un DataTable en el body de una tabla Html del documento
	/// </summary>
	/// <param name="table">Tabla HTML donde se cargaran los datos</param>
	/// <param name="drs">Rows que van a ser cargadas</param>
	/// <param name="body">Cuerpo de la tabla</param>
	/// <remarks></remarks>
    public static string LoadZVarTableBody(String table, DataRowCollection drs, string definededitablecolumns, string body, string editableColumnsAttributes)
    {
        String CurrentRow = String.Empty;
        Dictionary<int, List<string[]>> dic = null;
        if (!string.IsNullOrEmpty(editableColumnsAttributes))
        {
            dic = ParseColumConfiguration(editableColumnsAttributes);
        }

        int rowOrdinal = 0;

        string[] editablecolumns = null;
        if (body.ToLower().Contains("editablecolumns"))
            editablecolumns = HTML.GetAttributeValue(body, "editablecolumns").Split(',');

        foreach (DataRow dr in drs)
        {
            rowOrdinal++;
            CurrentRow = CurrentRow + "<tr>";

            if (string.IsNullOrEmpty(body.Trim()) == false)
            {

                if (body.ToLower().Contains("zamba_rule"))
                {
                    StringBuilder Link = new StringBuilder();

                    Link.Append("<tr class=\"FormRowStyle ZGridRow\"><td class=\"ZGridCell ZRuleCell\" style='text-decoration: none' width='20'><input type=button ");

                    //Hay un error con este metodo, que para obtener el atributo quita el nombre del atributo del contenido del mismos, ejemplo de cantidad si el atributo es id queda cantad
                    string idvalue = HTML.GetAttributeValue(body, "id");
                    String[] zvarsItems = idvalue.Split(char.Parse("/"));
                    if (zvarsItems.Length > 2)
                    {

                        string[] Items = zvarsItems[2].Trim().Split(char.Parse("="));

                        String FirstStringForRuleId = zvarsItems[0].Trim();
                        Int32 Start = FirstStringForRuleId.IndexOf("zamba_rule");
                        Int32 End = FirstStringForRuleId.Length - Start;
                        string ruleId = FirstStringForRuleId.Substring(Start, End);

                        Link.Append("id=" + Convert.ToChar(34) + ruleId + "_" + Items[0] + "=");
                        Link.Append("IdTarea" + Convert.ToChar(34) + " onclick=" + Convert.ToChar(34) + "SetRuleId(this);" + Convert.ToChar(34) + " value = " + Convert.ToChar(34) + zvarsItems[1] + Convert.ToChar(34) + " Name = " + Convert.ToChar(34) + zvarsItems[0] + Convert.ToChar(34) + " /></td>");

                        //string[] editablecols = null;

                        //if (body.ToLower().Contains("editablecolumns"))
                        //{
                        //    string colsvalue = HTML.GetAttributeValue(body, "editablecolumns");
                        //    editablecols = colsvalue.Split(Char.Parse(","));

                        //}

                        //foreach (String item in Items)
                        //{
                        //    if (string.IsNullOrEmpty(item) == false)
                        //    {
                    //            string[] values = item.Split(char.Parse("="));
                    //            String row = string.Empty;
                    //            if (item.ToLower().Contains("zvar"))
                    //            {
                    //                values[0] = "id";
                    //                row = values[0] + "=";
                    //                Link.Append(row);
                    //                if (editablecols != null && editablecols.Contains(values[1].Replace("]", string.Empty)))
                    //                {
                    //                    Link.Append("' + $(this).closest('tr').find('td').eq(" + values[1].Replace("]", string.Empty) + ").children('input').val() + '");
                    //                }
                    //                else
                    //                {
                    //                    Link.Append("' + $(this).closest('tr').find('td').eq(" + values[1] + ").text() + '");
                    //                }
                    //            }

                    //            Link.Append("&");
                           //}
                  }
                    //    Link.Remove(Link.Length - 4, 4);
                    //    Link.Append(")" + Convert.ToChar(34));
                   // }

                    //Link.Append(" style=" + Convert.ToChar(34) + "" + Convert.ToChar(34) + " /></td>");


                    CurrentRow = CurrentRow.Replace("<tr>", Link.ToString());
                }
                else if (body.ToLower().Contains("hideopentask"))
                {

                }
                else
                {
                    StringBuilder Link = new StringBuilder();
                    Link.Append("<tr class=\"FormRowStyle ZGridRow\"><td class=\"ZGridCell\" style='text-decoration: none'  width='20'><a href=" + Convert.ToChar(34));
                    Link.Append(Tools.GetProtocol(HttpContext.Current.Request));
                    Link.Append(HttpContext.Current.Request.ServerVariables["HTTP_HOST"]);
                    Link.Append(HttpContext.Current.Request.ApplicationPath);
                    Link.Append("/Views/WF/TaskSelector.ashx?");

                    String[] zvarsItems = body.Split(char.Parse("/"));
                    if (zvarsItems.Length > 2)
                    {
                        string[] Items = zvarsItems[2].Trim().Split(char.Parse(")"));

                        foreach (String item in Items)
                        {
                            if (string.IsNullOrEmpty(item) == false)
                            {
                                string[] values = item.Split(char.Parse("="));
                                String row = string.Empty;
                                if (item.ToLower().Contains("zvar"))
                                {
                                    row = item.Replace("zvar(", string.Empty);
                                    Link.Append(row.Split(char.Parse("="))[0]);
                                }
                                else
                                {
                                    Link.Append(values[0]);
                                }

                                if (values.Length > 1)
                                {
                                    Link.Append("=");
                                    if (string.IsNullOrEmpty(row))
                                    {
                                        Link.Append(values[1]);
                                    }
                                    else
                                    {
                                        Link.Append(dr[Int32.Parse(values[1])]);
                                    }
                                }
                                Link.Append("&");
                            }
                        }
                        Link.Remove(Link.Length - 1, 1);
                    }
                    Link.Append("&userId=");
                    Link.Append(Zamba.Membership.MembershipHelper.CurrentUser.ID);
                    Link.Append(Convert.ToChar(34));
                    Link.Append(" style=" + Convert.ToChar(34) + "text-decoration: none" + Convert.ToChar(34) + " ><img height='20' src='");
                    Link.Append(Tools.GetProtocol(HttpContext.Current.Request));
                    Link.Append(HttpContext.Current.Request.ServerVariables["HTTP_HOST"]);
                    Link.Append(HttpContext.Current.Request.ApplicationPath);
                    Link.Append("/Content/Images/Toolbars/play.png' border='0' class=\"ZGridImagePlay\"/> </a></td>");
                    CurrentRow = CurrentRow.Replace("<tr>", Link.ToString());

                }
            }

            CurrentRow += GetHTMLCells(dr, editablecolumns, dic, rowOrdinal) + "</tr>";
        }
        return table + CurrentRow;
    }

	/// <summary>
	/// Convierte una lista de string[nombre atributo][valor] en atributos html
	/// </summary>
	/// <param name="list"></param>
	/// <returns></returns>
	private static string GetAttributesString(List<string[]> list)
	{
		StringBuilder sbAttributes = new StringBuilder();
		try
		{
			int maxAttributes = list.Count;
			for (int i = 0; i < maxAttributes; i++)
			{
				sbAttributes.AppendFormat(HTML_ATTRIBUTE_TEMPLATE, list[i][0], list[i][1]);
			}
			return sbAttributes.ToString();
		}
		finally
		{
			if (sbAttributes != null)
			{
				sbAttributes.Length = 0;
				sbAttributes.Capacity = 0;
			}
		}
	}

	/// <summary>
	/// Parsea la configuracion de columnas en un diccionario que tiene como key el ordinal de la columna y como value la lista de atributos para los input
	/// </summary>
	/// <param name="editableColumnsAttributes"></param>
	/// <returns></returns>
	private static Dictionary<int, List<string[]>> ParseColumConfiguration(string editableColumnsAttributes)
	{
		if (string.IsNullOrEmpty(editableColumnsAttributes))
			return null;

		Dictionary<int, List<string[]>> dicToReturn = new Dictionary<int, List<string[]>>();
		string[] columnsConfiguration = editableColumnsAttributes.Split('^');
		string[] splittedConfiguration;
		int configCount = columnsConfiguration.Length;
		int colPosition;
		List<string[]> listAttributes;
		for (int i = 0; i < configCount; i++)
		{
			columnsConfiguration[i] = columnsConfiguration[i].Replace("[", string.Empty);
			columnsConfiguration[i] = columnsConfiguration[i].Replace("]", string.Empty);
			splittedConfiguration = columnsConfiguration[i].Split('|');
			colPosition = int.Parse( splittedConfiguration[0]);
			listAttributes = new List<string[]>();
			foreach (string item in splittedConfiguration.Skip(1))
			{
				listAttributes.Add(item.Split(':'));
			}
			dicToReturn.Add(colPosition, listAttributes);
		}

		return dicToReturn;
	}

	/// <summary>
	/// Otiene el string de cada una de las celdas de un DataRow dado
	/// </summary>
	/// <param name="dr"></param>
	/// <param name="editablecolumns"></param>
	/// <param name="columnConfigurations"></param>
	/// <param name="rowOrdinal"></param>
	/// <returns></returns>
	private static string GetHTMLCells(DataRow dr, string[] editablecolumns, Dictionary<int, List<string[]>> columnConfigurations, int rowOrdinal)
	{
		StringBuilder sbCell = new StringBuilder();
		try
		{
			int cellCount = dr.ItemArray.Length;
			bool isCellEditable;
			object CellValue;
			string sCellValue;
			string editableCellAtributes;
			Random r = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
			string idName;
			int randomId = r.Next();

			//Por cada columna de la fila
			for (int i = 1; i <= cellCount; i++)
			{
				//Verifica se es editable(si hay columnas editables, verifica que la actual este dentro de ellas
				isCellEditable = editablecolumns != null && editablecolumns.Contains(i.ToString());
				//Obtengo el valor de la columna
				CellValue = dr.ItemArray[i -1];

				if (CellValue is DateTime)
				{
					sCellValue = CellValue.ToString();
					if (sCellValue.Length > 10)
						sCellValue = sCellValue.Substring(0, 10);
				}
				else
					sCellValue = HttpUtility.HtmlEncode(CellValue.ToString());

				if (isCellEditable)
				{
					editableCellAtributes = string.Empty;
					if (columnConfigurations != null && columnConfigurations.ContainsKey(i))
					{
						editableCellAtributes = GetAttributesString(columnConfigurations[i]);
					}

					//Armo el id del input (numero random-posicion de fila-posicion de columna)
					idName = string.Format(HTML_INPUT_TABLE_ID_TEMPLATE, randomId.ToString(), rowOrdinal, i.ToString());
					sbCell.AppendFormat(HTML_EDITABLE_CELL, sCellValue, editableCellAtributes, idName, idName);
				}
				else
				{
					sbCell.AppendFormat(HTML_TD_FORMAT, sCellValue);
				}
			}

			return sbCell.ToString();
		}
		finally
		{
			if (sbCell != null)
			{
				sbCell.Length = 0;
				sbCell.Capacity = 0;
			}
		}
	}

	/// <summary>
	/// Carga las columnas header de un DataTable en una tabla Html del documento
	/// para variables zvar
	/// </summary>
	/// <param name="table"></param>
	/// <param name="dcs"></param>
	/// <remarks></remarks>
	public static string LoadZVarTableHeader(String table, DataColumnCollection dcs, String body)
	{
		StringBuilder sb = new StringBuilder();

		try
		{
			

			if (string.IsNullOrEmpty(body.Trim()) == false && body.ToLower().Contains("hideopentask") == false)
			{
                sb.AppendLine("<th class=\"ZGridCellHeader\"></th>");
			}

			//Agrego columnas de atributos
			foreach (DataColumn Column in dcs)
			{
				sb.AppendFormat(HTML_TH_FORMAT, Column.ColumnName);
			}

			return table + string.Format(HTML_TR_FORMAT, sb.ToString());
		}
		finally
		{
			if (sb != null)
			{
				sb.Length = 0;
				sb.Capacity = 0;
				sb = null;
			}
		}
	}

	#endregion

	#region AutoComplete
    /// <summary>
    /// Devuelve una lista de key|value para completar las opciones de un autocomplete
    /// </summary>
    /// <param name="query"></param>
    /// <param name="dataSourceName"></param>
    /// <param name="displayMember"></param>
    /// <param name="valueMember"></param>
    /// <param name="additionalFilters"></param>
    /// <returns></returns>
    public List<KeyValuePair<string, string>> GetAutoCompleteOptions(string query,
																		string dataSourceName, string displayMember, string valueMember, string additionalFilters)
	{
		try
		{
            List<KeyValuePair<string, string>> aOp;

			if (string.IsNullOrEmpty(dataSourceName))
				throw new Exception("No se encuentra nombre de fuente de datos");

            //agrego a los filtros adicionales el valor del ingresado en el textbox
            if (additionalFilters.Length > 0)
                additionalFilters = additionalFilters.Insert(0, query + "|");
            else
                additionalFilters = query;

			object dataSource;
			dataSource = FillSource(dataSourceName, additionalFilters);

            //Llamo al parse del source a key|value
            aOp = ParseSourceToDatakey(dataSource, valueMember, displayMember);

			return aOp;
		}
		catch (Exception ex)
		{
			ZClass.raiseerror(ex);
			throw;
		}
	}

    /// <summary>
    /// Convierte el source en una lista de key|value, la key será el displayMember, y el value será el valueMember
    /// </summary>
    /// <param name="dataSource"></param>
    /// <param name="valueMember"></param>
    /// <param name="displayMember"></param>
    /// <returns></returns>
    private List<KeyValuePair<string, string>> ParseSourceToDatakey(object dataSource, string valueMember, string displayMember)
    {
        List<KeyValuePair<string, string>> toReturn = new List<KeyValuePair<string, string>>();

        //Intenta obtener el source. Tipos aceptados: DataTable o DataSet
        DataTable dtSource = dataSource as DataTable;
        if (dtSource == null)
        {
            DataSet ds = dataSource as DataSet;
            if (ds != null)
            {
                dtSource = ds.Tables[0];
            }
            ds = null;
        }

        KeyValuePair<string, string> kp;

        //Si hay source itera por las filas del source para irlo parseando.
        if (dtSource != null)
        {
            foreach (DataRow item in dtSource.Rows)
            {
                kp = new KeyValuePair<string,string>(item[displayMember].ToString(),item[valueMember].ToString());
                toReturn.Add(kp);
            }
        }
        else
            throw new Exception("Tipo de variable no reconocido");

        dtSource = null;

        return toReturn;
    }

	#endregion
}