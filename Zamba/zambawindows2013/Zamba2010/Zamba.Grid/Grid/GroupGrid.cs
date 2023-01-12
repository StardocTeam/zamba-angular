#region
using PrintControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text;
using System.Windows.Forms;
//using Telerik.WinControls.Export;
using Telerik.WinControls;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.Export;
using Telerik.WinControls.Themes;
//using Telerik.WinControls.Export;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using Telerik.WinControls.UI.Export;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI.Localization;
using Zamba.AppBlock;
using Zamba.Core;
using Zamba.Data;
using Zamba.Filters;
using Zamba.Grid.Properties;
using Zamba.Indexs;
using Zamba.Office;
using Process = System.Diagnostics.Process;
#endregion

namespace Zamba.Grid.Grid
{
    #region Delegates
    public delegate void GroupGridClickEventHandler(object sender, EventArgs e);
    public delegate void GroupGridRowClickEventHandler(object sender, EventArgs e);
    public delegate void GroupDoubleGridClickEventHandler(object sender, EventArgs e);
    public delegate void GroupRefreshEventHandler();
    public delegate void SelectAllClick(object sender, EventArgs e);
    public delegate void DeselectAllClick(object sender, EventArgs e);
    public delegate void UpdateDs();
    public delegate void GroupGridRightClick(object sender, EventArgs e);
    #endregion

    public sealed partial class GroupGrid : UserControl, IDataSource
    {
        #region Constantes
        private const string MSG_REMOVE_FILTER = "Ha ocurrido un error al remover los filtros.";
        private const string MSG_ADD_FILTER = "Ha ocurrido un error al agregar el filtro.";
        private const string MSG_TITLE = "Zamba Software";
        private const string MSG_REG_TEXT = "Registro: ";
        private const string VER_LABEL = "Ver";
        private const string ZERO_VALUE = "0";
        #endregion

        #region Atributos

        ToolTip cmbOpTooltip;
        private Hashtable GroupsLevels;
        private bool fromSort;
        private GridViewSpreadExport spreadExporter;
        private GridViewPdfExport pdfExporter;
        private SpreadExportRenderer exportRenderer;

        private List<string> hiddenColumns; // almacena las filas a ocultarse para cargar el combo de filtros.
        private List<string> visibleColumns;
        private bool reloadGrid; // bandera para saber si la grilla debe agregar filas o recargarse de cero.

        public Hashtable LoadedIndexs;
        private Boolean _alwaysFit;
        private String _oldDocTypeId = String.Empty;

        private DataTable _dataTable;

        public DataTable OriginalDataTable;

        public Hashtable _GroupByCountList;
        //Formato de las fechas de la grilla
        private Boolean _shortDateFormat;
        private Boolean _withExcel;
        private Boolean _showRefresh;

        private readonly Int64 _currentUserId;
        private readonly IGrid _gridController;
        private Decimal _pagesCount;
        private int _totalCount;
        private int _pageSize;
        FilterTypes _filtertype;
        #endregion

        #region Propiedades

        public Hashtable GroupByCountList
        {
            get { return _GroupByCountList; }

            set
            {
                _GroupByCountList = value;
                GroupsLevels.Clear();

                if (_GroupByCountList != null && _GroupByCountList.Count > 0)
                {
                    for (int i = 0; i < newGrid.GroupDescriptors.Count; i++)
                        GroupsLevels.Add(i, newGrid.GroupDescriptors[i].GroupNames[0].PropertyName);
                }
                else
                {
                    if (this.NewGrid.GroupDescriptors.Count > 0)
                        this.NewGrid.GroupDescriptors.Clear();
                }
            }
        }

        public bool FirstTimeLoad { get; set; }

        public bool AllowTelerikGridFilter
        {
            set
            {
                NewGrid.EnableFiltering = value;
            }
        }

        public bool ShowFiltersPanel
        {
            set
            {
                btnHideFilter.Visible = value;
            }
        }

        /// <summary>
        ///   Especifica el formato de la fecha
        /// </summary>
        public Boolean ShortDateFormat
        {
            get { return _shortDateFormat; }
            set { _shortDateFormat = value; }
        }

        /// <summary>
        /// Grilla siempre autoajustada
        /// </summary>
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

        /// <summary>
        /// Si se usa zamba, el comportamiento de la grilla sera distinto
        /// </summary>
        public Boolean UseZamba
        {
            private get
            {
                if (newGrid != null)
                {
                    return newGrid.UseZamba;
                }
                return false;
            }
            set
            {
                if (newGrid != null)
                    newGrid.UseZamba = value;

            }
        }

        /// <summary>
        /// Datos de la grilla
        /// </summary>
        public DataTable DataTable
        {
            get { return _dataTable; }
            set { _dataTable = value; }
        }

        /// <summary>
        /// Si se utiliza excel o no (para el exportar)
        /// </summary>
        public Boolean WithExcel
        {
            get { return _withExcel; }
            set
            {
                btnExport.Visible = value;
                _withExcel = value;
            }
        }

        /// <summary>
        /// Si se muestra o no el boton actualizar
        /// </summary>
        public Boolean showRefreshButton
        {
            get { return _showRefresh; }
            set
            {
                btnRefresh.Visible = value;
                _showRefresh = value;
            }
        }

        /// <summary>
        /// Si se utiliza o no color (para las tareas vencidas)
        /// </summary>
        public Boolean UseColor
        {
            get { return newGrid.useColor; }
            set { newGrid.useColor = value; }
        }

        /// <summary>
        /// Grilla donde se muestran los datos
        /// </summary>
        public RadGridView NewGrid
        {
            get { return newGrid; }
        }

        /// <summary>
        /// Propiedad DataSource.
        /// </summary>
        public object DataSource
        {
            get { return newGrid.DataSource; }

            set
            {
                try
                {
                    if (currentFont == null)
                    {
                        currentFont = newGrid.Font;

                        string size = UserPreferences.getValue("GridFontSize", UPSections.UserPreferences, currentFont.Size.ToString(CultureInfo.InvariantCulture));
                        if (!string.IsNullOrEmpty(size) && size.Contains(","))
                            size = size.Replace(",", ".");

                        if (float.Parse(size) > 100) size = "9";

                        currentFontSize = float.Parse(size, NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
                        currentFont = new Font(currentFont.FontFamily, currentFontSize, currentFont.Style);
                        currentRowHeight = (int)(currentFontSize * 3.5);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }

                if (value == null)
                {
                    InitializeLogic(new DataTable(), true);
                }
                else
                {
                    newGrid.LastHorizontalScrollValue = newGrid.TableElement.HScrollBar.Value;
                    InitializeLogic((DataTable)value, true);
                    if (newGrid.LastHorizontalScrollValue > newGrid.TableElement.HScrollBar.Minimum && newGrid.LastHorizontalScrollValue < newGrid.TableElement.HScrollBar.Maximum)
                        newGrid.TableElement.HScrollBar.Value = newGrid.LastHorizontalScrollValue;
                }

                if (value != null && _gridController.LastPage == 0)
                {

                    if (NewGrid.Columns.Contains(GridColumns.VER_COLUMNNAME))
                    {
                        NewGrid.Columns[GridColumns.VER_COLUMNNAME].AllowSort = false;
                        NewGrid.Columns[GridColumns.VER_COLUMNNAME].AllowGroup = false;
                    }
                    if (NewGrid.Columns.Contains(GridColumns.IMAGEN_COLUMNNAME))
                    {
                        NewGrid.Columns[GridColumns.IMAGEN_COLUMNNAME].AllowSort = false;
                        NewGrid.Columns[GridColumns.IMAGEN_COLUMNNAME].AllowGroup = false;
                    }
                    if (NewGrid.Columns.Contains(GridColumns.SITUACIONICON_COLUMNNAME))
                    {
                        NewGrid.Columns[GridColumns.SITUACIONICON_COLUMNNAME].AllowSort = false;
                        NewGrid.Columns[GridColumns.SITUACIONICON_COLUMNNAME].AllowGroup = false;
                    }

                    if (!fromSort)
                        FixColumns();

                    _totalCount = DataTable.MinimumCapacity;
                    _pageSize = Int32.Parse(UserPreferences.getValue("CantidadFilas", UPSections.UserPreferences, 100));
                    SetPagesCount(this._totalCount, this._pageSize);
                    SetRegisterText();

                }


                FlagUnreadDocuments();
            }
        }

        public Int32 FilterCount
        {
            get { return lsvFilters.Items.Count; }
        }

        public FilterTypes filterType
        {
            get
            {
                return _filtertype;
            }
            set
            {
                _filtertype = value;
            }
        }

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
        #endregion

        #region Constructor & load

        public GroupGrid(Boolean useExcel, Int64 currentUserId, ref IGrid gridController, FilterTypes FType)
        {
            try
            {
                _gridController = gridController;
                InitializeComponent();
                ApplyStyles();
                filterType = FType;
                _currentUserId = currentUserId;
                if (useExcel)
                    btnExport.Visible = true;
                _withExcel = useExcel;
                HideFiltersAtStart();
                GroupsLevels = new Hashtable();
                cmbOpTooltip = new ToolTip();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void ApplyStyles()
        {
            try
            {
                btnAdd.BackColor = AppBlock.ZambaUIHelpers.GetToolbarsAndButtonsColor();
                btnAdd.Font = AppBlock.ZambaUIHelpers.GetFontFamily();
                btnAdd.ForeColor = AppBlock.ZambaUIHelpers.GetToolbarsAndButtonsFontColor();

                btnRemoveAllFilters.BackColor = AppBlock.ZambaUIHelpers.GetToolbarsAndButtonsColor();
                btnRemoveAllFilters.Font = AppBlock.ZambaUIHelpers.GetFontFamily();
                btnRemoveAllFilters.ForeColor = AppBlock.ZambaUIHelpers.GetToolbarsAndButtonsFontColor();

                btnUncheckAllFilters.BackColor = AppBlock.ZambaUIHelpers.GetToolbarsAndButtonsColor();
                btnUncheckAllFilters.Font = AppBlock.ZambaUIHelpers.GetFontFamily();
                btnUncheckAllFilters.ForeColor = AppBlock.ZambaUIHelpers.GetToolbarsAndButtonsFontColor();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void Form1Load(object sender, EventArgs e)
        {
            try
            {
                if (_alwaysFit && this.newGrid.Rows.Count > 0)
                    FixColumns();

                this.newGrid.MasterTemplate.EnableSorting = true;

                // ivan - 07/12/15. Suscribo los eventos de la grilla.
                this.newGrid.TableElement.VScrollBar.Scroll += VScrollBar_Scroll;
                this.newGrid.MouseWheel += newGrid_MouseWheel;
                this.NewGrid.SortChanged += NewGrid_SortChanged;
                this.NewGrid.GroupByChanged += NewGrid_GroupByChanged;
                this.NewGrid.GroupSummaryEvaluate += NewGrid_GroupSummaryEvaluate;
                this.NewGrid.GroupExpanding += NewGrid_GroupExpanding;
                this.newGrid.GridDataLoadedEvent += NewGrid_GridDataLoadedEvent;

                //ivan - 07 / 12 / 15.si usa color susbscribo el evento
                //if (UseColor)
                this.newGrid.RowFormatting += newGrid_RowFormatting;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void NewGrid_GridDataLoadedEvent(object sender, EventArgs e)
        {
            try
            {
                newGrid.BeginEdit();
                newGrid.BeginUpdate();
                if (bool.Parse(UserPreferences.getValue("AutoResizeGridRows", UPSections.UserPreferences, false)))
                {
                    this.newGrid.AutoSizeRows = true;
                }
                else
                {
                    this.newGrid.AutoSizeRows = false;
                    //if (NewGrid.Rows.Count > 0 && NewGrid.Columns.Contains("I") && NewGrid.Rows[0].Cells["I"].Value != null && NewGrid.Rows[0].Cells["I"].Value != DBNull.Value)
                    //{
                    //    this.newGrid.TableElement.RowHeight = ((Bitmap)NewGrid.Rows[0].Cells["I"].Value).Height + 8;
                    //}
                    //else
                    //{
                    //    //this.newGrid.TableElement.RowHeight = int.Parse(UserPreferences.getValue("GridRowHeight", Sections.UserPreferences, "27"));
                    //    this.newGrid.TableElement.Font = currentFont;//new Font(currentFont.FontFamily, currentFontSize);
                    //    this.newGrid.TableElement.RowHeight = currentRowHeight;
                    //    //this.newGrid.TableElement.Font.Size = currentFontSize;
                    //    //this.newGrid.TableElement.Font.FontFamily = currentFont;
                    //}
                    this.newGrid.TableElement.Font = currentFont;//new Font(currentFont.FontFamily, currentFontSize);
                    this.newGrid.TableElement.RowHeight = currentRowHeight;
                    //    //this.newGrid.TableElement.Font.Size = currentFontSize;
                }
                newGrid.EndUpdate();
                newGrid.EndEdit();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void RefreshGridWithOrder()
        {
            fromSort = true;
            string orderString = string.Empty;

            StringBuilder groupby = new StringBuilder();
            string orderby = string.Empty;

            IList groupDescriptors = NewGrid.GroupDescriptors;
            SortDescriptor sortDescriptor = NewGrid.SortDescriptors.Count > 0 ? NewGrid.SortDescriptors[0] : null;

            if (groupDescriptors != null)
            {
                foreach (var item in groupDescriptors)
                {
                    groupby.Append(GridColumns.AddOrderColumnsQuote(item.ToString().Trim()));
                    groupby.Append(",");
                }
            }

            if (sortDescriptor != null)
                orderby = CreateSortString(sortDescriptor.PropertyName, sortDescriptor.Direction.ToString());

            orderString = (groupby.ToString().Trim() + " " + orderby.Trim()).Trim();

            if (orderString.EndsWith(",", StringComparison.OrdinalIgnoreCase))
                orderString = orderString.Substring(0, orderString.Length - 1);

            _gridController.AddOrderComponent(orderString);
            _gridController.AddGroupByComponent(groupby.ToString());
            _gridController.SortChanged = true;
            ReLoadGrid(true);
            fromSort = false;
            _gridController.SortChanged = false;
        }

        public void ResetGrid()
        {
            try
            {
                this.NewGrid.SortChanged -= NewGrid_SortChanged;
                this.NewGrid.GroupByChanged -= NewGrid_GroupByChanged;
                NewGrid.DataSource = null;
                NewGrid.GroupDescriptors.Clear();
                NewGrid.SortDescriptors.Clear();
                if (GroupByCountList != null)
                    GroupByCountList.Clear();
                if (GroupsLevels != null)
                    GroupsLevels.Clear();
                this.NewGrid.SortChanged += NewGrid_SortChanged;
                this.NewGrid.GroupByChanged += NewGrid_GroupByChanged;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        public void RemoveOrder()
        {
            try
            {
                NewGrid.MasterTemplate.SortDescriptors.Clear();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void NewGrid_GroupByChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            try
            {
                RefreshGridWithOrder();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void NewGrid_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            try
            {
                if (GroupByCountList != null && GroupsLevels.Count > 0)
                {
                    String FullGroupDescriptor = string.Empty;

                    for (int i = 0; i < GroupsLevels.Count; i++)
                    {
                        FullGroupDescriptor += GroupsLevels[i];
                        if (i < GroupsLevels.Count - 1)
                            FullGroupDescriptor += ",";
                    }

                    string groupLabel = string.Empty;

                    if (e.Group.Groups.Count > 0) //Si es padre.
                    {
                        int count = 0;
                        foreach (var item in e.Group.Groups)
                        {
                            count += item.ItemCount;
                        }
                        groupLabel = String.Format("{0}: {1}. Grupos: {2}. Datos encontrados: {3}.",
                            e.SummaryItem.Name,
                            e.Value,
                            e.Group.Groups.Count,
                            count);
                    }
                    else if (GroupByCountList.ContainsKey(FullGroupDescriptor))
                    {
                        Hashtable ListOfGroupsWithItsCounts = ((Hashtable)GroupByCountList[FullGroupDescriptor]);

                        Group<GridViewRowInfo> parent = e.Group.Parent;
                        StringBuilder value = new StringBuilder();
                        while (parent != null)
                        {
                            string header = parent.Header != null ? parent.Header.ToString() : string.Empty;
                            value.Insert(0, header + "-");
                            parent = parent.Parent;
                        }

                        value.Append(e.Value != null ? e.Value.ToString().Trim() : string.Empty);

                        if (ListOfGroupsWithItsCounts.ContainsKey(value.ToString()))
                        {
                            Int64 Count = Int64.Parse(ListOfGroupsWithItsCounts[value.ToString()].ToString());
                            groupLabel = String.Format("{0}: {1}, datos encontrados: {2}.",
                                e.SummaryItem.Name,
                                e.Value != null ? e.Value.ToString().Trim() : string.Empty,
                                Count);
                        }
                    }

                    e.FormatString = groupLabel;
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void NewGrid_GroupExpanding(object sender, GroupExpandingEventArgs e)
        {

        }

        private void NewGrid_SortChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            try
            {
                RefreshGridWithOrder();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private string CreateSortString(string Column, string direction)
        {
            if (!string.IsNullOrEmpty(direction))
            {
                string orderDirection = string.Empty;
                if (direction == "Ascending")
                {
                    orderDirection = Enum.GetName(typeof(OrderSorts), OrderSorts.ASC);
                }
                else
                {
                    orderDirection = Enum.GetName(typeof(OrderSorts), OrderSorts.DESC);
                }

                return GridColumns.AddOrderColumnsQuote(Column + " " + orderDirection);
            }

            return string.Empty;
        }



        #endregion

        #region ReloadGrid

        private void VScrollBar_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (MustReloadGrid())
                    ReLoadGrid(false);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void newGrid_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                // Si hay filas seleccionadas
                if (this.newGrid != null && this.newGrid.SelectedRows.Count > 0)
                {
                    if (MustReloadGrid())
                        ReLoadGrid(false);

                    SetRegisterText();
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void newGrid_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (MustReloadGrid())
                    ReLoadGrid(false);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }


        private bool MustReloadGrid()
        {
            newGrid.CurrentVerticalScrollValue = newGrid.TableElement.VScrollBar.Value;
            newGrid.CurrentHorizontalScrollValue = newGrid.TableElement.HScrollBar.Value;

            if (newGrid.LastVerticalScrollValue == 0)
                newGrid.LastVerticalScrollValue = newGrid.CurrentVerticalScrollValue;
            if (newGrid.LastHorizontalScrollValue == 0)
                newGrid.LastHorizontalScrollValue = newGrid.CurrentHorizontalScrollValue;

            if ((ReachedBottom() && NewGrid.Groups.Count <= 0) || (ReachedBottom() && ThereIsAGroupOpen()))
                if (newGrid.CurrentVerticalScrollValue > newGrid.LastVerticalScrollValue)
                    return true;

            newGrid.LastVerticalScrollValue = newGrid.CurrentVerticalScrollValue;
            newGrid.LastHorizontalScrollValue = newGrid.LastHorizontalScrollValue;
            return false;
        }

        private bool ThereIsAGroupOpen()
        {
            if (newGrid.Groups.Count > 0)
            {
                foreach (DataGroup group in newGrid.Groups)
                {
                    if (group.IsExpanded)
                        return true;
                }
            }
            return false;
        }

        private bool ReachedBottom()
        {
            RadScrollBarElement scrollbar = newGrid.TableElement.RowScroller.Scrollbar;
            if (scrollbar.Maximum <= newGrid.CurrentVerticalScrollValue + scrollbar.LargeChange)
            {
                return true;
            }
            return false;
        }

        //RadWaitingBar WB = new RadWaitingBar();
        //Form Waitingform;

        public void ReLoadGrid(bool mustClearGrid)
        {
            try
            {
                //Waitingform = new Form();
                //Waitingform.StartPosition = FormStartPosition.CenterScreen;
                //Waitingform.ShowDialog();

                //WB.WaitingStyle = Telerik.WinControls.Enumerations.WaitingBarStyles.DotsLine;
                //WB.WaitingDirection = ProgressOrientation.Left;
                //WB.AssociatedControl = this.newGrid;
                //WB.Dock = DockStyle.Fill;
                //WB.StartWaiting();

                this.Cursor = Cursors.WaitCursor;
                this.SuspendLayout();
                int LastHorizontalScrollPosition = (newGrid.CurrentHorizontalScrollValue == 0) ? newGrid.TableElement.HScrollBar.Value : newGrid.CurrentHorizontalScrollValue;

                if (mustClearGrid)
                {
                    // Cargo la grilla limpiando los datos actuales.
                    ReloadTasks(mustClearGrid);
                    // Vuelvo a pararme en la primer fila de la grilla, mantengo la posicion horizontal.
                    newGrid.TableElement.VScrollBar.Value = 0;
                    newGrid.TableElement.HScrollBar.Value = LastHorizontalScrollPosition;
                    // Selecciono la primer fila.
                    if (NewGrid.Rows.Count > 0)
                        NewGrid.Rows[0].IsSelected = true;
                }
                else
                {
                    int LastVerticalScrollPosition = newGrid.CurrentVerticalScrollValue;
                    //LastHorizontalScrollPosition = newGrid.LastHorizontalScrollValue;
                    // Cargo la grilla sin borrar datos actuales (recarga).
                    ReloadTasks(mustClearGrid);
                    // Me paro en la misma posicion que estaba antes de recargar.
                    newGrid.TableElement.VScrollBar.Value = LastVerticalScrollPosition;
                    newGrid.TableElement.HScrollBar.Value = LastHorizontalScrollPosition;

                    newGrid.LastVerticalScrollValue = newGrid.CurrentVerticalScrollValue;
                    newGrid.LastHorizontalScrollValue = newGrid.LastHorizontalScrollValue;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                reloadGrid = false;
                _gridController.FiltersChanged = reloadGrid;
                this.ResumeLayout();
                this.Cursor = Cursors.Default;
                //Waitingform.Close();
                //Waitingform = null;
                //WB.StopWaiting();
            }
        }

        private void ReloadTasks(bool mustClearGrid)
        {
            if (mustClearGrid)
                _gridController.LastPage = 0;
            else
                _gridController.LastPage++;

            reloadGrid = mustClearGrid;
            _gridController.FiltersChanged = reloadGrid;
            _gridController.SaveSearch = false;
            _gridController.ShowTaskOfDT();
            _gridController.SaveSearch = true;
        }

        #endregion ReloadGrid

        #region SortGrid



        #endregion

        /// <summary>
        /// si se muestra el boton selectAll 
        /// </summary>
        /// <param name="blnShow"></param>
        public void ShowSelectBtn(Boolean blnShow)
        {
            btnSelectAll.Visible = blnShow;
            ToolStripSeparator1.Visible = false;
        }

        /// <summary>
        /// Marca los documentos no leidos en negrita
        /// </summary>
        public void FlagUnreadDocuments()
        {
            //Verifica si debe buscar los documentos
            if (newGrid.Columns.Contains("READDATE") && newGrid.RowCount > 0)
            {
                //Obtiene la posicion de la columna que contiene la marca de leida
                int readColPosition = newGrid.Columns["READDATE"].Index;
                // Crea objetos formateadores
                ConditionalFormattingObject objFormato1 = new ConditionalFormattingObject("negrita vacios", ConditionTypes.Equal, string.Empty, "", true);
                ConditionalFormattingObject objFormato2 = new ConditionalFormattingObject("negrita nulos", ConditionTypes.Equal, null, "", true);
                // Aplico el estilo de la fuente a los formateadores
                objFormato1.RowFont = new Font(newGrid.Font, FontStyle.Bold);
                objFormato2.RowFont = new Font(newGrid.Font, FontStyle.Bold);
                // Agrego los objFormato a la columna a verificar
                newGrid.Columns[readColPosition].ConditionalFormattingObjectList.Add(objFormato1);
                newGrid.Columns[readColPosition].ConditionalFormattingObjectList.Add(objFormato2);
            }
        }

        /// <summary>
        /// Le quita la negrita a los documentos leidos
        /// </summary>
        public void UnFlagUnreadDocuments()
        {
            //Verifica si debe buscar los documentos
            if (newGrid.Columns.Contains("READDATE") && newGrid.RowCount > 0)
            {
                //Obtiene la posicion de la columna que contiene la marca de leida
                int readColPosition = newGrid.Columns["READDATE"].Index;
                // Elimina los objetos formateadores de la columna.
                newGrid.Columns[readColPosition].ConditionalFormattingObjectList.Clear();
            }
        }

        /// <summary>
        /// Bindea los datos a grilla y carga el comboBox de filtros.
        /// </summary>
        /// <param name = "datatable"></param>
        private void InitializeLogic(DataTable datatable, Boolean useOriginal)
        {
            try
            {
                if (_gridController.LastPage == 0)
                {
                    bool loadCmbFilterColumn = false;
                    this.DataTable = datatable;
                    newGrid.BindData(datatable, null, _shortDateFormat, _gridController.LastPage, reloadGrid);

                    if (_dataTable != null && _dataTable.Rows.Count > 0)
                    {
                        if (_dataTable.Columns.Contains("Doctypeid"))
                        {
                            LoadCmbFilterColumn(_dataTable.Rows[0]["Doctypeid"].ToString());
                            loadCmbFilterColumn = true;
                        }
                        else if (_dataTable.Columns.Contains("DOC_TYPE_ID"))
                        {
                            if (_dataTable.Rows[0]["DOC_TYPE_ID"] != DBNull.Value)
                                LoadCmbFilterColumn(_dataTable.Rows[0]["DOC_TYPE_ID"].ToString());
                            loadCmbFilterColumn = true;
                        }

                        //Desde Report Builder
                        if (!loadCmbFilterColumn)
                            LoadCmbFilterColumn(null);
                    }

                    SetRegisterText();

                    if (visibleColumns != null && visibleColumns.Count > 0)
                    {
                        this.cmbSearchColumn.ComboBox.DataSource = visibleColumns;
                        if (this.cmbSearchColumn.ComboBox.Items.Count > 0)
                            this.cmbSearchColumn.ComboBox.SelectedIndex = 0;
                    }
                }
                else
                {
                    newGrid.BindData(datatable, null, _shortDateFormat, _gridController.LastPage, reloadGrid);
                    this.DataTable = newGrid.tabla;
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
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
                if (newGrid.Columns.Contains(name))
                {
                    newGrid.Columns[name].IsVisible = bolVisible;
                }

                if (bolVisible == false)
                {
                    newGrid.ColumnsVisible.Add(name);
                }
                else
                {
                    newGrid.ColumnsVisible.Remove(name);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
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
                newGrid.ColumnsFixed.Remove(name);

                if (bolFixed && newGrid.Columns.Contains(name))
                {
                    newGrid.Columns[name].Width = width;
                    newGrid.ColumnsFixed.Add(name, width);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        ///   Ajusta el ancho de las columnas
        /// </summary>
        /// <history> 
        ///   [Gaston]    12/08/2008   Modified    
        ///   ivan - 07/12/15. Modifique el metodo para que si no viene el tamaño desde la db,
        ///   la ajuste al contenido, usando metodo propio de telerik.
        /// </history>
        public void FixColumns()
        {
            try
            {
                newGrid.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
                if (newGrid != null && newGrid.ColumnCount > 0)
                {
                    int anchoTotal = 0;
                    foreach (GridViewDataColumn col in newGrid.Columns)
                    {
                        if (col.IsVisible)
                        {
                            // Si está el tamaño de esa columna
                            if (newGrid.ColumnsFixed[col.Name] != null)
                            {
                                // Le asigno el tamaño que viene desde la db
                                if (Int32.Parse(newGrid.ColumnsFixed[col.Name].ToString()) <= 2000)
                                    newGrid.Columns[col.Name].Width = Int32.Parse(newGrid.ColumnsFixed[col.Name].ToString());
                                else
                                    newGrid.Columns[col.Name].Width = 2000;
                            }
                            else
                            {
                                // Sino ajusto ancho al contenido
                                newGrid.Columns[col.Name].BestFit();
                                if (col.Width > 2000)
                                    newGrid.Columns[col.Name].Width = 2000;
                            }
                            anchoTotal += col.Width;
                        }
                    }

                    if (anchoTotal < newGrid.Parent.Width)
                        newGrid.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                }

                btnSelectAll.Tag = ZERO_VALUE;

                if (newGrid != null)
                {
                    newGrid.Tag = ZERO_VALUE;
                    btnSelectAll.Text = Resources.GroupGrid_FixColumns_Seleccionar_todos;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
        /// <summary>
        /// Setea el label que muestra cantidad de filas de la grilla,
        /// y en que fila estamos parados.
        /// </summary>
        public void SetRegisterText()
        {
            try
            {
                if (this.DataTable != null)
                {
                    if (newGrid.Rows.Count >= 0 && newGrid.SelectedRows.Count > 0)
                    {
                        lblRows.Text = MSG_REG_TEXT + (newGrid.SelectedRows[0].Index + 1) + " de " + _dataTable.MinimumCapacity;
                    }
                    else
                    {
                        lblRows.Text = "No hay registros disponibles";
                    }

                    //SetPaging(this._totalCount, this._dataTable.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        ///   Cargo el combo de filtros
        /// </summary>
        public void LoadCmbFilterColumn(String docTypeId)
        {
            try
            {
                if (docTypeId == null || docTypeId != _oldDocTypeId)
                {
                    _oldDocTypeId = docTypeId;

                    if (cmbFilterColumn != null)
                    {
                        cmbFilterColumn.Items.Clear();

                        var imagenUserConfig = UserPreferences.getValue("ColumnNameImagen", UPSections.UserPreferences, "Imagen");
                        var verUserConfig = UserPreferences.getValue("ColumnNameVer", UPSections.UserPreferences, VER_LABEL);
                        var situacionUserConfig = UserPreferences.getValue("ColumnNameSituacion", UPSections.UserPreferences, "Situacion");

                        hiddenColumns = new List<string>();
                        visibleColumns = new List<string>();

                        foreach (var item in GridColumns.ColumnsVisibility)
                        {
                            if (!item.Value)
                            {
                                hiddenColumns.Add(item.Key);
                            }
                        }

                        foreach (DataColumn columna in this.DataTable.Columns)
                        {
                            int aux;
                            // Si esta entre las columnas ocultas, o es una imagen, o empieza con I y los siguientes caracteres son numeros o es la columna VER
                            if (hiddenColumns.Contains(columna.ColumnName.ToLower()) ||
                                columna.DataType.ToString() == "System.Drawing.Image" ||
                                columna.ColumnName == GridColumns.VER_COLUMNNAME ||
                                (columna.ColumnName.StartsWith("I") &&
                                int.TryParse(columna.ColumnName.Remove(0, 1), out aux)))
                                continue;

                            visibleColumns.Add(columna.ToString());
                            //this.cmbFilterColumn.Items.Add(columna.ToString());
                            //this.cmbSearchColumn.Items.Add(columna.ToString());
                        }

                        if (visibleColumns != null && visibleColumns.Count > 0)
                        {
                            this.cmbFilterColumn.Items.AddRange(visibleColumns.ToArray());
                        }

                        String lastFilterIndexUsed = UserPreferences.getValue("LastFilterIndexUsed", UPSections.Filters, String.Empty);

                        if (lastFilterIndexUsed != String.Empty && cmbFilterColumn.Items.Contains(lastFilterIndexUsed))
                        {
                            if (cmbOperator.Items.Count > 0)
                                cmbOperator.SelectedIndex = 8;

                            if (cmbFilterColumn.Items.Contains(lastFilterIndexUsed))
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
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show("Ha ocurrido un error al cargar los filtros de la entidad", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Obtiene un filtro
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private FilterElem GetFilter(String key)
        {
            try
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
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
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
        ///   ivan - 07/12/15 - Modified - Hago uso de la bandera 'esPorFiltro', seteo la ultima pagina en cero, 
        ///   y luego que se recarga la grilla quitando los filtros, vuelvo a poner la bandera en false.
        /// </history>
        private void BtnRemoveClick(object sender, EventArgs e)
        {
            reloadGrid = true;
            _gridController.FiltersChanged = reloadGrid;
            _gridController.LastPage = 0;
            RemoveAllFilters();
            reloadGrid = false;
            _gridController.FiltersChanged = reloadGrid;
        }

        /// <summary>
        ///   Quita todos los filtros manuales
        /// </summary>
        /// ivan - 07/12/15 - Modified - agregue uso de la bandera 'esPorFiltro', seteo ultima pagina en cero
        /// y luego que recarga la grilla vuelto a setear la bandera en false.
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
                    _gridController.Fc.ClearFilters(DocTypeId, Zamba.Membership.MembershipHelper.CurrentUser.ID, filterType, DataTable, UserBusiness.CanRemoveDefaultFilters(docTypeIds));
                    //Se los elimina visualmente del cliente.
                    foreach (FilterElem item in filtersToRemove)
                    {
                        lsvFilters.Items.Remove(item);
                    }

                    //Se refresca la grilla.
                    reloadGrid = true;
                    _gridController.FiltersChanged = reloadGrid;
                    _gridController.LastPage = 0;
                    _gridController.ShowTaskOfDT();
                    reloadGrid = false;
                    _gridController.FiltersChanged = reloadGrid;

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
                MessageBox.Show(MSG_REMOVE_FILTER, MSG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// ivan - 07/12/15 - Modified - agregue uso de la bandera 'esPorFiltro', seteo ultima pagina en cero
        /// y luego que recarga la grilla vuelto a setear la bandera en false.
        private void RemoveAllFilters()
        {
            try
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
                        _gridController.Fc.ClearFilters(DocTypeId, _currentUserId, filterType, DataTable, removeDefaultFilters);

                        //Se remueven los filtros visualmente.
                        lsvFilters.Items.Clear();

                        //Se refresca la grilla.
                        if (flagAnyEnabled)
                        {
                            reloadGrid = true;
                            _gridController.FiltersChanged = reloadGrid;
                            _gridController.LastPage = 0;
                            this.SuspendLayout();
                            _gridController.ShowTaskOfDT();
                            ShowFilters();
                            this.ResumeLayout();
                            reloadGrid = false;
                            _gridController.FiltersChanged = reloadGrid;
                        }

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
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show("Ha ocurrido un error al quitar todos los filtros", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (lsvFilters.Items.Count == 0)
                return false;
            else
                return (lsvFilters.CheckedItems.Count > 0);
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
            //else
            //    HideFiltersAtStart();
        }

        /// <summary>
        ///   Devuelve si hay algun filtro por defecto
        /// </summary>
        /// <returns></returns>
        private bool HasDefaultFilters()
        {
            try
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
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }

        /// <summary>
        ///   Remueve el filtro seleccionado y actualiza.
        /// </summary>
        /// <history> 
        ///   [Tomas]     31/03/2009  Created
        ///   [sebastian] 03-04-2009 se aplico el permiso para borrar filtros
        ///   Marcelo     30/04/2009  Modified    Se modifico la validacion de los permisos porque no funcionaba
        ///   Sebastian  01-10-09 Modified reload filters after remove one
        ///   ivan - 07/12/15 - Modified - agregue uso de la bandera 'esPorFiltro', seteo ultima pagina en cero
        ///   y luego que recarga la grilla vuelto a setear la bandera en false.
        /// </history>
        private void RemoveSelectedFilter()
        {
            try
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
                    _gridController.Fc.RemoveFilter(CurrentFilter, filterType);
                    if (lsvFilters.SelectedItems.Count > 0)
                        lsvFilters.Items.Remove(lsvFilters.SelectedItem);

                    //Si la grilla esta dentro del la grilla c/ paginado
                    if ((CurrentFilter).Enabled == true)
                    {
                        reloadGrid = true;
                        _gridController.FiltersChanged = reloadGrid;
                        _gridController.LastPage = 0;
                        _gridController.ShowTaskOfDT();
                        reloadGrid = false;
                        _gridController.FiltersChanged = reloadGrid;
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show("Ha ocurrido un error al quitar el filtro seleccionado", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                lsvFilters.Items.Clear();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show("Ha ocurrido un error al quitar los filtros", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Visualiza los filtros cargados.
        /// </summary>
        /// <param name = "DocTypes"></param>
        public void ReloadLsvFilters()
        {
            //Se remueven los filtros cargados.
            ClearFilters();

            AddDocTypeFilters(DocTypeId);

            ShowFiltersIfExists();
        }

        /// <summary>
        /// Visualiza los filtros cargados indicandole los tipos de documento a verificar.
        /// </summary>
        /// <param name = "DocTypes"></param>
        public void ReloadLsvFilters(System.Collections.Generic.List<Int64> dtIds)
        {
            //Se remueven los filtros cargados.
            ClearFilters();

            for (int i = 0; i < dtIds.Count; i++)
                AddDocTypeFilters(dtIds[i]);

            ShowFiltersIfExists();
        }

        private void AddDocTypeFilters(Int64 dtId)
        {
            if (_gridController.Fc.GetDocumentFiltersCount(dtId, filterType) > 0)
            {
                //Se agregan los filtros.
                lsvFilters.ItemCheck -= LsvFiltersItemCheck;
                foreach (var fe in _gridController.Fc.GetLastUsedFilters(dtId, _currentUserId, filterType))
                {
                    lsvFilters.Items.Add(fe, fe.Enabled);
                }
                lsvFilters.ItemCheck += LsvFiltersItemCheck;
                btnRemoveAllFilters.Enabled = true;
            }
        }


        private static IndexDataType GetZColumnType(string colName, FilterTypes filterType)
        {

            var nombreDocumentoUserConfig = GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME;
            var imagenUserConfig = GridColumns.IMAGEN_COLUMNNAME;
            var verUserConfig = GridColumns.VER_COLUMNNAME;
            var estadoTareaUserConfig = GridColumns.STATE_COLUMNNAME;
            var asignadoUserConfig = GridColumns.ASIGNADO_COLUMNNAME;
            var situacionUserConfig = GridColumns.SITUACION_COLUMNNAME;
            var nombreOriginalUserConfig = GridColumns.ORIGINAL_FILENAME_COLUMNNAME;
            var tipoDocumentoUserConfig = GridColumns.DOC_TYPE_NAME_COLUMNNAME;
            var fechaCreacionUserConfig = GridColumns.CRDATE_COLUMNNAME;
            var fechaModificacionUserConfig = GridColumns.LASTUPDATE_COLUMNNAME;
            var versionUserConfig = GridColumns.VERSION_COLUMNNAME;
            var nroVersionUserConfig = GridColumns.NUMERO_DE_VERSION_COLUMNNAME;
            var CheckInUserConfig = GridColumns.CHECKIN_COLUMNNAME;
            var ExpireDateUserConfig = GridColumns.EXPIREDATE_COLUMNNAME;
            var TaskIdUserConfig = UserPreferences.getValue("ColumnNameTaskId", UPSections.UserPreferences, "TareaID");

            if (filterType == FilterTypes.Task)
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
                if (String.Compare(colName, fechaCreacionUserConfig) == 0)
                    return IndexDataType.Fecha_Hora;
                if (String.Compare(colName, fechaModificacionUserConfig) == 0)
                    return IndexDataType.Fecha_Hora;
                if (String.Compare(colName, CheckInUserConfig) == 0)
                    return IndexDataType.Fecha_Hora;
                if (String.Compare(colName, ExpireDateUserConfig) == 0)
                    return IndexDataType.Fecha_Hora;
                if (String.Compare(colName, TaskIdUserConfig) == 0)
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

        /// <summary>
        ///   Deselecciona una fila de la grilla
        /// </summary>
        /// <history>
        ///   Marcelo 09/11/09    created
        /// </history>
        /// <param name = "row">Fila a ser deseleccionada</param>
        public void DeselectRow(GridViewRowInfo row)
        {
            if (newGrid.Columns.Contains(VER_LABEL))
            {
                if (newGrid != null)
                    if (newGrid.Columns[VER_LABEL].IsVisible)
                    {
                        row.Cells[VER_LABEL].Value = false;
                    }
            }
            else
                row.IsSelected = false;
        }

        /// <summary>
        ///   Método que activa los checkboxs de las filas seleccionadas
        /// </summary>
        /// <history> 
        ///   [Gaston]    15/09/2008  Created
        /// </history>
        private void ActivateCheckBoxs(int columnindex)
        {
            for (int i = 0; i < newGrid.SelectedRows.Count; i++)
                newGrid.SelectedRows[i].Cells[columnindex].Value = true;

            newGrid.Tag = newGrid.SelectedRows.Count.ToString();
        }

        /// <summary>
        ///   Método que desactiva los checkboxs de las filas seleccionadas
        /// </summary>
        /// <history> 
        ///   [Gaston]    15/09/2008  Created
        /// </history>
        private void DesactivateCheckBoxs(int columnindex)
        {

            for (int i = 0; i < newGrid.SelectedRows.Count; i++)
                newGrid.SelectedRows[i].Cells[columnindex].Value = false;

            newGrid.Tag = ZERO_VALUE;
        }

        private void AddFilter()
        {
            try
            {
                var cantidadFiltrosActuales = lsvFilters.Items.Count;
                IFilterElem fe = null;

                //Agrego el filtro seleccionado
                if (cmbFilterColumn.SelectedItem != null && cmbOperator.SelectedItem != null && pnlCtrlIndex.Controls.Count > 0)
                {
                    String data;
                    if (pnlCtrlIndex.Controls[0] is DisplayindexCtl)
                    {
                        DisplayindexCtl tempIndexCtrl = (DisplayindexCtl)pnlCtrlIndex.Controls[0];

                        if ((tempIndexCtrl.Index.DropDown == IndexAdditionalType.AutoSustitución
                            || tempIndexCtrl.Index.DropDown == IndexAdditionalType.AutoSustituciónJerarquico) && (cmbOperator.SelectedItem == "Contiene" || cmbOperator.SelectedItem == "No Contiene"))
                        {
                            data = string.IsNullOrEmpty(tempIndexCtrl.Index.dataDescriptionTemp) == false
                                    ? tempIndexCtrl.Index.dataDescriptionTemp
                                    : tempIndexCtrl.getText();
                        }
                        else
                        {
                            data = tempIndexCtrl.Index.DataTemp;
                        }

                        tempIndexCtrl = null;
                    }
                    else
                        data = pnlCtrlIndex.Controls[0].Text;



                    String fType;

                    if (filterType == FilterTypes.Document)
                    {
                        fType = "search";
                    }
                    else
                    {
                        fType = "manual";
                    }

                    var docTypeId = DocTypeId;

                    // ivan 1/02/16
                    if (cmbOperator.SelectedItem == "Contiene" || cmbOperator.SelectedItem == "No Contiene")
                    {


                        StringBuilder temporalData = null;

                        //Si no es un OR y tiene espacio en blanco es un Y
                        if (!data.Contains(",") && data.Contains(" "))
                        {
                            temporalData = new StringBuilder();
                            data = data.Trim();

                            // Se separan las palabras por las que filtrar
                            foreach (var palabra in data.Split(' '))
                            {
                                if (palabra.Trim().Length > 0)
                                {
                                    // Se agregan al stringBuilder
                                    temporalData.Append(palabra.Trim());
                                    // Se le agrega entre medio el %
                                    temporalData.Append("%");
                                }
                            }
                            // Se remueve el % que se agrega al final
                            temporalData.Remove(temporalData.Length - 1, 1);
                        }

                        if (temporalData != null && !string.IsNullOrEmpty(temporalData.ToString()))
                        {
                            data = temporalData.ToString();
                        }
                    }

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
                                pnl.Index.DropDown, fType, filterType);
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
                                selectedIndex.Type == IndexDataType.Numerico ||
                                selectedIndex.Type == IndexDataType.Alfanumerico ||
                                selectedIndex.Type == IndexDataType.Alfanumerico_Largo)
                            {
                                fe = _gridController.Fc.SetNewFilter(
                                    selectedIndex.ID,
                                    GridColumns.GetColumnNameByAliasName(cmbFilterColumn.Text.Trim()),
                                    selectedIndex.Type,
                                    _currentUserId,
                                    cmbOperator.SelectedItem.ToString(),
                                    data,
                                    docTypeId,
                                    UseZamba,
                                    selectedIndex.Name,
                                    selectedIndex.DropDown, fType, filterType);
                            }
                        }
                        else
                        {
                            var filterColumnText = cmbFilterColumn.Text.Trim();
                            fe = _gridController.Fc.SetNewFilter(
                                0,
                                GridColumns.GetColumnNameByAliasName(filterColumnText),
                                GetZColumnType(filterColumnText, filterType),
                                _currentUserId,
                                cmbOperator.SelectedItem.ToString(),
                                data,
                                docTypeId,
                                UseZamba,
                                filterColumnText,
                                IndexAdditionalType.NoIndex, fType, filterType);
                        }
                    }
                    else
                    {
                        var filterColumnText = cmbFilterColumn.Text.Trim();
                        fe = _gridController.Fc.SetNewFilter(
                            0,
                            GridColumns.GetColumnNameByAliasName(filterColumnText),
                            GetZColumnType(filterColumnText, filterType),
                            _currentUserId,
                            cmbOperator.SelectedItem.ToString(),
                            data,
                            docTypeId,
                            UseZamba,
                            filterColumnText,
                            IndexAdditionalType.NoIndex, fType, filterType);
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
                        MessageBox.Show(MSG_ADD_FILTER, MSG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (fe != null)
                        {
                            //Remuevo el filtro seleccionado
                            _gridController.Fc.RemoveFilter(fe, filterType);
                            if (lsvFilters.Items.Count > 0)
                                lsvFilters.Items.Remove(fe);

                            _gridController.ShowTaskOfDT();
                        }
                    }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show("Ha ocurrido un error al agregar un filtro", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        (cmbOperator.SelectedItem == "Contiene" || cmbOperator.SelectedItem == "No Contiene" || cmbOperator.SelectedItem == "Alguno"))
                {
                    var txtValue = new TextBox();
                    txtValue.Dock = DockStyle.Fill;
                    RadButton btnhelp = new RadButton();
                    btnhelp.ToolTipTextNeeded += Btnhelp_ToolTipTextNeeded;
                    pnlCtrlIndex.Controls.Clear();
                    pnlCtrlIndex.Controls.Add(txtValue);
                    //txtValue.KeyPress += CtrlEnterPressed;
                }
                else
                {
                    //Si el atributo es del mismo tipo que el anterior mantengo el valor cargado, por si me confundi de indice al seleccionar pero ya cargue el valor a filtrar
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
                        Boolean.Parse(UserPreferences.getValue("GridShortDateFormat", UPSections.UserPreferences, true)))
                    {
                        selectedIndex.Type = IndexDataType.Fecha;
                    }

                    var ctrl = new DisplayindexCtl(selectedIndex, true, string.Empty, false) { Dock = DockStyle.Fill };


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
                    //txtValue.KeyPress += CtrlEnterPressed;
                    //AGREGAR EL EVENTO DEL ENTER Y EL TAB
                }
                else
                {
                    //Si el atributo es del mismo tipo que el anterior mantengo el valor cargado, por si me confundi de indice al seleccionar pero ya cargue el valor a filtrar
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
                        Boolean.Parse(UserPreferences.getValue("GridShortDateFormat", UPSections.UserPreferences, true)))
                    {
                        selectedIndex.Type = IndexDataType.Fecha;
                    }

                    var ctrl = new DisplayindexCtl(selectedIndex, true, string.Empty, false) { Dock = DockStyle.Fill };


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

            //Pone el foco en el control de texto para que el usuario ingrese el valor por el cual va a filtrar
            if (pnlCtrlIndex != null)
                if (pnlCtrlIndex.Controls.Count > 0)
                    pnlCtrlIndex.SelectNextControl(pnlCtrlIndex.Controls[0], true, true, false, true);
        }

        private void Btnhelp_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = "Para realizar busquedas de varios valores, separando con espacio se buscan todas las palabras, separando con una coma (,) se busca cualquiera de los valores";
        }

        private void CtrlEnterPressed()
        {
            AddFilter();
        }

        private void SetPagesCount(Int32 totalCount, Int32 pageSize)
        {
            try
            {
                if (totalCount > 0 && pageSize > 0)
                {
                    int resto = 0;
                    _pagesCount = Math.DivRem(totalCount, pageSize, out resto);
                    if (resto > 0)
                        _pagesCount++;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Retorna un color de acuerdo al parametro.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private Color ChangeColor(string color)
        {
            switch (color.ToUpper())
            {
                case "ROJO":
                    return (Color.Red);
                case "VERDE":
                    return (Color.Green);
                case "AMARILLO":
                    return (Color.Yellow);
                case "AZUL":
                    return (Color.Blue);
                case "VIOLETA":
                    return (Color.Violet);
                case "GRIS":
                    return (Color.Gray);
            }

            return (Color.Black);
        }

        /// <summary>
        /// [ivan] Metodo para exportar la grilla a excell,
        /// 
        /// Si se crea un archivo con un nombre que ya existe, no se sobreescribe el libro, 
        /// sino que en ese libro se agrega una nueva hoja. Esto se puede cambiar
        /// para que lo sobreescriba, modificando la propiedad FileExportMode.
        /// </summary>
        /// <param name="nombreArchivo">Nombre con el que se va a guardar el archivo</param>
        /// <param name="nombreHoja">Nombre que va a llevar cada hoja del libro de Excell</param>
        /// <returns>True si exporto sin problemas, sino false</returns>
        private bool exportar(string nombreArchivo, int formatoDestino, string nombreHoja = "")
        {
            try
            {
                switch (formatoDestino)
                {
                    case 1:
                        if (exportarExcel(nombreArchivo, nombreHoja))
                            return true;
                        break;
                    case 2:
                        if (exportarPDF(nombreArchivo))
                            return true;
                        break;
                    case 3:
                        if (exportarCSV(nombreArchivo))
                            return true;
                        break;
                    default:
                        return false;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool exportarCSV(string nombreArchivo)
        {
            try
            {
                this.newGrid.SuspendLayout();
                hideCheckAndImageColumn();
                SetColumnsWith();
                SetColumnsType();
                spreadExporter = new GridViewSpreadExport(this.newGrid);
                exportRenderer = new SpreadExportRenderer();
                spreadExporter.ExportFormat = SpreadExportFormat.Csv;
                spreadExporter.FileExportMode = FileExportMode.CreateOrOverrideFile;
                spreadExporter.HiddenColumnOption = HiddenOption.DoNotExport;
                Cursor.Current = Cursors.WaitCursor;
                spreadExporter.RunExport(nombreArchivo, exportRenderer);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                spreadExporter_ExportCompleted();
                this.newGrid.ResumeLayout();
            }
        }

        private bool exportarExcel(string nombreArchivoExportar, string nombreHoja)
        {
            try
            {
                this.newGrid.SuspendLayout();
                hideCheckAndImageColumn();
                SetColumnsWith();
                SetColumnsType();
                spreadExporter = new GridViewSpreadExport(this.newGrid);
                exportRenderer = new SpreadExportRenderer();
                spreadExporter.ExportFormat = SpreadExportFormat.Xlsx;
                spreadExporter.ExportVisualSettings = true;
                spreadExporter.FileExportMode = FileExportMode.CreateOrOverrideFile;
                spreadExporter.HiddenColumnOption = HiddenOption.DoNotExport;
                spreadExporter.HiddenRowOption = HiddenOption.DoNotExport;
                Cursor.Current = Cursors.WaitCursor;

                spreadExporter.RunExport(nombreArchivoExportar, exportRenderer, nombreHoja);

                return true;
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return false;
            }
            finally
            {
                spreadExporter_ExportCompleted();
                this.newGrid.ResumeLayout();
            }
        }

        private void SetColumnsType()
        {
            try
            {
                foreach (GridViewDataColumn column in newGrid.Columns)
                {
                    foreach (GridViewDataRowInfo row in newGrid.Rows)
                    {
                        string value = string.Empty;

                        if (row.Cells[column.Index].Value != null)
                            value = row.Cells[column.Index].Value.ToString();   

                        try
                        {
                            if (IsNumeric(value))
                            {
                                if ((value.ToString().StartsWith("=") || value.ToString().Contains("-")) || value.ToString().StartsWith("0"))
                                {
                                    column.ExcelExportType = DisplayFormatType.Text;
                                }
                                else
                                {
                                    decimal decimalNumber;
                                    bool canBeDecimal = Decimal.TryParse(value, out decimalNumber);

                                    if (canBeDecimal && IsDecimal(decimalNumber))
                                    {
                                        column.ExcelExportType = DisplayFormatType.Custom;
                                        column.ExcelExportFormatString = "0,00";
                                        break;
                                    }
                                    else if (canBeDecimal && !IsDecimal(decimalNumber))
                                    {
                                        column.ExcelExportType = DisplayFormatType.Custom;
                                        column.ExcelExportFormatString = "0";
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void SetColumnsWith()
        {
            foreach (GridViewColumn column in newGrid.Columns)
            {
                if (column.IsVisible)
                {
                    if (column.Width < 0)
                        column.Width = 0;
                    else if (column.Width > 2000)
                        column.Width = 2000;
                }
            }
        }

        private bool IsDecimal(decimal value)
        {
            decimal d = (decimal)value;
            decimal rest = (d % 1);

            if (rest > 0)
                return true;//is decimal
            else
                return false;//is int
        }

        private bool IsNumeric(string value)
        {
            decimal testDe;
            double testDo;
            int testI;

            if (decimal.TryParse(value, out testDe) || double.TryParse(value, out testDo) || int.TryParse(value, out testI))
                return true;

            return false;
        }

        private void hideCheckAndImageColumn()
        {
            if (newGrid.Columns.Contains(GridColumns.VER_COLUMNNAME)/* && newGrid.Columns[GridColumns.VER_COLUMNNAME] is DataGridViewCheckBoxColumn*/)
                newGrid.Columns[GridColumns.VER_COLUMNNAME].IsVisible = false;

            if (newGrid.Columns.Contains(GridColumns.IMAGEN_COLUMNNAME)/* && newGrid.Columns[GridColumns.VER_COLUMNNAME] is DataGridViewCheckBoxColumn*/)
                newGrid.Columns[GridColumns.IMAGEN_COLUMNNAME].IsVisible = false;

            if (newGrid.Columns.Contains(GridColumns.SITUACIONICON_COLUMNNAME)/* && newGrid.Columns[GridColumns.VER_COLUMNNAME] is DataGridViewCheckBoxColumn*/)
                newGrid.Columns[GridColumns.SITUACIONICON_COLUMNNAME].IsVisible = false;

        }

        private bool exportarPDF(string nombreArchivoExportar)
        {
            try
            {
                pdfExporter = new GridViewPdfExport(this.newGrid);
                pdfExporter.FileExtension = "pdf";
                pdfExporter.FitToPageWidth = true;
                Cursor.Current = Cursors.WaitCursor;
                pdfExporter.RunExport(nombreArchivoExportar, new PdfExportRenderer());
                pdfExporter = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DisposeSources()
        {
            if (_dataTable != null)
            {
                _dataTable.Dispose();
                _dataTable = null;
            }

            if (newGrid != null && newGrid.DataSource != null && newGrid.DataSource is IDisposable)
            {
                ((IDisposable)newGrid.DataSource).Dispose();
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

        private void TxtFitClick(object sender, EventArgs e)
        {
            FixColumns();
        }

        /// <summary>
        ///   Evento que se ejecuta cuando se presiona el botón "Seleccionar Todos"
        /// </summary>
        /// <history> 
        ///   [Gaston]    12/08/2008  Created
        ///   05/09/2008  Modified
        /// </history>
        private void BtnSelectAllClick(object sender, EventArgs e)
        {
            try
            {
                if (btnSelectAll.Tag.ToString() == ZERO_VALUE)
                {
                    if (newGrid.Columns.Count > 0 && _dataTable.Columns.Count > 0)
                    {
                        if (newGrid.Columns[0].IsVisible && _dataTable.Columns[0].DataType == typeof(Boolean))
                        {
                            for (int i = 0; i < newGrid.Rows.Count; i++)
                            {
                                newGrid.Rows[i].Cells[0].Value = true;
                            }
                        }
                        else
                            newGrid.SelectAll();

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
                    if (newGrid.Columns.Count > 0 && _dataTable.Columns.Count > 0)
                    {
                        if (newGrid.Columns[0].IsVisible && _dataTable.Columns[0].DataType == typeof(Boolean))
                        {
                            for (int i = 0; i < newGrid.Rows.Count; i++)
                            {
                                newGrid.Rows[i].Cells[0].Value = false;
                            }
                        }
                        else
                            newGrid.ClearSelection();
                    }

                    btnSelectAll.Tag = ZERO_VALUE;
                    btnSelectAll.Text = Resources.GroupGrid_FixColumns_Seleccionar_todos;

                    if (_dDeselectAllClickEvent != null)
                    {
                        // Se ejecuta el evento para deshabilitar las reglas de la toolbar
                        _dDeselectAllClickEvent(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Exportar la grilla.
        /// </summary>
        /// <param name = "sender"></param>
        /// <param name = "e"></param>
        /// ivan - 07/12/15.    Modifique el metodo para utilizar metodos de telerik, y agregue export a pdf y csv.
        private void BtnExport(object sender, EventArgs e)
        {
            try
            {
                if (newGrid.DataSource != null)
                {
                    if (newGrid.Rows.Count > 0)
                    {
                        try
                        {
                            var sfDialog = new SaveFileDialog();
                            sfDialog.RestoreDirectory = true;
                            sfDialog.ValidateNames = true;
                            sfDialog.OverwritePrompt = true;
                            sfDialog.Filter = "Excel|*.xlsx|PDF|*.pdf|CSV|*.csv|All Files|*.*";

                            if (sfDialog.ShowDialog() == DialogResult.OK)
                            {
                                string nombreArchivoExportar = sfDialog.FileName;
                                int formatoArchivoExportar = sfDialog.FilterIndex;
                                sfDialog.Dispose();
                                sfDialog = null;

                                if (formatoArchivoExportar != 2)
                                {
                                    _gridController.LastPage = 0;
                                    _gridController.ExportSize = int.Parse(UserPreferences.getValue("ExportToExcelPageSize", UPSections.UserPreferences, 10000));
                                    _gridController.Exporting = true;
                                    _gridController.ShowTaskOfDT();
                                    _gridController.Exporting = false;
                                }

                                this.exportar(nombreArchivoExportar, formatoArchivoExportar, "Hoja");

                                // Abre el documento recien creado.
                                ProcessStartInfo PInfo = new ProcessStartInfo(nombreArchivoExportar);
                                PInfo.UseShellExecute = true;
                                PInfo.WindowStyle = ProcessWindowStyle.Maximized;
                                Process.Start(PInfo);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Resources.STRING_No_se_pudo_exportar_a_Excel, Resources.STRING_Exportar_Grilla_a_Excel);
                            ZClass.raiseerror(ex);
                            return;
                        }
                    }
                    else
                        MessageBox.Show(Resources.STRING_No_hay_datos_para_exportar, Resources.STRING_Impresion);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
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
            try
            {
                var ucGraph = new ExcelGraphType();
                ucGraph.ShowDialog();

                if (ucGraph.hasGraphic)
                {
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.DefaultExt = "xls";
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        string path = sf.FileName;
                        ExcelInterop.setExcelGraphics((DataTable)newGrid.DataSource, ucGraph.typeGraphic, path, true, true);
                        var proc = new Process { StartInfo = { FileName = path } };
                        proc.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show("Ha ocurrido un error al abrir el gráfico", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Agrego el filtro seleccionado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ivan - 07/12/15.    Agrego bandera si se actualiza la grilla por filtros,
        /// pongo la ultima pagina en cero, y despues de recargar la grilla reseteo la bandera.
        private void BtnAddClick(object sender, EventArgs e)
        {
            reloadGrid = true;
            _gridController.FiltersChanged = reloadGrid;
            _gridController.LastPage = 0;
            AddFilter();
            reloadGrid = false;
            _gridController.FiltersChanged = reloadGrid;
        }

        private void CmbOperatorSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbOperator.SelectedItem == "Contiene")
                {
                    //cmbOpTooltip.SetToolTip(cmbOperator, "Si ingreso una coma busco por O, si deja espacio es Y");
                    pbFiltersHelp.Visible = true;
                    cmbOpTooltip.SetToolTip(pbFiltersHelp, "Si ingreso una coma busco por O, si deja espacio es Y");
                }
                else
                {
                    //cmbOpTooltip.SetToolTip(cmbOperator, string.Empty);
                    pbFiltersHelp.Visible = false;
                    cmbOpTooltip.SetToolTip(pbFiltersHelp, string.Empty);
                }

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
            try
            {
                LoadFilterDataIndexControl();
                if (cmbFilterColumn.SelectedIndex != -1)
                    UserPreferences.setValue("LastFilterIndexUsed", cmbFilterColumn.SelectedItem.ToString(),
                                             UPSections.Filters);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void CtrlEnterPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                AddFilter();
            }
        }

        private void LsvFiltersItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                FilterElem fe = (FilterElem)((ListBox)sender).SelectedItem;

                if (UserBusiness.CanDisableDefaultFilter(fe.DocTypeId) || fe.Type != "defecto")
                {
                    fe.Enabled = !fe.Enabled;
                    _gridController.Fc.SetEnabledFilter(fe, filterType);

                    // (pablo) - 03032011
                    if (fe.Type != "defecto")
                    {
                        _gridController.Fc.SaveFilterInDatabase(fe);
                    }
                    else if (fe.Type == "defecto")
                    {
                        _gridController.Fc.SaveFilterInDatabase(fe);
                    }

                    reloadGrid = true;
                    _gridController.LastPage = 0;
                    _gridController.FiltersChanged = true;
                    _gridController.ShowTaskOfDT();
                    reloadGrid = false;
                    _gridController.FiltersChanged = false;
                }
                else
                {
                    if (e.CurrentValue.ToString() == "Unchecked")
                    {
                        MessageBox.Show("No tiene permiso para volver a habilitar los filtros por defecto", "ATENCION");
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
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show("Ha ocurrido un error al modificar el estado de un filtro", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            reloadGrid = true;
            _gridController.FiltersChanged = reloadGrid;
            RemoveAllFilters();
            ShowFilters();
            reloadGrid = false;
            _gridController.FiltersChanged = reloadGrid;
        }

        private void BtnUncheckAllFiltersClick(object sender, EventArgs e)
        {
            try
            {
                DisableAllFilters();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show("Ha ocurrido un error al desmarcar todos los filtros", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowFilters();
            }
        }

        public void DisableAllFilters(bool reloadGrid = true)
        {
            var filterCount = lsvFilters.Items.Count;
            lsvFilters.ItemCheck -= LsvFiltersItemCheck;
            for (Int16 t = 0; t < filterCount; t++)
            {
                if (UserBusiness.CanDisableDefaultFilter(((FilterElem)lsvFilters.Items[t]).DocTypeId))
                {
                    ((FilterElem)lsvFilters.Items[t]).Enabled = false;
                    lsvFilters.SetItemChecked(t, false);
                    _gridController.Fc.SetEnabledFilter((FilterElem)lsvFilters.Items[t], filterType);

                    if (filterType == FilterTypes.Task || filterType == FilterTypes.Document)
                        _gridController.Fc.SaveFilterInDatabase(((FilterElem)lsvFilters.Items[t]));
                }
                else if (((FilterElem)(lsvFilters.Items[t])).Type != "defecto")
                {
                    ((FilterElem)lsvFilters.Items[t]).Enabled = false;
                    lsvFilters.SetItemChecked(t, false);
                    _gridController.Fc.SetEnabledFilter((FilterElem)lsvFilters.Items[t], filterType);

                    if (filterType == FilterTypes.Task || filterType == FilterTypes.Document)
                        _gridController.Fc.SaveFilterInDatabase(((FilterElem)lsvFilters.Items[t]));
                }
                else
                    MessageBox.Show("No tiene permiso para deshabilitar los filtros por defecto", Resources.GroupGrid_BtnUncheckAllFiltersClick_ATENCION);
            }

            if (reloadGrid)
            {
                lsvFilters.ItemCheck += LsvFiltersItemCheck;
                reloadGrid = true;
                _gridController.FiltersChanged = reloadGrid;
                _gridController.LastPage = 0;
                _gridController.ShowTaskOfDT();
                reloadGrid = false;
                _gridController.FiltersChanged = reloadGrid;
            }
        }

        private void CmbDocTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbDocType.Enabled)
                    _gridController.ShowTaskOfDT();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show("Ha ocurrido un error al cargar la entidad seleccionada", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                _gridController.ShowTaskOfDT();
                //_dRefreshEvent();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show("Ha ocurrido un error al refrescar los datos", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void newGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si la tecla presionada es la barra espaciadora
            if (String.Compare(e.KeyChar.ToString(), " ") == 0)
            {
                checkGridRow(sender, e);
            }
        }

        private void checkGridRow(object sender, EventArgs e)
        {
            try
            {
                var columnindex = -1;
                if (newGrid.Columns[0] is GridViewCheckBoxColumn)
                {
                    columnindex = newGrid.Columns[0].Index;
                }
                else if (newGrid.Columns.Contains(VER_LABEL))
                {
                    columnindex = newGrid.Columns[VER_LABEL].Index;
                }

                if (columnindex != -1)
                {
                    if (newGrid.SelectedRows.Count > 0)
                    {
                        if (newGrid.Tag != null)
                        {
                            if ((newGrid.Tag.ToString() == ZERO_VALUE) || (newGrid.Tag.ToString() != newGrid.SelectedRows.Count.ToString()))
                            {
                                if ((String.IsNullOrEmpty(newGrid.SelectedRows[0].Cells[columnindex].Value.ToString())) ||
                                    (String.Compare(newGrid.SelectedRows[0].Cells[columnindex].Value.ToString().ToUpper(), "FALSE") == 0))
                                    ActivateCheckBoxs(columnindex);
                                else
                                    DesactivateCheckBoxs(columnindex);

                                // Se ejecuta el evento que hace que se llame a UCTaskGrid
                                if (_dSelectAllClickEvent != null)
                                    _dSelectAllClickEvent(sender, e);
                            }
                            else
                            {
                                if (String.Compare(newGrid.SelectedRows[0].Cells[columnindex].Value.ToString().ToUpper(), "TRUE") == 0)
                                    DesactivateCheckBoxs(columnindex);
                                else
                                    ActivateCheckBoxs(columnindex);

                                if (_dSelectAllClickEvent != null)
                                    _dSelectAllClickEvent(sender, e);
                            }
                        }
                        newGrid.TableElement.UpdateLayout();
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void newGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40 || e.KeyValue == 38)
            {
                //Navegación entre las filas con las flechas de arriba y abajo
                try
                {
                    if (null != _dRowClickEvent)
                    {
                        _dRowClickEvent(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
        }

        private void newGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Si se presiona la tecla enter se abre el documento
                try
                {
                    if (_dRowClickEvent != null)
                        _dRowClickEvent(null, e);
                    if (_dDoubleClickEvent != null)
                        _dDoubleClickEvent(null, e);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
                finally
                {
                    e.Handled = true;
                }
            }
        }

        private void newGrid_CellClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (null != _dRowClickEvent)
                {
                    _dRowClickEvent(this, e);
                }

                if (e.Column != null && (e.Column.FieldName == GridColumns.VER_COLUMNNAME || (e.Column.Index == 0 && e.Column is GridViewCheckBoxColumn)))
                {
                    checkGridRow(sender, e);
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }


        }

        private void newGrid_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (_dDoubleClickEvent != null && (e.RowIndex >= 0 && e.RowIndex < newGrid.Rows.Count))
                {
                    _dDoubleClickEvent(sender, e);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        public Font currentFont;
        public int currentRowHeight = 26;
        public float currentFontSize = 8;
        /// <summary>
        /// Evento que se dispara cuando se esta creando uan fila al cargar la grilla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ivan - 07/12/15.    Implemento este evento para agregar color a las filas. 
        private void newGrid_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (UseColor)
            {
                try
                {
                    GridViewCellInfo taskColorCell = e.RowElement.RowInfo.Cells[GridColumns.TASKCOLOR_COLUMNNAME];
                    ((GridDataRowElement)sender).ForeColor = ChangeColor(taskColorCell.Value != null ? taskColorCell.Value.ToString() : string.Empty);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }

                //try
                //{
                //    e.RowElement.Font = currentFont;
                //    this.newGrid.TableElement.RowHeight = currentRowHeight;
                //}
                //catch (Exception ex)
                //{
                //    ZClass.raiseerror(ex);
                //}
                //// recorro las celdas de la fila
                //foreach (GridViewCellInfo celda in e.RowElement.RowInfo.Cells)
                //{
                //    if (celda.ColumnInfo.Name == GridColumns.TASKCOLOR_COLUMNNAME)
                //    {
                //        // creo el color
                //        Color color = ChangeColor(celda.Value.ToString().ToUpper());
                //        // asigno el color a la fila
                //        e.RowElement.ForeColor = color;
                //        return;
                //    }
                //}
            }

            //try
            //{
            //    e.RowElement.Font = currentFont;
            //    this.newGrid.TableElement.RowHeight = currentRowHeight;
            //}
            //catch (Exception ex)
            //{
            //    ZClass.raiseerror(ex);
            //}
        }

        /// <summary>
        /// Evento que se dispara cuando se completo el export de la grilla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ivan - 07/12/15.    Implemento este metodo para cuando se completa el export
        /// vuelva a hacer visible la columna que oculte al iniciar el export (la de la imagen).
        private void spreadExporter_ExportCompleted()
        {
            if (newGrid.Columns.Contains(GridColumns.VER_COLUMNNAME)/* && newGrid.Columns[GridColumns.VER_COLUMNNAME] is DataGridViewCheckBoxColumn*/)
                newGrid.Columns[GridColumns.VER_COLUMNNAME].IsVisible = true;

            if (newGrid.Columns.Contains(GridColumns.IMAGEN_COLUMNNAME)/* && newGrid.Columns[GridColumns.VER_COLUMNNAME] is DataGridViewCheckBoxColumn*/)
                newGrid.Columns[GridColumns.IMAGEN_COLUMNNAME].IsVisible = true;

            if (newGrid.Columns.Contains(GridColumns.SITUACIONICON_COLUMNNAME)/* && newGrid.Columns[GridColumns.VER_COLUMNNAME] is DataGridViewCheckBoxColumn*/)
                newGrid.Columns[GridColumns.SITUACIONICON_COLUMNNAME].IsVisible = true;

            Cursor.Current = Cursors.Default;

            ReLoadGrid(true);
        }

        private void cmbSearchColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSearchColumn != null && NewGrid.Columns.Count > 0)
                {
                    var selectedItem = cmbSearchColumn.ComboBox.SelectedItem;
                    if (selectedItem != null && !string.IsNullOrEmpty(selectedItem.ToString()))
                    {
                        NewGrid.Columns[selectedItem.ToString()].IsCurrent = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
        private void cmbSearchColumn_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = cmbSearchColumn.ComboBox.SelectedItem;
                this.cmbSearchColumn.ComboBox.DataSource = visibleColumns;
                this.cmbSearchColumn.SelectedItem = selectedItem;
                this.NewGrid.Focus();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }
        private void cmbSearchColumn_TextUpdate(object sender, EventArgs e)
        {
            try
            {
                string strTemp = this.cmbSearchColumn.ComboBox.Text;

                if (!this.cmbSearchColumn.DroppedDown)
                {
                    this.cmbSearchColumn.DroppedDown = true;
                    this.cmbSearchColumn.SelectedIndex = -1;
                    this.cmbSearchColumn.ComboBox.Text = strTemp;
                }

                if (strTemp != string.Empty)
                {
                    var columns = visibleColumns.FindAll(s => s.IndexOf(this.cmbSearchColumn.ComboBox.Text, 0, StringComparison.CurrentCultureIgnoreCase) >= 0);
                    columns.Sort(new ColumnsComparer(strTemp));
                    this.cmbSearchColumn.SelectedIndexChanged -= cmbSearchColumn_SelectedIndexChanged;
                    this.cmbSearchColumn.ComboBox.DataSource = columns;
                    this.cmbSearchColumn.SelectedIndexChanged += cmbSearchColumn_SelectedIndexChanged;
                    this.cmbSearchColumn.ComboBox.Text = strTemp;
                    this.cmbSearchColumn.ComboBox.SelectionStart = this.cmbSearchColumn.ComboBox.Text.Length;
                }
                else
                {
                    this.cmbSearchColumn.ComboBox.DataSource = visibleColumns;
                    this.cmbSearchColumn.Text = string.Empty;
                    this.cmbSearchColumn.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }

        private void newGrid_Click(object sender, EventArgs e)
        {

        }

        private void btnfontsizeup_Click(object sender, EventArgs e)
        {
            _gridController.FontSizeChanged = true;
            SetFontSizeUp();
            _gridController.FontSizeChanged = false;
        }

        public void SetFontSizeUp()
        {
            try
            {
                currentFontSize = currentFontSize + 1;
                UserPreferences.setValue("GridFontSize", currentFont.Size.ToString(), UPSections.UserPreferences);
                currentFont = new Font(currentFont.FontFamily, currentFontSize, currentFont.Style);
                currentRowHeight = (int)(currentFontSize * 3);
                _gridController.ShowTaskOfDT();

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void btnfontsizedown_Click(object sender, EventArgs e)
        {
            _gridController.FontSizeChanged = true;
            SetFontSizeDown();
            _gridController.FontSizeChanged = false;
        }

        public void SetFontSizeDown()
        {
            try
            {
                currentFontSize = currentFontSize - 1;
                UserPreferences.setValue("GridFontSize", currentFont.Size.ToString(), UPSections.UserPreferences);
                currentFont = new Font(currentFont.FontFamily, currentFontSize, currentFont.Style);
                currentRowHeight = (int)(currentFontSize * 3);
                _gridController.ShowTaskOfDT();

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
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

        public int ColumnIndex()
        {
            return _columnIndex;
        }
    }

    /// <summary>
    /// Se crea esta clase comparer para ordenar las columnas que se van filtrando en los combobox.
    /// </summary>
    public class ColumnsComparer : Comparer<String>
    {
        public string Text { get; set; }
        public ColumnsComparer(string text)
        {
            this.Text = text;
        }

        public override int Compare(string x, string y)
        {
            if (x.IndexOf(Text, StringComparison.CurrentCultureIgnoreCase) < y.IndexOf(Text, StringComparison.CurrentCultureIgnoreCase))
            {
                return -1;
            }
            else if (x.IndexOf(Text, StringComparison.CurrentCultureIgnoreCase) > y.IndexOf(Text, StringComparison.CurrentCultureIgnoreCase))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }


    #endregion Comparers
}
