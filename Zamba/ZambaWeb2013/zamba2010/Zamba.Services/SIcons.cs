using System;
using System.Drawing;
using Zamba.AppBlock;

namespace Zamba.Services
{
    public class SIcons : IService
    {
        #region IService Members
        public ServicesTypes ServiceType()
        {
            return ServicesTypes.Icons;
        }
        #endregion

        //public static void Draw(Graphics g, Int32 x, Int32 y, Int32 Width, Int32 Height, Int32 Index)
        //{
        //    ZIconsList il = new ZIconsList();
        //    il.ZIconList.Draw (g, x, y, Width, Height, Index);
        //}
    }
}
