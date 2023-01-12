using System;
using System.Collections.Generic;
using System.DirectoryServices;
using Zamba.Core;
using System.Collections;
using System.Text;
using Zamba.Servers;
using System.Data;

namespace Zamba.FAD
{
    public class ADResources
    {
        public class UserAD
        {
            public string Name { get; set; }
            public string Group { get; set; }

            public UserAD(string name, string group)
            {
                this.Name = name;
                this.Group = group;
            }
        }

        public bool ADValidation(string user)
        {

            try
            {
                var url = ADResources.GetValue("ActiveDirectoryUrl");
                //static String patch = "LDAP://ldap.forumsys.com/dc=example,dc=com"; // reemplazar por
                DirectoryEntry root = new DirectoryEntry(url, null, null, AuthenticationTypes.None);

                List<UserAD> userlist = new List<UserAD>();
                DirectorySearcher adsSearcherUsers = new DirectorySearcher(root);
                adsSearcherUsers.Filter = "(&(objectClass=user)(sAMAccountName=" + user.ToLower() + "))";
                var resultUsers = adsSearcherUsers.FindAll();

                if (resultUsers.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string> GetUsers(string objectClass)
        {
            ZOptBusiness zoptb = new ZOptBusiness();
            var url = zoptb.GetValue("ActiveDirectoryUrl");
            //static String patch = "LDAP://ldap.forumsys.com/dc=example,dc=com"; // reemplazar por  
            DirectoryEntry root = new DirectoryEntry(url, null, null, AuthenticationTypes.None);
            ZTrace.WriteLineIf(ZTrace.IsVerbose, url);

            List<string> userlist = new List<string>();

            DirectorySearcher adsSearcherUsers = new DirectorySearcher(root);
            adsSearcherUsers.Filter = "(&(objectClass=" + objectClass + "))";
            //person

            var resultUsers = adsSearcherUsers.FindAll();
            foreach (SearchResult entryUser in resultUsers)
            {
                var name = entryUser.GetDirectoryEntry().Properties["sn"].Value.ToString();
                ZTrace.WriteLineIf(ZTrace.IsVerbose, name);

                //var email = entryUser.GetDirectoryEntry().Properties["mail"].Value.ToString();
                //if (name.ToLowerInvariant() == user.Name.ToLowerInvariant())
                userlist.Add(name);
            }
            return userlist;
        }


        public List<string> GetRolesForUser(string username)
        {

            var url = ADResources.GetValue("ActiveDirectoryUrl");
            ZTrace.WriteLineIf(ZTrace.IsInfo,"busco roles del usuario " + username + " en el server" + url);

            DirectoryEntry root = new DirectoryEntry(url, null, null, AuthenticationTypes.None);
            try
            {
                DirectorySearcher adsSearcherGroups = new DirectorySearcher(root);
                adsSearcherGroups.Filter = @"(&(objectClass=user)(sAMAccountName=" + username + "))";
                adsSearcherGroups.PropertiesToLoad.Add("memberOf");

                SearchResult result = adsSearcherGroups.FindOne();

                var allRoles = new List<string>();

                if (result != null && !string.IsNullOrEmpty(result.Path))
                {
                    DirectoryEntry user = result.GetDirectoryEntry();

                    PropertyValueCollection groups = user.Properties["memberOf"];

                    foreach (string path in groups)
                    {
                        string[] parts = path.Split(',');

                        if (parts.Length > 0)
                        {
                            foreach (string part in parts)
                            {
                                string[] p = part.Split('=');

                                if (p[0].Equals("cn", StringComparison.OrdinalIgnoreCase))
                                {
                                    allRoles.Add(p[1]);
                                }
                            }
                        }
                    }
                }

                return allRoles;

            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());

                return null;
            }

        }


        public Dictionary<string, object> GetUserProperties(string username)
        {

            Dictionary<string, object> PropertiesList = new Dictionary<string, object>();

            try
            {

                var url = ADResources.GetValue("ActiveDirectoryUrl");
                if (!string.IsNullOrEmpty(url))
                {
                    DirectoryEntry root = new DirectoryEntry(url, null, null, AuthenticationTypes.None);

                    DirectorySearcher adsSearcherGroups = new DirectorySearcher(root);
                    adsSearcherGroups.Filter = @"(&(objectClass=user)(sAMAccountName=" + username + "))";

                    var result = adsSearcherGroups.FindAll();

                    foreach (SearchResult props in result)
                    {

                        if (props.GetDirectoryEntry().Properties.Contains("mail"))
                            PropertiesList.Add("EMAIL", props.GetDirectoryEntry().Properties["mail"].Value.ToString());
                        else
                            PropertiesList.Add("EMAIL", string.Empty);

                        if (props.GetDirectoryEntry().Properties.Contains("sn"))
                            PropertiesList.Add("APELLIDO", props.GetDirectoryEntry().Properties["sn"].Value.ToString());
                        else
                            PropertiesList.Add("APELLIDO", string.Empty);

                        if (props.GetDirectoryEntry().Properties.Contains("givenName"))
                            PropertiesList.Add("NOMBRE", props.GetDirectoryEntry().Properties["givenName"].Value.ToString());
                        else
                            PropertiesList.Add("NOMBRE", string.Empty);

                        if (props.GetDirectoryEntry().Properties.Contains("ThumbNailPhoto"))
                            PropertiesList.Add("ThumbNailPhoto", props.GetDirectoryEntry().Properties["ThumbNailPhoto"].Value);
                        else
                            PropertiesList.Add("ThumbNailPhoto", null);

                    }
                }
                return PropertiesList;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unknown error") || ex.Message.Contains("The specified domain either does not exist"))
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "Error consultando AD" + ex.ToString());
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "Error consultando AD" + ex.ToString());
                }
                return null;
            }
        }


        public void GetAllUsersFromAD()
        {
            List<string> allUsers = GetUsersFromGroup("users");
            foreach (string userName in allUsers)
            { }


        }

            public List<string> GetUsersFromGroup(string groupName)
        {
            ZOptBusiness zoptb = new ZOptBusiness();
            var url = zoptb.GetValue("ActiveDirectoryUrl");
            DirectoryEntry root = new DirectoryEntry(url, null, null, AuthenticationTypes.None);
            List<string> UserList = new List<string>();
            try
            {
                DirectorySearcher adsSearcherGroups = new DirectorySearcher(root);
                adsSearcherGroups.Filter = "(&(objectClass=groupOfuniqueNames))";
                var resultGroups = adsSearcherGroups.FindAll();
                foreach (SearchResult g in resultGroups)
                {
                    var group = g.GetDirectoryEntry().Properties["cn"].Value.ToString();
                    var users = g.GetDirectoryEntry().Properties["uniqueMember"].Value;
                    if (group == groupName)
                        foreach (var user in users as IEnumerable)
                        {
                            var username = user.ToString().Split(new string[] { "=", "," }, StringSplitOptions.None);
                            UserList.Add(username[1].ToString());
                        }
                }

                if (UserList != null)
                    return UserList;
                else
                    return null;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool ADIsActive()
        {
            var url = ADResources.GetValue("UseADLogin");

            if (url == "true")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static string GetValue(string key)
        {
            StringBuilder QueryBuilder = new StringBuilder();
            //Ignore strings case
            if (Server.isOracle)
            {
                QueryBuilder.Append("SELECT Value FROM Zopt WHERE UPPER(Item)='");
                QueryBuilder.Append(key.ToUpper());
            }
            else
            {
                QueryBuilder.Append("SELECT Value FROM Zopt WHERE Item='");
                QueryBuilder.Append(key);
            }
            QueryBuilder.Append("'");

            object ReturnValue = Server.get_Con().ExecuteScalar(CommandType.Text, QueryBuilder.ToString());
            QueryBuilder.Remove(0, QueryBuilder.Length);

            if (ReturnValue == null) return string.Empty;
            return ReturnValue.ToString();
        }


    }





}
