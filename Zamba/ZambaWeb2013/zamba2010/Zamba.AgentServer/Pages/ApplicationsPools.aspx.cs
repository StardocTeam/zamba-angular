using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Microsoft.Web.Management.Server;
using Microsoft.Web.Administration;
using System.Threading;

namespace Zamba.AgentServer.Pages
{
    public partial class ApplicationsPools : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetApplicationPoolCollection();
        }


        protected void BtnRecycleAppPool_Click(object sender, EventArgs e)
        {
            try
            {
                GetApplicationPoolCollection();
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }



        [ModuleServiceMethod(PassThrough = true)]
        public void GetApplicationPoolCollection()
        {
           try
                { 
            
            ArrayList arrayOfApplicationBags = new ArrayList();
            List<String> ApplicationsPools = new List<string>();

            ServerManager serverManager = new ServerManager();
            ApplicationPoolCollection applicationPoolCollection = serverManager.ApplicationPools;
            foreach (ApplicationPool applicationPool in applicationPoolCollection)
            {
               
                String Item = applicationPool.Name;
                try
                {
Item += " - " + applicationPool.State;
                }
                catch (Exception)
                {
                }
                       ApplicationsPools.Add(Item);
                                         
        }
            this.RadGrid1.DataSource = null;

               this.RadGrid1.DataSource =ApplicationsPools;
            this.RadGrid1.DataBind();
        
        
              
           }
           catch (Exception ex)
           {
               this.Labelinfo.Text = ex.ToString();
           }
        }


        protected void BtnRecycleAppPoolIIS6_Click(object sender, EventArgs e)
        {
            try
            {
                RecyclePool();
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }

        }


        public static void RecyclePool()
        {
            using (var manager = new ServerManager())
            {
                var pool = manager.ApplicationPools["RequestReduce"];
                System.Diagnostics.Process process = null;
                if (pool.WorkerProcesses.Count > 0)
                    process = System.Diagnostics.Process.GetProcessById(pool.WorkerProcesses[0].ProcessId);
                pool.Recycle();
                if (process != null)
                {
                    while (!process.HasExited)
                        Thread.Sleep(0);
                    process.Dispose();
                }
            }
        }

        protected void RadGrid1_EditCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        [ModuleServiceMethod(PassThrough = true)]
        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try { 
            if (e.CommandName == "Parar" || e.CommandName == "Iniciar" || e.CommandName == "Reciclar")
            {
                String currentAppPool = ((System.Web.UI.WebControls.TableRow)(e.Item)).Cells[5].Text;



                ServerManager serverManager = new ServerManager();
                ApplicationPoolCollection applicationPoolCollection = serverManager.ApplicationPools;

                foreach (ApplicationPool applicationPool in applicationPoolCollection)
                {
                    if (currentAppPool.Contains(applicationPool.Name))
                    {
                        if (e.CommandName == "Parar")
                        {
                            this.Labelinfo.Text = "Deteniendo Pool"; 
                            applicationPool.Stop();
                            this.Labelinfo.Text = "Pool Detenido";
                        }
                        if (e.CommandName == "Iniciar" )
                        {
                            this.Labelinfo.Text = "Iniciando Pool"; 
                            applicationPool.Start();
                            this.Labelinfo.Text = "Pool Iniciado";
                        }
                        if (e.CommandName == "Reciclar")
                        {
                            this.Labelinfo.Text = "Reciclando Pool"; 
                            applicationPool.Stop();
                            Thread.Sleep(10000);
                            applicationPool.Start();
                            this.Labelinfo.Text = "Pool Reciclado";
                        }
                    }
                }

                serverManager.CommitChanges();
                GetApplicationPoolCollection();
            }
            }
            catch (Exception ex)
            {
                this.Labelinfo.Text = ex.ToString();
            }
        }



        //protected void status()
        //{
        //    string appPoolName = "dev.somesite.com";
        //    string appPoolPath = @"IIS://" + System.Environment.MachineName + "/W3SVC/AppPools/" + appPoolName;
        //    int intStatus = 0;
        //    try
        //    {
        //        DirectoryEntry w3svc = new DirectoryEntry(appPoolPath);
        //        intStatus = (int)w3svc.InvokeGet("AppPoolState");
        //        switch (intStatus)
        //        {
        //            case 2:
        //                lblStatus.Text = "Running";
        //                break;
        //            case 4:
        //                lblStatus.Text = "Stopped";
        //                break;
        //            default:
        //                lblStatus.Text = "Unknown";
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.ToString());
        //    }
        //}
        //protected void stopAppPool(object sender, EventArgs e)
        //{
        //    Button btn = (Button)sender;
        //    string appPoolName = btn.CommandArgument;
        //    string appPoolPath = @"IIS://" + System.Environment.MachineName + "/W3SVC/AppPools/" + appPoolName;
        //    try
        //    {
        //        DirectoryEntry w3svc = new DirectoryEntry(appPoolPath);
        //        w3svc.Invoke("Stop", null);
        //        status();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.ToString());
        //    }
        //}

        //protected void startAppPool(object sender, EventArgs e)
        //{
        //    Button btn = (Button)sender;
        //    string appPoolName = btn.CommandArgument;
        //    string appPoolPath = @"IIS://" + System.Environment.MachineName + "/W3SVC/AppPools/" + appPoolName;
        //    try
        //    {
        //        DirectoryEntry w3svc = new DirectoryEntry(appPoolPath);
        //        w3svc.Invoke("Start", null);
        //        status();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.ToString());
        //    }
        //}



    }
}