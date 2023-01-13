/// Amplefile 
/// http://dotnetremoting.com
/// 
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace DotNetRemoting
{
    public class GACInstaller
    {
        public void Install(string AssembPath)
        {
            AssemblyCache.InstallAssembly(AssembPath, null, AssemblyCommitFlags.Force);
        }

        public void RemoveAssembly(string AssemblyName)
        {
            RemoveAssembly(AssemblyName, null);
        }

        public void RemoveAssembly(string ShortAssemblyName, string PublicToken)
        {
            AssemblyCacheEnum AssembCache = new AssemblyCacheEnum(null);

            string FullAssembName = null;

            for (; ; )
            {
                string AssembNameLoc = AssembCache.GetNextAssembly();
                if (AssembNameLoc == null)
                    break;

                string Pt;
                string ShortName = GetAssemblyShortName(AssembNameLoc, out Pt);

                if (ShortAssemblyName == ShortName)
                {
                    if (PublicToken != null)
                    {
                        PublicToken = PublicToken.Trim().ToLower();
                        if (Pt == null)
                        {
                            FullAssembName = AssembNameLoc;
                            break;
                        }

                        Pt = Pt.ToLower().Trim();

                        if (PublicToken == Pt)
                        {
                            FullAssembName = AssembNameLoc;
                            break;
                        }
                    }
                    else
                    {
                        FullAssembName = AssembNameLoc;
                        break;
                    }
                }
            }

            string Stoken = "null";
            if (PublicToken != null)
            {
                Stoken = PublicToken;
            }

            if (FullAssembName == null)
                throw new Exception("Assembly=" + ShortAssemblyName + ",PublicToken=" + Stoken + " not found in GAC");

            AssemblyCacheUninstallDisposition UninstDisp;
            ClearRegKey(ShortAssemblyName, Registry.CurrentUser);
            ClearRegKey(ShortAssemblyName, Registry.LocalMachine);

            AssemblyCache.UninstallAssembly(FullAssembName, null, out UninstDisp);
        }

        private string GetAssemblyShortName(string FullName, out string PublicToken)
        {
            PublicToken = null;
            if (FullName == null)
                return null;
            string[] Strings = FullName.Split(',');
            foreach (string ss in Strings)
            {
                int index = ss.IndexOf("PublicKeyToken");
                if (index != -1)
                {
                    index = ss.IndexOf("=");
                    if (index != -1)
                    {
                        PublicToken = ss.Substring(index + 1);
                        PublicToken = PublicToken.Trim();
                        break;
                    }
                }
            }

            string Sout = Strings[0];
            return Sout;
        }

        private void ClearRegKey(string AssemblyShortName, RegistryKey BaseKey)
        {
            RegistryKey key = BaseKey.OpenSubKey(@"Software\Microsoft\Installer\Assemblies\Global", true);

            if (key != null)
            {
                string[] names = key.GetValueNames();

                foreach (string Name in names)
                {
                    string[] Words = Name.Split(',');

                    string nn = Words[0];
                    string nn2 = Words[4];

                    if (AssemblyShortName == nn)
                    {
                        key.SetValue(Name, "", RegistryValueKind.String);
                        key.Close();
                        return;
                    }
                }
            }
        }
    }
}
