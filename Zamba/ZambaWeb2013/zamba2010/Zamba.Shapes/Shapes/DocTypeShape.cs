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
    class DocTypeShape : GenericShape, IShape, IDisposable
    {
        #region Properties

        /// <summary>
        /// Representa un Actor graficado que puede ser un usuario o un grupo de Zamba
        /// </summary>
        public IDocType DocType { get; set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo actor que puede ser un usuario o un grupo de Zamba
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="actor">Actor a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public DocTypeShape(Diagram diagram, IDocType docType, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, docType, parentNode, attachToNodeType)
        {
            this.Shape = MindFusion.Diagramming.Shapes.Document;
            this.DocType = docType;
            this.ShapeType = Core.ShapeType.DocType;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this != null)
            {
                if (this.DocType != null)
                {
                    this.DocType.Dispose();
                    this.DocType = null;
                }
                base.Dispose();
            }
        }

        #endregion
    }
}