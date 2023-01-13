using Zamba.AppBlock;

namespace Zamba.Simulator
{
    partial class FrmDictionaryABM
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.txtDictionaryName = new System.Windows.Forms.TextBox();
            this.lblDictionaryName = new ZLabel();
            this.btnSave = new ZButton();
            this.btnCancel = new ZButton();
            this.btnGenerateData = new ZButton();
            this.btnSelectTask = new ZButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(15, 37);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(823, 405);
            this.dgv.TabIndex = 10;
            // 
            // txtDictionaryName
            // 
            this.txtDictionaryName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDictionaryName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDictionaryName.Location = new System.Drawing.Point(70, 10);
            this.txtDictionaryName.Name = "txtDictionaryName";
            this.txtDictionaryName.Size = new System.Drawing.Size(766, 21);
            this.txtDictionaryName.TabIndex = 13;
            this.txtDictionaryName.TextChanged += new System.EventHandler(this.txtDictionaryName_TextChanged);
            // 
            // lblDictionaryName
            // 
            this.lblDictionaryName.AutoSize = true;
            this.lblDictionaryName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDictionaryName.Location = new System.Drawing.Point(12, 13);
            this.lblDictionaryName.Name = "lblDictionaryName";
            this.lblDictionaryName.Size = new System.Drawing.Size(52, 15);
            this.lblDictionaryName.TabIndex = 14;
            this.lblDictionaryName.Text = "Nombre";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Honeydew;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(582, 457);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 35);
            this.btnSave.TabIndex = 34;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(711, 457);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 35);
            this.btnCancel.TabIndex = 35;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnGenerateData
            // 
            this.btnGenerateData.BackgroundImage = global::Zamba.Simulator.Properties.Resources.settings2_64;
            this.btnGenerateData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGenerateData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateData.ForeColor = System.Drawing.Color.Transparent;
            this.btnGenerateData.Location = new System.Drawing.Point(67, 446);
            this.btnGenerateData.Name = "btnGenerateData";
            this.btnGenerateData.Size = new System.Drawing.Size(46, 46);
            this.btnGenerateData.TabIndex = 37;
            this.btnGenerateData.UseVisualStyleBackColor = true;
            this.btnGenerateData.Click += new System.EventHandler(this.btnGenerateData_Click);
            // 
            // btnSelectTask
            // 
            this.btnSelectTask.BackgroundImage = global::Zamba.Simulator.Properties.Resources.outline_64;
            this.btnSelectTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectTask.ForeColor = System.Drawing.Color.Transparent;
            this.btnSelectTask.Location = new System.Drawing.Point(15, 446);
            this.btnSelectTask.Name = "btnSelectTask";
            this.btnSelectTask.Size = new System.Drawing.Size(46, 46);
            this.btnSelectTask.TabIndex = 36;
            this.btnSelectTask.UseVisualStyleBackColor = true;
            this.btnSelectTask.Click += new System.EventHandler(this.btnSelectTask_Click);
            // 
            // FrmDictionaryABM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(848, 504);
            this.Controls.Add(this.btnGenerateData);
            this.Controls.Add(this.btnSelectTask);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblDictionaryName);
            this.Controls.Add(this.txtDictionaryName);
            this.Controls.Add(this.dgv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDictionaryABM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SimDataEntry";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox txtDictionaryName;
        private ZLabel lblDictionaryName;
        private ZButton btnSave;
        private ZButton btnCancel;
        private ZButton btnGenerateData;
        private ZButton btnSelectTask;
    }
}