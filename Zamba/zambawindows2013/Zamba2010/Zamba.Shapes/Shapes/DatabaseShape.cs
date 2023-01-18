using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;

namespace Zamba.Diagrams.Shapes
{
    /// <summary>
    /// Representa un ShapeNode de tipo Base de datos
    /// </summary>
    class DatabaseShape : GenericShape, IShape, IDisposable
    {
        #region Properties

        /// <summary>
        /// Representa una base de datos graficada donde Zamba interactua
        /// </summary>
        public DatabaseEntity Database { get; set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo Base de datos
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="database">Base de datos a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public DatabaseShape(Diagram diagram, DatabaseEntity database, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, database, parentNode, attachToNodeType)
        {
            this.Shape = MindFusion.Diagramming.Shapes.Database;
            this.Database = database;
            this.ShapeType = Core.ShapeType.Database;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this != null)
            {
                if (this.Database != null)
                {
                    this.Database.Dispose();
                    this.Database = null;
                }
                base.Dispose();
            }
        }

        #endregion
    }
}
