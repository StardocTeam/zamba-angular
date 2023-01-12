namespace DbToXML
{
    partial class CompareOptions
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
            this.gridSource = new System.Windows.Forms.DataGridView();
            this.gridDestiny = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdkeppboth = new System.Windows.Forms.RadioButton();
            this.btncancel = new System.Windows.Forms.Button();
            this.btnok = new System.Windows.Forms.Button();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.gridResult = new System.Windows.Forms.DataGridView();
            this.chkdontaskagain = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDestiny)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridResult)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSource
            // 
            this.gridSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridSource.Location = new System.Drawing.Point(0, 0);
            this.gridSource.Name = "gridSource";
            this.gridSource.Size = new System.Drawing.Size(933, 96);
            this.gridSource.TabIndex = 0;
            // 
            // gridDestiny
            // 
            this.gridDestiny.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDestiny.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridDestiny.Location = new System.Drawing.Point(0, 96);
            this.gridDestiny.Name = "gridDestiny";
            this.gridDestiny.Size = new System.Drawing.Size(933, 101);
            this.gridDestiny.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkdontaskagain);
            this.groupBox1.Controls.Add(this.rdkeppboth);
            this.groupBox1.Controls.Add(this.btncancel);
            this.groupBox1.Controls.Add(this.btnok);
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 269);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(933, 194);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // rdkeppboth
            // 
            this.rdkeppboth.AutoSize = true;
            this.rdkeppboth.Location = new System.Drawing.Point(6, 154);
            this.rdkeppboth.Name = "rdkeppboth";
            this.rdkeppboth.Size = new System.Drawing.Size(146, 17);
            this.rdkeppboth.TabIndex = 6;
            this.rdkeppboth.TabStop = true;
            this.rdkeppboth.Text = "Mantener ambos registros";
            this.rdkeppboth.UseVisualStyleBackColor = true;
            this.rdkeppboth.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(667, 93);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(75, 23);
            this.btncancel.TabIndex = 5;
            this.btncancel.Text = "Cancelar";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnok
            // 
            this.btnok.Location = new System.Drawing.Point(574, 93);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(75, 23);
            this.btnok.TabIndex = 4;
            this.btnok.Text = "Guardar";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(7, 121);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(293, 17);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "No insertar ni actualziar el registro de origen en el destino";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(7, 87);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(313, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Cambiar Id en Destino e Insertar Registro Origen con su valor";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(7, 53);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(439, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Insertar Origen como registro nuevo cambiando el ID y mantener Origen con sus val" +
    "ores";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 18);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(231, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Actualizar destino con los valores de Origen";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // gridResult
            // 
            this.gridResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridResult.Location = new System.Drawing.Point(0, 197);
            this.gridResult.Name = "gridResult";
            this.gridResult.Size = new System.Drawing.Size(933, 72);
            this.gridResult.TabIndex = 3;
            // 
            // chkdontaskagain
            // 
            this.chkdontaskagain.AutoSize = true;
            this.chkdontaskagain.Location = new System.Drawing.Point(574, 139);
            this.chkdontaskagain.Name = "chkdontaskagain";
            this.chkdontaskagain.Size = new System.Drawing.Size(194, 17);
            this.chkdontaskagain.TabIndex = 7;
            this.chkdontaskagain.Text = "Repetir respuesta para toda la tabla";
            this.chkdontaskagain.UseVisualStyleBackColor = true;
            // 
            // CompareOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(933, 463);
            this.Controls.Add(this.gridResult);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gridDestiny);
            this.Controls.Add(this.gridSource);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CompareOptions";
            this.Text = "Resolver Conflicto";
            this.Load += new System.EventHandler(this.CompareOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDestiny)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridSource;
        private System.Windows.Forms.DataGridView gridDestiny;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.DataGridView gridResult;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Button btnok;
        private System.Windows.Forms.RadioButton rdkeppboth;
        private System.Windows.Forms.CheckBox chkdontaskagain;
    }
}