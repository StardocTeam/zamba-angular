using System;
using System.Collections;
using System.Text;
using System.Data;
using zamba.collections;

namespace Zamba.Grid.PageGroupGrid
{
    //Adrian te comente esta linea porque tiraba error y no dejaba compilar. Marcelo
    //class TemplatePageGroupList : Abstract
    public class TemplateDataPageGroupList : PageGroupList
    {
        public TemplateDataPageGroupList(object dataSource) {
            this.DataSource = dataSource;
        }

        public TemplateDataPageGroupList(object dataSource, int pageSize ) {
            this.pageSize = pageSize;
            this.DataSource = dataSource;
        }

        /// <summary>
        /// Return a ArrayList of DataSource object 
        /// for use un de page list
        /// </summary>
        /// <param name="source">dataSource</param>
        /// <returns>Object list</returns>
        protected override ArrayList templateGetObjects(object source)
        {
            if (source is ArrayList) return (ArrayList)source;

            ArrayList list = new ArrayList();

            if (source is DataTable)    {
                DataTable table = (DataTable)source;
                foreach (DataRow item in table.Rows)
                    list.Add(item);                
            }
            else if (source is DataSet ) {
                DataSet dataSet = (DataSet)source;
                if( 0 < dataSet.Tables.Count )
                    foreach (DataRow item in dataSet.Tables[0].Rows)
                        list.Add(item);                                    
            }
            return list;            
        }
    }
}
