using System;
using System.Net;
using System.IO;


namespace Zamba.Framework
{
    public  class FtpHelper
    {

        public Boolean DownloadFileFromFtp(string RemoteFtpPath, string Username, string Password, string LocalDestinationPath)
        {
            try
            {

                //RemoteFtpPath = "ftp://svrpdf.main.pseguros.com/../../pdfs/2072/20720732.pdf";
                //LocalDestinationPath = "./log/temp/test.pdf";
                //Username = "zambausr";
                //Password = "Zamba1234";

                // No reconoce los dos puntos para moverse por las carpetas
                string parentDirStr = @"../";
                string parentDirStrCorrection = @"%2f";

                while (RemoteFtpPath.Contains(parentDirStr))
                    RemoteFtpPath = RemoteFtpPath.Replace(parentDirStr, parentDirStrCorrection);


                Boolean UseBinary = true; // use true for .zip file or false for a text file
                Boolean UsePassive = false;

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(RemoteFtpPath);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.KeepAlive = true;
                request.UsePassive = UsePassive;
                request.UseBinary = UseBinary;

                request.Credentials = new NetworkCredential(Username, Password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                FileInfo fi = new FileInfo(LocalDestinationPath);
                if (fi.Directory.Exists == false) {
                    fi.Directory.Create();
                }

                if (fi.Exists == true)
                {
                    fi.Delete();
                }

                fi = null;;

                using (FileStream writer = new FileStream(LocalDestinationPath, FileMode.Create))
                {

                    long length = response.ContentLength;
                    int bufferSize = 2048;
                    int readCount;
                    byte[] buffer = new byte[2048];

                    readCount = responseStream.Read(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        writer.Write(buffer, 0, readCount);
                        readCount = responseStream.Read(buffer, 0, bufferSize);
                    }
                }

                reader.Close();
                response.Close();

                return true;
            }
            catch (Exception ex)
            {
                
                throw;
            }

        }
    }
}
