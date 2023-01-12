using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamba.Core;

namespace Zamba.FileTools
{
    public class ThumbConfig
    {
        public int Width;
        public int Height;
        public int Dpix;
        public int Dpiy;
        public bool StoreInDB;
        public string ThumbDirectory { get; internal set; }

        public ThumbConfig()
        {
            try
            {
                if (!int.TryParse(ZOptBusiness.GetValueOrDefault("ThumbConfigWidth", "500"), out Width))
                    Width = 500;

                if (!int.TryParse(ZOptBusiness.GetValueOrDefault("ThumbConfigHeight", "750"), out Height))
                    Height = 750;

                if (!int.TryParse(ZOptBusiness.GetValueOrDefault("ThumbConfigDpix", "750"), out Dpix))
                    Dpix = 500;

                if (!int.TryParse(ZOptBusiness.GetValueOrDefault("ThumbConfigDpiy", "750"), out Dpiy))
                    Dpiy = 500;

                if (!Boolean.TryParse(ZOptBusiness.GetValueOrDefault("ThumbStoreInDB", "false"), out StoreInDB))
                    StoreInDB = false;

                if (StoreInDB == false)
                {
                    ThumbDirectory = ZOptBusiness.GetValue("ThumbStoreDirectory");
                    if (ThumbDirectory == string.Empty)
                        throw new Exception("Thumbs Directory For File System is not configured in ZOPT for ThumbStoreDirectory");
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }



    }
}
