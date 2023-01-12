using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;

namespace Zamba.Diagrams.Shapes
{
    /// <summary>
    /// Representa un ShapeNode de tipo actor
    /// </summary>
    class IndexShape : GenericShape, IShape, IDisposable
    {
        #region Properties

        /// <summary>
        /// Representa un Actor graficado que puede ser un usuario o un grupo de Zamba
        /// </summary>
        public IIndex Index { get; set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo actor que puede ser un usuario o un grupo de Zamba
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="actor">Actor a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public IndexShape(Diagram diagram, IIndex index, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, index, parentNode, attachToNodeType)
        {
            this.Shape = MindFusion.Diagramming.Shapes.Rectangle;
            this.Index = index;
            this.ShapeType = Core.ShapeType.Index;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this != null)
            {
                if (this.Index != null)
                {
                    this.Index.Dispose();
                    this.Index = null;
                }
                base.Dispose();
            }
        }

        #endregion
    }
}