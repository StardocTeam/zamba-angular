using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamba.Core;

namespace Zamba.FileTools
{
    public  class ThumbConfig
    {
        public  int Width;
        public  int Height;
        public  string TempPath;
       public ThumbConfig()
        {
            try
            {
                if (! int.TryParse(ZOptBusiness.GetValue("ThumbConfigWidth"), out Width))
                 Width = 200;

                 if (! int.TryParse(ZOptBusiness.GetValue("ThumbConfigHeight"),out Height))
                Height = 250;

                TempPath = Membership.MembershipHelper.AppTempPath + "\\temp\\";

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }
}
