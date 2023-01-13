using System.Drawing;
using Telerik.WinControls.UI;

namespace Zamba.Grid
{
    partial class NewGrid
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //this.DataError -= new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridView1_DataError);
                
                if (components != null)
                {
                    components.Dispose();
                    components = null;
                }
                //if (dataSource != null)
                //{
                //    dataSource.Dispose();
                //    dataSource = null;
                //}
                //if (iconCollapse != null)
                //{
                //    iconCollapse.Dispose();
                //    iconCollapse = null;
                //}
                //if (ColumnsFixed != null)
                //{
                //    ColumnsFixed.Clear();
                //    ColumnsFixed = null;
                //}
                //if (ColumnsVisible != null)
                //{
                //    ColumnsVisible.Clear();
                //    ColumnsVisible = null;
                //}
                //if (iconExpand != null)
                //{
                //    iconExpand.Dispose();
                //    iconExpand = null;
                //}
                //if (groupTemplate != null)
                //{
                //    groupTemplate.Dispose();
                //    groupTemplate = null;
                //}

                //if(Rows.Count > 0)
                //{
                //    foreach (OutlookGridRow row in Rows)
                //    {
                //        row.Dispose();
                //    }
                //}
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // NewGrid
            // 
            // 
            // 
            // 
            this.MasterTemplate.ViewDefinition = tableViewDefinition1;
            ((System.ComponentModel.ISupportInitialize)(this.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
