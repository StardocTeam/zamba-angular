#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PrintControl;
using Zamba.AppBlock;
using Zamba.Core;
using Zamba.Filters;

using Zamba.Grid.Properties;
using Zamba.Indexs;
using Zamba.Office;
using Zamba.Print;
using Process = System.Diagnostics.Process;

#endregion

namespace Zamba.Grid.Grid
{
    public delegate void GroupGridClickEventHandler(object sender, EventArgs e);

    public delegate void GroupGridRowClickEventHandler(object sender, EventArgs e);

    public delegate void GroupDoubleGridClickEventHandler(object sender, EventArgs e);
    public delegate void GroupRefreshEventHandler();

    public delegate void SelectAllClick(object sender, EventArgs e);

    public delegate void DeselectAllClick(object sender, EventArgs e);

    public delegate void UpdateDs();

    public delegate void GroupGridRightClick(object sender, EventArgs e);

    public sealed partial class GroupGrid : UserControl, IDataSource
    {
        #region Properties, Constants and Variables
        private const string MsgRemoveFilter = "Ha ocurrido un error al remover los filtros.";
        private const string MsgAddFilter = "Ha ocurrido un error al agregar el filtro.";
        private const string MsgTitle = "Zamba Software";
        private const string RegText = "Registro: ";
        private delegate void ChangeCursorDelegate(Cursor cur);

        //Listado de atributos
        public Hashtable LoadedIndexs;
        private DataTable _dTable;
        private Boolean _alwaysFit;

        ///If the grid is ordered by group = true; else = false
        private Boolean _bolSorted;

        private DataTable _dataTable;
        private ListSortDirection _direction = ListSortDirection.Descending;
        private DataTable _originalDataTable;
        private Int32 _prevOrderedIndex;

        //Formato de las fechas de la grilla
        private Boolean _shortDateFormat;
        private Boolean _withExcel;
        private Boolean _showRefresh;

        /// <summary>
        ///   Especifica el formato de la fecha
        /// </summary>
        public Boolean ShortDateFormat
        {
            get { return _shortDateFormat; }
            set { _shortDateFormat = value; }
        }

        //Grilla siempre autoajustada

        public Boolean AlwaysFit
        {
            get { return _alwaysFit; }
            set
            {
                _alwaysFit = value;
                //Oculto el boton porque la grilla esta siempre ajustada
                btnFit.Visible = !value;
            }
        }

        /// <summary>
        /// Obtiene el DocTypeId de los documentos de la grilla mediante el valor
        /// seleccionado del combo de tipos de documento.
        /// En caso de que se seleccione la opción de Todos, devuelve cero.
        /// </summary>
        private Int64 DocTypeId
        {
            get
            {
                if (cmbDocType.ComboBox == null || cmbDocType.ComboBox.SelectedValue == null)
                    if (String.IsNullOrEmpty(_oldDocTypeId))
                        return 0;
                    else
                        return Int64.Parse(_oldDocTypeId);
                return Int64.Parse(cmbDocType.ComboBox.SelectedValue.ToString());
            }
        }

        //Si se usa zamba, el comportamiento de la grilla sera distinto
        public Boolean UseZamba
        {
            private get
            {
                if (outlookGrid1 != null)
                {
                    return outlookGrid1.UseZamba;
                }
                return false;
            }
            set
            {
                if (outlookGrid1 != null)
                    outlookGrid1.UseZamba = value;

                btnGraphic.Visible =
                    Convert.ToBoolean(UserPreferences.getValue("ShowToolBarGraphicButton", Sections.UserPreferences,
                                                               "true"));
                btnPrint.Visible =
                    Convert.ToBoolean(UserPreferences.getValue("ShowToolBarPrintTaskGrid", Sections.UserPreferences,
                                                               "true"));

                //Se comenta este bloque de código ya que se decidió mostrar  
                //los filtros solamente cuando tiene datos cargados.
                //
                //if (Convert.ToBoolean(UserPreferences.getValue("ShowFilterGridDefaultValue", Sections.UserPreferences, "true")))
                //{
                //    this.btnHideFilter.Visible = true;
                //    this.btnHideFilter.Text = "Ocultar Filtros";
                //}
                //else
                //{
                //    this.btnHideFilter.Visible = false;
                //    this.btnHideFilter.Text = "Mostrar Filtros";
                //}
            }
        }

        //Especifica como se ordena la grilla
        public Boolean GridSort
        {
            set
            {
                if (outlookGrid1 != null)
                {
                    outlookGrid1.gridSort = value;
                }
            }
        }

        //Datos de la grilla

        public DataTable DataTable
        {
            get { return _dataTable; }
            set
            {
                _dataTable = value;
                OriginalDataTable = value;
            }
        }

        //Datos de la grilla
        public object DataSourcePage
        {
            set
            {
                if (value is IListSource)
                {
                    InitializeLogic((DataTable)value, false);
                    if (UseZamba == false)
                    {
                        outlookGrid1.ClearGroups();
                        outlookGrid1.FillGrid(null);
                    }
                    else
                        outlookGrid1.Fill();
                }
                else if (value == null)
                {
                    InitializeLogic(new DataTable(), false);
                }
                outlookGrid1.Refresh();
                _totalCount = _dataTable.MinimumCapacity;
                SetRegisterText(_totalCount, _dataTable.Rows.Count, -1);
            }
        }

        //Si se utiliza excel o no (para el exportar)

        public Boolean WithExcel
        {
            get { return _withExcel; }
            set
            {
                btnExportToExcel.Visible = value;
                _withExcel = value;
            }
        }

        public Boolean showRefreshButton
        {
            get { return _showRefresh; }
            set
            {
                btnRefresh.Visible = value;
                _showRefresh = value;
            }
        }

        //Si se utiliza o no color (para las tareas vencidas)
        public Boolean UseColor
        {
            get { return outlookGrid1.useColor; }
            set { outlookGrid1.useColor = value; }
        }

        //DataTable original

        public DataTable OriginalDataTable
        {
            set { _originalDataTable = value; }
        }


        //Grilla donde se muestran los datos
        public DataGridView OutLookGrid
        {
            get { return outlookGrid1; }
        }

        public object DataSource
        {
            get { return outlookGrid1.DataSource; }
            set
            {
                if (value is IListSource)
                {
                    InitializeLogic((DataTable)value, true);
                    if (UseZamba == false)
                    {
                        outlookGrid1.ClearGroups();
                        outlookGrid1.FillGrid(null);
                    }
                    else
                        outlookGrid1.Fill();
                }
                else if (value == null)
                {
                    InitializeLogic(new DataTable(), true);
                }
                outlookGrid1.Refresh();
                _totalCount = DataTable.MinimumCapacity;
                _pageSize = Int32.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100));



                SetRegisterText(_totalCount, _pageSize, -1);

            }
        }

        public void ShowGroupBtn(Boolean blnShow)
        {
            toolStripLabel1.Visible = blnShow;
            cmbColumn.Visible = blnShow;
            btnGroup.Visible = blnShow;
            BtnUngroup.Visible = blnShow;
            btnCollapseAll.Visible = blnShow;
            toolStripSeparator3.Visible = blnShow;
            btnExpandAll.Visible = blnShow;
        }

        #endregion

        #region Methods

        private String _oldDocTypeId = String.Empty;

        /// <summary>
        ///   Mostrar o no el boton de print
        /// </summary>
        /// <param name = "useIt"></param>
        public void ShowPrintButton(Boolean useIt)
        {
            btnPrint.Visible = useIt;
        }

        /// <summary>
        ///   Inicializo la grilla
        /// </summary>
        /// <param name = "datatable"></param>
        private void InitializeLogic(DataTable datatable, Boolean useOriginal)
        {
            try
            {
                bool loadCmbFilterColumn = false;
                //Modifico o no el original
                if (useOriginal)
                    DataTable = datatable;
                else
                    _dataTable = datatable;
                outlookGrid1.BindData(datatable, null, _shortDateFormat);
                if (_dataTable != null && _dataTable.Rows.Count != 0)
                {
                    if (_dataTable.Columns.Contains("Doctypeid"))
                    {
                        LoadCmbFilterColumn(_dataTable.Rows[0]["Doctypeid"].ToString());
                        loadCmbFilterColumn = true;
                    }
                    else if (_dataTable.Columns.Contains("DOC_TYPE_ID"))
                    {
                        if (_dataTable.Rows[0]["DOC_TYPE_ID"] is DBNull == false)
                            LoadCmbFilterColumn(_dataTable.Rows[0]["DOC_TYPE_ID"].ToString());
                        loadCmbFilterColumn = true;
                    }
                    //Desde Report Builder
                    if (!loadCmbFilterColumn)
                    {
                        LoadCmbFilterColumn(null);
                    }
                }
                if (null != _dataTable)
                    SetRegisterText(_totalCount, _pageSize, -1);
            }
            catch (Exception ex)
            {
                ZException.Log(ex, true);
            }
        }

        private void ChangeCursor(Cursor cur)
        {
            try
            {
                this.Cursor = cur;
            }
            catch(Exception ex)
            {
                ZException.Log(ex, true);
            }
        }

        /// <summary>
        ///   Agrupa
        /// </summary>
        /// <param name = "direction"></param>
        /// <param name = "column"></param>
        public void Group(DataGridViewColumn column)
        {
            try
            {
                if (column != null)
                {
                    //DIEGO
                    ////Le asigno el nombre de columna al combobox porque se puede llegar a agrupar por fuera
                    cmbColumn.Text = column.Name;
                    outlookGrid1.GroupTemplate.Column = column;
                    outlookGrid1.Sort(new DataRowComparer(column.Index, ListSortDirection.Ascending));

                    //Save the state
                    _bolSorted = true;

                    //Show the collapse and expandButtons
                    btnCollapseAll.Visible = true;
                    btnExpandAll.Visible = true;
                    SetRegisterText(_totalCount, _pageSize, -1);
                }
            }
            catch (Exception ex)
            {
                ZException.Log(ex, true);
            }
        }

        /// <summary>
        ///   Set the column as visible or not
        /// </summary>
        /// <param name = "name"></param>
        /// <param name = "bolVisible"></param>
        public void SetColumnVisible(string name, Boolean bolVisible)
        {
            try
            {
                if (outlookGrid1.Columns.Contains(name))
                {
                    outlookGrid1.Columns[name].Visible = bolVisible;
                }
                if (bolVisible == false)
                {
                    outlookGrid1.ColumnsVisible.Add(name);
                    if (cmbColumn.Items.Contains(name))
                    {
                        cmbColumn.Items.Remove(name);
                        cmbFilterColumn.Items.Remove(name);
                    }
                }
                else
                {
                    outlookGrid1.ColumnsVisible.Remove(name);
                    if (cmbColumn.Items.Contains(name) == false)
                        cmbColumn.Items.Add(name);
                }
            }
            catch (Exception ex)
            {
                ZException.Log(ex, true);
            }
        }

        /// <summary>
        ///   Set the column as fixed or not
        /// </summary>
        /// <param name = "name"></param>
        /// <param name = "bolVisible"></param>
        public void SetColumnFixed(string name, Boolean bolFixed, Int32 width)
        {
            try
            {
                outlookGrid1.ColumnsFixed.Remove(name);
                if (bolFixed && outlookGrid1.Columns.Contains(name))
                {
                    outlookGrid1.Columns[name].Width = width;
                    outlookGrid1.ColumnsFixed.Add(name, width);
                }
            }
            catch (Exception ex)
            {
                ZException.Log(ex, true);
            }
        }

        /// <summary>
        ///   Ajusta el ancho de las columnas
        /// </summary>
        /// <history> 
        ///   [Gaston]    12/08/2008   Modified    
        /// </history>
        public void FixColumns()
        {
            try
            {

                foreach (DataGridViewColumn col in outlookGrid1.Columns)
                {
                    if (outlookGrid1 != null)
                        if (outlookGrid1.ColumnsFixed[col.Name] != null)
                        {
                            outlookGrid1.Columns[col.Name].Width =
                                Int32.Parse(outlookGrid1.ColumnsFixed[col.Name].ToString());
                        }
                        else
                            outlookGrid1.AutoResizeColumn(col.Index);
                }

                btnSelectAll.Tag = "0";
                if (outlookGrid1 != null)
                {
                    outlookGrid1.Tag = "0";
                    btnSelectAll.Text = Resources.GroupGrid_FixColumns_Seleccionar_todos;
                }
            }
            catch (Exception ex)
            {
                ZException.Log(ex, true);
            }
        }

        /// <summary>
        ///   Show the count of registers
        ///   [Sebastian 27-07-2009] MODIFIED se modifico para que se viera siempre el total de filas visibles en la grilla
        ///   sin importar si esta o no filtrada6
        /// </summary>
        public void SetRegisterText(Int32 totalCount, Int32 pageSize, Int32 SelectedRowIndex)
        {
            try
            {
                if (outlookGrid1.Rows.Count > 0)
                    {
                        if (outlookGrid1.SelectedRows.Count > 0)
                        {

                            //[Sebastian 27-07-2009] se pidio que se vieran la cantidad de filas que corresponda segun este 
                            //la grilla o no filtrada la grilla.
                            if (null != _dataTable)
                                if (SelectedRowIndex != -1)
                                    lblRows.Text = RegText +
                                                   ((SelectedRowIndex + 1) + _gridController.LastPage*pageSize) + " de " +
                                                   _dataTable.MinimumCapacity;
                                else
                                    lblRows.Text = RegText + (GetTextRowIndex() + _gridController.LastPage*pageSize) +
                                                   " de " + _dataTable.MinimumCapacity;
                            else 
                                if (SelectedRowIndex != -1)
                                    lblRows.Text = RegText + ((SelectedRowIndex + 1) + _gridController.LastPage*pageSize) +
                                               " de " + _dataTable.MinimumCapacity;
                                else
                                    lblRows.Text = RegText + (GetTextRowIndex() + _gridController.LastPage*pageSize) +
                                               " de " + ((DataTable) outlookGrid1.DataSource).Rows.Count;
                        }
                        else
                            //Se cambio para mostrara el total de tareas de la etapa y no de la pagina que
                            //visualizando actualmente [Sebastian 23/12/2008]
                            if (null != _originalDataTable)
                                if (lblRows.Text == string.Empty)
                                    lblRows.Text = RegText + (1 + _gridController.LastPage*pageSize) + "de" +
                                                   _originalDataTable.Rows.Count;
                                else if (lblRows.Text == string.Empty)
                                    lblRows.Text = RegText + (1 + _gridController.LastPage*pageSize) + "de" +
                                                   ((DataTable) outlookGrid1.DataSource).Rows.Count;

                    }
                    else
                        lblRows.Text = "Registro: 1 de 0";


                    SetPaging(totalCount, pageSize);

                }

            
            catch (Exception ex)
            {
                ZException.Log(ex, true);
            }
        }


        /// <summary>
        ///   Get the row index by counting the non group rows
        /// </summary>
        /// <returns></returns>
        private Int32 GetTextRowIndex()
        {
            Int32 index = 0;
            foreach (OutlookGridRow row in outlookGrid1.Rows)
            {
                if (row.Index == outlookGrid1.SelectedRows[0].Index)
                {
                    if (row.IsGroupRow == false)
                        return index + 1;
                    return 0;
                }
                if (row.IsGroupRow == false)
                    index++;
            }
            return 0;
        }

        #region Filters

        /// <summary>
        ///   Cargo el combo de filtros
        /// </summary>
        public void LoadCmbFilterColumn(String docTypeId)
        {
            if (docTypeId == null || docTypeId != _oldDocTypeId)
            {
                _oldDocTypeId = docTypeId;

                var dt = new DataTable();
                dt.Columns.Add("Index");
                dt.Columns.Add("IndexName");

                if (cmbFilterColumn != null && cmbColumn != null)
                {
                    {
                        cmbFilterColumn.Items.Clear();
                        cmbColumn.Items.Clear();

                        var imagenUserConfig = UserPreferences.getValue("ColumnNameImagen", Sections.UserPreferences, "Imagen");
                        var verUserConfig = UserPreferences.getValue("ColumnNameVer", Sections.UserPreferences, "Ver");
                        var situacionUserConfig = UserPreferences.getValue("ColumnNameSituacion", Sections.UserPreferences,
                                                               "Situacion");

                        //Si hay algun indice de Zamba cargado en la grilla
                        if (LoadedIndexs != null && LoadedIndexs.Count > 0)
                        {
                            foreach (DictionaryEntry item in LoadedIndexs)
                            {
                                //DataRow Row = dt.NewRow();

                                //Row["Index"] = item.Key;
                                //Row["IndexName"] = item.Value;
                                cmbFilterColumn.Items.Add(item.Value);
                                //dt.Rows.Add(Row);
                            }
                            //cmbFilterColumn.SuspendLayout();
                            //cmbFilterColumn.ValueMember = "Index";
                            //cmbFilterColumn.DisplayMember = "IndexName";
                            //cmbFilterColumn.DataSource = dt;
                            //cmbFilterColumn.ResumeLayout();

                            //Cargo las demas columnas de la grilla
                            foreach (DataGridViewColumn c in outlookGrid1.Columns)
                            {
                                try
                                {
                                    if (
                                        String.Compare(c.CellType.FullName, "System.Windows.Forms.DataGridViewImageCell") !=
                                        0 && c.Visible && c.Name != imagenUserConfig && c.Name != verUserConfig)
                                    {
                                        cmbColumn.Items.Add(c.Name);
                                        if (LoadedIndexs.ContainsValue(c.Name) == false)
                                            cmbFilterColumn.Items.Add(c.Name);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ZClass.raiseerror(ex);
                                }
                            }
                        }
                        else
                        {
                            foreach (DataGridViewColumn c in outlookGrid1.Columns)
                            {
                                if (String.Compare(c.CellType.FullName, "System.Windows.Forms.DataGridViewImageCell") !=
                                    0 && c.Visible && c.Name != imagenUserConfig && c.Name != verUserConfig)
                                {
                                    var dtable = (DataTable)outlookGrid1.DataSource;
                                    if (dtable.Columns[c.Name].DataType.ToString() == "System.Drawing.Image")
                                    {
                                        continue;
                                    }
                                    cmbFilterColumn.Items.Add(c.Name);
                                    cmbColumn.Items.Add(c.Name);
                                }
                            }
                        }
                    }
                    String lastFilterIndexUsed = UserPreferences.getValue("LastFilterIndexUsed", Sections.Filters,
                                                                          String.Empty);
                    if (lastFilterIndexUsed != String.Empty && cmbFilterColumn.Items.Contains(lastFilterIndexUsed))
                    {
                        cmbOperator.SelectedIndex = 8;
                        cmbFilterColumn.SelectedIndex = cmbFilterColumn.Items.IndexOf(lastFilterIndexUsed);
                        LoadFilterDataIndexControl();
                    }
                    else
                    {
                        cmbFilterColumn.SelectedIndex = -1;
                    }
                }
            }

            cmbOperator.MaxDropDownItems = cmbOperator.Items.Count;
        }

        private FilterElem GetFilter(String key)
        {
            foreach (FilterElem I in lsvFilters.Items)
            {
                if (String.Compare(I.Text, key) == 0)
                {
                    return I;
                }
            }
            return null;
        }

        /// <summary>
        ///   Método que sirve para quitar todos los filtros
        /// </summary>
        /// <history> 
        ///   [Gaston]    16/10/2008   Created
        ///   [Gaston]    20/10/2008   Modified    Eliminación del elemento utilizando un listview
        ///   [Tomas]     31/03/2009   Modified    Se eliminan todos los elementos y luego se actualiza automáticamente.
        ///   [sebastian] 03-04-2009   Modified    se aplico el permiso para borrar los filtros
        ///   [Marcelo]   30/04/2008   Modified    Se modifico la aplicacion de los permisos, ya que la misma no funcionaba
        ///   [Sebastian] 25-09-09     Modified    clear de panel witch contains index value to filter grid.
        /// </history>
        private void BtnRemoveClick(object sender, EventArgs e)
        {
            RemoveAllFilters();
        }

        /// <summary>
        ///   Quita todos los filtros manuales
        /// </summary>
        private bool RemoveManualFilters()
        {
            var filtersToRemove = new ArrayList();
            System.Collections.Generic.List<Int64> docTypeIds = new System.Collections.Generic.List<long>();

            try
            {
                //Se recorren los filtros existentes buscando los manuales.
                foreach (FilterElem item in lsvFilters.Items)
                {
                    if (string.Compare(item.Type, "manual") == 0 || string.Compare(item.Type, "search") == 0)
                    {
                        filtersToRemove.Add(item);
                        docTypeIds.Add(item.DocTypeId);
                    }
                }

                //Si existen manuales, se procede a removerlos.
                if (filtersToRemove.Count > 0)
                {
                    //Se los elimina de cache y de la base.
                    _gridController.Fc.ClearFilters(DocTypeId, UserBusiness.CurrentUser().ID, !flagsearch, DataTable, UserBusiness.CanRemoveDefaultFilters(docTypeIds));
                    //Se los elimina visualmente del cliente.
                    foreach (FilterElem item in filtersToRemove)
                    {
                        lsvFilters.Items.Remove(item);
                    }

                    //Se refresca la grilla.
                    _gridController.ShowTaskOfDT();

                    //True, en caso de haber removido los filtros.
                    return true;
                }
                else
                {
                    //False, en caso de no haber removido los filtros.
                    return false;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show(MsgRemoveFilter, MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                filtersToRemove.Clear();
                filtersToRemove = null;
            }
        }

        /// <summary>
        ///   Quita todos los filtros
        /// </summary>
        private void RemoveAllFilters()
        {
            if (lsvFilters.Items.Count > 0)
            {
                System.Collections.Generic.List<Int64> docTypeIds = new System.Collections.Generic.List<long>();
                bool removeDefaultFilters;
                bool hasDefaultFilters = false;

                //Se recorren los filtros existentes obteniendo el docTypeId al que afecta
                //y verificando si alguno de ellos es un filtro por defecto.
                foreach (FilterElem fe in lsvFilters.Items)
                {
                    docTypeIds.Add(fe.DocTypeId);
                    if (string.Compare(fe.Type, "defecto") == 0)
                    {
                        hasDefaultFilters = true;
                    }
                }

                //Se verifica si el usuario tiene permisos para remover filtros
                removeDefaultFilters = UserBusiness.CanRemoveDefaultFilters(docTypeIds);
                
                //Si el usuario tiene permiso de quitar los filtros por defecto
                if (removeDefaultFilters || !hasDefaultFilters)
                {
                    Boolean flagAnyEnabled = false;
                    foreach (FilterElem fe in lsvFilters.Items)
                    {
                        if (!fe.Enabled) continue;
                        flagAnyEnabled = true;
                        break;
                    }

                    //Se remueven los filtros de la base y de cache.
                    _gridController.Fc.ClearFilters(DocTypeId, _currentUserId, !flagsearch, DataTable, removeDefaultFilters);

                    //Se remueven los filtros visualmente.
                    lsvFilters.Items.Clear();

                    //Se refresca la grilla.
                    if (flagAnyEnabled)
                        _gridController.ShowTaskOfDT();
                }
                else
                {
                    //Si hay filtros por defecto y no pueden ser quitados, le aviso al usuario
                    if (RemoveManualFilters())
                    {
                        MessageBox.Show(Resources.GroupGrid_BtnUncheckAllFiltersClick_No_tiene_permiso_para_remover_los_filtros_por_defecto__se_quitaran_solo_los_filtros_agregados_manualmente, Resources.GroupGrid_BtnUncheckAllFiltersClick_ATENCION);
                    }
                    else
                    {
                        MessageBox.Show(Resources.GroupGrid_RemoveSelectedFilter_No_tiene_permiso_para_remover_los_filtros_por_defecto, Resources.GroupGrid_BtnUncheckAllFiltersClick_ATENCION);
                    }
                }
            }
        }


        /// <summary>
        ///   Show the filters panel
        /// </summary>
        private void ShowFilters()
        {
            splitGrid.Panel1Collapsed = false;
            btnHideFilter.Text = Resources.GroupGrid_ShowFilters_Ocultar_Filtros;
        }

        /// <summary>
        ///   Hide the filters panel
        /// </summary>
        private void HideFilters()
        {
            splitGrid.Panel1Collapsed = true;
            btnHideFilter.Text = Resources.GroupGrid_HideFilters_Mostrar_Filtros;
        }

        /// <summary>
        ///   Hide the filters panel and deselect the button
        /// </summary>
        public void HideFiltersAtStart()
        {
            HideFilters();
            btnHideFilter.Checked = false;
        }

        /// <summary>
        ///   Show the filters panel and deselect the button
        /// </summary>
        private void ShowFiltersAtStart()
        {
            ShowFilters();
            btnHideFilter.Checked = false;
        }

        /// <summary>
        ///   Checks if there are filters loaded.
        /// </summary>
        private bool IsFiltered()
        {
            if (lsvFilters.Items.Count > 0)
                return true;
            return false;
        }


        /// <summary>
        ///   Muestra el panel de filtros si es que hay valores filtrados.
        /// </summary>
        private void ShowFiltersIfExists()
        {
            if (IsFiltered())
            {
                ShowFiltersAtStart();
                btnHideFilter.Checked = true;
            }
            else
                HideFiltersAtStart();
            //this.SetVisibilityLogicalOperatorsControls();
        }


        /// <summary>
        ///   Devuelve si hay algun filtro por defecto
        /// </summary>
        /// <returns></returns>
        private bool HasDefaultFilters()
        {
            foreach (FilterElem item in lsvFilters.Items)
            {
                if (string.Compare(item.Type, "defecto") == 0)
                {
                    return true;
                }
            }
            return false;
        }

     

        /// <summary>
        ///   Remueve el filtro seleccionado y actualiza.
        /// </summary>
        /// <history> 
        ///   [Tomas]     31/03/2009  Created
        ///   [sebastian] 03-04-2009 se aplico el permiso para borrar filtros
        ///   Marcelo     30/04/2009  Modified    Se modifico la validacion de los permisos porque no funcionaba
        ///   Sebastian  01-10-09 Modified reload filters after remove one
        /// </history>
        private void RemoveSelectedFilter()
        {
            if (lsvFilters.SelectedItems.Count > 0)
            {
                FilterElem CurrentFilter = (FilterElem)lsvFilters.SelectedItem;

                if (string.Compare(CurrentFilter.Type, "defecto") == 0)
                {
                    if (UserBusiness.CanRemoveDefaultFilter(CurrentFilter.DocTypeId) == false)
                    {
                        MessageBox.Show(
                            Resources.GroupGrid_RemoveSelectedFilter_No_tiene_permiso_para_remover_los_filtros_por_defecto,
                            Resources.GroupGrid_BtnUncheckAllFiltersClick_ATENCION);
                        return;
                    }
                }

                //Remuevo el filtro seleccionado
                _gridController.Fc.RemoveFilter(CurrentFilter, !flagsearch);
                if (lsvFilters.SelectedItems.Count > 0)
                    lsvFilters.Items.Remove(lsvFilters.SelectedItem);

                //Si la grilla esta dentro del la grilla c/ paginado
                if ((CurrentFilter).Enabled == true)
                _gridController.ShowTaskOfDT();
            }
        }



        /// <summary>
        ///   Método que sirve para limpiar el combobox "Filtros" en caso de que se pase a una nueva etapa
        /// </summary>
        /// <history> 
        ///   [Gaston]    17/10/2008   Created
        /// </history>
        public void ClearFilters()
        {
            lsvFilters.Items.Clear();
        }

        #endregion

        #endregion

        #region Events

        private GroupGridClickEventHandler _dClickEvent;
        private DeselectAllClick _dDeselectAllClickEvent;
        private GroupDoubleGridClickEventHandler _dDoubleClickEvent;
        private GroupRefreshEventHandler _dRefreshEvent;
        private GroupGridRightClick _dRightClick;
        private GroupGridRowClickEventHandler _dRowClickEvent;
        private SelectAllClick _dSelectAllClickEvent;
        private UpdateDs _updateDataSource;

        /// <summary>
        ///   Group by the selectedItem on the cmbColumn
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        private void BtnGroupClick(object sender, EventArgs e)
        {
            try
            {
                if (cmbColumn.SelectedIndex >= 0)
                {
                    Group(outlookGrid1.Columns[cmbColumn.Text]);
                }
            }
            catch (Exception ex)
            {
                ZException.Log(ex, true);
            }
        }

        //Sets the grid visual properties
        private void MenuSkinOutlookClick()
        {
            try
            {
                //[Pablo] Se comentan las siguientes lineas para que se pueda
                //setear el ancho de las columnas desde el user Config

                //this.outlookGrid1.AutoSizeColumnsMode =
                //    System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;

                var dataGridViewCellStyle2 = new DataGridViewCellStyle();
                dataGridViewCellStyle2.BackColor = SystemColors.Window;
                dataGridViewCellStyle2.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((0)));
                dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
                dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
                dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
                outlookGrid1.DefaultCellStyle = dataGridViewCellStyle2;
                outlookGrid1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;

                var dataGridViewCellStyle3 = new DataGridViewCellStyle();
                dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridViewCellStyle3.BackColor = SystemColors.Desktop;
                dataGridViewCellStyle3.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((0)));
                dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
                dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
                dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
                outlookGrid1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;

                outlookGrid1.GridColor = SystemColors.Control;
                outlookGrid1.RowTemplate.Height = 19;
                outlookGrid1.BackgroundColor = SystemColors.Window;
                outlookGrid1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                outlookGrid1.RowHeadersVisible = false;
                outlookGrid1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                outlookGrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                outlookGrid1.EditMode = DataGridViewEditMode.EditProgrammatically;
                outlookGrid1.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((0)));

                if (_alwaysFit)
                {
                    FixColumns();
                }

                if (UseZamba == false)
                {
                    outlookGrid1.ClearGroups(); // reset
                    outlookGrid1.FillGrid(null);
                }
                else
                    outlookGrid1.Fill();

                SetRegisterText(_totalCount, _pageSize, -1);
            }
            catch (Exception ex)
            {
                ZException.Log(ex, true);
            }
        }

        /// <summary>
        ///   Order the column by Descending or Ascending way
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        /// <history> 
        ///   [Gaston]    11/11/2008   Modified    Actualización de DataSource sólo si se distribuyeron tareas
        /// </history>
        private void OutlookGrid1ColumnHeaderMouseClick(Object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_updateDataSource != null)
            {
                // Se ejecuta el evento que hace que se llame a UCTaskGrid. Si si distribuyeron tareas entonces se debe actualizar el DataSource para
                // obtener el datatable actualizado (que no contiene las tareas que se distribuyeron). Esto es para mantener el orden de las filas y que
                // no se muestren las tareas que se distribuyeron. La actualización sólo se realiza una vez
                _updateDataSource();
            }

            try
            {
                //If there arent any columns exit
                if (e.ColumnIndex < 0)
                    return;

                //If the column is doublecliked again order by Descending else Ascending
                if (e.ColumnIndex == _prevOrderedIndex)
                {
                    if (_direction == ListSortDirection.Ascending)
                        _direction = ListSortDirection.Descending;
                    else
                        _direction = ListSortDirection.Ascending;
                }
                else
                {
                    _direction = ListSortDirection.Ascending;
                }

                //If is not ordered by groups
                if (UseZamba)
                {
                    var dv = new DataView(_dataTable);
                    if (outlookGrid1.GroupTemplate.Column == null || string.Compare(outlookGrid1.GroupTemplate.Column.Name, string.Empty) == 0)
                        outlookGrid1.GroupTemplate.Column = outlookGrid1.Columns["FolderId"];
                    if (_direction == ListSortDirection.Ascending)
                    {
                        dv.Sort = "[" + outlookGrid1.Columns[e.ColumnIndex].Name + "]";
                        // , [" + outlookGrid1.GroupTemplate.Column.Name + "]";
                    }
                    else
                        dv.Sort = "[" + outlookGrid1.Columns[e.ColumnIndex].Name + "] DESC";
                    //, [" + outlookGrid1.GroupTemplate.Column.Name + "] DESC";

                    //Asign the Datatable to the Grid
                    outlookGrid1.BindData(dv.ToTable(), null, _shortDateFormat);
                    outlookGrid1.Fill();
                }
                else
                {
                    if (_bolSorted == false)
                    {
                        outlookGrid1.OnlySort(new DataRowComparer(e.ColumnIndex, _direction));
                        MenuSkinOutlookClick();
                    }
                    else
                    {
                        outlookGrid1.GroupTemplate.Column = outlookGrid1.Columns[cmbColumn.SelectedIndex];
                        outlookGrid1.Sort(new DataRowComparer(e.ColumnIndex, _direction));
                    }
                }
                //Save column Index
                _prevOrderedIndex = e.ColumnIndex;
                //Se agregó esta llamada porque en toda la llamada del evento, se perdia
                //el seteo de las medidas de las columnas. [Alejandro]
                if (_alwaysFit)
                    FixColumns();
            }
            catch (Exception ex)
            {
                ZException.Log(ex, true);
            }
        }

        /// <summary>
        ///   Collapse All Groups
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        private void BtnCollapseAllClick(object sender, EventArgs e)
        {
            outlookGrid1.CollapseAll();
        }

        /// <summary>
        ///   Expand all the groups
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        private void BtnExpandAllClick(object sender, EventArgs e)
        {
            outlookGrid1.ExpandAll();
        }

        /// <summary>
        /// Visualiza los filtros cargados.
        /// </summary>
        /// <param name = "DocTypes"></param>
        public void ReloadLsvFilters()
        {

            ClearFilters();
            
            if (_gridController.Fc.GetDocumentFiltersCount(DocTypeId, !flagsearch) > 0)
            {
                //Se remueven los filtros cargados.

                //Se agregan los filtros.
                lsvFilters.ItemCheck -= LsvFiltersItemCheck;
                foreach (var fe in _gridController.Fc.GetLastUsedFilters(DocTypeId, _currentUserId, !flagsearch))
                {
                    lsvFilters.Items.Add(fe, fe.Enabled);
                }
                lsvFilters.ItemCheck += LsvFiltersItemCheck;
                btnRemoveAllFilters.Enabled = true;
            }

            ShowFiltersIfExists();
        }


        /// <summary>
        ///   [Sebastian] 07-10-09 CREATED  It removes column that don't visible in outlookgrid.
        /// </summary>
        /// <param name = "dt"></param>
        /// <returns></returns>
        private DataTable SetColumnVisibleOnDt(DataTable dt)
        {
            var filteredDt = dt.Copy();

            foreach (DataColumn column in dt.Columns)
            {
                if (outlookGrid1 != null)
                    if (outlookGrid1.Columns.Count > 0 && outlookGrid1.Columns.Contains(column.ColumnName))
                        if (outlookGrid1.Columns[column.ColumnName].Visible == false)
                        {
                            filteredDt.Columns.Remove(column.ColumnName);
                        }
            }

            return filteredDt;
        }

        /// <summary>
        ///   Export Grid To Excel with the visible columns
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        private void BtnExportToExcelClick(object sender, EventArgs e)
        {
            try
            {
                if (outlookGrid1.DataSource != null)
                {
                    if (((DataTable)outlookGrid1.DataSource).Rows.Count > 0)
                    {
                        var sfDialog = new SaveFileDialog();
                        //Export to Excel
                        try
                        {
                            sfDialog.RestoreDirectory = true;
                            sfDialog.ValidateNames = true;
                            sfDialog.OverwritePrompt = true;
                            sfDialog.Filter = Resources.STRING_Microsoft_Office_Excel___xls_Todos_los_Archivos____;
                            if (sfDialog.ShowDialog() == DialogResult.OK)
                            {
                                var name = sfDialog.FileName;
                                sfDialog.Dispose();
                                sfDialog = null;
                                var dt = SetColumnVisibleOnDt((DataTable)outlookGrid1.DataSource);
                                var forceCulture =
                                    Boolean.Parse(UserPreferences.getValue("ForceCultureToEnglish",
                                                                           Sections.UserPreferences, "False"));

                                if (
                                    MessageBox.Show(Resources.STRING_Desea_ver_el_archivo_al_finalizar, Resources.STRING_Exportar_a_Excel,
                                                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    MessageBox.Show(Resources.STRING_Se_ha_comenzado_a_exportar, Resources.STRING_Exportar_a_Excel,
                                                    MessageBoxButtons.OK);
                                    ExcelInterop.DataTableDirectlyToExcel(dt, name, ExcelInterop.eEstilosHoja.Reporte2,
                                                                          true, forceCulture);
                                }
                                else
                                {
                                    MessageBox.Show(Resources.STRING_Se_ha_comenzado_a_exportar, Resources.STRING_Exportar_a_Excel,
                                                    MessageBoxButtons.OK);

                                    if (ExcelInterop.DataTableDirectlyToExcel(dt, name,
                                                                              ExcelInterop.eEstilosHoja.Reporte2, false,
                                                                              forceCulture))
                                        MessageBox.Show(Resources.STRING_Se_ha_exportado_con_exito_a_Excel, Resources.STRING_Exportar_a_Excel);
                                    else
                                        MessageBox.Show(Resources.STRING_No_se_ha_podido_exportar_con_exito_a_Excel,
                                                        Resources.STRING_Exportar_a_Excel);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Resources.STRING_No_se_pudo_exportar_a_Excel, Resources.STRING_Exportar_Grilla_a_Excel);
                            ZException.Log(ex, true);
                            return;
                        }
                    }
                    else
                        MessageBox.Show(Resources.STRING_No_hay_datos_para_exportar, Resources.STRING_Impresion);
                }
            }
            catch (Exception ex)
            {
                ZException.Log(ex, true);
            }
        }

        /// <summary>
        ///   Print Grid
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        private void BtnPrintClick(object sender, EventArgs e)
        {
            try
            {
                this.Invoke(new ChangeCursorDelegate(ChangeCursor), new Object[] { Cursors.WaitCursor });

                if (outlookGrid1.DataSource != null)
                {
                    if (((DataTable)outlookGrid1.DataSource).Rows.Count > 0)
                    {
                        var mPrint = new ControlPrint(outlookGrid1);
                        var d = new PageSetupDialog { Document = mPrint };
                        if (d.ShowDialog().Equals(DialogResult.OK))
                        {
                            mPrint.PrinterSettings = d.PrinterSettings;
                            mPrint.Cols = GetColsToPrint(outlookGrid1, d.PageSettings.PrintableArea.Width);
                            mPrint.Rows = GetRowsToPrint(outlookGrid1, d.PageSettings.Bounds.Height - d.PageSettings.Margins.Top - d.PageSettings.Margins.Bottom);
                            var printPreviewDialog1 = new PrintPreviewDialog { Document = mPrint };
                            printPreviewDialog1.ShowDialog(this);
                        }
                        return;
                    }
                }
                MessageBox.Show("No hay datos para imprimir", "Impresion");
            }
            catch (Exception ex)
            {
                ZException.Log(ex, true);
            }
            finally
            {
                this.Invoke(new ChangeCursorDelegate(ChangeCursor), new Object[] { Cursors.Default });
            }
        }

        private ArrayList GetColsToPrint(OutlookGrid Grid, float MaxWidth)
        {
            ArrayList ColsWidth = new ArrayList();

            if (Grid == null || Grid.Columns.Count.Equals(0))
                return null;

            int ColWidth = Grid.Columns[0].DividerWidth;

            foreach (DataGridViewColumn column in Grid.Columns)
            {
                if (column.Visible)
                {
                    string a = column.HeaderText;
                    if ((ColWidth + column.Width + column.DividerWidth) <= (int)MaxWidth)
                        ColWidth += column.Width + column.DividerWidth;
                    else
                    {
                        ColsWidth.Add(ColWidth);
                        ColWidth = column.Width + column.DividerWidth;
                    }
                }
            }

            ColsWidth.Add(ColWidth);

            return ColsWidth;
        }

        private ArrayList GetRowsToPrint(OutlookGrid Grid, float MaxHeight)
        {
            ArrayList RowsHeight = new ArrayList();

            if (Grid == null || Grid.Rows.Count.Equals(0))
                return null;

            int RowHeight = Grid.Rows[0].Height +5;

            foreach (DataGridViewRow row in Grid.Rows)
            {
                if (row.Visible)
                {
                    if ((RowHeight + row.Height + row.DividerHeight) <= (int)MaxHeight)
                        RowHeight += row.Height + row.DividerHeight;
                    else
                    {
                        RowsHeight.Add(RowHeight);
                        RowHeight = row.Height + row.DividerHeight;
                    }
                }
            }

            RowsHeight.Add(RowHeight);

            return RowsHeight;
        }

        private void OutlookGrid1CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (null != _dRowClickEvent)
                {
                    _dRowClickEvent(this, e);
                    if (e.RowIndex != 0)
                        SetRegisterText(_totalCount, _pageSize, -1);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void OutlookGrid1DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (null != _dDoubleClickEvent)
                {
                    _dDoubleClickEvent(this, e);
                    SetRegisterText(_totalCount, _pageSize, -1);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        public event GroupGridRowClickEventHandler OnRowClick
        {
            add { _dRowClickEvent += value; }
            remove { _dRowClickEvent -= value; }
        }

        public event GroupDoubleGridClickEventHandler OnDoubleClick
        {
            add { _dDoubleClickEvent += value; }
            remove { _dDoubleClickEvent -= value; }
        }

        public event GroupRefreshEventHandler OnRefreshClick
        {
            add { _dRefreshEvent += value; }
            remove { _dRefreshEvent -= value; }
        }

        public event GroupGridRightClick OnRightClick
        {
            add { _dRightClick += value; }
            remove { _dRightClick -= value; }
        }

        /// <summary>
        ///   Evento selectClick
        /// </summary>
        /// <history> 
        ///   [Gaston]    12/08/2008  Created
        /// </history>
        public event SelectAllClick SelectAllClick
        {
            add { _dSelectAllClickEvent += value; }
            remove { _dSelectAllClickEvent -= value; }
        }

        public event DeselectAllClick DeselectAllClick
        {
            add { _dDeselectAllClickEvent += value; }
            remove { _dDeselectAllClickEvent -= value; }
        }

        public event UpdateDs UpdateDs
        {
            add { _updateDataSource += value; }
            remove { _updateDataSource -= value; }
        }

        private void OutlookGrid1CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (null != _dDoubleClickEvent)
                {
                    _dDoubleClickEvent(sender, e);
                    SetRegisterText(_totalCount, _pageSize ,e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void TxtFitClick(object sender, EventArgs e)
        {
            FixColumns();
        }

        #endregion

        #region Form setup

        private readonly Int64 _currentUserId;
        private readonly IFilter _gridController;
        private Decimal _pagesCount;
        private int _totalCount;
        private int _pageSize;
        private bool flagsearch;

        public GroupGrid(Boolean useExcel, Int64 currentUserId, ref IFilter gridController)
        {
            try
            {
                _gridController = gridController;
                InitializeComponent();

                //Valido que el paginado con Selecteddropdownlistpage no se visualize desde resultados o desde ReportBuilder
                if (gridController == null)
                {
                    toolStrip2.Hide();
                }
                else if (gridController.ToString() != 		"Zamba.Controls.WF.TasksCtls.UCTaskGrid")
                {
                    flagsearch = true;
                }
                _currentUserId = currentUserId;

                //[Pablo] se comentan las siguientes lineas para que se pueda 
                //setear el ancho desde el User Config

                //this.outlookGrid1.AutoSizeColumnsMode =
                //    DataGridViewAutoSizeColumnsMode.ColumnHeader;
                outlookGrid1.AllowUserToResizeColumns = true;
                outlookGrid1.AllowUserToResizeRows = true;
                if (useExcel)
                {
                    btnExportToExcel.Visible = true;
                }
                _withExcel = useExcel;

                HideFiltersAtStart();
            }
            catch (Exception ex)
            {
                ZException.Log(ex, true);
            }
        }


        private void Form1Load(object sender, EventArgs e)
        {
            // invoke the outlook style
            MenuSkinOutlookClick();
        }

        #endregion Form setup

        /// <summary>
        ///   Evento que se ejecuta cuando se presiona el botón "Seleccionar Todos"
        /// </summary>
        /// <history> 
        ///   [Gaston]    12/08/2008  Created
        ///   05/09/2008  Modified
        /// </history>
        private void BtnSelectAllClick(object sender, EventArgs e)
        {
            if (btnSelectAll.Tag.ToString() == "0")
            {
                if (outlookGrid1.Columns.Count > 0 && _dataTable.Columns.Count > 0)
                {
                    if (outlookGrid1.Columns[0].Visible && _dataTable.Columns[0].DataType == typeof(Boolean))
                    {
                        for (int i = 0; i < outlookGrid1.Rows.Count; i++)
                        {
                            outlookGrid1.Rows[i].Cells[0].Value = true;
                        }
                    }
                    else
                        outlookGrid1.SelectAll();

                    btnSelectAll.Tag = "1";
                    btnSelectAll.Text = Resources.GroupGrid_BtnSelectAllClick_Deseleccionar_todos;

                    if (_dSelectAllClickEvent != null)
                    {
                        // Se ejecuta el evento que hace que se llame a UCTaskGrid
                        _dSelectAllClickEvent(sender, e);
                    }
                }
            }
            else
            {
                if (outlookGrid1.Columns.Count > 0 && _dataTable.Columns.Count > 0)
                {
                    if (outlookGrid1.Columns[0].Visible && _dataTable.Columns[0].DataType == typeof(Boolean))
                    {
                        for (int i = 0; i < outlookGrid1.Rows.Count; i++)
                        {
                            outlookGrid1.Rows[i].Cells[0].Value = false;
                        }
                    }
                    else
                        outlookGrid1.ClearSelection();
                }

                btnSelectAll.Tag = "0";
                btnSelectAll.Text = Resources.GroupGrid_FixColumns_Seleccionar_todos;

                if (_dDeselectAllClickEvent != null)
                {
                    // Se ejecuta el evento para deshabilitar las reglas de la toolbar
                    _dDeselectAllClickEvent(sender, e);
                }
            }
        }

        /// <summary>
        ///   Deselecciona una fila de la grilla
        /// </summary>
        /// <history>
        ///   Marcelo 09/11/09    created
        /// </history>
        /// <param name = "row">Fila a ser deseleccionada</param>
        public void DeselectRow(OutlookGridRow row)
        {
            if (outlookGrid1.Columns.Contains("Ver"))
            {
                if (outlookGrid1 != null)
                    if (outlookGrid1.Columns["Ver"].Visible)
                    {
                        row.Cells["Ver"].Value = false;
                    }
            }
            else
                row.Selected = false;
        }

        /// <summary>
        ///   Evento que se ejecuta cuando se presiona una tecla
        /// </summary>
        /// <history> 
        ///   [Gaston]    19/08/2008  Created
        ///   05/09/2008  Modified
        ///   15/09/2008  Modified to efficiency
        /// </history>
        private void OutlookGrid1KeyPress(object sender, KeyPressEventArgs e)
        {
            //si se presiona <Enter> sobre la grilla de resultados
            if (e.KeyChar.ToString() == "\r")
            {
                try
                {
                    if (null != _dDoubleClickEvent)
                    {
                        _dDoubleClickEvent(sender, e);
                        _dRowClickEvent(sender, e);
                    }
                    SetRegisterText(_totalCount, _pageSize, -1);
                    e.Handled = true;
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
            else if (outlookGrid1.Columns.Contains("Ver"))
            {
                // Si la tecla presionada es la barra espaciadora
                if (String.Compare(e.KeyChar.ToString(), " ") == 0)
                {
                    if (outlookGrid1.SelectedRows.Count > 0)
                    {
                        if ((outlookGrid1.Tag.ToString() == "0") ||
                            (outlookGrid1.Tag.ToString() != outlookGrid1.SelectedRows.Count.ToString()))
                        {
                            if ((String.IsNullOrEmpty(outlookGrid1.SelectedRows[0].Cells["Ver"].Value.ToString())) ||
                                (String.Compare(outlookGrid1.SelectedRows[0].Cells["Ver"].Value.ToString().ToUpper(),
                                                "FALSE") == 0))
                                ActivateCheckBoxs();
                            else
                                DesactivateCheckBoxs();

                            // Se ejecuta el evento que hace que se llame a UCTaskGrid
                            if (_dSelectAllClickEvent != null)
                                _dSelectAllClickEvent(sender, e);
                        }
                        else
                        {
                            if (
                                String.Compare(outlookGrid1.SelectedRows[0].Cells["Ver"].Value.ToString().ToUpper(),
                                               "TRUE") == 0)
                                DesactivateCheckBoxs();
                            else
                                ActivateCheckBoxs();

                            if (_dSelectAllClickEvent != null)
                                _dSelectAllClickEvent(sender, e);
                        }

                        outlookGrid1.Refresh();
                    }
                }
            }
        }

        /// <summary>
        ///   Método que activa los checkboxs de las filas seleccionadas
        /// </summary>
        /// <history> 
        ///   [Gaston]    15/09/2008  Created
        /// </history>
        private void ActivateCheckBoxs()
        {
            for (int i = 0; i < outlookGrid1.SelectedRows.Count; i++)
                outlookGrid1.SelectedRows[i].Cells["Ver"].Value = true;

            outlookGrid1.Tag = outlookGrid1.SelectedRows.Count.ToString();
        }

        /// <summary>
        ///   Método que desactiva los checkboxs de las filas seleccionadas
        /// </summary>
        /// <history> 
        ///   [Gaston]    15/09/2008  Created
        /// </history>
        private void DesactivateCheckBoxs()
        {
            for (int i = 0; i < outlookGrid1.SelectedRows.Count; i++)
                outlookGrid1.SelectedRows[i].Cells["Ver"].Value = false;

            outlookGrid1.Tag = "0";
        }

        /// <summary>
        ///   Metodo que sirve para ocultar los filtros de la etapa
        ///   sebastian 22/12/2008
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        private void HideClick(object sender, EventArgs e)
        {
            if (btnHideFilter.Checked)
            {
                ShowFilters();
            }
            else
            {
                HideFilters();
            }
        }

        /// <summary>
        ///   Genera el grafico de excel
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        private void BtnGraphicClick(object sender, EventArgs e)
        {
            var ucGraph = new ExcelGraphType();
            ucGraph.ShowDialog();

            if (ucGraph.hasGraphic)
            {
                string path = Membership.MembershipHelper.StartUpPath + "\\grafico.xls";
                ExcelInterop.setExcelGraphics((DataTable)outlookGrid1.DataSource, ucGraph.typeGraphic, path, true, true);
                var proc = new Process { StartInfo = { FileName = path } };
                proc.Start();
            }
        }

        private void OutlookGrid1CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _dRightClick(sender, e);
            }
            else
            {
                if(e.RowIndex !=0)
                    SetRegisterText(_totalCount, _pageSize, e.RowIndex);
            }
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
            _gridController.LastPage = 0;
            AddFilter();
        }

        private void AddFilter()
        {
            var cantidadFiltrosActuales = lsvFilters.Items.Count;
            IFilterElem fe = null;

            //Agrego el filtro seleccionado
            if (cmbFilterColumn.SelectedItem != null && cmbOperator.SelectedItem != null
               && pnlCtrlIndex.Controls.Count > 0)
            {

                String data;
                if (pnlCtrlIndex.Controls[0] is DisplayindexCtl)
                {
                    if (((DisplayindexCtl)pnlCtrlIndex.Controls[0]).Index.DropDown ==
                        IndexAdditionalType.AutoSustitución)
                    {
                        data =
                            string.IsNullOrEmpty(((DisplayindexCtl)pnlCtrlIndex.Controls[0]).Index.dataDescriptionTemp) ==
                            false
                                ? ((DisplayindexCtl)pnlCtrlIndex.Controls[0]).Index.dataDescriptionTemp
                                : ((DisplayindexCtl)pnlCtrlIndex.Controls[0]).getText();
                    }
                    else
                        data = ((DisplayindexCtl)pnlCtrlIndex.Controls[0]).Index.DataTemp;
                }
                else
                    data = pnlCtrlIndex.Controls[0].Text;

                String filterType;
                if (flagsearch)
                {
                    filterType = "search";
                }
                else
                {
                    filterType = "manual";
                }
                var docTypeId = DocTypeId;
                if (pnlCtrlIndex.Controls.Count > 0 && pnlCtrlIndex.Controls[0] is DisplayindexCtl)
                {
                    var pnl = (DisplayindexCtl)pnlCtrlIndex.Controls[0];
                    fe = _gridController.Fc.SetNewFilter(
                            pnl.Index.ID,
                            pnl.Index.ID.ToString(),
                            pnl.Index.Type,
                            _currentUserId,
                            cmbOperator.SelectedItem.ToString(),
                            data,
                            docTypeId,
                            UseZamba,
                            pnl.Index.Name,
                            pnl.Index.DropDown, filterType, !flagsearch);
                }
                else if (cmbOperator.SelectedItem == "Contiene" || cmbOperator.SelectedItem == "No Contiene")
                {
                    IIndex selectedIndex = null;

                    if (string.Compare(cmbFilterColumn.SelectedItem.ToString(), "System.Data.DataRowView") != 0)
                    {
                        Int64 selectedIndexId = 0;
                        if (DocTypeId != 0)
                            selectedIndexId = IndexsBusiness.GetIndexIdByName(cmbFilterColumn.Text);

                        if (selectedIndexId != 0)
                        {
                            selectedIndex = ZCore.GetIndex(selectedIndexId);
                        }
                    }

                    if (selectedIndex != null)
                    {
                        if (selectedIndex.Type == IndexDataType.Numerico_Largo ||
                            selectedIndex.Type == IndexDataType.Numerico_Decimales ||
                            selectedIndex.Type == IndexDataType.Moneda ||
                            selectedIndex.Type == IndexDataType.Numerico)
                        {
                            fe = _gridController.Fc.SetNewFilter(
                                selectedIndex.ID,
                                cmbFilterColumn.Text.Trim(),
                                selectedIndex.Type,
                                _currentUserId,
                                cmbOperator.SelectedItem.ToString(),
                                data,
                                docTypeId,
                                UseZamba,
                                selectedIndex.Name,
                                selectedIndex.DropDown, filterType, !flagsearch);
                        }
                    }
                    else
                    {
                        var filterColumnText = cmbFilterColumn.Text.Trim();
                        fe = _gridController.Fc.SetNewFilter(
                            0,
                            FormatZColumn(filterColumnText,!flagsearch),
                            GetZColumnType(filterColumnText, !flagsearch),
                            _currentUserId,
                            cmbOperator.SelectedItem.ToString(),
                            data,
                            docTypeId,
                            UseZamba,
                            filterColumnText,
                            IndexAdditionalType.NoIndex, filterType, !flagsearch);
                    }
                }
                else
                {
                    var filterColumnText = cmbFilterColumn.Text.Trim();
                    fe = _gridController.Fc.SetNewFilter(
                        0,
                        FormatZColumn(filterColumnText, !flagsearch),
                        GetZColumnType(filterColumnText, !flagsearch),
                        _currentUserId,
                        cmbOperator.SelectedItem.ToString(),
                        data,
                        docTypeId,
                        UseZamba,
                        filterColumnText,
                        IndexAdditionalType.NoIndex, filterType, !flagsearch);
                }

                if (fe != null)
                    if (GetFilter(fe.Text.Trim()) == null)
                    {
                        lsvFilters.ItemCheck -= LsvFiltersItemCheck;
                        lsvFilters.Items.Add(fe, true);
                        lsvFilters.ItemCheck += LsvFiltersItemCheck;
                    }
            }

            ShowFiltersIfExists();

            //Habilito el boton de quitar filtros
            if (lsvFilters.Items.Count > 0)
                btnRemoveAllFilters.Enabled = true;

            //Recarga la grilla con los datos filtrados si se agrego filtro
            if (lsvFilters.Items.Count > cantidadFiltrosActuales)
                try
                {
                    _gridController.ShowTaskOfDT();
                }
                //Si falla la carga, quito el filtro
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    MessageBox.Show(MsgAddFilter, MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (fe != null)
                    {
                        //Remuevo el filtro seleccionado
                        _gridController.Fc.RemoveFilter(fe, !flagsearch);
                        if (lsvFilters.Items.Count > 0)
                            lsvFilters.Items.Remove(fe);

                        _gridController.ShowTaskOfDT();
                    }
                }

        }

        /// <summary>
        /// Realiza el formateo de las columnas propias de Zamba para que puedan ser filtradas correctamente.
        /// </summary>
        /// <param name="colName">Nombre de la columna como se visualiza en el combo de columnas a filtrar</param>
        /// <returns>Nombre de columna formateada para filtrar</returns>
        private static string FormatZColumn(string colName, Boolean isTask)
        {
            var nombreDocumentoUserConfig = UserPreferences.getValue("ColumnNameNombreDelDocumento", Sections.UserPreferences, "Nombre del Documento");
            var imagenUserConfig = UserPreferences.getValue("ColumnNameImagen", Sections.UserPreferences, "Imagen");
            var verUserConfig = UserPreferences.getValue("ColumnNameVer", Sections.UserPreferences, "Ver");
            var estadoTareaUserConfig = UserPreferences.getValue("ColumnNameEstadoTarea", Sections.UserPreferences,
                                                                 "Estado Tarea");
            var asignadoUserConfig = UserPreferences.getValue("ColumnNameAsignado", Sections.UserPreferences, "Asignado");
            var situacionUserConfig = UserPreferences.getValue("ColumnNameSituacion", Sections.UserPreferences,
                                                               "Situacion");
            var nombreOriginalUserConfig = UserPreferences.getValue("ColumnNameNombreOriginal",
                                                                     Sections.UserPreferences, "Nombre Original");
                        
    
            var tipoDocumentoUserConfig = UserPreferences.getValue("ColumnNameTipoDocumento", Sections.UserPreferences, "Tipo Documento");
            var fechaCreacionUserConfig  = UserPreferences.getValue("ColumnNameFechaCreacion", Sections.UserPreferences, "Fecha Creacion");
            var fechaModificacionUserConfig  = UserPreferences.getValue("ColumnNameFechaModificacion", Sections.UserPreferences, "Fecha Modificacion");
            var versionUserConfig  = UserPreferences.getValue("ColumnNameVersion", Sections.UserPreferences, "Version");
            var nroVersionUserConfig = UserPreferences.getValue("ColumnNameNroVersion", Sections.UserPreferences, "Nro Version");

            if (isTask)
            {
                if (String.Compare(colName, asignadoUserConfig) == 0)
                    return "Asignado";
                if (String.Compare(colName, nombreDocumentoUserConfig) == 0)
                    return "NAME";                
                if (String.Compare(colName, estadoTareaUserConfig) == 0)
                    return "state";
                if (String.Compare(colName, situacionUserConfig) == 0)
                    return "Situacion";
                if (String.Compare(colName, nombreOriginalUserConfig) == 0)
                    return "original_filename";
                if (String.Compare(colName, "Ingreso") == 0)
                    return "checkin";
                if (String.Compare(colName, "Vencimiento") == 0)
                    return "ExpireDate";
                if (String.Compare(colName, "Task_ID") == 0)
                    return "Task_ID";
                if (String.Compare(colName, "Folder_ID") == 0)
                    return "Folder_ID";
            }
            else
            {
                if (String.Compare(colName, nombreDocumentoUserConfig) == 0)
                    return "Nombre del Documento";
                if (String.Compare(colName, nombreOriginalUserConfig) == 0)
                    return "Nombre Original";
                if (String.Compare(colName, tipoDocumentoUserConfig) == 0)
                    return "Entidad";
                if (String.Compare(colName, fechaCreacionUserConfig) == 0)
                    return "Fecha Creacion";
                if (String.Compare(colName, fechaModificacionUserConfig) == 0)
                    return "Fecha Modificacion";
                if (String.Compare(colName, versionUserConfig) == 0)
                    return "Version";
                if (String.Compare(colName, nroVersionUserConfig) == 0)
                    return "Numero de Version";              
            }
            //Agregar lo que haga falta.

            return colName;
        }

        /// <summary>
        /// Obtiene el tipo de datos de las columnas propias de Zamba.
        /// </summary>
        /// <param name="colName">Nombre de la columna como se visualiza en el combo de columnas a filtrar</param>
        /// <returns>Tipo de dato de la columna</returns>
        private static IndexDataType GetZColumnType(string colName, Boolean isTask)
        {
            
                var nombreDocumentoUserConfig = UserPreferences.getValue("ColumnNameNombreDelDocumento", Sections.UserPreferences, "Nombre del Documento");
                var imagenUserConfig = UserPreferences.getValue("ColumnNameImagen", Sections.UserPreferences, "Imagen");
                var verUserConfig = UserPreferences.getValue("ColumnNameVer", Sections.UserPreferences, "Ver");
                var estadoTareaUserConfig = UserPreferences.getValue("ColumnNameEstadoTarea", Sections.UserPreferences,
                                                                     "Estado Tarea");
                var asignadoUserConfig = UserPreferences.getValue("ColumnNameAsignado", Sections.UserPreferences, "Asignado");
                var situacionUserConfig = UserPreferences.getValue("ColumnNameSituacion", Sections.UserPreferences,
                                                                   "Situacion");
                var nombreOriginalUserConfig = UserPreferences.getValue("ColumnNameNombreOriginal",
                                                                         Sections.UserPreferences, "Nombre Original");

                var tipoDocumentoUserConfig = UserPreferences.getValue("ColumnNameTipoDocumento", Sections.UserPreferences, "Tipo Documento");
                var fechaCreacionUserConfig = UserPreferences.getValue("ColumnNameFechaCreacion", Sections.UserPreferences, "Fecha Creacion");
                var fechaModificacionUserConfig = UserPreferences.getValue("ColumnNameFechaModificacion", Sections.UserPreferences, "Fecha Modificacion");
                var versionUserConfig = UserPreferences.getValue("ColumnNameVersion", Sections.UserPreferences, "Version");
                var nroVersionUserConfig = UserPreferences.getValue("ColumnNameNroVersion", Sections.UserPreferences, "Nro Version");

            if (isTask)
            {
                if (String.Compare(colName, asignadoUserConfig) == 0)
                    return IndexDataType.Alfanumerico;
                if (String.Compare(colName, nombreDocumentoUserConfig) == 0)
                    return IndexDataType.Alfanumerico;                
                if (String.Compare(colName, estadoTareaUserConfig) == 0)
                    return IndexDataType.Alfanumerico;
                if (String.Compare(colName, situacionUserConfig) == 0)
                    return IndexDataType.Alfanumerico;
                if (String.Compare(colName, nombreOriginalUserConfig) == 0)
                    return IndexDataType.Alfanumerico;
                if (String.Compare(colName, "Ingreso") == 0)
                    return IndexDataType.Fecha_Hora;
                if (String.Compare(colName, "Vencimiento") == 0)
                    return IndexDataType.Fecha_Hora;
                if (String.Compare(colName, "Task_ID") == 0)
                    return IndexDataType.Alfanumerico;
                if (String.Compare(colName, "Folder_ID") == 0)
                    return IndexDataType.Alfanumerico;
            }
            else
            {
                //mapeo de búsqueda de documentos
                if (String.Compare(colName, nombreDocumentoUserConfig) == 0)
                    return IndexDataType.Alfanumerico;
                if (String.Compare(colName, nombreOriginalUserConfig) == 0)
                    return IndexDataType.Alfanumerico;
                if (String.Compare(colName, tipoDocumentoUserConfig) == 0)
                    return IndexDataType.Alfanumerico;
                if (String.Compare(colName, fechaCreacionUserConfig) == 0)
                    return IndexDataType.Fecha_Hora;
                if (String.Compare(colName, fechaModificacionUserConfig) == 0)
                    return IndexDataType.Fecha_Hora;
                if (String.Compare(colName, versionUserConfig) == 0)
                    return IndexDataType.Alfanumerico;
                if (String.Compare(colName, nroVersionUserConfig) == 0)
                    return IndexDataType.Alfanumerico;      
            }


            return IndexDataType.Alfanumerico;
        }

        private void CmbOperatorSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadFilterDataIndexControl();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        ///   Carga el textbox o control de indice para que el usuario complete el filtro
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        private void CmbFilterColumnSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFilterDataIndexControl();
            if (cmbFilterColumn.SelectedIndex != -1)
                UserPreferences.setValue("LastFilterIndexUsed", cmbFilterColumn.SelectedItem.ToString(),
                                         Sections.Filters);
        }

        private void LoadFilterDataIndexControl()
        {
            IIndex selectedIndex = null;
            //deshabilito el textbox de ingreso de filtros en caso de que
            //se hayan seleccionado los operadores 'Es Nulo' y 'No es Nulo'           
            if (cmbOperator.SelectedItem == "Es Nulo" || cmbOperator.SelectedItem == "No es Nulo")
            {
                var hiddenTxt = new TextBox();
                hiddenTxt.Dock = DockStyle.Fill;
                pnlCtrlIndex.Controls.Clear();
                pnlCtrlIndex.Controls.Add(hiddenTxt);
                pnlCtrlIndex.Visible = false;

            }
            else
                pnlCtrlIndex.Visible = true;


            if (cmbFilterColumn.SelectedItem != null &&
                string.Compare(cmbFilterColumn.SelectedItem.ToString(), "System.Data.DataRowView") != 0)
            {
                Int64 selectedIndexId = 0;
                if (DocTypeId != 0) selectedIndexId = IndexsBusiness.GetIndexIdByName(cmbFilterColumn.Text);

                if (selectedIndexId != 0)
                {
                    selectedIndex = ZCore.GetIndex(selectedIndexId);
                }

                if (selectedIndex == null ||
                        ((selectedIndex.Type == IndexDataType.Numerico_Largo) && (cmbOperator.SelectedItem == "Contiene")) ||
                        ((selectedIndex.Type == IndexDataType.Numerico) && (cmbOperator.SelectedItem == "Contiene")) ||
                        ((selectedIndex.Type == IndexDataType.Moneda) && (cmbOperator.SelectedItem == "Contiene")) ||
                        ((selectedIndex.Type == IndexDataType.Moneda) && (cmbOperator.SelectedItem == "No Contiene")) ||
                        ((selectedIndex.Type == IndexDataType.Numerico_Largo) && (cmbOperator.SelectedItem == "No Contiene")) ||
                        ((selectedIndex.Type == IndexDataType.Numerico) && (cmbOperator.SelectedItem == "No Contiene")))
                {
                    var txtValue = new TextBox();
                    txtValue.Dock = DockStyle.Fill;
                    pnlCtrlIndex.Controls.Clear();
                    pnlCtrlIndex.Controls.Add(txtValue);
                    txtValue.KeyPress += CtrlEnterPressed;
                }
                else
                {
                    //Si el indice es del mismo tipo que el anterior mantengo el valor cargado, por si me confundi de indice al seleccionar pero ya cargue el valor a filtrar
                    if (pnlCtrlIndex != null && pnlCtrlIndex.Controls.Count > 0 &&
                        (pnlCtrlIndex.Controls[0]).GetType().Name == "DisplayindexCtl")
                    {
                        IIndex oldFilteredIndex = ((DisplayindexCtl)pnlCtrlIndex.Controls[0]).Index;
                        if (oldFilteredIndex != null && selectedIndex.Type == oldFilteredIndex.Type &&
                            oldFilteredIndex.DataTemp != String.Empty)
                        {
                            selectedIndex.Data = oldFilteredIndex.DataTemp;
                            selectedIndex.DataTemp = oldFilteredIndex.DataTemp;
                            selectedIndex.dataDescription = oldFilteredIndex.dataDescriptionTemp;
                            selectedIndex.dataDescriptionTemp = oldFilteredIndex.dataDescriptionTemp;
                        }
                    }

                    if (selectedIndex.Type == IndexDataType.Fecha_Hora &&
                        Boolean.Parse(UserPreferences.getValue("GridShortDateFormat", Sections.UserPreferences, true)))
                    {
                        selectedIndex.Type = IndexDataType.Fecha;
                    }

                    var ctrl = new DisplayindexCtl(selectedIndex, true) { Dock = DockStyle.Fill };
                    ctrl.Controls["Panel5"].Visible = false;
                    ctrl.Controls["Splitter2"].Visible = false;
                    ctrl.Controls["lblindexname"].Visible = false;
                    ctrl.Controls["lblindexname"].Dock = DockStyle.Fill;
                    ctrl.EnterPressed += CtrlEnterPressed;


                    if (pnlCtrlIndex != null)
                    {
                        pnlCtrlIndex.Controls.Clear();
                        pnlCtrlIndex.Controls.Add(ctrl);
                    }
                }
            }
            else if (cmbFilterColumn.SelectedItem == null && string.IsNullOrEmpty(cmbFilterColumn.Text) == false)
            {
                Int64 selectedIndexId = IndexsBusiness.GetIndexIdByName(cmbFilterColumn.Text);
                selectedIndex = ZCore.GetIndex(selectedIndexId);

                if (selectedIndex == null)
                {
                    var txtValue = new TextBox();
                    txtValue.Dock = DockStyle.Fill;
                    pnlCtrlIndex.Controls.Clear();
                    pnlCtrlIndex.Controls.Add(txtValue);
                    txtValue.KeyPress += CtrlEnterPressed;
                    //AGREGAR EL EVENTO DEL ENTER Y EL TAB
                }
                else
                {
                    //Si el indice es del mismo tipo que el anterior mantengo el valor cargado, por si me confundi de indice al seleccionar pero ya cargue el valor a filtrar
                    if (pnlCtrlIndex != null && pnlCtrlIndex.Controls.Count > 0 &&
                        (pnlCtrlIndex.Controls[0]).GetType().Name == "DisplayindexCtl")
                    {
                        IIndex oldFilteredIndex = ((DisplayindexCtl)pnlCtrlIndex.Controls[0]).Index;
                        if (oldFilteredIndex != null && selectedIndex.Type == oldFilteredIndex.Type &&
                            oldFilteredIndex.DataTemp != String.Empty)
                        {
                            selectedIndex.Data = oldFilteredIndex.DataTemp;
                            selectedIndex.DataTemp = oldFilteredIndex.DataTemp;
                            selectedIndex.dataDescription = oldFilteredIndex.dataDescriptionTemp;
                            selectedIndex.dataDescriptionTemp = oldFilteredIndex.dataDescriptionTemp;
                        }
                    }

                    if (cmbOperator.SelectedItem == "Contiene" || cmbOperator.SelectedItem == "No Contiene")
                    {
                        selectedIndex.Type = IndexDataType.Alfanumerico;
                        selectedIndex.Len = 500;
                        selectedIndex.DropDown = 0;
                    }

                    if (selectedIndex.Type == IndexDataType.Fecha_Hora &&
                        Boolean.Parse(UserPreferences.getValue("GridShortDateFormat", Sections.UserPreferences, true)))
                    {
                        selectedIndex.Type = IndexDataType.Fecha;
                    }

                    var ctrl = new DisplayindexCtl(selectedIndex, true) { Dock = DockStyle.Fill };
                    ctrl.Controls["Panel5"].Visible = false;
                    ctrl.Controls["Splitter2"].Visible = false;
                    ctrl.Controls["lblindexname"].Visible = false;
                    ctrl.Controls["lblindexname"].Dock = DockStyle.Fill;
                    ctrl.EnterPressed += CtrlEnterPressed;

                    if (pnlCtrlIndex != null)
                    {
                        pnlCtrlIndex.Controls.Clear();
                        pnlCtrlIndex.Controls.Add(ctrl);
                    }
                }
            }


            if (cmbFilterColumn != null)
            {
                if (DataTable.Columns.Contains(cmbFilterColumn.Text))
                {
                    string columnType = DataTable.Columns[cmbFilterColumn.Text].DataType.Name.ToUpper();

                    if (!cmbOperator.Items.Contains("Contiene"))
                    {
                        cmbOperator.Items.Add("Contiene");
                    }
                    if (!cmbOperator.Items.Contains("No Contiene"))
                    {
                        cmbOperator.Items.Add("No Contiene");
                    }
                    if (!cmbOperator.Items.Contains("Es Nulo"))
                    {
                        cmbOperator.Items.Add("Es Nulo");
                    }
                    if (!cmbOperator.Items.Contains("No es Nulo"))
                    {
                        cmbOperator.Items.Add("No es Nulo");
                    }


                    //Seteo el operador por default segun sea el tipo de indice
                    //          this.cmbOperator.Items.AddRange(new object[] {
                    //"=",
                    //">",
                    //"<",
                    //">=",
                    //"<=",
                    //"<>",
                    //"Contiene",
                    //"Dentro de"});
                    if (selectedIndex != null)
                    {
                        switch (selectedIndex.Type)
                        {
                            case IndexDataType.Alfanumerico:
                            case IndexDataType.Alfanumerico_Largo:
                                if (cmbOperator.SelectedIndex == -1)
                                {
                                    cmbOperator.SelectedIndex = 6;
                                    cmbOperator.SelectedItem = cmbOperator.Items[cmbOperator.SelectedIndex];
                                }
                                break;
                            case IndexDataType.Fecha:
                            case IndexDataType.Fecha_Hora:
                            case IndexDataType.None:
                            case IndexDataType.Si_No:
                                if (cmbOperator.SelectedIndex == -1)
                                {
                                    cmbOperator.SelectedIndex = 0;
                                    cmbOperator.SelectedItem = cmbOperator.Items[cmbOperator.SelectedIndex];
                                }
                                break;
                            case IndexDataType.Moneda:
                            case IndexDataType.Numerico:
                            case IndexDataType.Numerico_Decimales:
                            case IndexDataType.Numerico_Largo:
                                if (cmbOperator.SelectedIndex == -1)
                                {
                                    cmbOperator.SelectedIndex = 6;
                                    cmbOperator.SelectedItem = cmbOperator.Items[cmbOperator.SelectedIndex];
                                }
                                break;
                        }
                    }
                }
            }

            //Pone el foco en el control de texto para que el usuario ingrese el valor por el cual va a filtra
            if (pnlCtrlIndex != null)
                if (pnlCtrlIndex.Controls.Count > 0)
                    pnlCtrlIndex.SelectNextControl(pnlCtrlIndex.Controls[0], true, true, false, true);
        }

        private void CtrlEnterPressed()
        {
            AddFilter();
        }

        private void CtrlEnterPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                AddFilter();
            }
        }

        /// <summary>
        ///   [Sebastian 25-08-2009]
        ///   Vuelva a agrupar la grilla de la forma original de Zamba, que es por FOLDER_ID.
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        private void BtnUngroupClick(object sender, EventArgs e)
        {
            //Si es paginado, envio el evento hacia arriba
            //if (UpdateByFiltersEvent != null)
            //{
            //    applyFilters();
            //    UpdateByFiltersEvent(this.dataTable);
            //}
            //else
            //{
            if (UseZamba)
            {
                if (Boolean.Parse(UserPreferences.getValue("GroupByFolder", Sections.UserPreferences, false)))
                {
                    outlookGrid1.ClearGroups();
                    if (_gridController!=null)
                    _gridController.ShowTaskOfDT(); 
                }
                else
                {
                    Group(outlookGrid1.Columns["FOLDER_ID"]);
                }
            }
            else
            {
                Refresh();
            }
            //}

            cmbColumn.SelectedIndex = 0;
        }


        private void OutlookGrid1KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40 || e.KeyValue == 38)
            {
                try
                {
                    if (null != _dRowClickEvent)
                    {
                        _dRowClickEvent(sender, e);
                    }
                    SetRegisterText(_totalCount, _pageSize, -1);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
            //Si se presiona la tecla enter se abre el documento
            else if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (null != _dDoubleClickEvent)
                        _dDoubleClickEvent(sender, e);
                    SetRegisterText(_totalCount, _pageSize, -1);
                    e.Handled = true;
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
        }
        
        private void LsvFiltersItemCheck(object sender, ItemCheckEventArgs e)
        {
            FilterElem fe = (FilterElem)((ListBox)sender).SelectedItem;

            if (UserBusiness.CanDisableDefaultFilter(fe.DocTypeId) || fe.Type != "defecto")
            {
                fe.Enabled = !fe.Enabled;
                _gridController.Fc.SetEnabledFilter(fe, !flagsearch);

                // (pablo) - 03032011
                if (fe.Type != "defecto")
                {
                    ((FiltersComponent)_gridController.Fc).SaveFilterInDatabase(fe);
                }
                else if (fe.Type == "defecto" && Convert.ToBoolean(UserPreferences.getValue("SaveFilterInDatabase", Sections.UserPreferences, "True")) == true)
                {
                    ((FiltersComponent)_gridController.Fc).SaveFilterInDatabase(fe);
                }

                _gridController.ShowTaskOfDT();
            }
            else
            {
                if (e.CurrentValue.ToString() == "Unchecked")
                {
                    MessageBox.Show("No tiene permiso para volver a habilitar los filtros por defecto","ATENCION");                    
                    e.NewValue = CheckState.Unchecked;
                }
                else
                {
                    MessageBox.Show(
                        "No tiene permiso para deshabilitar los filtros por defecto",
                        Resources.GroupGrid_BtnUncheckAllFiltersClick_ATENCION);
                    if (e.NewValue == CheckState.Checked)
                        e.NewValue = CheckState.Unchecked;
                    else
                        e.NewValue = CheckState.Checked;
                }
            }

            fe = null;
        }


        private void LsvFiltersKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                RemoveSelectedFilter();
        }

        private void QuitarToolStripMenuItemClick(object sender, EventArgs e)
        {
            RemoveSelectedFilter();
        }

        private void QuitarTodosToolStripMenuItemClick(object sender, EventArgs e)
        {
            RemoveAllFilters();
        }

        private void BtnUncheckAllFiltersClick(object sender, EventArgs e)
        {
            var filterCount = lsvFilters.Items.Count;
            lsvFilters.ItemCheck -= LsvFiltersItemCheck;
            for (Int16 t = 0; t < filterCount; t++)
            {
                if (UserBusiness.CanDisableDefaultFilter(((FilterElem)lsvFilters.Items[t]).DocTypeId))
                {
                    ((FilterElem)lsvFilters.Items[t]).Enabled = false;
                    lsvFilters.SetItemChecked(t, false);
                    _gridController.Fc.SetEnabledFilter((FilterElem)lsvFilters.Items[t], !flagsearch);
                }
                else if (((FilterElem)(lsvFilters.Items[t])).Type != "defecto")
                {
                    ((FilterElem)lsvFilters.Items[t]).Enabled = false;
                    lsvFilters.SetItemChecked(t, false);
                    _gridController.Fc.SetEnabledFilter((FilterElem)lsvFilters.Items[t], !flagsearch);
                }
                else
                    MessageBox.Show("No tiene permiso para deshabilitar los filtros por defecto",Resources.GroupGrid_BtnUncheckAllFiltersClick_ATENCION);
            }

            lsvFilters.ItemCheck += LsvFiltersItemCheck;
            _gridController.ShowTaskOfDT();
        }

        private void CmbDocTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            _gridController.ShowTaskOfDT();
        }


        private void SetPaging(Int32 totalCount, Int32 pageSize)
        {
            try
            {
                if (pageSize != 0)
                {

                    Int32 resto = 0;
                    _pagesCount = Math.DivRem(totalCount, pageSize, out resto);
                    if (resto > 0) _pagesCount++;

                    selecteddropdownlistpage.Items.Clear();
                    for (int i = 1; i < _pagesCount + 1; i++)
                    {
                        selecteddropdownlistpage.Items.Add(i);
                    }
                    selecteddropdownlistpage.KeyUp -= SelecteddropdownlistpageKeyUp;
                    selecteddropdownlistpage.KeyUp += SelecteddropdownlistpageKeyUp;

                    selecteddropdownlistpage.SelectedIndexChanged -= SelecteddropdownlistpageSelectedIndexChanged;

                    selecteddropdownlistpage.Text = (_gridController.LastPage + 1).ToString();

                    selecteddropdownlistpage.SelectedIndexChanged += SelecteddropdownlistpageSelectedIndexChanged;

                }
            }
            catch (Exception ex)
            {
                ZCore.raiseerror(ex);
            }
        }

        private void BtnfirstpageClick(object sender, EventArgs e)
        {
            _gridController.LastPage = 0;
            _gridController.ShowTaskOfDT();
        }

        private void BtnpreviuspageClick(object sender, EventArgs e)
        {
            //valido que LastPage este dentro del rango de paginas
            if (_gridController.LastPage >= 0)
            {
                if (_gridController.LastPage == 0)
                {
                    _gridController.LastPage = _gridController.LastPage;
                    _gridController.ShowTaskOfDT();
                }
                else
                {
                    _gridController.LastPage = _gridController.LastPage - 1;
                    _gridController.ShowTaskOfDT();
                }
            }
        }

        private void SelecteddropdownlistpageSelectedIndexChanged(object sender, EventArgs e)
        {
            _gridController.LastPage = Convert.ToInt32(selecteddropdownlistpage.SelectedItem.ToString()) - 1;
            _gridController.ShowTaskOfDT();
        }


        private void SelecteddropdownlistpageKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Int32 page;
                var cmb = (ToolStripComboBox)sender;
                if (!Int32.TryParse(cmb.ComboBox.Text, out page))
                {
                    return;
                }
                else
                {
                    if (page <= selecteddropdownlistpage.Items.Count)
                    {
                        _gridController.LastPage = Convert.ToInt32(page.ToString()) - 1;
                        _gridController.ShowTaskOfDT();
                    }
                }
            }
        }

        private void BtnnextpageClick(object sender, EventArgs e)
        {
            //valido que LastPage este dentro del rango de paginas
            if (_gridController.LastPage < selecteddropdownlistpage.Items.Count - 1)
            {
                _gridController.LastPage = _gridController.LastPage + 1;
                _gridController.ShowTaskOfDT();
            }
        }

        private void BtnlastpageClick(object sender, EventArgs e)
        {
            _gridController.LastPage = (Int32)_pagesCount - 1;
            _gridController.ShowTaskOfDT();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _dRefreshEvent();
        }
    }

    #region Comparers - used to sort CustomerInfo objects and DataRows of a DataTable

    /// <summary>
    ///   reusable custom DataRow comparer implementation, can be used to sort DataTables
    /// </summary>
    public class DataRowComparer : IComparer
    {
        private readonly int _columnIndex;
        private readonly ListSortDirection _direction;

        public DataRowComparer(int columnIndex, ListSortDirection direction)
        {
            _columnIndex = columnIndex;
            _direction = direction;
        }

        #region IComparer Members

        /// <summary>
        ///   Comparo los 2 valores
        /// </summary>
        /// <param name = "x"></param>
        /// <param name = "y"></param>
        /// <history>Marcelo modified 17/09/2008</history>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            var obj1 = (DataRow)x;
            var obj2 = (DataRow)y;

            //Si alguno de los valores esta en blanco, comparo como string
            if (obj2[_columnIndex].ToString() == string.Empty || obj1[_columnIndex].ToString() == string.Empty)
                return string.Compare(obj1[_columnIndex].ToString(), obj2[_columnIndex].ToString()) *
                       (_direction == ListSortDirection.Ascending ? 1 : -1);

            //Dependendiendo del tipo de la columna comparo
            if (obj1[_columnIndex] is DateTime)
                return DateTime.Compare(((DateTime)obj1[_columnIndex]), ((DateTime)obj2[_columnIndex])) *
                       (_direction == ListSortDirection.Ascending ? 1 : -1);
            if (obj1[_columnIndex] is Decimal)
                return Decimal.Compare(((Decimal)obj1[_columnIndex]), ((Decimal)obj2[_columnIndex])) *
                       (_direction == ListSortDirection.Ascending ? 1 : -1);
            if (obj1[_columnIndex] is long)
                return
                    Decimal.Compare(Decimal.Parse(obj1[_columnIndex].ToString()),
                                    Decimal.Parse(obj2[_columnIndex].ToString())) *
                    (_direction == ListSortDirection.Ascending ? 1 : -1);
            if (obj1[_columnIndex] is double)
                return
                    Decimal.Compare(Decimal.Parse(obj1[_columnIndex].ToString()),
                                    Decimal.Parse(obj2[_columnIndex].ToString())) *
                    (_direction == ListSortDirection.Ascending ? 1 : -1);
            if (obj1[_columnIndex] is Int32)
                return
                    Decimal.Compare(Decimal.Parse(obj1[_columnIndex].ToString()),
                                    Decimal.Parse(obj2[_columnIndex].ToString())) *
                    (_direction == ListSortDirection.Ascending ? 1 : -1);
            return string.Compare(obj1[_columnIndex].ToString(), obj2[_columnIndex].ToString()) *
                   (_direction == ListSortDirection.Ascending ? 1 : -1);
        }

        #endregion

        public int ColumnIndex()
        {
            return _columnIndex;
        }
    }
    #endregion Comparers
}
