using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;
using Zamba.Shapes.Interfaces;

namespace Zamba.Diagrams.Shapes
{
    /// <summary>
    /// Representa un ShapeNode de tipo Regla condicional
    /// </summary>
    class ConditionalRuleShape : GenericShape, IShape, IDisposable, IGenericRuleShape
    {
        #region Properties

        /// <summary>
        /// Regla condicional graficada
        /// </summary>
        public IRule Rule { get; set; }
        public StepShape stepShape { get; set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo Regla condicional
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="conditionalRule">Regla condicional de una etapa a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public ConditionalRuleShape(Diagram diagram, IRule conditionalRule, StepShape stepshape, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, conditionalRule, parentNode, attachToNodeType)
        {
            this.Shape = MindFusion.Diagramming.Shapes.Decision;
            this.Rule = conditionalRule;
            this.stepShape = stepshape;
            this.ToolTip = conditionalRule.Description;
            this.ShapeType = Core.ShapeType.ConditionalRule;
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
