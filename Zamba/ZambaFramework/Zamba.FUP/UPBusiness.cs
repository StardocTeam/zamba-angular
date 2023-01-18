using Ionic.Zip;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using Zamba.Core;
using Zamba.Data;
using Zamba.Servers;

namespace Zamba.FUP
{
	public class UPBusiness : IDisposable
	{
        public string LastVersion = string.Empty;
        public string CurrentVersion = string.Empty;
    public   string LastVersionPath = string.Empty;
        public string DestinationPath = string.Empty;
	public	string AppsPath = string.Empty;
		string AppFile = string.Empty;
		string AppDirectory = "compilado";
        public string ClientToExecute = string.Empty;
		string LocalClientToExecute = string.Empty;

		public bool FirstTime = false;
		public string currentClient;

		public EventHandler DownloadCompleted;
        public EventHandler DownloadTryCompleted;
        public DownloadProgressChangedEventHandler DownloadInProgress;
		public EventHandler DownloadError;

		public bool CheckNews()
		{
			Zamba.Core.Cache.CacheBusiness.ClearAllCache();

			LastVersion = UserPreferences.getValue("LastVersion", Sections.UserPreferences, "3.0.0.0");
			LastVersionPath = UserPreferences.getValue("LastVersionPath", Sections.UserPreferences, "http://www.zambabpm.com.ar/zamba/release zamba cliente 2.9.4.0.zip");

			DestinationPath = UserPreferences.getValue("DestinationPath", Sections.UserPreferences, "ApplicationData\\Zamba Software\\app");
			DestinationPath = DestinationPath.Replace("ApplicationData", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
			AppsPath = DestinationPath;
			DestinationPath = Path.Combine(DestinationPath, LastVersion);


			CurrentVersion = UpdaterFactory.GetVersionByEstreg();

			if (CurrentVersion.Length == 0 || int.Parse(CurrentVersion.Replace(".", "")) < int.Parse(LastVersion.Replace(".", "")) || Directory.Exists(DestinationPath) == false)
			{
				TryGetFromServer(LastVersionPath, DestinationPath, LastVersion);
				return true;
			}
			else if (int.Parse(CurrentVersion.Replace(".", "")) > int.Parse(LastVersion.Replace(".", "")))
			{
				UpdateEstreg(LastVersion, Environment.MachineName);
			}
			return false;
		}

		public bool GetClientToExecute(string currentPath, string BtnAppToExecute, string Args)
		{
			try
			{

				LastVersion = UserPreferences.getValue("LastVersion", Sections.UserPreferences, "3.0.0.0");
				LastVersionPath = UserPreferences.getValue("LastVersionPath", Sections.UserPreferences, "http://www.zambabpm.com.ar/zamba/release zamba cliente 2.9.4.0.zip");

				DestinationPath = UserPreferences.getValue("DestinationPath", Sections.UserPreferences, "ApplicationData\\Zamba Software\\app");
				DestinationPath = DestinationPath.Replace("ApplicationData", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				AppsPath = DestinationPath;

				if (BtnAppToExecute.Length != 0)

					AppFile = BtnAppToExecute;
				else
					AppFile = "Zamba.Cliente.exe";

				ClientToExecute = Path.Combine(DestinationPath, LastVersion);
				ClientToExecute = Path.Combine(ClientToExecute, AppDirectory);
				ClientToExecute = Path.Combine(ClientToExecute, AppFile);
				DestinationPath = Path.Combine(DestinationPath, LastVersion);
				LocalClientToExecute = Path.Combine(currentPath, AppFile);

				System.IO.FileInfo fi = new System.IO.FileInfo(ClientToExecute);
				if (ClientToExecute.Length > 0 && fi.Exists && fi.FullName != Environment.GetCommandLineArgs()[0] && ZipDoNotExits(LastVersion, DestinationPath, LastVersionPath))
				{
					ExecuteNewClient(Args);
					return false;
				}
				else
				{

					string AppToExecute = TryGetAnotherClient(AppsPath, AppFile, LastVersion);
					AppToExecute = Path.Combine(AppToExecute, AppDirectory);
					AppToExecute = Path.Combine(AppToExecute, AppFile);
					System.IO.FileInfo Afi = new System.IO.FileInfo(AppToExecute);

					if (LastVersionPath.Length > 0 && DestinationPath.Length > 0 && LastVersion.Length > 0)//(!string.IsNullOrEmpty(Args))
					{
						TryGetFromServer(LastVersionPath, DestinationPath, LastVersion);
					}

					if (AppToExecute != string.Empty && Afi.Exists)
					{
						string commandline = string.Empty;
						if (!string.IsNullOrEmpty(Args)) commandline = Args;
						currentClient = Afi.FullName;

						commandline += " USERID=" + Membership.MembershipHelper.CurrentUser.ID;

						ProcessStartInfo Pinfo = new ProcessStartInfo(Afi.FullName, commandline);
						Pinfo.UseShellExecute = true;
						Pinfo.WindowStyle = ProcessWindowStyle.Maximized;
						System.Diagnostics.Process.Start(Pinfo);
						return true;
					}
					else
					{
						System.IO.FileInfo Lfi = new System.IO.FileInfo(LocalClientToExecute);
						if (Lfi.Exists)
						{
							string commandline = string.Empty;
							if (!string.IsNullOrEmpty(Args)) commandline = Args;
							currentClient = LocalClientToExecute;
							commandline += " USERID=" + Membership.MembershipHelper.CurrentUser.ID;
							ProcessStartInfo Pinfo = new ProcessStartInfo(LocalClientToExecute, commandline);
							Pinfo.UseShellExecute = true;
							Pinfo.WindowStyle = ProcessWindowStyle.Maximized;
							System.Diagnostics.Process.Start(Pinfo);
							return true;
						}
						else
						{
							FirstTime = true;
							return true;
						}
					}
				}
			}
			catch (Exception ex)
			{
                ZClass.raiseerror(ex);
				return false;
			}
		}

		private bool ZipDoNotExits(string lastVersion, string destinationPath, string LastVersionPath)
		{
			string file = string.Empty;

			if (LastVersionPath.IndexOf("/") != -1)

				file = LastVersionPath.Substring(LastVersionPath.LastIndexOf("/") + 1);
			else
				file = LastVersionPath.Substring(LastVersionPath.LastIndexOf("\\") + 1);

			DirectoryInfo zipdirectory = new DirectoryInfo(destinationPath).Parent;
			string zipfile = Path.Combine(zipdirectory.FullName, file);

			if (File.Exists(zipfile)) return false; else return true;
		}

		public void ExecuteCurrentClient(string currentPath, string Args)
		{
			try
			{
				string AppToExecute = string.Empty;

				if (Args.ToLower().Contains("zamba:"))
					AppToExecute = "Zamba.Cliente.exe";
				else if (Args.ToLower().Contains("builderid:"))
					AppToExecute = "Zamba.ReportBuilder.exe";
				else AppToExecute = "Zamba.Cliente.exe";

				this.GetClientToExecute(currentPath, AppToExecute, Args);
			}
			catch (Exception ex)
			{
                ZClass.raiseerror(ex);
            }
        }

		private string TryGetAnotherClient(string appsPath, string appFile, string failedVersion)
		{
			DirectoryInfo dir = new DirectoryInfo(appsPath);
			if (dir.Exists == false) dir.Create();

			int failedVersionNum = 0;
			int.TryParse(failedVersion.Replace(".", ""), out failedVersionNum);
			int targetversion = 0;
			int currentversion = 0;
			string targetAppDirectory = string.Empty;
			foreach (DirectoryInfo f in dir.GetDirectories())
			{
				if (int.TryParse(f.Name.Replace(".", ""), out currentversion))
				{
					if (targetversion < currentversion && targetversion != failedVersionNum)
					{
						targetversion = currentversion;
						targetAppDirectory = f.FullName;
					}
				}
			}
			return targetAppDirectory;
		}

        public void DeleteOldVersions(string appsPath, string ActualVersion, string MinimumVersion )
        {
            DirectoryInfo dir = new DirectoryInfo(appsPath);
            if (dir.Exists == false) dir.Create();

            int ActualVersionNum = 0;
            int.TryParse(ActualVersion.Replace(".", ""), out ActualVersionNum);
            int MinimumVersionNum = 0;
            int.TryParse(MinimumVersion.Replace(".", ""), out MinimumVersionNum);
            int currentversion = 0;

            foreach (DirectoryInfo f in dir.GetDirectories())
            {
                if (int.TryParse(f.Name.Replace(".", ""), out currentversion))
                {
                    if (currentversion != ActualVersionNum && currentversion < MinimumVersionNum)
                    {
                        Directory.Delete(f.FullName, true);
                    }
                }
            }
        }

        public void ExecuteNewClient(string Args)
		{
			System.IO.FileInfo fi = new System.IO.FileInfo(ClientToExecute);

			if (fi.Exists)
			{

				string commandline = string.Empty;
				if (!string.IsNullOrEmpty(Args)) commandline = Args;
                
				commandline += " USERID=" + Membership.MembershipHelper.CurrentUser.ID;

				currentClient = ClientToExecute;
				ProcessStartInfo Pinfo = new ProcessStartInfo(ClientToExecute, commandline);
				Pinfo.UseShellExecute = true;
				Pinfo.WindowStyle = ProcessWindowStyle.Maximized;
				System.Diagnostics.Process.Start(Pinfo);
			}
			else
			{
				DownloadError(this, new EventArgs());
			}
		}

		private void UpdateEstreg(string strVersion, string strMachineName)
		{
			string dateFunction = null;
			if (Server.isOracle)
			{
				dateFunction = "sysdate";
			}
			else
			{
				dateFunction = "getDate()";
			}
			ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("Update de Estreg a {0}", strVersion));

			Server.get_Con().ExecuteNonQuery(CommandType.Text, "Update Estreg set VER='" + strVersion + "',updated=" + dateFunction + " where M_Name='" + strMachineName + "'");
		}


		private Boolean TryGetFromWeb(string clientToExecute)
		{
			try
			{
				return false;
			}
			catch (Exception ex)
			{
                ZClass.raiseerror(ex);
				return false;
			}
		}

		BackgroundWorker wp = new BackgroundWorker();
		private Boolean TryGetFromServer(string lastVersionPath, string destinationPath, string lastVersion)
		{
			try
			{
				LastVersionPath = lastVersionPath;
				LastVersion = lastVersion;
				DestinationPath = destinationPath;

				if (wp == null)
					wp = new BackgroundWorker();

                wp.DoWork -= Wp_DoWork;
                wp.RunWorkerCompleted -= Wp_RunWorkerCompleted;
                wp.ProgressChanged -= Wp_ProgressChanged;
                wp.DoWork += Wp_DoWork;
				wp.RunWorkerCompleted += Wp_RunWorkerCompleted;
				wp.ProgressChanged += Wp_ProgressChanged;
				wp.RunWorkerAsync();

				return false;

			}
			catch (Exception ex)
			{
                ZClass.raiseerror(ex);
				return false;
			}
		}
		private void Wp_DoWork(object sender, DoWorkEventArgs e)
		{
            Downloading = true;
            e.Result = downloadPackage(string.Empty, string.Empty, LastVersionPath, DestinationPath);
          
        }

        private void Wp_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			//          DownloadInProgress(this, new DownloadProgressChangedEventArgs());
		}

		private void Wp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
                Downloading = false;
                if (((bool)e.Result) == true)
				{
					ZTrace.WriteLineIf(ZTrace.IsVerbose, "Wp_RunWorkerCompleted");
					UpdateEstreg(LastVersion, Environment.MachineName);
					if (FirstTime) ExecuteNewClient(string.Empty);
					DownloadCompleted(this, new EventArgs());
                    DeleteOldVersions(AppsPath, LastVersion, CurrentVersion);
                }
				else
				{
                    if (this != null && DownloadCompleted != null) DownloadTryCompleted(this, new EventArgs());
				}
                
            }
			catch (Exception ex)
			{
                ZClass.raiseerror(ex);
			}
		}

		public Boolean downloadPackage(string user, string password, string uri, string path)
		{
			try
			{
				ZTrace.WriteLineIf(ZTrace.IsVerbose, "Uri: " + uri);
				if (uri != string.Empty)
				{
					string file = string.Empty;

					if (uri.IndexOf("/") != -1)

						file = uri.Substring(uri.LastIndexOf("/") + 1);
					else
						file = uri.Substring(uri.LastIndexOf("\\") + 1);


					DirectoryInfo zipdirectory = new DirectoryInfo(path).Parent;
					string zipfile = Path.Combine(zipdirectory.FullName, file);

					if (File.Exists(zipfile)) File.Delete(zipfile);
					if (Directory.Exists(path)) Directory.Delete(path, true);


					if (uri != string.Empty)
					{
						if (uri.ToLower().StartsWith("http:"))
						{
							try
							{
								//Compruebo que URI = archivo disponible
								WebResponse response = null;
								var request = WebRequest.Create(uri);
								request.Method = "HEAD";
								request.Timeout = 1200000;
								//request.UseDefaultCredentials = true;
								request.Proxy = WebRequest.DefaultWebProxy;
								request.Credentials = System.Net.CredentialCache.DefaultCredentials;
								request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
								response = (WebResponse)request.GetResponse();

								if (response.ContentLength >= 1 && (response.ContentType == "application/octet-stream" || response.ContentType == "application/x-zip-compressed"))
								{
									//Descarga Zip en Path directorio

									using (var client = new WebClient())
									{
										client.DownloadProgressChanged += Client_DownloadProgressChanged;
										client.DownloadFile(new Uri(uri), zipfile);

									}
								}
							}
							catch (Exception ex)
							{
								if (ex.Message.IndexOf("404") > 0)
								{
									Zamba.Core.ZTrace.WriteLineIf(Zamba.Core.ZTrace.IsError, string.Format("ERROR: La URL de Actualizacion no esta disponible: {0}", uri));
								}
								else
								{
                                    ZClass.raiseerror(ex);
								}
								return false;
							}

						}
						else
						{
							File.Copy(uri, zipfile);
						}
					}
					if (!CreateDirectory(path))
						return false;
					//Descomprime ZIP y lo elimina
					this.Descompress(path, zipfile);
					return true;

				}
				else
					return false;
			}
			catch (Exception ex)
			{
                ZClass.raiseerror(ex);
				return false;
			}
		}

		private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			DownloadInProgress(this, e);
		}

		public Boolean Descompress(string path, string file)
		{
			try
			{
				if (File.Exists(file))
				{
					using (ZipFile zip = ZipFile.Read(file))
					{
						zip.ExtractAll(path, ExtractExistingFileAction.OverwriteSilently);
					}
				}

				File.Delete(file);
				return true;
			}
			catch (Exception )
			{
				return false;
			}
		}

		public Boolean CreateDirectory(string file)
		{
			try
			{
				if (!Directory.Exists(file))
					Directory.CreateDirectory(file);
				return true;
			}
			catch (Exception ex)
			{
				ZClass.raiseerror(ex);
				return false;
			}
		}

		#region IDisposable Support
		private bool disposedValue = false; // Para detectar llamadas redundantes

        public bool Downloading { get; private set; }

        public virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: elimine el estado administrado (objetos administrados).
					wp.Dispose();
				}

				// TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
				// TODO: configure los campos grandes en nulos.
				
				disposedValue = true;
			}
		}

		// TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
		// ~UPBusiness() {
		//   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
		//   Dispose(false);
		// }

		// Este código se agrega para implementar correctamente el patrón descartable.
		void IDisposable.Dispose()
		{
			// No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
			Dispose(true);
			// TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
			// GC.SuppressFinalize(this);
		}
		#endregion

	}
}
