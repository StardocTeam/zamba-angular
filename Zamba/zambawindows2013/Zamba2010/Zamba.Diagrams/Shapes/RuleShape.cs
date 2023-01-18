using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;

namespace Zamba.Diagrams.Shapes
{
    /// <summary>
    /// Representa un ShapeNode de tipo Regla
    /// </summary>
    class RuleShape : GenericShape, IShape, IDisposable
    {
        #region Properties

        /// <summary>
        /// Regla de un proceso graficada
        /// </summary>
        public IRule Rule { get; set; }

        /// <summary>
        /// Tipo de Shape
        /// </summary>
        public new ShapeType ShapeType
        {
            get
            {
                return Core.ShapeType.Rule;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo Regla
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="rule">Regla de una etapa a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public RuleShape(Diagram diagram, IRule rule, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, rule, parentNode, attachToNodeType)
        {
            this.Shape = MindFusion.Diagramming.Shapes.RoundRect;
            this.Rule = rule;
            this.ToolTip = rule.Description;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this != null)
            {
                if (this.Rule != null)
                {
                    this.Rule.Dispose();
                    this.Rule = null;
                }
                base.Dispose();
            }
        }

        #endregion
    }
}
