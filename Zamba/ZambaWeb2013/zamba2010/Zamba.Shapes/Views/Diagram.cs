using System;
using MindFusion.Diagramming;
using Zamba.Core;
using System.Data;

namespace Zamba.Shapes.Views
{
    class Diagram: MindFusion.Diagramming.Diagram, IDiagram, IDisposable
    {
        #region IDiagram Members

        /// <summary>
        /// Especifica el tipo de diagrama
        /// </summary>
        public DiagramType DiagramType { get; set; }

        /// <summary>
        /// Actores
        /// </summary>
        public DataTable DiagramActors { get; set; }

        /// <summary>
        /// Tab de diagrama padre
        /// </summary>
        public DiagramTab PreviousDiagramTab { get; set; }

        #endregion

        #region Constructors

        public Diagram()
        {
            Font = new System.Drawing.Font("Microsoft Sans Serif", 2.9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            LinkHeadShape = ArrowHeads.PointerArrow;
            LinkHeadShapeSize = 3F;
            NodesExpandable = true;
            RecursiveExpand = false;
            SelectAfterCreate = false;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
