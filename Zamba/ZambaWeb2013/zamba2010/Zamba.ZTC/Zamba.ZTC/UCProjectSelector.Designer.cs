namespace Zamba.ZTC
{
    partial class UCProjectSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            this.radDropDownList1 = new Telerik.WinControls.UI.RadDropDownList();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(16, 47);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(57, 16);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.Text = "Proyecto :";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(16, 14);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(286, 16);
            this.radLabel2.TabIndex = 2;
            this.radLabel2.Text = "Seleccione el proyecto con el cual usted desea trabajar.";
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(16, 109);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(130, 24);
            this.radButton1.TabIndex = 3;
            this.radButton1.Text = "Aceptar";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // radButton2
            // 
            this.radButton2.Location = new System.Drawing.Point(166, 109);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(130, 24);
            this.radButton2.TabIndex = 4;
            this.radButton2.Text = "Cancelar";
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // radDropDownList1
            // 
            this.radDropDownList1.DropDownAnimationEnabled = true;
            this.radDropDownList1.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.radDropDownList1.Location = new System.Drawing.Point(99, 47);
            this.radDropDownList1.Name = "radDropDownList1";
            this.radDropDownList1.ShowImageInEditorArea = true;
            this.radDropDownList1.Size = new System.Drawing.Size(197, 20);
            this.radDropDownList1.TabIndex = 5;
            // 
            // UCProjectSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.radDropDownList1);
            this.Controls.Add(this.radButton2);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel1);
            this.Name = "UCProjectSelector";
            this.Size = new System.Drawing.Size(331, 153);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton radButton2;
        private Telerik.WinControls.UI.RadDropDownList radDropDownList1;
    }
}
