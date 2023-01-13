using System;
using System.Collections.Generic;
using System.Text;
using MindFusion.Diagramming;
using Zamba.Core;

namespace Zamba.Diagrams.Shapes
{
    /// <summary>
    /// Representa un ShapeNode genérico
    /// </summary>
    class InterfaceShape : GenericShape, IShape, IDisposable
    {
        #region Properties

        /// <summary>
        /// Id del ShapeNode padre
        /// </summary>
        public object ParentId { get; set; }

        /// <summary>
        /// Tipo de Shape
        /// </summary>
        private ShapeType shapetype = ShapeType.Generic;

        public ShapeType ShapeType
        {
            get { return shapetype; }
            set { shapetype = value; }
        }

        /// <summary>
        /// Nodos hijos
        /// </summary>
        public List<ShapeNode> Childs { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Genera un ShapeNode generico
        /// </summary>
        /// <param name="diagram">Diagrama de ubicación</param>
        /// <param name="generic">Objeto de Zamba genérico a graficar</param>
        /// <param name="parentNode">Nodo padre</param>
        /// <param name="attachToNodeType">Modo de ubicar los nodos</param>
        public InterfaceShape(Diagram diagram, IInterfaceDiagram generic, GenericShape parentNode = null, AttachToNode attachToNodeType = AttachToNode.TopLeft)
            : base(diagram, generic, parentNode, attachToNodeType)
        {
            //Generic attributes
            //this.Id = generic.ID;
            //this.Text = generic.Name;
            this.ToolTip = generic.Name;
            this.ShapeType = Core.ShapeType.Interfaces;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (this != null)
            {
                if (this.Childs != null)
                {
                    for (int i = 0; i < this.Childs.Count; i++)
                        if (this.Childs[i] != null) this.Childs[i].Dispose();
                    this.Childs.Clear();
                }

                this.Childs = null;
                this.ParentId = null;
                base.Dispose();
            }
        }

        #endregion
    }
}
