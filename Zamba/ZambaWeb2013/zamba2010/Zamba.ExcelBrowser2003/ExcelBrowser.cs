using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Zamba
{
    namespace EmbeddedExcel
{
	public partial class ExcelBrowser : Control
	{
		public ExcelBrowser() {
			InitializeComponent();
		}

		public void ShowFile(string File)
        {
				this.excelWrapper1.OpenFile(File);
		}

		public void ShowToolBar(bool Value)
        {
			this.excelWrapper1.ToolBarVisible=Value;
		}
          
    }
}
}