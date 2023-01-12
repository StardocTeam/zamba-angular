// Copyright 2006 Herre Kuijpers - <herre@xs4all.nl>
//
// This source file(s) may be redistributed, altered and customized
// by any means PROVIDING the authors name and all copyright
// notices remain intact.
// THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED. USE IT AT YOUR OWN RISK. THE AUTHOR ACCEPTS NO
// LIABILITY FOR ANY DATA DAMAGE/LOSS THAT THIS PRODUCT MAY CAUSE.
//-----------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Zamba.Grid.Grid;

//using Zamba.Core;


namespace Zamba.Grid
{
    public partial class OutlookGrid : DataGridView
    {
        #region OutlookGrid constructor
        public OutlookGrid()
        {
            InitializeComponent();

            // very important, this indicates that a new default row class is going to be used to fill the grid
            // in this case our custom OutlookGridRow class
            base.RowTemplate = new OutlookGridRow();
            _groupTemplate = new OutlookgGridDefaultGroup();
            AllowUserToResizeColumns = true;
            AllowUserToResizeRows = true;
            AllowUserToAddRows = false;
        }
        #endregion OutlookGrid constructor
        #region OutlookGrid property definitions
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DataGridViewRow RowTemplate
        {
            get { return base.RowTemplate;}
        }

        private IOutlookGridGroup _groupTemplate;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IOutlookGridGroup GroupTemplate
        {
            get
            {
                return _groupTemplate;
            }
            set
            {
                _groupTemplate = value;
            }
        }

        private Image _iconCollapse;
        [Category("Appearance")]
        public Image CollapseIcon
        {
            get { return _iconCollapse; }
            set { _iconCollapse = value; }
        }

        private Image iconExpand;
        [Category("Appearance")]
        public Image ExpandIcon
        {
            get { return iconExpand; }
            set { iconExpand = value; }
        }

        //private Int32 KeyIndex;
        private DataSourceManager dataSource;

        public new object DataSource
        {
            get
            {
                if (dataSource == null) return null;

                // special case, datasource is bound to itself.
                // for client it must look like no binding is set,so return null in this case
                if (dataSource.DataSource.Equals(this)) return null;

                // return the origional datasource.
                return dataSource.DataSource;
            }
        }
        #endregion OutlookGrid property definitions
        #region OutlookGrid new methods
        public void CollapseAll()
        {
            SetGroupCollapse(true);
        }
        public void ExpandAll()
        {
            SetGroupCollapse(false);
        }
        public void ClearGroups()
        {
            try
            {
                _groupTemplate.Column = null; //reset
            }
            catch (Exception ex)
            {
             Zamba.AppBlock.ZException.Log(ex,true);
            }
        }
        public void BindData(object dataSource,string dataMember,bool shortDataTimeFormat )
        {
            try
            {
                this.DataMember = DataMember;
                if (dataSource == null)
                {
                    this.dataSource = null;
                    Rows.Clear();
                    Columns.Clear();
                }
                else
                {
                    this.dataSource =
                        new DataSourceManager(
                                dataSource,
                                dataMember,
                                shortDataTimeFormat
                            );
                    SetupColumns();
                }
            }
            catch (Exception ex)
            {
              Zamba.AppBlock.ZException.Log(ex,true);
            }
        }
        public void Fill()
        {
            try
            {
                if (dataSource == null) // if no datasource is set, then bind to the grid itself
                    dataSource = new DataSourceManager(this, null, false);

                FillGrid(_groupTemplate);
            }
            catch (Exception ex)
            {
              Zamba.AppBlock.ZException.Log(ex,true);
            }
        }
        public override void Sort(System.Collections.IComparer comparer)
        {
            try
            {
                if (dataSource == null) // if no datasource is set, then bind to the grid itself
                    dataSource = new DataSourceManager(this, null, false);

                int columnIndex = ((DataRowComparer)comparer).ColumnIndex();
                if (gridSort == false && columnIndex == 3)
                {
                    FillGrid(_groupTemplate);
                    }
                else
                    {
                    dataSource.Sort(comparer);
                    FillGrid(_groupTemplate);
                }
            }
            catch (Exception ex)
            {
              Zamba.AppBlock.ZException.Log(ex,true);
            }
        }
        public void OnlySort(System.Collections.IComparer comparer)
        {
            try
            {
                if (dataSource == null) // if no datasource is set, then bind to the grid itself
                    dataSource = new DataSourceManager(this, null, false);

                dataSource.Sort(comparer);
            }
            catch (Exception ex)
            {
              Zamba.AppBlock.ZException.Log(ex,true);
            }
        }
        public override void Sort(DataGridViewColumn dataGridViewColumn, ListSortDirection direction)
        {
            try
            {
                if (dataSource == null) // if no datasource is set, then bind to the grid itself
                    dataSource = new DataSourceManager(this, null, false);

                dataSource.Sort(new OutlookGridRowComparer(dataGridViewColumn.Index, direction));
                FillGrid(_groupTemplate);
            }
            catch (Exception ex)
            {
              Zamba.AppBlock.ZException.Log(ex,true);
            }
        }
        #endregion OutlookGrid new methods
        #region OutlookGrid event handlers
        protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            try
            {
                OutlookGridRow row = (OutlookGridRow)base.Rows[e.RowIndex];
                if (row.IsGroupRow)
                    e.Cancel = true;
                else
                    base.OnCellBeginEdit(e);
            }
            catch (Exception ex)
            {
              Zamba.AppBlock.ZException.Log(ex,true);
            }        
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    OutlookGridRow row = (OutlookGridRow)base.SelectedRows[0];         
                    e.Handled = true;
                    base.OnKeyDown(e);
                }
               
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex, true);
            }
        }

        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {

                    OutlookGridRow row = (OutlookGridRow)base.Rows[e.RowIndex];
                    if (row.IsGroupRow)
                    {
                        row.Group.Collapsed = !row.Group.Collapsed;

                        //this is a workaround to make the grid re-calculate it's contents and backgroun bounds
                        // so the background is updated correctly.
                        // this will also invalidate the control, so it will redraw itself
                        row.Visible = false;
                        row.Visible = true;
                        return;
                    }
                }
                base.OnCellClick(e);
            }
            catch (Exception ex)
            {
              Zamba.AppBlock.ZException.Log(ex,true);
            }
        }
        // the OnCellMouseDown is overriden so the control can check to see if the
        // user clicked the + or - sign of the group-row
        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                OutlookGridRow row = (OutlookGridRow)base.Rows[e.RowIndex];
                if (row.IsGroupRow && row.IsIconHit(e))
                {
                    System.Diagnostics.Debug.WriteLine("OnCellMouseDown " + DateTime.Now.Ticks.ToString());
                    row.Group.Collapsed = !row.Group.Collapsed;

                    //this is a workaround to make the grid re-calculate it's contents and backgroun bounds
                    // so the background is updated correctly.
                    // this will also invalidate the control, so it will redraw itself
                    row.Visible = false;
                    row.Visible = true;
                }
                else
                {
                    
                    base.OnCellMouseDown(e);

                    if (row.Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
                    {
                        if (row.Cells[e.ColumnIndex].Value != null)
                        {
                            if (row.ReadOnly == false)
                            {
                                row.Cells[e.ColumnIndex].Value = !((Boolean)row.Cells[e.ColumnIndex].Value);
                                row.Selected = !row.Selected;
                                row.Selected = !row.Selected;
                                if ((bool)row.Cells[e.ColumnIndex].Value == true)
                                {
                                    //se comentó esta linea por que si se hacia click en el check de la ultima
                                    //fila se "pintaban" 3 o 4 filas anteriores. 
                                    //base.OnCellContentClick(new DataGridViewCellEventArgs(0,0));
                                }
                            }
                        }
                    }
                }
            }


            catch (FormatException ex)
            {
                Zamba.AppBlock.ZException.Log(ex, true);
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex, true);
            }
        }
        /// <summary>
        /// [Sebastian 08-09-09]
        /// Se creo este método para salvar el error en pantalla que producia al agrupar 
        /// y seleccionar una fila de la grilla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
            private void DataGridView1_DataError( System.Object sender , System.Windows.Forms.DataGridViewDataErrorEventArgs e  ) 
            {
                Zamba.AppBlock.ZException.Log(e.Exception, true); 
            }
        
        protected override void OnScroll(ScrollEventArgs se)
        {
            this.SuspendLayout();
            if (se.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                this.RefreshEdit();
                this.Update();
            }
            this.ResumeLayout();
            base.OnScroll(se);
            this.Refresh();
        }
       #endregion OutlookGrid event handlers
        #region Grid Fill functions
        private void SetGroupCollapse(bool collapsed)
        {
            try
            {
                if (Rows.Count == 0) return;
                if (_groupTemplate == null) return;

                // set the default grouping style template collapsed property
                _groupTemplate.Collapsed = collapsed;

                // loop through all rows to find the GroupRows
                foreach (OutlookGridRow row in Rows)
                {
                    if (row.IsGroupRow)
                        row.Group.Collapsed = collapsed;
                }

                // workaround, make the grid refresh properly
                Rows[0].Visible = !Rows[0].Visible;
                Rows[0].Visible = !Rows[0].Visible;
            }
            catch (Exception ex)
            {
              Zamba.AppBlock.ZException.Log(ex,true);
            }
        }
        public Boolean UseZamba = false;
        public Boolean gridSort = true;
        public ArrayList ColumnsVisible = new ArrayList();
        public SortedList ColumnsFixed = new SortedList();
        public Boolean useColor = false;

        private void SetupColumns()
        {
            try
            {
                ArrayList list;

                // clear all columns, this is a somewhat crude implementation
                // refinement may be welcome.
                Columns.Clear();

                // start filling the grid
                if (dataSource == null)
                    return;
                else
                    list = dataSource.Rows;
                if (list.Count <= 0) return;
                foreach (DataColumn c in dataSource.Columns)
                {
                    int index;                    
                    if (c.DataType == System.Type.GetType("System.Drawing.Bitmap"))
                    {
                        DataGridViewImageColumn col = new DataGridViewImageColumn(false);
                        col.Name = c.ColumnName;
                        col.HeaderText = "";
                        index = Columns.Add(col);
                    }
                    else if (c.DataType == System.Type.GetType("System.Boolean"))
                        {
                            DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn(false);
                            col.HeaderText = c.ColumnName;
                            col.Name = c.ColumnName;
                            index = Columns.Add(col);
                        }
                    else
                    {
                        index = Columns.Add(c.ColumnName, c.ColumnName);
                    }
                    //DataGridViewColumn column = Columns[c];
                    //index = Columns.Add(c, c);
                    Columns[index].SortMode = DataGridViewColumnSortMode.Programmatic; // always programmatic!
                }
                foreach (string name in this.ColumnsVisible)
                {
                    if (Columns[name] != null)
                        Columns[name].Visible = false;
                }
            }
            catch (Exception ex)
            {
              Zamba.AppBlock.ZException.Log(ex,true);
            }
        }

        /// <summary>
        /// the fill grid method fills the grid with the data from the DataSourceManager
        /// It takes the grouping style into account, if it is set.
        /// </summary>
        /// <history> 
        ///    [Gaston]    27/10/2008  Modified     Si useColor es igual a True cambia el color de la fila (tarea) 
        ///    [Marcelo]   21/11/2009  Modified     Add validation of columns and replace row.clear
        /// </history>
        public void FillGrid(IOutlookGridGroup groupingStyle)
        {
            try
            {
                ArrayList list;
                //OutlookGridRow row = new OutlookGridRow(groupingStyle);
                OutlookGridRow row;

                if (this.Columns.Count <= 0)
                {
                    return;
                }
                else
                {
                    //Clear the rows, this.Rows.Clear() throws exception
                    while (this.Rows.Count > 0)
                        this.Rows.Remove(this.Rows[0]);
                }
            

               // start filling the grid
                if (dataSource == null)
                    return;
                else
                    list = dataSource.Rows;
                if (list.Count <= 0) return;

                // this block is used of grouping is turned off
                // this will simply list all attributes of each object in the list
                if (groupingStyle == null || groupingStyle.Column==null || (groupingStyle.Column.Name.ToLower().CompareTo("folder_id") == 0 && this.Columns.Contains("TaskColor"))) 
                {
                    Color c = new Color();

                    foreach (DataSourceRow r in list)
                    {
                        row = (OutlookGridRow)this.RowTemplate.Clone();
                        
                        foreach (object val in r)
                        {
                            if (val is System.Drawing.Bitmap)
                            {
                                DataGridViewCell cell = new DataGridViewImageCell(false);
                                cell.Value = val;
                                row.Cells.Add(cell);
                            }
                            else if (val is Boolean)
                            {
                                DataGridViewCheckBoxCell cell = new DataGridViewCheckBoxCell(false);
                                cell.Value = val;
                                row.Cells.Add(cell);
                            }
                            else
                            {
                                DataGridViewCell cell = new DataGridViewTextBoxCell();
                                cell.Value = val.ToString();
                                row.Cells.Add(cell);

                                if(useColor == true)
                                {
                                    if (val.ToString().Contains("color:"))
                                        c = changeColor(val.ToString().Remove(0, 7));
                                }
                            }
                        }

                        if (useColor == true)
                            row.DefaultCellStyle.ForeColor = c;
                            
                        Rows.Add(row);
                    }
                }

                // this block is used when grouping is used
                // items in the list must be sorted, and then they will automatically be grouped
                else
                {
                    IOutlookGridGroup groupCur = null;
                    object result = null;
                    int counter = 0; // counts number of items in the group
                    //Boolean bolGroup = false;
                    Color c = new Color();
                    foreach (DataSourceRow r in list)
                    {
                        row = (OutlookGridRow)this.RowTemplate.Clone();
                        result = r[groupingStyle.Column.Index];
                        if (groupCur != null && groupCur.CompareTo(result) == 0) // item is part of the group
                        {
                            //bolGroup = true;
                            row.Group = groupCur;
                            counter++;
                        }
                        else // item is not part of the group, so create new group
                        {
                            //// si el grupo anterior tiene un solo item, si es zamba quitar el grupo
                            //if (UseZamba == true && counter == 1)
                            //{
                            //    Rows.Remove(Rows[Rows.Count - 2]);
                            //    ((OutlookGridRow)Rows[Rows.Count - 1]).Group = null;
                            //}
                            if (groupCur != null)
                                groupCur.ItemCount = counter;

                            groupCur = (IOutlookGridGroup)groupingStyle.Clone(); // init
                            groupCur.Value = result;

                            if (UseZamba == true)
                            {
                                if (r.Count >= groupCur.Column.Index)
                                    groupCur.Text = "Documento:" + r[groupCur.Column.Index];
                                else
                                groupCur.Text = "Documento:" + r[0].ToString();
                                groupCur.Height = 30;
                            }
                            row.Group = groupCur;
                            row.IsGroupRow = true;
                            row.Height = groupCur.Height;
                            row.CreateCells(this, groupCur.Value);
                            Rows.Add(row);

                            // add content row after this
                            row = (OutlookGridRow)this.RowTemplate.Clone();
                            row.Group = groupCur;
                            counter = 1; // reset counter for next group
                        }

                        foreach (object obj in r)
                        {
                            if (obj is System.Drawing.Bitmap)
                            {
                                DataGridViewCell cell = new DataGridViewImageCell(false);
                                cell.Value = obj;
                                row.Cells.Add(cell);
                            }
                            else if (obj is Boolean)
                            {
                                DataGridViewCheckBoxCell cell = new DataGridViewCheckBoxCell(false);
                                cell.Value = obj;
                                row.Cells.Add(cell);
                            }
                            else
                            {
                                DataGridViewCell cell = new DataGridViewTextBoxCell();
                                cell.Value = obj.ToString();
                                row.Cells.Add(cell);

                                if (useColor == true)
                                {
                                    if (obj.ToString().Contains("color:"))
                                        c = changeColor(obj.ToString().Remove(0, 7));
                                }
                            }
                        }

                        //if (bolGroup == true)
                        //{
                        if (UseZamba == true)
                            {
                            row.Cells[0].Value = "    - " + row.Cells[0].Value;
                            if (Rows[Rows.Count - 1].Cells[0].Value.ToString().StartsWith("    - ") == false)
                                Rows[Rows.Count - 1].Cells[0].Value = "    - " + Rows[Rows.Count - 1].Cells[0].Value;
                            }
                            //bolGroup = false;
                        //}
                        if (useColor == true)
                            row.DefaultCellStyle.ForeColor = c;
                        Rows.Add(row);
                        groupCur.ItemCount = counter;

                        //if (UseZamba == true && list.LastIndexOf(r) == list.Count - 1 && counter == 1)
                        //{
                        //    Rows.Remove(Rows[Rows.Count - 2]);
                        //    ((OutlookGridRow)Rows[Rows.Count - 1]).Group = null;
                        //}
                    }
                    this.Refresh();
                }
            }
            catch (Exception ex)
            {
              Zamba.AppBlock.ZException.Log(ex,true);
            }
        }

        /// <summary>
        /// Método que sirve para retornar un color de acuerdo al contenido de la celda que viene como parámetro
        /// </summary>
        /// <history> 
        ///    [Gaston]    27/10/2008  Created     
        /// </history>
        private Color changeColor(string color)
        {
            switch (color)
            {
                case "ROJO":
                    return (Color.Red);
                    //break;
                case "VERDE":
                    return (Color.Green);
                    //break;
                case "AMARILLO":
                    return (Color.Yellow);
                   // break;
                case "AZUL":
                    return (Color.Blue);
//break;
                case "VIOLETA":
                    return (Color.Violet);
                  //  break;
                case "GRIS":
                    return (Color.Gray);
                   // break;
            }

            return (Color.Black);
        }

        #endregion Grid Fill functions
    }
}
