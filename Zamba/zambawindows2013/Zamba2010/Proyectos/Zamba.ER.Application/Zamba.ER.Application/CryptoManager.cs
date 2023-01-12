using System;
using System.Security.Cryptography;
using System.IO;

namespace CryptoFileManager
{
    public enum CryptoEventType
    {
        Message,
        FileProgress
    }

    public class CryptoEventArgs : EventArgs
    {
        CryptoEventType type;
        string message;
        int fileLength;
        int filePosition;

        public CryptoEventArgs(string message)
        {
            type = CryptoEventType.Message;
            this.message = message;
        }

        public CryptoEventArgs(string fileName, int fileLength, int filePosition)
        {
            type = CryptoEventType.FileProgress;
            this.fileLength = fileLength;
            this.filePosition = filePosition;
            message = fileName;
        }

        public CryptoEventType Type
        {
            get { return type; }
        }

        public string Message
        {
            get { return message; }
        }

        public string FileName
        {
            get { return message; }
        }

        public int FileLength
        {
            get { return fileLength; }
        }

        public int FilePosition
        {
            get { return filePosition; }
        }
    }

    public delegate void cryptoEventHandler(object sender, CryptoEventArgs e);
    
    public class CryptoManager
    {
        byte[] testHeader = null;//used to verify if decryption succeeded 
        string testHeaderString = null;

        public CryptoManager()
        {
            testHeader = System.Text.Encoding.ASCII.GetBytes("testing header");
            testHeaderString = BitConverter.ToString(testHeader);
        }

        #region RijnDael_key encrypt

        public void getKeysFromPassword(string pass, out byte[] rijnKey, out byte[] rijnIV)
        {
            byte[] salt = System.Text.Encoding.ASCII.GetBytes("System.Text.Encoding.ASCII.GetBytes");
            PasswordDeriveBytes pb = new PasswordDeriveBytes(pass, salt);
            rijnKey = pb.GetBytes(32);
            rijnIV = pb.GetBytes(16);
        }

        #endregion

        #region RijnDael encrypt
        const int bufLen = 4096;

        public void EncryptData(String inName, String outName, byte[] rijnKey, byte[] rijnIV)
        {
            FileStream fin = null;
            FileStream fout = null;
            CryptoStream encStream = null;
            try
            {
                //Create the file streams to handle the input and output files.
                fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
                fout = new FileStream(outName, FileMode.Create, FileAccess.Write);
                //Create variables to help with read and write.
                byte[] bin = new byte[bufLen]; //This is intermediate storage for the encryption.
                long rdlen = 0; //This is the total number of bytes written.
                long totlen = fin.Length; //This is the total length of the input file.
                int len; //This is the number of bytes to be written at a time.
                RijndaelManaged rijn = new RijndaelManaged();
                
                encStream = new CryptoStream(fout, rijn.CreateEncryptor(rijnKey, rijnIV), CryptoStreamMode.Write);

                //zakoduj testowy fragment
                encStream.Write(testHeader, 0, testHeader.Length);

                //Read from the input file, then encrypt and write to the output file.
                while (true)
                {
                    len = fin.Read(bin, 0, bufLen);
                    if (len == 0)
                        break;
                    encStream.Write(bin, 0, len);
                    rdlen += len;
                }
            }
            finally
            {
                if (encStream != null)
                    encStream.Close();
                if (fout != null)
                    fout.Close();
                if (fin != null)
                    fin.Close();
            }
        }

        #endregion

        #region RijnDael decrypt

        public bool DecryptData(String inName, String outName, byte[] rijnKey, byte[] rijnIV)
        {
            //Create the file streams to handle the input and output files.
            FileStream fin = null;
            FileStream fout = null;
            CryptoStream decStream = null;
            try
            {
                fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
                //Create variables to help with read and write.
                byte[] bin = new byte[bufLen]; //This is intermediate storage for the encryption.
                long rdlen = 0; //This is the total number of bytes written.
                long totlen = fin.Length; //This is the total length of the input file.
                int len; //This is the number of bytes to be written at a time.
                RijndaelManaged rijn = new RijndaelManaged();
                //DES ds = new DESCryptoServiceProvider();
                decStream = new CryptoStream(fin, rijn.CreateDecryptor(rijnKey, rijnIV), CryptoStreamMode.Read);
                //odkoduj testowy fragment
                byte[] test = new byte[testHeader.Length];
                decStream.Read(test, 0, testHeader.Length);
                if (BitConverter.ToString(test) != testHeaderString)
                {
                    decStream.Clear();
                    decStream = null;
                    return false;
                }

                //create output file
                fout = new FileStream(outName, FileMode.Create, FileAccess.Write);

                //Read from the encrypted file and write dercypted data
                while (true)
                {
                    len = decStream.Read(bin, 0, bufLen);

                    if (len == 0)
                        break;

                    fout.Write(bin, 0, len);
                    rdlen += len;                    
                }
                return true;
            }

            finally
            {
                if (decStream != null)
                    decStream.Close();

                if (fout != null)
                    fout.Close();

                if (fin != null)
                    fin.Close();
            }
        }

        #endregion
    }

}