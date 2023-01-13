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
    class SectionShape : GenericShape, IShape, IDisposable
    {
        #region Properties

        /// <summary>
        ///  Representa una Seccion graficada de Zamba
        /// </summary>
        public ISectionDiagram Section { get; set; }


        #endregion
        #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo Sections 
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="actor">Actor a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public SectionShape(Diagram diagram, ISectionDiagram section, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, section, parentNode, attachToNodeType)
        {
            this.Id = section.ID;
            this.Text = section.Name;
            this.Shape = MindFusion.Diagramming.Shapes.Ellipse;
            this.Section = section;
            this.ShapeType = Core.ShapeType.Sections;            
            
        }
        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this != null)
            {
                if (this.Section != null)
                {
                    this.Section.Dispose();
                    this.Section = null;
                }
                base.Dispose();
            }
        }

        #endregion
    }
}
