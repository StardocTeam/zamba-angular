using System.Windows.Forms;
using Zamba.AppBlock;

namespace Zamba.WorkFlow.Execution.Control
{
    partial class WorkflowDesignerControl
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <_param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</_param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkflowDesignerControl));
            this.ZToolBar = new ZToolBar();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnEjecutar = new System.Windows.Forms.ToolStripButton();
            this.btnZoom = new System.Windows.Forms.ToolStripButton();
            this.txtFakeZoom = new ToolStripTextBox();
            this.lblZoom = new System.Windows.Forms.ToolStripLabel();
            this.txtZoom = new System.Windows.Forms.NumericUpDown();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.ZToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // ZToolBar
            // 
            this.ZToolBar.AllowItemReorder = true;
            this.ZToolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(1)))));
            this.ZToolBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ZToolBar.Items.AddRange(new ToolStripItem[] {
            this.btnSave,
            this.btnEjecutar,
            this.btnZoom,
            this.txtFakeZoom,
            this.lblZoom,
            this.btnClose});
            this.ZToolBar.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ZToolBar.Location = new System.Drawing.Point(0, 0);
            this.ZToolBar.Name = "ZToolBar";
            this.ZToolBar.RenderMode = ToolStripRenderMode.System;
            this.ZToolBar.Size = new System.Drawing.Size(741, 25);
            this.ZToolBar.TabIndex = 2;
            this.ZToolBar.Text = "ZToolBar1";
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
            // btnEjecutar
            // 
            this.btnEjecutar.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnEjecutar.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutar.Image")));
            this.btnEjecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(51, 22);
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.Alignment = ToolStripItemAlignment.Right;
            this.btnZoom.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.btnZoom.Image = ((System.Drawing.Image)(resources.GetObject("btnZoom.Image")));
            this.btnZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(23, 22);
            this.btnZoom.Text = "ToolStripButton1";
            this.btnZoom.ToolTipText = "Zoom";
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // txtFakeZoom
            // 
            this.txtFakeZoom.Alignment = ToolStripItemAlignment.Right;
            this.txtFakeZoom.Name = "txtFakeZoom";
            this.txtFakeZoom.Size = new System.Drawing.Size(45, 25);
            // 
            // lblZoom
            // 
            this.lblZoom.Alignment = ToolStripItemAlignment.Right;
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(33, 22);
            this.lblZoom.Text = "Zoom";
            // 
            // txtZoom
            // 
            this.txtZoom.Location = new System.Drawing.Point(672, 18);
            this.txtZoom.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.txtZoom.Name = "txtZoom";
            this.txtZoom.Size = new System.Drawing.Size(45, 20);
            this.txtZoom.TabIndex = 3;
            this.txtZoom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtZoom.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // btnClose
            // 
            this.btnClose.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(42, 22);
            this.btnClose.Text = "Cerrar";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // WorkflowDesignerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtZoom);
            this.Controls.Add(this.ZToolBar);
            this.Name = "WorkflowDesignerControl";
            this.Size = new System.Drawing.Size(741, 441);
            this.ZToolBar.ResumeLayout(false);
            this.ZToolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtZoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZToolBar ZToolBar;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnEjecutar;
        private System.Windows.Forms.ToolStripButton btnZoom;
        private ToolStripTextBox txtFakeZoom;
        private System.Windows.Forms.ToolStripLabel lblZoom;
        private System.Windows.Forms.NumericUpDown txtZoom;
        private System.Windows.Forms.ToolStripButton btnClose;
    }
}
