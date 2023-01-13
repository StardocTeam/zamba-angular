using System;
using System.Collections.Generic;
using System.Text;

namespace zamba.scan.twain
{
    internal class InvalidCodec_TwainException : Exception
    {
        public override string Message
        {
            get
            {
                return "No existe codec para ekl tipo de imagen";                                      
            }
        }
    }
}
