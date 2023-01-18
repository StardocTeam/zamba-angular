using System;
using System.Collections.Generic;

namespace Zamba.Web.App_Code.Helpers
{
    public static class Functions
    {
        public static string ListToString(List<long> list)
        {
            string s = string.Empty;

            list.Sort();

            foreach (long item in list)
                s += item + ",";

            s = s.Remove(s.Length - 1);
            return s;
        }

        public static string ListToString(List<int> list)
        {
            string s = string.Empty;

            list.Sort();

            foreach (int item in list)
                s += item + ",";

            s = s.Remove(s.Length - 1);
            return s;
        }

/*
        public static string ListToString(List<string> list)
        {
            list.Sort();
            return string.Join(",", list.ToArray());
        }
*/

        public static string Md5(string value)
        {
            var x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var data = System.Text.Encoding.ASCII.GetBytes(value);
            data = x.ComputeHash(data);
            var ret = "";
            for (var i = 0; i < data.Length; i++)
                ret += data[i].ToString("x2").ToLower();
            return ret;
        }

        public static void WriteCacheLog(string message)
        {
            try
            {
                    string path = Zamba.Membership.MembershipHelper.AppTempPath + "\\Log\\";

                    if (System.IO.Directory.Exists(path) == false)
                        System.IO.Directory.CreateDirectory(path);

                    path += DateTime.Now.ToString("yyyyMMdd");
                    path += ".txt";

                    var writer = new System.IO.StreamWriter(path, true);
                    writer.Write(message + "\n");
                    writer.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}