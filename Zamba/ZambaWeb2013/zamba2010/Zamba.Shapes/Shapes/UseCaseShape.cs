using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;

namespace Zamba.Diagrams.Shapes
{
    /// <summary>
    /// Representa un ShapeNode de tipo Caso de uso
    /// </summary>
    class UseCaseShape : GenericShape, IShape, IDisposable
    {
        #region Properties

        /// <summary>
        /// Representa el caso de uso graficado
        /// </summary>
        public UseCase UseCase { get; set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo Caso de uso.
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="useCase">Caso de uso a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public UseCaseShape(Diagram diagram, UseCase useCase, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, useCase, parentNode, attachToNodeType)
        {
            this.Shape = MindFusion.Diagramming.Shapes.Ellipse;
            this.UseCase = useCase;
            this.ShapeType = Core.ShapeType.UseCase;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this != null)
            {
                if (this.UseCase != null)
                {
                    this.UseCase.Dispose();
                    this.UseCase = null;
                }
                base.Dispose();
            }
        }

        #endregion
    }
}
