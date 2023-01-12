using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MindFusion.Diagramming;
using Zamba.Core;
using Zamba.Diagrams.Controllers;

namespace Zamba.Diagrams.UserControls
{
    public partial class UCDiagrams : UserControl
    {
        /// <summary>
        /// Genera un UserControl que contiene todos los tabs con los diagramas
        /// </summary>
        public UCDiagrams()
        {
            InitializeComponent();
            TabManager.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// Agrega un tab con un diagrama específico
        /// </summary>
        /// <param name="type">Tipo de diagrama a graficar</param>
        /// <param name="param">Parámetros opcionales que definen espécificamente lo que se irá a dibujar</param>
        public void AddDiagram(DiagramType type, Object[] param = null)
        {
            //Se obtiene el diagrama
            IDiagram diagram = GenericController.GetDiagram(type, param);
            
            //Se crea el tab con el diagrama
            DiagramTab diagramTab = new DiagramTab(diagram);
            diagramTab.DiagramView.Diagram.NodeDoubleClicked += new NodeEventHandler(Diagram_NodeDoubleClicked);

            //Se agrega el tab al TabManager
            TabManager.TabPages.Add(diagramTab);
            TabManager.SelectTab(diagramTab);
        }

        /// <summary>
        /// Evento disparado en el diagrama al hacer doble click sobre un Shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Contiene la propiedad Node que representa el nodo que disparó el evento</param>
        public void Diagram_NodeDoubleClicked(object sender, NodeEventArgs e)
        {
            object[] shape = { e.Node };
            AddDiagram(((IDiagram)e.Node.Parent).DiagramType, shape);
        }
    }
}
