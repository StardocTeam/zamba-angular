using System;
using System.Collections.Generic;
using System.Text;
using Zamba.Core;
using Zamba.FileTools;
using System.Data;

namespace Zamba.Services
{
    public class SFileTools : IService 
    {
        /// <summary>
        /// Exporta a excel un datatable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="path"></param>
        public Boolean ExportToXLS(DataTable dt, String path)
        {
            Zamba.FileTools.SpireTools sp = new Zamba.FileTools.SpireTools();
            try
            {
                sp.ExportToXLS(dt, path);
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
            finally
            {
                sp = null;
            }
        }

        /// <summary>
        /// Exporta a csv un datatable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="path"></param>
        public Boolean ExportToCSV(DataTable dt, String path)
        {
            Zamba.FileTools.SpireTools sp = new Zamba.FileTools.SpireTools();
            try
            {
                sp.ExportToCSV(dt, path);
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
            finally
            {
                sp = null;
            }
        }

        public ServicesTypes ServiceType()
        {
           return ServicesTypes.FileTools;
        }
    }
}
