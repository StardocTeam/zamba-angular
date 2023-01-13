using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zamba.WorkFlow.Business;
using System.Diagnostics;
using System.Deployment.Application;
using Zamba.Core;
using System.Threading;

namespace Zamba.AgentClient
{
	public partial class Configuration : Form
	{
		private NotifyIcon trayIcon;
		private ContextMenu trayMenu;
        private AgentBusiness.AgentBusiness AB = new AgentBusiness.AgentBusiness();
		public Configuration()
		{
			InitializeComponent();
			trayMenu = new ContextMenu();
			trayMenu.MenuItems.Add("Salir", OnExit);
			trayMenu.MenuItems.Add("Configurar", OnConfig);
			trayMenu.MenuItems.Add("Verificar Nueva Version", CheckForUpdates);
			trayMenu.MenuItems.Add("Ejecutar", StartBackGroudProcess);
			trayIcon = new NotifyIcon();

			if (ApplicationDeployment.IsNetworkDeployed)
			{
				trayIcon.Text = "Zamba Agent " + ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
			}
			else
			{
				trayIcon.Text = "Zamba Agent";
			}
			trayIcon.Icon = global::Zamba.AgentClient.Properties.Resources.monitoreo;// new Icon(SystemIcons.Application, 40, 40);
			trayIcon.ContextMenu = trayMenu;
			trayIcon.Visible = true;

			LoadTrace();
		}

		private static void LoadTrace()
		{
			try
			{
				ZTrace.SetLevel(4, "Zamba.AgentClient");
			}
			catch (Exception ex)
			{
				ZClass.raiseerror(ex);
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//Get Client Name

			Visible = false;
			ShowInTaskbar = false;
			Url = this.textBox1.Text;
			CheckStardocSQLServer();

//			if (CheckClientTitle()) StartBackGroudProcess(this, new EventArgs());

		}

		private Boolean CheckClientTitle()
		{
			try
			{
				String currentClientName = Zamba.Core.ZOptBusiness.GetValue("ClientName");
				if (currentClientName != null && currentClientName.Length > 0)
				{
					Client = currentClientName;
					return true;

				}
				else
				{
					trayIcon.BalloonTipTitle = "Zamba Error";
					trayIcon.BalloonTipText = "Falta la configuracion del Nombre de Cliente";
					trayIcon.ShowBalloonTip(5000);
					return false;
				}
			}
			catch (Exception ex)
			{
				ZClass.raiseerror(ex);

				trayIcon.BalloonTipTitle = "Zamba Error";
				trayIcon.BalloonTipText = "Falta la configuracion del Nombre de Cliente";
				trayIcon.ShowBalloonTip(5000);

				trayIcon.BalloonTipTitle = ex.ToString();
				trayIcon.BalloonTipText = trayIcon.BalloonTipTitle;
				trayIcon.ShowBalloonTip(5000);

				return false;
			}
		}
		private void StartBackGroudProcess(object sender, EventArgs e)
		{
			StartUCMService();

		}

		System.Threading.Thread UCMBP = null;
		private void StartUCMService()
		{
			try
			{

				try
				{

					if (UCMBP != null)
					{
						UCMBP.Abort();
					}
				}
				catch (ThreadAbortException ex)
				{
				}
				UCMBP = new System.Threading.Thread(new ThreadStart(UCMBP_DoWork));
				UCMBP.Start();
			}
			catch (Exception ex)
			{
				ZClass.raiseerror(ex);

				trayIcon.BalloonTipTitle = ex.ToString();
				trayIcon.BalloonTipText = trayIcon.BalloonTipTitle;
				trayIcon.ShowBalloonTip(5000);
			}
		}

		public void UCMBP_DoWork()
		{
			try
			{
				while (Close == false)
				{

					CheckForUpdates(this, new EventArgs());

					if (Close == false)
					{
                        String Result;
Boolean returnvalue = 						AB.RegisterUCMActivity(Client,ref Result);

						trayIcon.BalloonTipTitle = "Espera por " + _period / 1000 + " segundos";
						trayIcon.BalloonTipText = trayIcon.BalloonTipTitle;
						trayIcon.ShowBalloonTip(5000);

						System.Threading.Thread.Sleep(_period);

						if (DateTime.Now.Hour > 22 || DateTime.Now.Hour < 7)
						{
							trayIcon.BalloonTipTitle = "Periodo de Inactividad";
                            trayIcon.BalloonTipText = "Pasando a Inactividad de 23hs a 6hs";
							trayIcon.ShowBalloonTip(5000);

							System.Threading.Thread.Sleep(360000);
						}
					}
				}
			}
			catch (System.Threading.ThreadAbortException ex)
			{
			}
			catch (Exception ex)
			{
				ZClass.raiseerror(ex);

				trayIcon.BalloonTipTitle = ex.ToString();
				trayIcon.BalloonTipText = trayIcon.BalloonTipTitle;
				trayIcon.ShowBalloonTip(5000);
			}

		}

	

		private Boolean Close;

		protected override void OnClosing(CancelEventArgs e)
		{
			if (Close)
			{
				base.OnClosing(e);
			}
			else
			{
				e.Cancel = true;
				this.WindowState = FormWindowState.Minimized;
				Visible = false;
				ShowInTaskbar = false;
				trayIcon.BalloonTipTitle = "Zamba.AgentClient continuara ejecutandose";
				trayIcon.BalloonTipText = "Zamba.AgentClient continuara ejecutandose";
				trayIcon.ShowBalloonTip(5000);
			}
		}

		private void OnExit(object sender, EventArgs e)
		{
			Close = true;
			Application.Exit();
		}
		private void OnConfig(object sender, EventArgs e)
		{
			Visible = true;
			ShowInTaskbar = true;
			this.WindowState = FormWindowState.Normal;
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				trayIcon.Dispose();
				components.Dispose();
			}
			base.Dispose(disposing);
		}


		public string Url { get; set; }

		
		private Int32 _period = 3600000;
		private string _Client;

		public string Client
		{
			get { return _Client; }
			set
			{
				_Client = value;
				this.textBox2.Text = Client;
			}
		}
		

		public Int32 Period
		{
			get { return _period; }
			set { _period = value; }
		}


		
		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			this.Period = (Int32)this.numericUpDown1.Value;
			StartUCMService();
		}

		private void BtnTest_Click(object sender, EventArgs e)
		{
			this.Url = this.textBox1.Text;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.textBox2.Text.Trim().Length > 0)
				{
					//Save Client name
					String currentClientName = Zamba.Core.ZOptBusiness.GetValue("ClientName");

					if (currentClientName == null)
					{
						Zamba.Core.ZOptBusiness.Insert("ClientName", this.textBox2.Text.Trim());
						Client = this.textBox2.Text.Trim();
					}
					else
					{
						Zamba.Core.ZOptBusiness.Update("ClientName", this.textBox2.Text.Trim());
						Client = this.textBox2.Text.Trim();
					}
					StartUCMService();

				}
			}
			catch (Exception ex)
			{
				ZClass.raiseerror(ex);

				trayIcon.BalloonTipTitle = ex.ToString();
				trayIcon.BalloonTipText = trayIcon.BalloonTipTitle;
				trayIcon.ShowBalloonTip(5000);
			}
		}

		private void ucSelectConnection1_ConnectionChanged(string File)
		{
			try
			{
				if (CheckClientTitle()) StartBackGroudProcess(this, new EventArgs());
			}
			catch (Exception ex)
			{
				ZClass.raiseerror(ex);

				trayIcon.BalloonTipTitle = ex.ToString();
				trayIcon.BalloonTipText = trayIcon.BalloonTipTitle;
				trayIcon.ShowBalloonTip(5000);
			}
		}

		void CheckForUpdates(object sender, EventArgs e)
		{
			try
			{
				trayIcon.BalloonTipTitle = "Verificando Nueva Version";
				trayIcon.BalloonTipText = trayIcon.BalloonTipTitle;
				trayIcon.ShowBalloonTip(5000);

				UpdateCheckInfo info = null;
				if (ApplicationDeployment.IsNetworkDeployed)
				{
					ApplicationDeployment AD = ApplicationDeployment.CurrentDeployment;
					try
					{
						info = AD.CheckForDetailedUpdate();
					}
					catch (DeploymentDownloadException)
					{
						return;
					}
					catch (InvalidOperationException)
					{
						return;

					}
					if (info.UpdateAvailable)
					{
						try
						{
							trayIcon.BalloonTipTitle = "Existe una nueva Version de la aplicacion, se procedera a actualizar";
							trayIcon.BalloonTipText = trayIcon.BalloonTipTitle;
							trayIcon.ShowBalloonTip(5000);

							this.Close = true;
							AD.Update();

							Application.Restart();
						}
						catch (DeploymentDownloadException ex)
						{
							ZClass.raiseerror(ex);

							trayIcon.BalloonTipTitle = ex.ToString();
							trayIcon.BalloonTipText = trayIcon.BalloonTipTitle;
							trayIcon.ShowBalloonTip(5000);
						}
					}
					else
					{
						trayIcon.BalloonTipTitle = "No hay nuevas versiones";
						trayIcon.BalloonTipText = trayIcon.BalloonTipTitle;
						trayIcon.ShowBalloonTip(5000);

					}
				}
			}
			catch (Exception ex)
			{
				ZClass.raiseerror(ex);

				trayIcon.BalloonTipTitle = ex.ToString();
				trayIcon.BalloonTipText = trayIcon.BalloonTipTitle;
				trayIcon.ShowBalloonTip(5000);
			}

		}
		private String StardocSQLServer = "www.stardoc.com.ar,1437";

		private Boolean CheckStardocSQLServer()
		{
			try
			{
				String currentStardocSQLServer = Zamba.Core.ZOptBusiness.GetValue("StardocSQLServer");
				if (currentStardocSQLServer != null && currentStardocSQLServer.Length > 0)
				{
					StardocSQLServer = currentStardocSQLServer;
					return true;

				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				ZClass.raiseerror(ex);

				trayIcon.BalloonTipTitle = "Zamba Error";
				trayIcon.BalloonTipText = "Error en la configuracion del StardocSQLServer";
				trayIcon.ShowBalloonTip(5000);

				trayIcon.BalloonTipTitle = ex.ToString();
				trayIcon.BalloonTipText = trayIcon.BalloonTipTitle;
				trayIcon.ShowBalloonTip(5000);

				return false;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.txtstardocsqlserver.Text.Trim().Length > 0)
				{

					String currentStardocSQLServer = Zamba.Core.ZOptBusiness.GetValue("StardocSQLServer");

					if (currentStardocSQLServer == null)
					{
						Zamba.Core.ZOptBusiness.Insert("StardocSQLServer", this.txtstardocsqlserver.Text.Trim());
						StardocSQLServer = this.txtstardocsqlserver.Text.Trim();
					}
					else
					{
						Zamba.Core.ZOptBusiness.Update("StardocSQLServer", this.txtstardocsqlserver.Text.Trim());
						StardocSQLServer = this.txtstardocsqlserver.Text.Trim();
					}
					StartUCMService();

				}
			}
			catch (Exception ex)
			{
				ZClass.raiseerror(ex);

				trayIcon.BalloonTipTitle = ex.ToString();
				trayIcon.BalloonTipText = trayIcon.BalloonTipTitle;
				trayIcon.ShowBalloonTip(5000);
			}

			StartUCMService();

		}
	}
}
