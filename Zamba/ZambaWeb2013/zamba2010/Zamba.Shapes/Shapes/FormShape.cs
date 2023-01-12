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
    class FormShape : GenericShape, IShape, IDisposable
    {
        #region Properties

        /// <summary>
        /// Representa un Actor graficado que puede ser un usuario o un grupo de Zamba
        /// </summary>
        public IZwebForm ZForm { get; set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode de tipo actor que puede ser un usuario o un grupo de Zamba
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="zForm">Actor a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public FormShape(Diagram diagram, IZwebForm zForm, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, new ZCoreView(zForm.ID,zForm.Name), parentNode, attachToNodeType)
        {
            this.Shape = MindFusion.Diagramming.Shapes.LinedDocument;
            this.ZForm = zForm;
            this.ShapeType = Core.ShapeType.ZForms;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this != null)
            {
                if (this.ZForm != null)
                {
                    this.ZForm = null;
                }
                base.Dispose();
            }
        }

        #endregion
    }
}
