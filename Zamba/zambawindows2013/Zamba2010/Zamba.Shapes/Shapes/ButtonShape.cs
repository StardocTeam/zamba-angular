using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;

namespace Zamba.Diagrams.Shapes
{
    class ButtonShape : GenericShape, IShape, IDisposable
    {
         #region Properties

        /// <summary>
        ///  Representa un Bottde Zamba
        /// </summary>
        public IButtonDiagram Button { get; set; }


         #endregion
          #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo Sections 
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="actor">Actor a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public ButtonShape(Diagram diagram, IButtonDiagram btn, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, btn, parentNode, attachToNodeType)
        {
            
            this.Id = btn.ID;
            this.Text = btn.Name;
            this.Shape = MindFusion.Diagramming.Shapes.File;
            this.Button = btn;
            this.ShapeType = Core.ShapeType.Button;
        }
        #endregion
    }
}
