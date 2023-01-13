using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MindFusion.Diagramming.WinForms;
using Zamba.Core;

namespace Zamba.Diagrams.UserControls
{
    class DiagramTab : TabPage
    {
        public DiagramView DiagramView { get; set; }

        public DiagramTab(IDiagram diagram){
            //Agrega el diagrama al controlador de diagramas de windows
            this.DiagramView = new DiagramView((MindFusion.Diagramming.Diagram)diagram);
            this.DiagramView.ShowScrollbars = true;
            this.DiagramView.AutoScroll = true;

            //Agrega el controlador de diagramas al tab
            this.DiagramView.Dock = DockStyle.Fill;
            this.Controls.Add(this.DiagramView);

            //Oculta el trial
            Panel pnlTrial = new Panel();
            pnlTrial.Height = 20;
            pnlTrial.Width = 300;
            pnlTrial.Top = 0;
            pnlTrial.Left = 0;
            Label lblMessage = new Label();
            lblMessage.Width = this.DiagramView.Width;
            lblMessage.Text = "Panel que oculta el trial de DiagramView. Aca poner el nombre del diagrama y punto";
            pnlTrial.Controls.Add(lblMessage);
            
            this.Controls.Add(pnlTrial);
            pnlTrial.BringToFront();
        }
    }
    
}
