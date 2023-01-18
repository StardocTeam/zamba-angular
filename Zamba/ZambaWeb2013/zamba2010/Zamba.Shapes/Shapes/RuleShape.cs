using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;
using System.Drawing;
using Zamba.Shapes.Interfaces;

namespace Zamba.Diagrams.Shapes
{
    /// <summary>
    /// Representa un ShapeNode de tipo Regla
    /// </summary>
  public  class RuleShape : GenericShape, IShape, IDisposable, IGenericRuleShape
    {
        #region Properties

        /// <summary>
        /// Regla de un proceso graficada
        /// </summary>
        public IRule Rule { get; set; } 

        /// <summary>
        /// Etapa de Regla de un proceso graficada
        /// </summary>
        public StepShape stepShape { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo Regla
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="rule">Regla de una etapa a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public RuleShape(Diagram diagram, IRule rule, StepShape stepshape, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft,Boolean HasTestCase = false)
            : base(diagram, rule, parentNode, attachToNodeType)
        {
            this.Shape = MindFusion.Diagramming.Shapes.RoundRect;
            if (HasTestCase == true)
            {
                this.ImageAlign = MindFusion.Drawing.ImageAlign.BottomRight;
                this.Image = global::Zamba.Shapes.Properties.Resources.TestCase;
            }

            // move text to the right
            StringFormat rightAlmt = new StringFormat();
            rightAlmt.Alignment = StringAlignment.Center;
            rightAlmt.LineAlignment = StringAlignment.Center;
            this.TextFormat = new StringFormat(rightAlmt);

            this.Rule = rule;
            this.stepShape = stepshape;
            this.ShapeType = Core.ShapeType.Rule;

            if (string.Compare(this.Rule.RuleClass, "DoDesign") == 0)
                this.ToolTip = ((IDoDesign)Rule).Help;
            else
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
