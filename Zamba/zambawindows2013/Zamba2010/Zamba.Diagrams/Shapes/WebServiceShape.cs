using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;

namespace Zamba.Diagrams.Shapes
{
    /// <summary>
    /// Representa un ShapeNode de tipo WebService
    /// </summary>
    class WebServiceShape : GenericShape, IShape, IDisposable
    {
        #region Properties

        /// <summary>
        /// Representa un WebService graficado consumido por Zamba
        /// </summary>
        public WebService WebService { get; set; }

        /// <summary>
        /// Tipo de Shape
        /// </summary>
        public new ShapeType ShapeType
        {
            get
            {
                return Core.ShapeType.WebService;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo WebService
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="webService">WebService a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public WebServiceShape(Diagram diagram, WebService webService, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, webService, parentNode, attachToNodeType)
        {
            this.Shape = MindFusion.Diagramming.Shapes.Cloud;
            this.WebService = webService;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this != null)
            {
                if (this.WebService != null)
                {
                    this.WebService.Dispose();
                    this.WebService = null;
                }
                base.Dispose();
            }
        }

        #endregion
    }
}
