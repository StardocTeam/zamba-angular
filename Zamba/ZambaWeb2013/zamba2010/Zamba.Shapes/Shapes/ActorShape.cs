using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;
using System.Drawing;

namespace Zamba.Diagrams.Shapes
{
    /// <summary>
    /// Representa un ShapeNode de tipo actor
    /// </summary>
    class ActorShape : GenericShape, IShape, IDisposable
    {
        #region Properties

        /// <summary>
        /// Representa un Actor graficado que puede ser un usuario o un grupo de Zamba
        /// </summary>
        public IActor Actor { get; set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo actor que puede ser un usuario o un grupo de Zamba
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="actor">Actor a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public ActorShape(Diagram diagram, IActor actor, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, actor, parentNode, attachToNodeType)
        {
            this.Shape = MindFusion.Diagramming.Shapes.Actor;
            this.Actor = actor;
            this.ShapeType = Core.ShapeType.Actor;

            // move text to the right
            StringFormat rightAlmt = new StringFormat();
            rightAlmt.Alignment = StringAlignment.Center;
            rightAlmt.LineAlignment = StringAlignment.Center;
            this.TextFormat = new StringFormat(rightAlmt);
            
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this != null)
            {
                if (this.Actor != null)
                {
                    this.Actor.Dispose();
                    this.Actor = null;
                }
                base.Dispose();
            }
        }

        #endregion
    }
}
