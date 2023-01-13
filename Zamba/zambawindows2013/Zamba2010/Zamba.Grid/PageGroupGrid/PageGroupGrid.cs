using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Zamba.Core;
using Zamba.Filters;
using Zamba.Grid.Grid;

namespace Zamba.Grid.PageGroupGrid
{
    public delegate void PageGroupGridClickEventHandler(Object sender, EventArgs e);
    public delegate void PageGroupGridDoubleClickEventHandler(Object sender, EventArgs e);

    public partial class PageGroupGrid : UserControl, IDataSource
    {
        protected PageGroupGridClickEventHandler dClick = null;
        protected PageGroupGridDoubleClickEventHandler dDoubleClick = null;

        private Int64 CurrentUserId = 0;

        private IGrid GridController;
        public PageGroupGrid(Int64 currentUserId, ref IGrid GridController)
        {
            this.GridController = GridController;

            //Se asigna el userid antes de InitializeComponent para cuando se instancie GroupGrid
            CurrentUserId = currentUserId;
            InitializeComponent();

            this.panel2.AutoSize = true;
            // [ivan] comentado
            //this.pageBar.OnClickEvent += new BarraPaginaClickEventHandler(this.pageBar_Click);
            this.view.ShortDateFormat = Boolean.Parse(UserPreferences.getValue("GridShortDateFormat", UPSections.UserPreferences, "True"));
            this.pageBar.Visible = true;
        }

        public Boolean UseColor
        {
            get { return this.view.UseColor; }
            set { this.view.UseColor = value; }
        }

        public void FixColumns()
        {
            this.view.FixColumns();
        }

        /// <summary>
        /// Método que sirve para limpiar el combobox "Filtros" en caso de que se pase a una nueva etapa
        /// </summary>
        /// <history> 
        ///    [Gaston]    17/10/2008   Created
        /// </history
        public void clearFilters()
        {
            this.view.ClearFilters();
        }


        /// <summary>
        /// Siempre se ajusten las columnas
        /// </summary>
        public Boolean AlwaysFit
        {
            get
            {
                return this.view.AlwaysFit;
            }
            set
            {
                this.view.AlwaysFit = value;
            }
        }

        public Boolean ViewDocTypeCmb
        {
            get { return this.view.cmbDocType.Visible; }
            set { this.view.cmbDocType.Visible = value; this.view.ToolStripSeparator4.Visible = value; }
        }

        public ToolStripComboBox cmbDocType
        {
            get { return this.view.cmbDocType; }
            set { this.view.cmbDocType = value; }
        }


        public void FilterAfterRefresh()
        {
            this.view.Refresh();
            setPage(this.DataTable);
        }

        /// <summary>
        /// Deselecciona una fila de la grilla
        /// </summary>
        /// <history>
        ///         Marcelo 09/11/09    created
        /// </history>
        /// <param name="row">Fila a ser deseleccionada</param>
        public void deselectRow(GridViewRowInfo row)
        {
            this.view.DeselectRow(row);
        }

        /// <summary>
        /// Set the column as visible or not
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bolVisible"></param>
        public void setColumnVisible(string name, Boolean bolVisible)
        {
            this.view.SetColumnVisible(name, bolVisible);
        }

        //public void pageBar_Click(Object sender, IBarraPaginaClickEventArgs e)
        //{
        //    this.view.DataSourcePage = e.ItemSelectedPage;
        //}

        public object LoadedIndexInTheGrid
        {
            get
            {
                return this.view.LoadedIndexs;
            }
            set
            {
                this.view.LoadedIndexs = (System.Collections.Hashtable)value;
            }

        }

        public DataTable DataTable
        {
            get { return this.view.DataTable; }
            set { this.view.DataTable = value; }
        }

        private void setPage(Object value)
        {
            TemplateDataPageGroupList list;
            if (pageSize != 0)
            {
                list = new TemplateDataPageGroupList(value, pageSize);
            }
            else
                list = new TemplateDataPageGroupList(value);

            if (list.PageCount <= 0)
            {
                this.pageBar.Visible = false;
                //this.view.DataSourcePage = this.pageBar.DataSource = null;
            }
            else
            {
                this.pageBar.DataSource = list;
                this.pageBar.Visible = true;
            }

            if (AlwaysFit == true)
            {
                FixColumns();
            }
        }

        private Int32 pageSize = 0;

        public Int32 PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        /// <summary>
        /// Especifica el formato de la fecha
        /// </summary>
        public Boolean ShortDateFormat
        {
            get
            {
                return this.view.ShortDateFormat;
            }
            set
            {
                this.view.ShortDateFormat = value;
            }
        }

        public event PageGroupGridClickEventHandler OnGridClick
        {
            add { this.dClick += value; }
            remove { this.dClick -= value; }
        }

        public event PageGroupGridDoubleClickEventHandler OnGridCDoubleClick
        {
            add { this.dDoubleClick += value; }
            remove { this.dDoubleClick -= value; }
        }

        private void view_OnClick(object sender, EventArgs e)
        {
            if (null != this.dClick)
                this.dClick(this, e);
        }

        private void view_OnDoubleClick(object sender, EventArgs e)
        {
            if (null != this.dDoubleClick)
                this.dDoubleClick(this, e);
        }


        public object DataSource
        {
            get
            {
                return this.view.DataSource;
            }
            set
            {
                //Si es un DataTable con valores los guardo en las tablas de impresion auxiliares
                if (value != null)
                {
                    if (value.GetType().ToString() == "System.Data.DataTable")
                    {
                        this.view.OriginalDataTable = ((DataTable)value).Copy();
                    }
                }

                //muestra el paginado
                setPage(value);
            }
        }

        //public DataGridView OutLookGrid
        //{
        //    get { return this.view.OutLookGrid; }
        //}
        public RadGridView NewGrid
        {
            get { return this.view.NewGrid; }
        }

    }
}

