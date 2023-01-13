using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for Encode64
/// </summary>
public class Encode64
{
    public Encode64()
    { }

    //ENCODE y DECODE Base32  to Base64 file
    private string Encode(String txtINFilePath)
    {
        try
        {
            string encodedData = string.Empty;
            if (!string.IsNullOrEmpty(txtINFilePath))
            {

                FileStream fs = new FileStream(txtINFilePath,
                                               FileMode.Open,
                                               FileAccess.Read);
                byte[] filebytes = new byte[fs.Length];
                fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
                encodedData = Convert.ToBase64String(filebytes,
                                           Base64FormattingOptions.InsertLineBreaks);
            }

            return encodedData;
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
            return string.Empty;
        }
    }

    private void Decode(String txtOutFile, string txtEncoded)
    {
        try
        {
            FileStream fs;
            if (!string.IsNullOrEmpty(txtOutFile))
            {
                byte[] filebytes = Convert.FromBase64String(txtEncoded);
                fs = new FileStream(txtOutFile,
                                               FileMode.CreateNew,
                                               FileAccess.Write,
                                               FileShare.None);
                fs.Write(filebytes, 0, filebytes.Length);
                fs.Close();
            }
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }
}
