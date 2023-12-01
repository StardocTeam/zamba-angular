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
            sqlCommand.AppendLine("(companyname,firstname,lastname,phonenumber,email,password,department_id,rol_id,isActive) ");
            sqlCommand.AppendLine("VALUES ('" + dashboarUserDTO.CompanyName + "','" + dashboarUserDTO.FirstName + "','" + dashboarUserDTO.LastName + "','" + dashboarUserDTO.PhoneNumber + "','" + dashboarUserDTO.Email + "','" + dashboarUserDTO.Password + "', " + dashboarUserDTO.DepartmentId + "," + dashboarUserDTO.RolId + ",'" + dashboarUserDTO.isActive + "');");

            Server.get_Con().ExecuteScalar(CommandType.Text, sqlCommand.ToString());
        }

        public DataTable GetDepartment()
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT * FROM zambabpm_RRHH.Departments");

            DataSet DSResult = new DataSet("Deparments");

            DSResult = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());
            return DSResult.Tables[0];
        }

        public DataTable GetRol()
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT * FROM zambabpm_RRHH.Rol");

            DataSet DSResult = new DataSet("Rol");

            DSResult = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());
            return DSResult.Tables[0];
        }

        public class DashboarUserDTO {

            public int? EnterpriseUserId { get; set; }
            public string CompanyName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
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
                Email = String.Empty;
                Password = String.Empty;
                DepartmentId = 0;
                RolId = 0;
                isActive = false;
            }

            public DashboarUserDTO(int? enterpriseUserId, string companyName, string firstName, string lastName, string phoneNumber, string email, string password, int? departmentId, int? rolId, bool isActive)
            :this()
            {
                EnterpriseUserId = enterpriseUserId;
                CompanyName = companyName;
                FirstName = firstName;
                LastName = lastName;
                PhoneNumber = phoneNumber;
                Email = email;
                Password = password;
                DepartmentId = departmentId;
                RolId = rolId;
                this.isActive = isActive;
            }

        }

    }
}