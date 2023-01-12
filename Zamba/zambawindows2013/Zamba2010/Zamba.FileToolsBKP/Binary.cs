using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Zamba.FileTools
{
    public class Binary
    {
        public string GetFileText(string FileName, bool Errors)
        {
            string Text = string.Empty;

            FileInfo Fi = new FileInfo(FileName);

            FileStream Fs = null;
            StreamReader Sr = null;

            Errors = false;

            try
            {
                Fs = Fi.OpenRead();
                Sr = new StreamReader(FileName, System.Text.Encoding.GetEncoding(1252));
            }
            catch (System.IO.IOException )
            {
                if ((Fs == null) == false) Fs.Close();
                if ((Sr == null) == false) Sr.Close();

                Errors = true;
            }

            if (Errors == false)
            {
                Text = Sr.ReadToEnd().ToLower();
                Sr.Close();
                Fs.Close();
            }
            return Text;
        }
    }
}
