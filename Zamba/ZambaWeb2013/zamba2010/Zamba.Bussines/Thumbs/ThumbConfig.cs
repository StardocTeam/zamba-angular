using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamba.FileTools
{
    public static class ThumbConfig
    {
        public static readonly int Width = 200;
        public static readonly int Height = 250;
        public static readonly string TempPath = Zamba.Membership.MembershipHelper.AppTempPath + "\\temp\\";
        static ThumbConfig()
        {
        }
    }  
}
