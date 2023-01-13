using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.IO;

namespace FileUploadWS
{
	[WebService(Namespace="Http://tahazayed.tk/webservices/")]

	public class FileUploadWS : System.Web.Services.WebService
	{
		public FileUploadWS()
		{
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		[WebMethod]
		public void FileUpload(byte[]data,string strFileName)
		{
			string str=data.Length.ToString();
			string filePath=Server.MapPath(@"..\testscan\upload\"+strFileName);
			FileStream victimFilew=new FileStream(filePath,FileMode.Create);
			BinaryWriter victimFileBW=new BinaryWriter(victimFilew);
			victimFileBW.Write(data);
			victimFileBW.Write(data);
			victimFilew.Close();

			#region Zip filter
//			sbyte[] buf = new sbyte[1024];
//			int len;
//			java.io.FileInputStream fis = new java.io.FileInputStream(filePath);
//			java.util.zip.ZipInputStream zis = new java.util.zip.ZipInputStream(fis);
//			java.util.zip.ZipEntry ze=zis.getNextEntry();
//			java.io.FileOutputStream fos = new java.io.FileOutputStream(Server.MapPath(@"..\testscan\upload\j2.jpg"));
//			while ((len = zis.read(buf)) >= 0)
//			{
//				fos.write(buf, 0, len);
//			}
//			fos.close();
//			zis.close();
//			fis.close();
//			File.Delete(filePath);
			#endregion

		}
	}
}
