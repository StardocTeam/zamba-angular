using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;

namespace Zamba.Diagrams.Diagrams
{
    class Diagram: MindFusion.Diagramming.Diagram, IDiagram, IDisposable
    {
        #region IDiagram Members

        /// <summary>
        /// Especifica el tipo de diagrama
        /// </summary>
        public DiagramType DiagramType { get; set; }

        #endregion

        #region Constructors

        public Diagram()
            : base()
        {
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 2.9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.LinkHeadShape = MindFusion.Diagramming.ArrowHeads.DefaultFlow;
            this.LinkHeadShapeSize = 3F;
            this.NodesExpandable = true;
            this.RecursiveExpand = true;
            this.SelectAfterCreate = false;
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
