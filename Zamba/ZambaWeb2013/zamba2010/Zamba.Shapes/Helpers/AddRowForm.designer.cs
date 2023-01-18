//
// Copyright (c) 2012, MindFusion LLC - Bulgaria.
//


using Zamba.AppBlock;

namespace MindFusion.Diagramming.WinForms.Samples.CS.DBDesign
{
	partial class AddRowForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddRowForm));
			this.label1 = new ZLabel();
			this.fieldName = new System.Windows.Forms.TextBox();
			this.label2 = new ZLabel();
			this.fieldType = new System.Windows.Forms.ComboBox();
			this.btnOK = new ZButton();
			this.btnCancel = new ZButton();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Field name:";
			// 
			// fieldName
			// 
			this.fieldName.Location = new System.Drawing.Point(92, 24);
			this.fieldName.Name = "fieldName";
			this.fieldName.Size = new System.Drawing.Size(152, 20);
			this.fieldName.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Field type:";
			// 
			// fieldType
			// 
			this.fieldType.Items.AddRange(new object[] {
			"NUMBER",
			"CHAR(32)",
			"DATE",
			"VARCHAR",
			"BLOB"});
			this.fieldType.Location = new System.Drawing.Point(92, 50);
			this.fieldType.Name = "fieldType";
			this.fieldType.Size = new System.Drawing.Size(152, 21);
			this.fieldType.TabIndex = 3;
			this.fieldType.Text = "NUMBER";
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(45, 90);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(80, 24);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(131, 90);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 24);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			// 
			// AddRowForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(256, 126);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.fieldType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.fieldName);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddRowForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "New Field";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private ZLabel label1;
		private System.Windows.Forms.TextBox fieldName;
		private ZLabel label2;
		private System.Windows.Forms.ComboBox fieldType;
		private ZButton btnOK;
		private ZButton btnCancel;
	}
}
