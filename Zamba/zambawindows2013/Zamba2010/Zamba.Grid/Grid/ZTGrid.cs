using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zamba.Grid.ZT
{
   public class ZTGrid: Telerik.WinControls.UI.RadGridView
    {

        public Boolean UseZamba = false;
        public Boolean useColor = false;

        public void ZTGRid()
        {
            this.ViewRowFormatting += ZTGrid_ViewRowFormatting;
        }

        void ZTGrid_ViewRowFormatting(object sender, Telerik.WinControls.UI.RowFormattingEventArgs e)
        {
       /// <summary>
        /// Marca los documentos no leidos en negrita
        /// </summary>
       
            //Verifica si debe buscar los documentos
            if (this.Columns.Contains("READDATE"))
            {
                //Obtiene la posicion de la columna que contiene la marca de leida
                int readColPosition = this.Columns["READDATE"].Index;

             
                            
           //if (e.RowElement.t is  GridTableHeaderRowElement)
           // {
           //     e.RowElement.DrawFill = true;
           //     e.RowElement.BackColor = Color.Navy;
           //     e.RowElement.NumberOfColors = 1;
           //     e.RowElement.ForeColor = Color.White;
           // }
           // else
           // {
              //  font = new System.Drawing.Font(e.RowElement.Font, FontStyle.Bold);
                if (e.RowElement.RowInfo.Cells[readColPosition].Value == null || e.RowElement.RowInfo.Cells[readColPosition].Value == string.Empty)
                {
                    //Se ponen en negrita
                    e.RowElement.Font = new System.Drawing.Font(e.RowElement.Font, System.Drawing.FontStyle.Bold);
                }
                else
                {
                    //Se quita negrita
                    e.RowElement.Font = new System.Drawing.Font(e.RowElement.Font, System.Drawing.FontStyle.Regular);
                }
            //}
        }

    }
    }

}
