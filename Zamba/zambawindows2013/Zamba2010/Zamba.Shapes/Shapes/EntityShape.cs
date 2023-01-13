using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;

namespace Zamba.Diagrams.Shapes
{
    /// <summary>
    /// Representa un ShapeNode de tipo DocGroup
    /// </summary>
    class EntityShape : GenericShape, IShape, IDisposable
    {
        #region Properties

        /// <summary>
        ///  Representa una Seccion graficada de Zamba
        /// </summary>
        public IEntityDiagram Entity { get; set; }

        #endregion

                #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo Sections 
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="actor">Actor a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public EntityShape(Diagram diagram, IEntityDiagram entity, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, entity, parentNode, attachToNodeType)
        {
            
            this.Id = entity.ID;
            this.Text = entity.Name;
            this.Shape = MindFusion.Diagramming.Shapes.File;
            this.Entity = entity;
            this.ShapeType = Core.ShapeType.Entities;
        }
        #endregion
    }
}
