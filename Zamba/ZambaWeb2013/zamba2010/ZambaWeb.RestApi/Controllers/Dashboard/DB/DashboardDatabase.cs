using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using Zamba.Core;
using Zamba.Framework;
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


        public void InsertResetToken(string email, string token)
        {

            email = email.Trim();
            email = email.Replace(" ", "");

            StringBuilder sqlCommand1 = new StringBuilder();
            sqlCommand1.AppendLine("DELETE FROM zambabpm_RRHH.resetpasswordtokens ");
            sqlCommand1.AppendLine("WHERE email = '" + email + "' ;");

            Server.get_Con().ExecuteScalar(CommandType.Text, sqlCommand1.ToString());


            DateTime expirationDate = DateTime.Now.AddHours(24);
            string expirationDateString = expirationDate.ToString("yyyy-MM-dd HH:mm:ss");
            StringBuilder sqlCommand2 = new StringBuilder();
            sqlCommand2.AppendLine("INSERT into zambabpm_RRHH.resetpasswordtokens ");
            sqlCommand2.AppendLine("(email,tokendata,expirationdate,used) ");
            sqlCommand2.AppendLine("VALUES ('" + email + "','" + token + "','" + expirationDateString + "', " + 0 + " );");

            Server.get_Con().ExecuteScalar(CommandType.Text, sqlCommand2.ToString());
        }

        public void InsertResetToken(string email, string token, int durationInDays)
        {

            email = email.Trim();
            email = email.Replace(" ", "");

            StringBuilder sqlCommand1 = new StringBuilder();
            sqlCommand1.AppendLine("DELETE FROM zambabpm_RRHH.resetpasswordtokens ");
            sqlCommand1.AppendLine("WHERE email = '" + email + "' ;");

            Server.get_Con().ExecuteScalar(CommandType.Text, sqlCommand1.ToString());

            DateTime expirationDate = DateTime.Now.AddDays(durationInDays);
            string expirationDateString = expirationDate.ToString("yyyy-MM-dd HH:mm:ss");
            StringBuilder sqlCommand2 = new StringBuilder();
            sqlCommand2.AppendLine("INSERT into zambabpm_RRHH.resetpasswordtokens ");
            sqlCommand2.AppendLine("(email,tokendata,expirationdate,used) ");
            sqlCommand2.AppendLine("VALUES ('" + email + "','" + token + "','" + expirationDateString + "', " + 0 + " );");

            Server.get_Con().ExecuteScalar(CommandType.Text, sqlCommand2.ToString());
        }


        public string ChangePassword(string tokendata,string newpassword)
        {
            string rv = string.Empty;
            DateTime expirationDate = DateTime.Now;
            string expirationDateString = expirationDate.ToString("yyyy-MM-dd HH:mm:ss");

            StringBuilder sqlCommand1 = new StringBuilder();
            sqlCommand1.AppendLine("SELECT * FROM zambabpm_RRHH.resetpasswordtokens ");
            sqlCommand1.AppendLine("WHERE tokendata = '" + tokendata + "' and expirationdate >= '" + expirationDateString + "' and used = 0;");

            DataSet dataSet = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand1.ToString());

            ResetPasswordToken resetToken = new ResetPasswordToken();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataSet.Tables[0].Rows[0];

                resetToken.id = row["id"] != DBNull.Value ? Convert.ToInt32(row["id"]) : 0;
                resetToken.email = row["email"].ToString();

                int zambaUserId = GetUserDashboardZambaID(resetToken.email);

                if (zambaUserId != -1) {

                    UserBusiness UB = new UserBusiness();
                    IUser User = UB.GetUserById(zambaUserId);
                    User.Password = newpassword;
                    UB.UpdateUserPassword(User);
                }

                StringBuilder sqlCommand2 = new StringBuilder();
                sqlCommand2.AppendLine("UPDATE zambabpm_RRHH.DashboardUsers ");
                sqlCommand2.AppendLine("set password = '" + newpassword + "'");
                sqlCommand2.AppendLine(" WHERE email = '" + resetToken.email + "'");

                Server.get_Con().ExecuteScalar(CommandType.Text, sqlCommand2.ToString());

                StringBuilder sqlCommand3 = new StringBuilder();
                sqlCommand3.AppendLine("UPDATE zambabpm_RRHH.resetpasswordtokens ");
                sqlCommand3.AppendLine("set used = '1'");
                sqlCommand3.AppendLine(" WHERE id = " + resetToken.id);

                Server.get_Con().ExecuteScalar(CommandType.Text, sqlCommand3.ToString());

                rv = "ok";
            }
            return rv;
        }

        public string validateResetTokendata(string tokendata)
        {
            string rv = string.Empty;
            DateTime expirationDate = DateTime.Now;
            string expirationDateString = expirationDate.ToString("yyyy-MM-dd HH:mm:ss");

            StringBuilder sqlCommand1 = new StringBuilder();
            sqlCommand1.AppendLine("SELECT * FROM zambabpm_RRHH.resetpasswordtokens ");
            sqlCommand1.AppendLine("WHERE tokendata = '" + tokendata + "' and expirationdate >= '" + expirationDateString + "' and used = 0;");

            DataSet dataSet = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand1.ToString());

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                rv = "ok";
            }
            return rv;
        }

        public LoginResponseData Login(string email,string password)
        {

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT * FROM zambabpm_RRHH.DashboardUsers ");
            sqlCommand.AppendLine("WHERE email = '" + email.Trim() +"' AND password = '" + password +"';"); 

            DataSet dataSet = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());
            LoginResponseData loginResponseData = new LoginResponseData();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataSet.Tables[0].Rows[0];

                loginResponseData.msg = "ok";
                loginResponseData.user.token = "123456789"; 
                loginResponseData.user.name = row["username"].ToString();
                loginResponseData.user.email = row["email"].ToString();
                loginResponseData.user.id = Convert.ToInt32(row["Enterpriseuser_id"]);
                loginResponseData.user.time = 0;
                loginResponseData.isActive = Convert.ToBoolean(row["isactive"]);
                loginResponseData.user.userid = row["userid"] != DBNull.Value ? Convert.ToInt32(row["userid"]) : 0;
                
            }
            else
            {
                loginResponseData.msg = "Invalid username or password";
            }

            return loginResponseData;
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

        public DashboarUserDTO GetUserDashboard(string Email)
        {
            DashboarUserDTO user = new DashboarUserDTO();
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT companyname,firstname,lastname,phonenumber,username,email,password,department_id,rol_id,isActive");
            sqlCommand.AppendLine("FROM zambabpm_RRHH.DashboardUsers");
            sqlCommand.AppendLine("WHERE email = '" + Email + "';");

            DataSet dataSet = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataSet.Tables[0].Rows[0];
                user.Password = row["password"].ToString();
                user.FirstName = row["firstname"].ToString();
                user.LastName = row["lastname"].ToString();
                user.Email = row["email"].ToString();
            }
            return user;
        }

        public int GetUserDashboardZambaID(string Email)
        {
            int userid = -1;
            DashboarUserDTO user = new DashboarUserDTO();
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT userid");
            sqlCommand.AppendLine("FROM zambabpm_RRHH.DashboardUsers");
            sqlCommand.AppendLine("WHERE email = '" + Email + "';");

            DataSet dataSet = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataSet.Tables[0].Rows[0];
                if (row["userid"] != DBNull.Value)
                    userid = Convert.ToInt32(row["userid"]);
            }
            return userid;
        }

        public DataTable GetUserDashboard(long userId)
        {
            DataSet dataSet = new DataSet();
            StringBuilder sqlCommand = new StringBuilder();

            sqlCommand.AppendLine("SELECT * ");
            sqlCommand.AppendLine("FROM zambabpm_RRHH.DashboardUsers");
            sqlCommand.AppendLine("WHERE userId = '" + userId + "'");

            dataSet = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());

            return dataSet.Tables[0];
        }

        public Validator EmailAlreadyTaken(string Email) {

            var validator = new Validator(); 

            StringBuilder sqlCommand = new StringBuilder();

            sqlCommand.AppendLine("SELECT * ");
            sqlCommand.AppendLine("FROM zambabpm_RRHH.DashboardUsers");
            sqlCommand.AppendLine("WHERE email = '" + Email.Trim() + "'");

            DataSet dataSet = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());
            if (((DataSet)dataSet).Tables[0].Rows.Count != 0)
                validator.emailIsTaken = true;

            return validator;
        }
        public bool UserNeedValidation(string Email)
        {
            bool rv = true;

            StringBuilder sqlCommand = new StringBuilder();

            sqlCommand.AppendLine("SELECT * ");
            sqlCommand.AppendLine("FROM zambabpm_RRHH.DashboardUsers");
            sqlCommand.AppendLine("WHERE email = '" + Email.Trim() + "' and isActive = 1");

            DataSet dataSet = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());
            if (((DataSet)dataSet).Tables[0].Rows.Count != 0)
                rv = false;

            return rv;
        }

        public bool UserIsActive(string Email) {
            return !UserNeedValidation(Email);
        }

        public void ActivateUser(string Password, string Email, long newUserId)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("UPDATE zambabpm_RRHH.DashboardUsers ");
            sqlCommand.AppendLine("set isActive = 1, userid = " + newUserId);
            sqlCommand.AppendLine(" WHERE password = '" + Password + "' and email = '" + Email + "'");

            Server.get_Con().ExecuteNonQuery(CommandType.Text, sqlCommand.ToString());
        }

        public DataTable GetDepartment()
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT * FROM zambabpm_RRHH.Departments");

            DataSet DSResult = new DataSet("Deparments");

            DSResult = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());
            return DSResult.Tables[0];
        }

        public DataTable configUserSidbar(string groupid)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT * FROM zambabpm_RRHH.DashboardUserConfig where valueconfig = 'viewSidbar' AND group_id IN ("+ groupid +");");

            DataSet DSResult = new DataSet("DashboardUsers");

            DSResult = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());
            return DSResult.Tables[0];
        }



        public DataTable CarouselContent(string UserId)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT * FROM zambabpm_RRHH.CarouselContent where UserId = " + UserId);

            DataSet DSResult = new DataSet("CarouselContent");

            DSResult = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());
            return DSResult.Tables[0];
        }





        public DataTable CarouselConfig(string UserId)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT * FROM zambabpm_RRHH.CarouselConfig where UserId = " + UserId);

            DataSet DSResult = new DataSet("CarouselConfig");

            DSResult = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());
            return DSResult.Tables[0];
        }


        public DataTable optionsUserSidbar(string groupid)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT * FROM zambabpm_RRHH.SidebarMenuItem WHERE group_id IN (" + groupid + ") ORDER BY position ASC");

            DataSet DSResult = new DataSet("DashboardUsersOption");

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

        public void InsertNewEvent(string eventData, string groupid, string userid)
        {
            string sqlCommand = "SELECT IDENT_CURRENT('[zambabpm_RRHH].[zambabpm_RRHH].[CalendarEvents]') + 1 AS nextid;";
            var ds = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand);

            JObject eventDataObject = JsonConvert.DeserializeObject<JObject>(eventData);
            eventDataObject["id"] = ds.Tables[0].Rows[0]["nextid"].ToString();
            string eventDataJsonWithID = JsonConvert.SerializeObject(eventDataObject);
            
            sqlCommand = $"INSERT INTO zambabpm_RRHH.CalendarEvents ([eventdata], [groupid], [userid]) " +
            $"VALUES ('{eventDataJsonWithID}', {(string.IsNullOrEmpty(groupid) ? "NULL" : groupid)}, {(string.IsNullOrEmpty(userid) ? "NULL" : userid)});";
            Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand);
        }
        public void UpdateEvent(string eventData, string groupid, string userid,string calendareventid)
        {

            string sqlCommand = $"UPDATE zambabpm_RRHH.CalendarEvents " +
            $"SET eventdata = '{eventData}', groupid = {(string.IsNullOrEmpty(groupid) ? "NULL" : groupid)}, userid = {(string.IsNullOrEmpty(userid) ? "NULL" : userid)} " +
            $" WHERE calendareventid = {calendareventid};";
            Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand);
        }

        internal void InsertWidgetsContainer_Sql(genericRequest request)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("INSERT INTO zambabpm_RRHH.WidgetsContainer " +
                "(UserId, Options, WidgetCoordinates) " +
                "VALUES " +
                "(" + request.UserId.ToString() + ", '" + request.Params["options"].ToString() + "', '" + request.Params["widgetsContainer"].ToString() + "')");

            Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());
        }



        internal string InsertWidgetsContainer_Oracle(genericRequest request)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("INSERT INTO zambabpm_RRHH.WidgetsContainer " +
                "(UserId, Options, WidgetCoordinates) " +
                "VALUES " +
                "(" + request.UserId.ToString() + ", '" + request.Params["options"].ToString() + "', '" + request.Params["widgetsContainer"].ToString() + "')");

            return sqlCommand.ToString();

        }





        internal void UpdateWidgetsContainer_Sql(genericRequest request)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("UPDATE zambabpm_RRHH.WidgetsContainer " +
                "SET UserId = " + request.UserId.ToString() +
                ", Options = " + request.Params["Options"].ToString() +
                ", WidgetCoordinates = " + request.Params["WidgetCoordinates"].ToString() +
                " WHERE UserId = " + request.UserId.ToString());

            Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());
        }



        internal string UpdateWidgetsContainer_Oracle(genericRequest request)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("UPDATE zambabpm_RRHH.WidgetsContainer " +
                "SET UserId = " + request.UserId.ToString() +
                ", Options = '" + request.Params["Options"].ToString() +
                "', WidgetCoordinates = '" + request.Params["WidgetCoordinates"].ToString() +
                "' WHERE UserId = " + request.UserId.ToString());

            return sqlCommand.ToString();

        }


        public object InsertOrUpdateWidgetsContainer(genericRequest request)
        {
            object count;
            if (Server.isSQLServer)
            {
                count = Server.get_Con().ExecuteScalar(CommandType.Text, $"SELECT count(1) FROM zambabpm_RRHH.WidgetsContainer where UserId = {request.UserId.ToString()}");
                if ((long)count == 0)                
                    InsertWidgetsContainer_Sql(request);                
                else                
                    UpdateWidgetsContainer_Sql(request);                
            }
            else if (Server.isOracle)
            {
                count = Server.get_Con().ExecuteScalar(CommandType.Text, $"SELECT count(1) FROM zambabpm_RRHH.WidgetsContainer where UserId = {request.UserId.ToString()}");
                StringBuilder sqlBuilder = new StringBuilder();

                sqlBuilder.Append("DECLARE ");
                sqlBuilder.AppendLine("BEGIN ");

                if ((long)count == 0)
                    sqlBuilder.Append(InsertWidgetsContainer_Oracle(request));
                else
                    sqlBuilder.Append(UpdateWidgetsContainer_Oracle(request));

                sqlBuilder.AppendLine(";");
                sqlBuilder.AppendLine("END;");

                Server.get_Con().ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString());
            }
            return null;
        }


        internal DataTable getWidgetsContainer(string groupid)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT * FROM zambabpm_RRHH.WidgetsContainer WHERE UserId = " + groupid);

            DataSet DSResult = new DataSet("WidgetsContainer");

            DSResult = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand.ToString());
            return DSResult.Tables[0];
        }
        public void DeleteEvent(string eventid)
        {
            string sqlCommand = $"DELETE zambabpm_RRHH.CalendarEvents " +
                                 $"WHERE calendareventid = {eventid};";

            Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand);
        }


        public DataSet GetEventsForUser(string userid)
        {
            string sqlCommand = $"SELECT eventdata from zambabpm_RRHH.CalendarEvents " +
                                 $"WHERE userid = {userid};";

            return Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand);
        }

        public DataSet GetEventsForGroups(string groupids)
        {
            groupids = groupids.Trim('[', ']', ' ');
            string sqlCommand = $"SELECT eventdata from zambabpm_RRHH.CalendarEvents " +
                                 $"WHERE groupid in ({groupids});";

            return Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand);
        }

        public VideoResourceDTO GetVideoplayerURL(long userid)
        {
            string sqlCommand = $"SELECT * from zambabpm_RRHH.VideoplayerURLSources " +
                                 $"WHERE userid = {userid};";

            DataSet dataSet = Server.get_Con().ExecuteDataset(CommandType.Text, sqlCommand);
            VideoResourceDTO returnValue = new VideoResourceDTO();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DataRow row = dataSet.Tables[0].Rows[0];

                returnValue.videoplayerResourceID = Convert.ToInt64(row["videoplayersourceid"]);
                returnValue.userid = Convert.ToInt64(row["userid"]);
                returnValue.YouTubeVideoID = row["youtubevideoid"].ToString();
            }
            return returnValue;
        }


        public class ResetPasswordToken {

            public int id { get; set; }
            public string email { get; set; }
        }
        public class VideoResourceDTO {

            public long videoplayerResourceID { get; set; }

            public long userid { get; set; }

            public string YouTubeVideoID { get; set; }
        }
        public class CalendarEventDTO {

            public string eventData { get; set; }
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

            public DashboarUserDTO(int? enterpriseUserId, string companyName, string firstName, string lastName, string phoneNumber, string email, string password, int? departmentId, bool isActive)
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
                this.isActive = isActive;
            }

        }

        public class Validator {
            public bool emailIsTaken { get; set; }
            public bool userDoesntExist { get; set; }

        }

        public class LoginResponseData {
            public string msg { get; set; }

            public UserDTOLogin user { get; set; }

            public bool isActive { get; set; }

            public LoginResponseData()
            {
                this.user = new UserDTOLogin();
                this.msg = String.Empty;
                this.isActive = false;
            }
        }

        public class UserDTOLogin {
            public string token { get; set; }
            public string name { get; set; }

            public string email { get; set; }
            public int id { get; set; }

            public int time { get; set; }

            public int userid { get; set; }

            public ArrayList groups { get; set; }
        }
    }
}