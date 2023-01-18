using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ZItoDOC_ID
{
     class ZItoDOC_ID_Bussiness
    {

         public static  DataSet GetCompleteZI()
        {
            return ZItoDOC_ID_Factory.GetCompleteZI ();
        }

         public static void UpdateDocTable(DataRow row)
         {
             ZItoDOC_ID_Factory.UpdateDocTable(row);
         }

         public static void CloseConection()
         {
             ZItoDOC_ID_Factory.CloseConection();
         }

        
    }
}
