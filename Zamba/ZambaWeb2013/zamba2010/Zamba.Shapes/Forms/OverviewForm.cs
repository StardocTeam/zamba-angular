//
// Copyright (c) 2012, MindFusion LLC - Bulgaria.
//

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace Zamba.Shapes.Forms
{
	/// <summary>
	/// Summary description for OverviewForm.
	/// </summary>
	public partial class OverviewForm : Form
	{
#if !STANDARD
		public OverviewForm()
		{
			InitializeComponent();

			this.Location = new Point(
				Screen.PrimaryScreen.Bounds.Width - this.Width - 50,
				Screen.PrimaryScreen.Bounds.Height - this.Height - 50);
		}

		public void SetView(MindFusion.Diagramming.WinForms.DiagramView view)
		{
			_overview.DiagramView = view;
			_overview.Update();
		}

		public void UpdateDocument()
		{
			_overview.Update();
		}

		private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			Hide();
		}

#else
		public System.Windows.Forms.Form Owner { set {} }
		public bool Visible { get{ return false; } set {} }

		public void SetView(MindFusion.Diagramming.WinForms.DiagramView document) {}
		public void UpdateDocument() {}
		public void Show() {}

		public PointF Location { set {} }
#endif // !STANDARD
	}
}