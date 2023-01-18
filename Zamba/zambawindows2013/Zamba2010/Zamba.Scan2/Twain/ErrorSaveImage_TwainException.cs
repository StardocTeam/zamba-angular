using System;
using System.Collections.Generic;
using System.Text;

namespace zamba.scan.twain
{
    internal class ErrorSaveImage_TwainException : Exception
    {
        public override string Message
        {
            get
            {
                return "no se pudo salvar el archivo.";
            }
        }
    }
}
