using Zamba.AppBlock;

namespace Zamba.Shapes.Views
{
    partial class UCWfExportPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlWfTree = new System.Windows.Forms.Panel();
            this.btnSelectNodes = new ZButton();
            this.chkUseTestCases = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // pnlWfTree
            // 
            this.pnlWfTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlWfTree.Location = new System.Drawing.Point(3, 10);
            this.pnlWfTree.Name = "pnlWfTree";
            this.pnlWfTree.Size = new System.Drawing.Size(286, 228);
            this.pnlWfTree.TabIndex = 0;
            // 
            // btnSelectNodes
            // 
            this.btnSelectNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectNodes.Location = new System.Drawing.Point(219, 263);
            this.btnSelectNodes.Name = "btnSelectNodes";
            this.btnSelectNodes.Size = new System.Drawing.Size(73, 23);
            this.btnSelectNodes.TabIndex = 1;
            this.btnSelectNodes.Text = "Seleccionar";
            this.btnSelectNodes.UseVisualStyleBackColor = true;
            this.btnSelectNodes.Click += new System.EventHandler(this.btnSelectNodes_Click);
            // 
            // chkUseTestCases
            // 
            this.chkUseTestCases.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkUseTestCases.AutoSize = true;
            this.chkUseTestCases.Location = new System.Drawing.Point(3, 244);
            this.chkUseTestCases.Name = "chkUseTestCases";
            this.chkUseTestCases.Size = new System.Drawing.Size(182, 17);
            this.chkUseTestCases.TabIndex = 2;
            this.chkUseTestCases.Text = "Imprimir casos de uso para reglas";
            this.chkUseTestCases.UseVisualStyleBackColor = true;
            // 
            // UCWfExportPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 289);
            this.Controls.Add(this.chkUseTestCases);
            this.Controls.Add(this.btnSelectNodes);
            this.Controls.Add(this.pnlWfTree);
            this.Name = "UCWfExportPanel";
            this.Text = "Zamba Software - Workflows";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UCWfExportPanel_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlWfTree;
        private ZButton btnSelectNodes;
        private System.Windows.Forms.CheckBox chkUseTestCases;
    }
}