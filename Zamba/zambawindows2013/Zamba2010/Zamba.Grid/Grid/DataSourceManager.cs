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
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Forms;

namespace Zamba.Grid
{
    #region DataSourceRowComparer - implementation of abstract comparer class
    /// <summary>
    /// because the DataSourceRow class is a wrapper class around the real data,
    /// the compared object used to sort the real data is wrapped by this DataSourceRowComparer class.
    /// </summary>
    internal class DataSourceRowComparer : IComparer
    {
        IComparer baseComparer;
        public DataSourceRowComparer(IComparer baseComparer)
        {
            this.baseComparer = baseComparer;
        }

        #region IComparer Members

        public int Compare(object x, object y)
        {
            DataSourceRow r1 = (DataSourceRow)x;
            DataSourceRow r2 = (DataSourceRow)y;
            return baseComparer.Compare(r1.BoundItem, r2.BoundItem);
        }

        #endregion
    }
    #endregion DataSourceRowComparer - implementation of abstract comparer class

    #region DataSourceRow - abstract representation of a data item.
    /// <summary>
    /// The DataSourceRow is a wrapper row class around the real bound data. This row is an abstraction
    /// so different types of data can be encaptulated in this class, although for the OutlookGrid it will
    /// simply look as one type of data. 
    /// Note: this class does not implement all row wrappers optimally. It is merely used for demonstration purposes
    /// </summary>
    internal class DataSourceRow : CollectionBase
    {
        DataSourceManager manager;
        object boundItem;
        public DataSourceRow(DataSourceManager manager, object boundItem)
        {
            this.manager = manager;
            this.boundItem = boundItem;
        }

        public object this[int index]
        {
            get
            {
                return List[index];
            }
        }

        public object BoundItem
        {
            get
            {
                return boundItem;
            }
        }

        public int Add(object val)
        {
            return List.Add(val);
        }

    }
    #endregion DataSourceRow - abstract representation of a data item.

    #region DataSourceManager - manages a bound datasource.
    /// <summary>
    /// the DataDourceManager class is a wrapper class 
    /// around different types of datasources.
    /// in this case the DataSet, object list using 
    /// reflection and the OutlooGridRow objects are supported
    /// by this class. Basically the DataDourceManager works 
    /// like a facade that provides access in a uniform way to the datasource.
    /// Note: this class is not implemented optimally. It is merely used 
    /// for demonstration purposes
    /// </summary>
    internal class DataSourceManager
    {
        #region Begin Atributes
            private object dataSource;
            private string dataMember;

            public ArrayList Columns;
            public ArrayList Rows;

            private bool shortDateFormat;
        #endregion End Atributes

        #region Constructors...
            public DataSourceManager(object dataSource, string dataMember, bool dataTimeShortFormat) 
            {
                this.dataSource = dataSource;
                this.dataMember = dataMember;
                this.shortDateFormat = dataTimeShortFormat;
                InitManager();
            }
        #endregion 

        #region Begin Properties

        /// <summary>
        /// datamember readonly for now
        /// </summary>
        public string DataMember { get { return dataMember; } }

        /// <summary>
        /// datasource is readonly for now
        /// </summary>
        public object DataSource { get { return dataSource; } }

        public bool ShortDateFormat {
            get { return this.shortDateFormat;  }
        }

        #endregion End Properties

        #region Begin Method
        /// <summary>
        /// this function initializes the DataSourceManager's internal state.
        /// it will analyse the datasource taking the following source into account:
        /// - DataSet
        /// - Object array (must implement IList)
        /// - OutlookGrid
        /// </summary> 
        private void InitManager() {
            if (dataSource is IListSource)
                InitDataTable();

            if (dataSource is IList)
                InitList();

            if (dataSource is OutlookGrid)
                InitGrid();
        }

        private void InitDataTable()
        {
            Columns = new ArrayList();
            Rows = new ArrayList();
            DataTable table = ((DataTable)dataSource);

            string colCheckIn = Zamba.Core.UserPreferences.getValue("ColumnNameCheckIn", Zamba.Core.Sections.UserPreferences, "CheckIn");
            bool shortDateFormat = Boolean.Parse(Zamba.Core.UserPreferences.getValue("CheckInColumnShortDateFormat", Zamba.Core.Sections.UserPreferences, true));
            string shortDate;
            DateTime shortDateOut;
            const string systemDateTime = "System.DateTime";

            foreach (DataColumn c in table.Columns)
                Columns.Add(c);

            foreach (DataRow r in table.Rows) {
                DataSourceRow row = new DataSourceRow(this, r);
                
                for (int i = 0; i < Columns.Count; i++)
                {
                    if (this.ShortDateFormat && r[i].GetType().ToString() == systemDateTime) 
                    {
                        if (shortDateFormat || Columns[i].ToString() != colCheckIn)
                        {
                            shortDate = ((System.DateTime)r[i]).ToShortDateString();
                            row.Add(shortDate);
                        }
                        else
                        {
                            row.Add((System.DateTime)r[i]);
                        }
                    }
                    else 
                    {
                        if (shortDateFormat && Columns[i].ToString() == colCheckIn && DateTime.TryParse(r[i].ToString(), out shortDateOut))
                        {
                            shortDate = shortDateOut.ToShortDateString();
                            row.Add(shortDate);
                        }
                        else
                        {
                            row.Add(r[i]); 
                        }                   
                    }                
                }

                Rows.Add(row);
            }
        }

        private void InitGrid() {
            Columns = new ArrayList();
            Rows = new ArrayList();

            OutlookGrid grid = (OutlookGrid)dataSource;
            // use reflection to discover all properties of the object
            foreach (DataGridViewColumn c in grid.Columns)
                Columns.Add(c.Name);

            foreach (OutlookGridRow r in grid.Rows) {
                if (!r.IsGroupRow && !r.IsNewRow) {
                    DataSourceRow row = new DataSourceRow(this, r);
                    for (int i = 0; i < Columns.Count; i++)
                        row.Add(r.Cells[i].Value);
                    Rows.Add(row);
                }
            }
        }

        private void InitList()
        {
            Columns = new ArrayList();
            Rows = new ArrayList();
            IList list = (IList)dataSource;

            // use reflection to discover all properties of the object
            BindingFlags bf = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty;
            PropertyInfo[] props = list[0].GetType().GetProperties();

            for (int i = 0; i < props.Length; i++)
                Columns.Add(props[i].Name);

            foreach (object obj in list)
            {
                DataSourceRow row = new DataSourceRow(this, obj);
                foreach (PropertyInfo pi in props)
                {
                    object result = obj.GetType().InvokeMember(pi.Name, bf, null, obj, null);
                    row.Add(result);
                }
                Rows.Add(row);
            }

            list = null;
            props = null;
        }

        public void Sort(System.Collections.IComparer comparer)
        {
            Rows.Sort(new DataSourceRowComparer(comparer));
        }

        #endregion Begin Method

        public void Dispose()
        {
            if (this.Rows != null)
            {
                this.Rows.Clear();
                this.Rows = null;
            }

            if (this.Columns != null)
            {
                this.Columns.Clear();
                this.Columns = null;
            }

            dataMember = null;

            if (this.dataSource != null)
            {
                if (dataSource is IDisposable)
                    ((IDisposable)dataSource).Dispose();

                dataSource = null;
            }
        }
    }
    #endregion DataSourceManager - manages a bound datasource.
}
