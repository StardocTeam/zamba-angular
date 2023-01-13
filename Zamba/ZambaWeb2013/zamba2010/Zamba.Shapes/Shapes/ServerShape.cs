using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;

namespace Zamba.Diagrams.Shapes
{
    /// <summary>
    /// Representa un ShapeNode de tipo Servidor
    /// </summary>
    class ServerShape : GenericShape, IShape, IDisposable
    {
        #region Properties

        /// <summary>
        /// Representa un Servidor graficado donde Zamba interactua
        /// </summary>
        public ServerEntity Server { get; set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo Servidor
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="server">Servidor a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public ServerShape(Diagram diagram, ServerEntity server, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, server, parentNode, attachToNodeType)
        {
            this.Shape = MindFusion.Diagramming.Shapes.Cube;
            this.Server = server;
            this.ToolTip = server.IP;
            this.ShapeType = Core.ShapeType.Server;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this != null)
            {
                if (this.Server != null)
                {
                    this.Server.Dispose();
                    this.Server = null;
                }
                base.Dispose();
            }
        }

        #endregion
    }
}
