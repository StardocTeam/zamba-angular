using System;
using System.Collections.Generic;
using System.DirectoryServices;
using Zamba.Core;
using System.Collections;
using System.Text;
using Zamba.Servers;
using System.Data;
using System.DirectoryServices.AccountManagement;

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
            }
            catch
            {
                return false;
            }

            return false;
        }

        public List<UserAD> GetUsers()
        {

            var url = ADResources.GetValue("ActiveDirectoryUrl");
            //static String patch = "LDAP://ldap.forumsys.com/dc=example,dc=com"; // reemplazar por  
            DirectoryEntry root = new DirectoryEntry(url, null, null, AuthenticationTypes.None);

            List<UserAD> userlist = new List<UserAD>();
            DirectorySearcher adsSearcherUsers = new DirectorySearcher(root);
            adsSearcherUsers.Filter = "(&(objectClass=person))";
            var resultUsers = adsSearcherUsers.FindAll();
            foreach (SearchResult entryUser in resultUsers)
            {
                var name = entryUser.GetDirectoryEntry().Properties["sn"].Value.ToString();
                //var email = entryUser.GetDirectoryEntry().Properties["mail"].Value.ToString();
                //if (name.ToLowerInvariant() == user.ToLowerInvariant())
                userlist.Add(new UserAD(name, "null"));

            }
            return userlist;
        }

        public Dictionary<string, object> GetAllGroup()
        {

            var url = ADResources.GetValue("ActiveDirectoryUrl");
            DirectoryEntry root = new DirectoryEntry(url, null, null, AuthenticationTypes.None);

            Dictionary<string, object> groups = new Dictionary<string, object>();
            try
            {
                DirectorySearcher adsSearcherGroups = new DirectorySearcher(root);
                adsSearcherGroups.Filter = "(&(objectClass=groupOfuniqueNames))";
                var resultGroups = adsSearcherGroups.FindAll();

                foreach (SearchResult entry in resultGroups)
                {
                    var group = entry.GetDirectoryEntry().Properties["cn"].Value.ToString();
                    object users = entry.GetDirectoryEntry().Properties["uniqueMember"].Value;
                    groups.Add(group, users);
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return groups;
        }


        public bool IsUserInRole(string username, string roleName)
        {
            List<string> roles = GetRolesForUser(username);

            foreach (string role in roles)
            {
                if (role.Equals(roleName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public Dictionary<string, object> GetUserProperties(string username)
        {
            Dictionary<string, object> PropertiesList = new Dictionary<string, object>();

            var url = ADResources.GetValue("ActiveDirectoryUrl");
            DirectoryEntry root = new DirectoryEntry(url, null, null, AuthenticationTypes.None);

            try
            {
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

                }
                return PropertiesList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<string> GetRolesForUser(string username)
        {

            var url = ADResources.GetValue("ActiveDirectoryUrl");
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
                                    if (allRoles.Contains(p[1]) == false)
                                    allRoles.Add(p[1]);
                                }
                            }
                        }
                    }
                }

                return allRoles;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public UserAD GetUserGroup(string user)
        {
            try
            {
                foreach (var iuser in GetAllGroup())
                {
                    //grupo con mas de 1 usuario
                    if (!(iuser.Value is string))
                    {
                        foreach (var value in iuser.Value as IEnumerable)
                        {
                            var username = value.ToString().Split(new string[] { "=", "," }, StringSplitOptions.None);
                            if (user == username[1].ToString())
                            {
                                foreach (var userad in GetUsers())
                                {
                                    if (user == userad.Name.ToLowerInvariant())
                                    {
                                        userad.Group = iuser.Key.ToString();

                                        return userad;
                                    }
                                }
                            }
                        }
                    }
                    //grupo con un solo usuario
                    else
                    {
                        var username = iuser.ToString().Split(new string[] { "=", "," }, StringSplitOptions.None);
                        if (user == username[2].ToLowerInvariant())
                        {
                            var userad = new UserAD(user, username[1]);

                            return userad;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }



        public bool ADIsActive()
        {
            string url = ADResources.GetValue("UseADLogin");

            if (url.ToLower() == "true")
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
