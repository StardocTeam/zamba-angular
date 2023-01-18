using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamba.Core;

namespace Zamba.Framework
{
   public class ZThumbsBusiness
    {

        public Image GetThumb(Int64 ResultId )
        {
            try
            {
                Object ThumbObject = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, string.Format("select THUMB from ZTHUMB T where T.DOC_ID = {0}", ResultId));
                if (ThumbObject != null)
                {
                    
                    Byte[] imageBytes = Convert.FromBase64String(ThumbObject.ToString());
                    using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                    {
                        Image image = Image.FromStream(ms, true);
                        return image;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }

    }
}
