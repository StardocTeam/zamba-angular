using System;
using System.Drawing;
namespace Zamba.WFShapes
{
    public interface IShapeMaterial: IPaintable, IServiceProvider
    {

        bool Resizable { get; set; }

        bool Gliding { get; set;}
        IShape Shape { get; set;}

        new Rectangle Rectangle
        {         
            get;
        }

        void Transform(Rectangle rectangle);

        bool Visible { get; set;}
    }
}
