using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CryptoFileManager;
using System.IO;
using Zamba.Servers;
using Zamba.Tools;

namespace Zamba.ER.Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 };
        byte[] iv = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6 };

        private void btnStarEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserPass.Text))
            {
                MessageBox.Show(this, "Debe seleccionar una contraseña para la encriptacion", "Zamba Software");
                return;
            }
            
            OpenFileDialog openF = new OpenFileDialog();
            if(openF.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty( openF.FileName))
            {
                string fileValues;
                using(StreamReader sr = new StreamReader(openF.FileName))
	            {
                    fileValues = sr.ReadToEnd();
	            }

                if (!string.IsNullOrEmpty(fileValues))
                {
                    string[] strings = fileValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] stringRecord;
                    byte[] cryptoKey = null;
                    byte[] cryptIV = null;
                    string pass;
                    string fileLocation;
                    BinaryWriter sw;
                    byte[] data;
                    string encryPass;

                    CryptoManager crpt = new CryptoManager();
                    foreach (string record in strings)
                    {
                        stringRecord = record.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        if (stringRecord != null && stringRecord.Length == 3)
                        {
                            pass = stringRecord[0] + txtUserPass.Text + stringRecord[2];
                            crpt.getKeysFromPassword(pass, out cryptoKey, out cryptIV);
                            fileLocation = stringRecord[1];
                            //crpt.EncryptData(fileLocation, fileLocation + ".enc", cryptoKey, cryptIV);
                            //crpt.DecryptData(fileLocation + ".enc", fileLocation + ".dec", cryptoKey, cryptIV);
                            crpt.EncryptData(fileLocation, fileLocation + ".enc", cryptoKey, cryptIV);
                            File.Delete(fileLocation);

                            data = File.ReadAllBytes(fileLocation + ".enc");
                            sw = new BinaryWriter(File.Create(fileLocation));
                            sw.Write(data);
                            sw.Flush();
                            sw.Close();
                            //Comprobacion de documentos
                            //crpt.DecryptData(fileLocation, fileLocation + ".dec", cryptoKey, cryptIV);
                            
                            encryPass = Zamba.Tools.Encryption.EncryptString(pass, key, iv);
                            Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "insert into ZSer (DocId,DocTypeId,Pss) values (" + stringRecord[0] + "," + stringRecord[2] + ",'" + encryPass+"')");

                            File.Delete(fileLocation + ".enc");
                        }
                    }
                }
                MessageBox.Show("Encriptacion correcta");
            }
        }
    }
}
