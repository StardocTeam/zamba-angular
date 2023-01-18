using System.Drawing;

namespace Zamba.Grid
{
    partial class OutlookGrid
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
                this.DataError -= new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridView1_DataError);
                
                if (components != null)
                {
                    components.Dispose();
                    components = null;
                }
                if (dataSource != null)
                {
                    dataSource.Dispose();
                    dataSource = null;
                }
                if (iconCollapse != null)
                {
                    iconCollapse.Dispose();
                    iconCollapse = null;
                }
                if (ColumnsFixed != null)
                {
                    ColumnsFixed.Clear();
                    ColumnsFixed = null;
                }
                if (ColumnsVisible != null)
                {
                    ColumnsVisible.Clear();
                    ColumnsVisible = null;
                }
                if (iconExpand != null)
                {
                    iconExpand.Dispose();
                    iconExpand = null;
                }
                if (groupTemplate != null)
                {
                    groupTemplate.Dispose();
                    groupTemplate = null;
                }

                if(Rows.Count > 0)
                {
                    foreach (OutlookGridRow row in Rows)
                    {
                        row.Dispose();
                    }
                }
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
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // OutlookGrid
            // 
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = true;
            this.AllowUserToResizeRows = false;
            this.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ShowCellErrors = false;
            this.ShowEditingIcon = false;
            this.ShowRowErrors = false;
            this.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridView1_DataError);
            
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
