using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Stardoc.HtmlEditor
{
    internal partial class ErrorHandler : Form
    {
        public ErrorHandler(List<String> errorMessages)
        {
            InitializeComponent();

            foreach (String Error in errorMessages)
                lstErrores.Items.Add(Error);
        }

        private void btAccept_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}