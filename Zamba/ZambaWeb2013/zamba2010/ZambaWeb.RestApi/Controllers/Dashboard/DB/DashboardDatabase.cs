using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Zamba.Core;
using Zamba.Servers;

namespace ZambaWeb.RestApi.Controllers.Dashboard.DB
{
    public class DashboardDatabase
    {

        public DashboardDatabase()
        {

            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Dashboard");
            }

        }
        public void RegisterUser(DashboarUserDTO dashboarUserDTO) {

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("INSERT into zambabpm_RRHH.DashboardUsers ");
            sqlCommand.AppendLine("(companyname,firstname,lastname,phonenumber,username,email,password,department_id,rol_id,isActive) ");
            sqlCommand.AppendLine("VALUES ('" + dashboarUserDTO.CompanyName + "','" + dashboarUserDTO.FirstName + "','" + dashboarUserDTO.LastName + "','" + dashboarUserDTO.PhoneNumber  + "','" + dashboarUserDTO.Username + "','" + dashboarUserDTO.Email + "','" + dashboarUserDTO.Password + "', " + dashboarUserDTO.DepartmentId + "," + dashboarUserDTO.RolId + ",'" + dashboarUserDTO.isActive + "');");

            Server.get_Con().ExecuteScalar(CommandType.Text, sqlCommand.ToString());
        }

        public DataTable GetUserDashboard(string Username, string Email)
        {
            DataSet dataSet = new DataSet();
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT (companyname,firstname,lastname,phonenumber,username,email,password,department_id,rol_id,isActive)");
            sqlCommand.AppendLine("FROM zambabpm_RRHH.DashboardUsers");
            sqlCommand.AppendLine("WHERE username = '" + Username + "' and email = '" + Email);

            dataSet = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());

            return dataSet.Tables[0];

        }

        public Validator UsernameOrEmailAlreadyTaken(string Username, string Email) {

            var validator = new Validator(); 

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT * ");
            sqlCommand.AppendLine("FROM zambabpm_RRHH.DashboardUsers");
            sqlCommand.AppendLine("WHERE username = '" + Username);

            DataSet dataSet = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());
            if (dataSet != null && ((DataSet)dataSet).Tables[0].Rows.Count == 0)
                validator.usernameIsTaken = true;


            dataSet = new DataSet();
            sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT * ");
            sqlCommand.AppendLine("FROM zambabpm_RRHH.DashboardUsers");
            sqlCommand.AppendLine("WHERE email = '" + Email);

            dataSet = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());
            if (dataSet != null && ((DataSet)dataSet).Tables[0].Rows.Count == 0)
                validator.emailIsTaken = true;

            return validator;
        }

        public void ActivateUser(string Username, string Email)
        {

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("UPDATE zambabpm_RRHH.DashboardUsers ");
            sqlCommand.AppendLine("set isActive = 1 ");
            sqlCommand.AppendLine("WHERE username = '" + Username + "' and email = '" + Email);

            Server.get_Con().ExecuteNonQuery(CommandType.Text, sqlCommand.ToString());
        }

        public class DashboarUserDTO {

            public int? EnterpriseUserId { get; set; }
            public string CompanyName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }

            public string Username { get; set; }
            public string Email { get; set; }

            public string Password { get; set; }
            public int? DepartmentId { get; set; }
            public int? RolId { get; set; }
            public bool isActive { get; set; }

            public DashboarUserDTO()
            {
                EnterpriseUserId = 0;
                CompanyName = String.Empty;
                FirstName = String.Empty;
                LastName = String.Empty;
                PhoneNumber = String.Empty;
                Username = String.Empty;
                Email = String.Empty;
                Password = String.Empty;
                DepartmentId = 0;
                RolId = 0;
                isActive = false;
            }

            public DashboarUserDTO(int? enterpriseUserId, string companyName, string firstName, string lastName, string phoneNumber, string username, string email, string password, int? departmentId, int? rolId, bool isActive)
            :this()
            {
                EnterpriseUserId = enterpriseUserId;
                CompanyName = companyName;
                FirstName = firstName;
                LastName = lastName;
                PhoneNumber = phoneNumber;
                Username = username;
                Email = email;
                Password = password;
                DepartmentId = departmentId;
                RolId = rolId;
                this.isActive = isActive;
            }

        }

        public class Validator {

            public bool usernameIsTaken { get; set; }

            public bool emailIsTaken { get; set; }
        }

    }
}