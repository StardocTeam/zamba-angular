//
// Copyright (c) 2012, MindFusion LLC - Bulgaria.
//

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace MindFusion.Diagramming.WinForms.Samples.CS.DBDesign
{
	/// <summary>
	/// Summary description for AddRowFrom.
	/// </summary>
	public partial class AddRowForm : Form
	{
		public AddRowForm()
		{
			InitializeComponent();
		}

		public string FieldName
		{
			get { return fieldName.Text; }
		}

		public string FieldType
		{
			get { return fieldType.Text; }
		}
	}
}
