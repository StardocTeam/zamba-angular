using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Zamba.Core;
using Zamba.Filters;


namespace Zamba.Grid
{
    public partial class GroupGridTest : Form
    {
        //#region Form setup
        private Int64 CurrentUserId;
        private FiltersComponent FC;

        /// <summary>
        /// Set the dataSet to the Grid and fill it
        /// </summary>
        /// <param name="dt">Dataset to by set to the grid</param>
        public GroupGridTest(DataTable dt, Boolean withExcel, Int64 currentUserId, ref IFilter GridController)
        {
            _gridController = GridController;
            InitializeComponent();
            CurrentUserId = currentUserId;
            this.pageGroupGrid.PageSize = 5000;
            if (bool.Parse(UserPreferences.getValue("GridShortDateFormat", Sections.UserPreferences, "True")) == true)
                this.pageGroupGrid.ShortDateFormat = true;
            else
                this.pageGroupGrid.ShortDateFormat = false;
            this.pageGroupGrid.view.WithExcel = withExcel;
            this.pageGroupGrid.DataSource = dt;
        }

        public GroupGridTest(DataTable dt, Boolean withExcel, string repname, Int64 currentUserId, ref IFilter GridController)
        {
            _gridController = GridController;
            InitializeComponent();
            CurrentUserId = currentUserId;
            this.pageGroupGrid.PageSize = 5000;
            this.Text += " - " + repname;
            if (bool.Parse(UserPreferences.getValue("GridShortDateFormat", Sections.UserPreferences, "True")) == true)
                this.pageGroupGrid.ShortDateFormat = true;
            else
                this.pageGroupGrid.ShortDateFormat = false;
            this.pageGroupGrid.view.WithExcel = withExcel;
            this.pageGroupGrid.DataSource = dt;
        }

        private IFilter _gridController;
        public GroupGridTest(DataTable dt, Boolean withExcel, string repname, Hashtable IndexLoaded, Int64 currentUserId,ref IFilter GridController)
        {
            _gridController = GridController;
            InitializeComponent();
            CurrentUserId = currentUserId;
           
            this.pageGroupGrid.PageSize = 5000;
            this.Text += " - " + repname;
            if (bool.Parse(UserPreferences.getValue("GridShortDateFormat", Sections.UserPreferences, "True")) == true)
                this.pageGroupGrid.ShortDateFormat = true;
            else
                this.pageGroupGrid.ShortDateFormat = false;
            this.pageGroupGrid.view.WithExcel = withExcel;
            this.pageGroupGrid.LoadedIndexInTheGrid = IndexLoaded;
            this.pageGroupGrid.DataSource = dt;
//            if (dt != null && dt.Rows.Count != 0 && dt.Columns.Contains("Doctypeid"))
         //       this.pageGroupGrid.LoadCmbFiltercolumn(dt.Rows[0]["Doctypeid"].ToString());
        }

        public void Group(string columname)
        {
            
            this.pageGroupGrid.Group(columname);
        }

        public Boolean WithExcel
        {
            get { return this.pageGroupGrid.view.WithExcel; }
            set
            {
                this.pageGroupGrid.view.WithExcel = value;
            }
        }

    }
}