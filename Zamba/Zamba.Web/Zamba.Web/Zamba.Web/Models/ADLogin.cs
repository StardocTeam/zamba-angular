using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using Zamba.Core;

namespace Zamba.Web.Models
{
    public class ADLogin
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


        static String patch = "LDAP://ldap.forumsys.com/dc=example,dc=com"; // reemplazar por variable en la zopt
        DirectoryEntry root = new DirectoryEntry(patch, null, null, AuthenticationTypes.None);

        public bool ADValidation(IUser user)
        {
            try
            {
                if (GetUserGroup(user))
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        private List<UserAD> GetUser(IUser user)
        {
            List<UserAD> userlist = new List<UserAD>();
            DirectorySearcher adsSearcherUsers = new DirectorySearcher(root);
            adsSearcherUsers.Filter = "(&(objectClass=person))";
            var resultUsers = adsSearcherUsers.FindAll();
            foreach (SearchResult entryUser in resultUsers)
            {
                var name = entryUser.GetDirectoryEntry().Properties["sn"].Value.ToString();
                //var email = entryUser.GetDirectoryEntry().Properties["mail"].Value.ToString();

                if(name.ToLowerInvariant() == user.Name.ToLowerInvariant())
                userlist.Add(new UserAD(name, "null"));
            }
            return userlist;
        }

        public bool GetUserGroup(IUser user)
        {
            Dictionary<string, object> dicc = new Dictionary<string, object>();
            try
            {
                DirectorySearcher adsSearcherGroups = new DirectorySearcher(root);
                adsSearcherGroups.Filter = "(&(objectClass=groupOfuniqueNames))";
                var resultGroups = adsSearcherGroups.FindAll();

                foreach (SearchResult entry in resultGroups)
                {
                    var group = entry.GetDirectoryEntry().Properties["cn"].Value.ToString();
                    object users = entry.GetDirectoryEntry().Properties["uniqueMember"].Value;
                    dicc.Add(group, users);
                }

                foreach (var iuser in dicc)
                {
                    //grupo con mas de 1 usuario
                    if (!(iuser.Value is string))
                    {
                        foreach (var value in iuser.Value as IEnumerable)
                        {
                            var username = value.ToString().Split(new string[] { "=", "," }, StringSplitOptions.None);
                            if (user.Name.ToString() == username[1].ToString())
                            {
                                foreach (var ud in GetUser(user))
                                {
                                    if (user.Name == ud.Name.ToLowerInvariant())
                                    {
                                        ud.Group = iuser.Key.ToString();
                                        //carga modulos
                                        LoadModuls(ud);

                                        return true;
                                    }
                                }
                            }
                        }
                    }
                    //grupo con un solo usuario
                    else
                    {
                        var username = iuser.ToString().Split(new string[] { "=", "," }, StringSplitOptions.None);

                        if (user.Name == username[2].ToLowerInvariant())
                        {
                            var userad = new UserAD(user.Name, username[1]);

                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return false;
        }

        //Carga los modulos dependiendo a que grupo pertenece
        public UserAD LoadModuls(UserAD user)
        {
            return user;
        }
    }
}