using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;
using Zamba.Core;
using Zamba.Grid;
using Zamba.Grid.Grid;

namespace Zamba.Grid
{
    public partial class NewGrid : RadGridView
    {
        #region Atributos y Propiedades

        public bool UseZamba = false;
        public bool useColor = false;

        // Seteado desde GroupGrid, guarda las columnas a ser visibles, se usa en SetupTable.
        public ArrayList ColumnsVisible = new ArrayList();

        // Seteado desde GroupGrid, guarda las columnas con su respectivo ancho.
        public SortedList ColumnsFixed = new SortedList();

        // Tabla que contiene los datos a mostrar en la grilla.
        public DataTable tabla;

        // Valores para controlar las posiciones de los scrolles
        public int CurrentVerticalScrollValue { get; set; }
        public int LastVerticalScrollValue { get; set; }
        public int CurrentHorizontalScrollValue { get; set; }
        public int LastHorizontalScrollValue { get; set; }

        #endregion

        public delegate void GridDataLoaded(object sender, EventArgs e);
        private GridDataLoaded _gridDataLoaded;

        public event GridDataLoaded GridDataLoadedEvent
        {
            add { _gridDataLoaded += value; }
            remove { _gridDataLoaded -= value; }
        }

        public NewGrid()
        {
            // Pone la grilla en español.
            RadGridLocalizationProvider.CurrentProvider = new GridLocalizationProviderEspanol();
        }


        /// <summary>
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="dataMember"></param>
        /// <param name="shortDataTimeFormat"></param>
        public void BindData(DataTable dataSource, string dataMember, bool shortDataTimeFormat, int lastPage, bool reloadGrid)
        {
            try
            {
                if (this.tabla == null)
                    this.tabla = new DataTable();

                if (dataSource != null)
                {
                    if (this.tabla.Rows.Count <= 0 || lastPage <= 0 || reloadGrid)
                        this.tabla = dataSource.Copy();
                    else
                        this.tabla.Merge(dataSource.Copy());
                }

                //this.BeginUpdate();
                this.DataSource = this.tabla;
                //this.EndUpdate();

                if (_gridDataLoaded != null)
                    _gridDataLoaded(null, null);
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }

    }
}
