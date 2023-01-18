using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

using Zamba.WebAdmin.Models;

namespace Zamba.WebAdmin.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AbmController : ApiController
    {
        public AbmController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/Abm/InsertZmachine")]
        [HttpPost]
        public void InsertZmachine(Zmachine Zmachine)
        {
           if(Zmachine.DefaultValue == null)
            {
                Zmachine.DefaultValue = "default";
            }

            StringBuilder querystring = new StringBuilder();
            querystring.Append("Insert into ZMachineConfig Values(" + "'" + Zmachine.DefaultValue + "'" + ',' + "'" + Zmachine.Name);
            querystring.Append("'" + ',' + "'" + Zmachine.Value + "'" + ',' + "'" + Zmachine.section + "'" + ")");

            string query = string.Format(querystring.ToString());
            Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, query);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/Abm/EditZmachine")]
        [HttpPost]
        public string EditZmachine(Zmachine Zmachine)
        {
            StringBuilder querystring = new StringBuilder();
            querystring.Append("Update ZMachineConfig Set ZMachineConfig.Value = ' " + Zmachine.Value + "' Where ZMachineConfig.name = '" + Zmachine.Name + "' and ZmachineConfig.MachineName = '"+ Zmachine.DefaultValue  + "' ");
            string query = string.Format(querystring.ToString());
            var set = Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, query);
            return set.ToString();
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/Abm/ZmachineData")]
        [HttpGet]
        public DataTable ZmachineData()
        {
            StringBuilder querystring = new StringBuilder();
            querystring.Append("select ZMachineConfig.MachineName,ZMachineConfig.name,ZMachineConfig.Value,ZSecciones.nombre from ZMachineConfig inner join ZSecciones on ZMachineConfig.Section = ZSecciones.IdSeccion and ZMachineConfig.MachineName = 'default'");
            string query = string.Format(querystring.ToString());
            var dataSet = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);
            DataTable firstTable = dataSet.Tables[0];

            return firstTable;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/Abm/ZmachineData2")]
        [HttpGet]
        public DataTable ZmachineData2()
        {

            StringBuilder querystring = new StringBuilder();
            querystring.Append("select ZMachineConfig.MachineName,ZMachineConfig.name,ZMachineConfig.Value,ZSecciones.nombre from ZMachineConfig inner join ZSecciones on ZMachineConfig.Section = ZSecciones.IdSeccion and ZMachineConfig.MachineName != 'default'");
            string query = string.Format(querystring.ToString());
            var dataSet = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);
            DataTable firstTable = dataSet.Tables[0];

            return firstTable;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/Abm/ZuserConfig")]
        [HttpGet]
        public DataTable ZuserConfigData()
        {

            StringBuilder querystring = new StringBuilder();
            querystring.Append("Select ZUserConfig.name,ZUserConfig.UserId,ZUserConfig.Value,ZSecciones.nombre from ZuserConfig inner join ZSecciones on ZUserConfig.Section = ZSecciones.IdSeccion and ZUserConfig.UserId = '0'");
            string query = string.Format(querystring.ToString());
            var dataSet = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);
            DataTable firstTable = dataSet.Tables[0];

            return firstTable;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/Abm/ZuserConfig2")]
        [HttpGet]
        public DataTable ZuserConfigData2()
        {

            StringBuilder querystring = new StringBuilder();
            querystring.Append("select USRTABLE.Name,ZuserConfig.UserId,ZuserConfig.name,ZuserConfig.Value,ZSecciones.nombre from ZuserConfig inner join USRTABLE On ZuserConfig.UserId = USRTABLE.ID inner join Zsecciones on ZuserConfig.Section = ZSecciones.IdSeccion");
            string query = string.Format(querystring.ToString());
            var dataSet = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);
            DataTable firstTable = dataSet.Tables[0];

            return firstTable;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/Abm/EditZuserconfig")]
        [HttpPost]
        public string EditZuserconfig(ZuserConfig ZuserConfig)
        {
            StringBuilder querystring = new StringBuilder();
            querystring.Append("Update ZuserConfig Set ZuserConfig.Value = ' " + ZuserConfig.Value + "' Where ZuserConfig.name = '" + ZuserConfig.Name + "' and ZuserConfig.UserId = '" + ZuserConfig.DefaultValue + "' ");
            string query = string.Format(querystring.ToString());
            var set = Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, query);
            return set.ToString();
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/Abm/InsertZuserconfig")]
        [HttpPost]
        public string InsertZuserconfig(ZuserConfig ZuserConfig)
        {
            if(ZuserConfig.DefaultValue == null)
            {
                ZuserConfig.DefaultValue = "0";
            }

            StringBuilder querystring = new StringBuilder();
            querystring.Append("Insert into Zuserconfig(Userid,name,Value,Section) Values(" + "'" + ZuserConfig.DefaultValue + "'" + ',' + "'" + ZuserConfig.Name);
            querystring.Append("'" + ',' + "'" + ZuserConfig.Value + "'" + ',' + "'" + ZuserConfig.section + "'" + ")");

            string query = string.Format(querystring.ToString());
            Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, query);
            return ZuserConfig.ToString();
        }



       
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/Abm/ZoptData")]
        [HttpGet]
        public DataTable ZoptData()
        {
            StringBuilder querystring = new StringBuilder();
            querystring.Append("select * from Zopt");
            string query = string.Format(querystring.ToString());
            var dataSet = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);
            DataTable firstTable = dataSet.Tables[0];

            return firstTable;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/Abm/EditZopt")]
        [HttpPost]
        public string EditZopt(Zopt zopt)
        {
            StringBuilder querystring = new StringBuilder();
            querystring.Append("Update Zopt Set Value = ' " + zopt.Value + "' Where Item = '" + zopt.Name + "' ");
            string query = string.Format(querystring.ToString());
            var set = Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, query);
            return set.ToString();
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/Abm/InsertZopt")]
        [HttpPost]
        public string InsertZopt(Zopt zopt)
        {
            StringBuilder querystring = new StringBuilder();
            querystring.Append("Insert into Zopt Values(" + "'" + zopt.Value + "'" + ',' + "'" + zopt.Name + "'" + ")");
            string query = string.Format(querystring.ToString());
            var set = Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, query);
            return set.ToString();
        }

    }
}
