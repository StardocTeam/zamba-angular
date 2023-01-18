using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;

namespace Zamba.Diagrams.Shapes
{
    /// <summary>
    /// Representa un ShapeNode de tipo Workflow
    /// </summary>
    class WorkflowShape : GenericShape, IShape, IDisposable
    {
        #region Properties

        /// <summary>
        /// Representa un Proceso graficado
        /// </summary>
        public IWorkFlow Workflow { get; set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo Workflow
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="workflow">Proceso de Zamba a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public WorkflowShape(Diagram diagram, IWorkFlow workflow, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, workflow, parentNode, attachToNodeType)
        {
            this.Shape = MindFusion.Diagramming.Shapes.RoundRect;
            this.Workflow = workflow;
            this.ToolTip = workflow.Description;
            this.ShapeType = Core.ShapeType.Workflow;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this != null)
            {
                if (this.Workflow != null)
                {
                    this.Workflow.Dispose();
                    this.Workflow = null;
                }
                base.Dispose();
            }
        }

        #endregion
    }
}
