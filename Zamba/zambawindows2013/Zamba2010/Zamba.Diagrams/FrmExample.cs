using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zamba.Diagrams.UserControls;

namespace Zamba.Diagrams
{
    public partial class FrmExample : Form
    {
        public FrmExample()
        {
            InitializeComponent();

            //Se agrega el control de tabs
            UCDiagrams ucDiagrams = new UCDiagrams();
            ucDiagrams.Dock = DockStyle.Fill;
            ucDiagrams.AddDiagram(Core.DiagramType.Actors);

            this.Controls.Add(ucDiagrams);
        }
    }
}
