using System;
using System.Collections.Generic;
using System.Text;

namespace Zamba.WFShapes
{
    public static class ShapeFactory
    {

        public static IShape GetShape(Int32 Id, string shapeName)
        {
            if(string.IsNullOrEmpty(shapeName))
                return null;

            foreach(string shapeType in Enum.GetNames(typeof(ShapeTypes)))
            {
                if(shapeType.ToString().ToLower() ==shapeName.ToLower())
                {
                      return GetShape(Id,(ShapeTypes) Enum.Parse(typeof(ShapeTypes), shapeType));                    
                }
            }
            return null;
        }

        public static IShape GetShape(Int32 Id,ShapeTypes shapeType)
        {
            switch(shapeType)
            {
                case ShapeTypes.SimpleRectangle:
                    return new SimpleRectangle();
                case ShapeTypes.SimpleEllipse:
                    return new SimpleEllipse();
                case ShapeTypes.TextLabel:
                    return new TextLabel();
                case ShapeTypes.ClassShape:
                    return new ClassShape(Id);
                case ShapeTypes.TextOnly:
                    return new TextOnly();                    
                case ShapeTypes.ImageShape:
                    return new ImageShape(Id);
                case ShapeTypes.DecisionShape:
                    return new ClassShape(Id);
                case ShapeTypes.RuleShape:
                    return new RuleShape(Id);
                default:
                    return new ClassShape(Id);
            }

            return null;
        }
  
    }
}
