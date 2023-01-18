using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;

namespace Zamba.Diagrams.Shapes
{
    /// <summary>
    /// Representa un ShapeNode de tipo DocGroup
    /// </summary>
    class WFShape : GenericShape, IShape, IDisposable
    {
        #region Properties  
        /// <summary>
        ///  Representa un Workflow graficada de Zamba
        /// </summary>
        public IWF WF { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo Sections 
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="actor">Actor a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public WFShape(Diagram diagram, IWF wf, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, wf, parentNode, attachToNodeType)
        {
            this.Id = wf.WorkId;
            this.Text = wf.Name;
            this.Shape = MindFusion.Diagramming.Shapes.Procedure;
            this.WF = wf;
            this.ShapeType = Core.ShapeType.Workflow;            
        }
        #endregion
        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this != null)
            {
                if (this.WF != null)
                {
                    this.WF.Dispose();
                    this.WF = null;
                }
                base.Dispose();
            }
        }

        #endregion
    }
}
