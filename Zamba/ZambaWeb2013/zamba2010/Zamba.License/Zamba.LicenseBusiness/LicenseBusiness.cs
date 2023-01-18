using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zamba.LicenseCore;
using Zamba.Data;
using Zamba.Tools;
using System.Data;
using Zamba.Core;

namespace Zamba.LicenseBusiness
{
    public class LicenseBusiness
    {
        /// <summary>
        /// Obtiene un listado detallado de las licencias del cliente
        /// </summary>
        /// <returns>List<ILicense></returns>
        public List<ILicense> GetLicenses()
        {
            //Lista que contendrá todas la información de licencias del cliente
            List<ILicense> licenses = new List<ILicense>();
            ClsLic licBus = new ClsLic();
            AgentFactoryExt afe = new AgentFactoryExt();

            //Se agregan las licencias documentales
            License licDoc = new License();
            licDoc.Name="Documentales";
            licDoc.Configured = Int32.Parse(licBus.GetLicenseCount(LicenseType.Documental));
            licDoc.Used = afe.ActiveDocConections();
            licenses.Add(licDoc);
            
            //Se agregan las licencias de workflow
            License licWf = new License();
            licWf.Name = "Workflow";
            licWf.Configured = Int32.Parse(licBus.GetLicenseCount(LicenseType.Workflow));
            licWf.Used = afe.ActiveWfConnections();
            licenses.Add(licWf);

            licBus = null;
            afe = null;
            return licenses;
        }

        /// <summary>
        /// Obtiene una lista de conexiones a Zamba
        /// </summary>
        /// <returns>List<IConnection></returns>
        public List<IConnection> GetConnections()
        {
            //Lista que contendrá todas la información de conexiones al cliente
            List<IConnection> connections = new List<IConnection>();

            //Se obtiene el reporte de conexiones
            AgentFactoryExt afe = new AgentFactoryExt();
            DataTable dt = afe.GetConnectionsReport();

            //Se crean los objetos de conexiones
            foreach(DataRow row in dt.Rows)
            {
                Connection con = new Connection();

                con.ZambaUser = row["NAME"].ToString();
                con.WindowsUser = row["WINUSER"].ToString();
                con.Host = row["WINPC"].ToString();
                switch(Int32.Parse(row["TYPE"].ToString()))
                {
                    case 0:
                        con.License = "Documental";
                        break;
                    case 1:
                        con.License = "Workflow";
                        break;
                    default:
                        con.License = "Otros";
                        break;
                }
                con.TimeOut = Int32.Parse(row["TIME_OUT"].ToString());
                con.Created = DateTime.Parse(row["C_TIME"].ToString());
                con.Updated = DateTime.Parse(row["U_TIME"].ToString());

                connections.Add(con);
            }

            return connections;
        }
    }
}
