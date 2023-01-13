using System.Windows.Forms;
using Zamba.AppBlock;

namespace Zamba.WorkFlow.Designer
{
    partial class WorkFlowDesignerControl
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkFlowDesignerControl));
            this.ZToolBar = new ZToolBar();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnName = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnDocAsoc = new System.Windows.Forms.ToolStripButton();
            this.ZToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // ZToolBar
            // 
            this.ZToolBar.GripStyle = ToolStripGripStyle.Hidden;
            this.ZToolBar.Items.AddRange(new ToolStripItem[] {
            this.btnDelete,
            this.btnName,
            this.ToolStripSeparator2,
            this.btnSave,
            this.ToolStripSeparator1,
            this.btnExport,
            this.btnPrint,
            this.btnDocAsoc});
            this.ZToolBar.Location = new System.Drawing.Point(0, 0);
            this.ZToolBar.Name = "ZToolBar";
            this.ZToolBar.RenderMode = ToolStripRenderMode.System;
            this.ZToolBar.Size = new System.Drawing.Size(527, 25);
            this.ZToolBar.TabIndex = 2;
            this.ZToolBar.Text = "ZToolBar1";
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(41, 22);
            this.btnDelete.Text = "Borrar";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnName
            // 
            this.btnName.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnName.Image = ((System.Drawing.Image)(resources.GetObject("btnName.Image")));
            this.btnName.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnName.Name = "btnName";
            this.btnName.Size = new System.Drawing.Size(48, 22);
            this.btnName.Text = "Nombre";
            this.btnName.Click += new System.EventHandler(this.btnName_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(50, 22);
            this.btnSave.Text = "Guardar";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(53, 22);
            this.btnExport.Text = "Exportar";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(49, 22);
            this.btnPrint.Text = "Imprimir";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDocAsoc
            // 
            this.btnDocAsoc.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnDocAsoc.Image = ((System.Drawing.Image)(resources.GetObject("btnDocAsoc.Image")));
            this.btnDocAsoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDocAsoc.Name = "btnDocAsoc";
            this.btnDocAsoc.Size = new System.Drawing.Size(134, 22);
            this.btnDocAsoc.Text = "Documento Respaldatorio";
            this.btnDocAsoc.Click += new System.EventHandler(this.btnDocAsoc_Click);
            // 
            // WorkFlowDesignerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ZToolBar);
            this.Name = "WorkFlowDesignerControl";
            this.Size = new System.Drawing.Size(527, 257);
            this.ZToolBar.ResumeLayout(false);
            this.ZToolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZToolBar ZToolBar;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton btnName;
        private System.Windows.Forms.ToolStripButton btnDocAsoc;
    }
}
