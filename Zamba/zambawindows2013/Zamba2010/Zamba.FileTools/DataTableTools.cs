using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamba.FileTools
{
    public class DataTableTools
    {
        public void RemoveEmptyRows(ref DataTable dt)
        {
            if ((dt != null) && (dt.Rows != null) && (dt.Rows.Count > 0))
            {
                List<DataRow> removeRowIndex = new List<DataRow>();
                //int RowCounter = 0;
                foreach (DataRow dRow in dt.Rows)
                {
                    if (dRow[0] == DBNull.Value || string.IsNullOrEmpty(dRow[0].ToString().Trim()))
                        removeRowIndex.Add(dRow);

                    //for (int index = 0; index < dt.Columns.Count; index++)
                    //{
                    //    if (dRow[index] == DBNull.Value)
                    //    {
                    //        removeRowIndex.Add(dRow);
                    //        break;
                    //    }
                    //    else if (string.IsNullOrEmpty(dRow[index].ToString().Trim()))
                    //    {
                    //        removeRowIndex.Add(dRow);
                    //        break;
                    //    }
                    //}
                    //RowCounter++;
                }

                // Remove all blank of in-valid rows
                foreach (DataRow rowIndex in removeRowIndex)
                    dt.Rows.Remove(rowIndex);

                dt.AcceptChanges();
            }


        }


    }
}
