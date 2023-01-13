using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;

namespace Zamba.Diagrams.Shapes
{
    /// <summary>
    /// Representa un ShapeNode de tipo Etapa
    /// </summary>
    public class StepShape : GenericShape, IShape, IDisposable
    {
        #region Properties

        /// <summary>
        /// Etapa de un proceso graficada
        /// </summary>
        public IWFStep Step { get; set; }

        public Zamba.Core.DsRules Rules { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo Etapa.
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="step">Etapa de un proceso a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public StepShape(Diagram diagram, IWFStep step, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, step, parentNode, attachToNodeType)
        {
            this.Shape = MindFusion.Diagramming.Shapes.Ellipse;
            this.Step = step;
            this.ToolTip = step.Description;
            this.ShapeType = Core.ShapeType.Step;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this != null)
            {
                if (this.Step != null)
                {
                    this.Step.Dispose();
                    this.Step = null;
                }
                base.Dispose();
            }
        }

        #endregion
    }
}
