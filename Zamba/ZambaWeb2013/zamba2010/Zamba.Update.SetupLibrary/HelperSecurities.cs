 using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;


namespace Zamba.SetupLibrary
{
    /// <summary>
    /// Esta clase posee los elementos 
    /// necesarios para configurar la seguraridad 
    /// de .net sobre el addin y su cosistencia
    /// respecto a las librerias de zamba necesarias.
    /// </summary>
    /// <remarks>
    /// osanchez - 11/05/2009 version incial
    /// </remarks>
    public class HelperSecurity
    {
        #region Constantes
        private const string ERROR_SETSECURITYZAMBA = "La ruta {0} debe ser valida.";
        private const string ERROR_REMOVESECURITYZAMBA = "La ruta {0} debe ser valida.";
        private const string ERROR_GETCASPOLPATH = "El archivo {0} es requerido.";
        private const string ERROR_GETGACUTILPATH = "El archivo {0} es requerido.";
        private const string ERROR_GETEXPORTOUTLOOKPATH = "El archivo {0} es requerido.";
        private const string ERROR_GETOFFICE2003PATH = "La ruta {0} no es valida.";
        private const string ERROR_GETOFFICE2003CLSID = "La aplicacion Outlook no esta instalada";
        private const string ERROR_GETOFFICE2003CURVER = "La aplicacion Outlook no esta instalada";
        private const string ERROR_GETOFFICE2003INCORRECTVER = "Debe tener instalado Outlook 2003";
        private const string ARG_CASPOLZAMBA_SETSECURITYZAMBA = "-m -ag All_Code -url \"{0}\\*\" FullTrust -n \"ExportaOutlook\" -d \"Exporta mails desde outlook a zamba\" -pp on";
        //private const string ARG_CASPOLOFFICE_SETSECURITYZAMBA = "-m -ag All_Code -url \"{0}\\*\" FullTrust -n \"ExportaOutlookOffice\" -d \"Exporta mails desde outlook a zamba\" -pp off";
        private const string ARG_CASPOLZAMBA_REMOVESECURITYZAMBA = "-m -rg \"ExportaOutlook\" -pp on";
        //private const string ARG_CASPOLOFFICE_REMOVESECURITYZAMBA = "-m -rg \"ExportaOutlookOffice\" -pp off";
        private const string ARG_CASPOLADDIN_SETSECURITYADDIN = " -af \"{0}\" -pp on";
        private const string ARG_GACUTILADDIN_SETSECURITYADDIN = " -i \"{0}\" /nologo";
        private const string ARG_CASPOLADDIN_REMOVESECURITYADDIN = " -rf \"{0}\" -pp on";
        private const string ARG_GACUTILADDIN_REMOVESECURITYADDIN = " -u \"{0}\" /nologo";
        private const string REGKEY_FRAMEWORK = @"SOFTWARE\Microsoft\.NETFramework";
        private const string REGKEY_OFFICE2003CLSID = @"Software\Classes\Outlook.Application\CLSID\";
        private const string REGKEY_OFFICE2003CURVER = @"Software\Classes\Outlook.Application\CurVer\";
        private const string REGKEY_OFFICE2003PATH = @"Software\Classes\CLSID\{0}\LocalServer32\";
        private const string REGKEY_VALUE = "InstallRoot";
        private const string PATH_CASPOL_GETCASPOLPATH = @"v2.0.50727\caspol.exe";
        private const string EXE_NAME_GETGACUTILPATH = "gacutil.exe";
        private const string DDL_NAME_EXPORTAOUTLOOK = "ExportaOutlook.dll";
        #endregion
        
        #region atributos
        private string _targetPath  = string.Empty;
        private Dictionary<string, string> dicAssembliesError = null;
        #endregion

        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetPath">ruta de instalación</param>
        public HelperSecurity(string targetPath)
        {   
            _targetPath = targetPath;
            VerificaPrerequisitos();
        }
        #endregion

        #region Metodos Privados
        /// <summary>
        /// Contruye un Objeto SecurityAddin
        /// </summary>
        /// <returns>SecurityAddin</returns>
        /// <remarks>
        /// osanchez - 11/05/2009 version inicial
        /// </remarks>
        private SecurityAddin getSecurityAddin()
        {
            HelperTrace.WriteFormatInitMethod("getSecurityAddin");
            SecurityAddin sec = new SecurityAddin();
            sec.currentTargetPath = GetTargetDir();
            HelperTrace.WriteFormatEndMethod("getSecurityAddin");
            return sec;
        }

        /// <summary>
        /// Configura y llama a la ejecucion de las lineas de comando
        /// que habilitan la seguridad para las dll´s de Zamba
        /// </summary>
        /// <remarks>
        /// osanchez - 12/05/2009 version inicial
        /// </remarks>
        private void SetSecurityZamba(string pathOffice)
        {
            HelperTrace.WriteFormatInitMethod("SetSecurityZamba");
            if (!Directory.Exists(pathOffice))
                throw new Exception(string.Format(ERROR_SETSECURITYZAMBA, pathOffice));

            List<SecurityAddin> lista = new List<SecurityAddin>();

             
            SecurityAddin sec = getSecurityAddin();
            sec.path = GetCaspolPath();
            HelperTrace.WriteAnswerFormat(sec.path);
            HelperTrace.WriteQuestionFormat("BuildArguments");
            sec.arg = String.Format(ARG_CASPOLZAMBA_SETSECURITYZAMBA, GetTargetDir());
            lista.Add(sec);
            HelperTrace.WriteAnswerFormat("add OK");

            sec = getSecurityAddin();
            sec.path = GetCaspolPath();
            HelperTrace.WriteAnswerFormat(sec.path);
            HelperTrace.WriteQuestionFormat("BuildArguments");
            sec.arg = string.Format(ARG_CASPOLZAMBA_SETSECURITYZAMBA, pathOffice);
            lista.Add(sec);
            HelperTrace.WriteAnswerFormat("add OK");

            HelperProcess hp = new HelperProcess();
            hp.ExecuteSecurityAddin(lista);
            HelperTrace.WriteFormatEndMethod("SetSecurityZamba");

        }

        /// <summary>
        /// Configura y llama a la ejecucion de las lineas de comando
        /// que remueven la seguridad para las dll´s de Zamba
        /// </summary>
        /// <remarks>
        /// osanchez - 12/05/2009 version inicial
        /// </remarks>
        private void RemoveSecurityZamba(string pathOffice)
        {
            HelperTrace.WriteFormatInitMethod("RemoveSecurityZamba");
            if (!Directory.Exists(pathOffice))
                throw new Exception(string.Format(ERROR_REMOVESECURITYZAMBA, pathOffice));

            List<SecurityAddin> lista = new List<SecurityAddin>();

            SecurityAddin sec = getSecurityAddin();
            sec.path = GetCaspolPath();
            HelperTrace.WriteAnswerFormat(sec.path);
            HelperTrace.WriteQuestionFormat("BuildArguments");
            sec.arg = ARG_CASPOLZAMBA_REMOVESECURITYZAMBA;
            HelperTrace.WriteAnswerFormat(sec.arg);
            lista.Add(sec);
            HelperTrace.WriteAnswerFormat("Remove OK");

            sec = getSecurityAddin();
            sec.path = GetCaspolPath();
            HelperTrace.WriteAnswerFormat(sec.path);
            HelperTrace.WriteQuestionFormat("BuildArguments");
            sec.arg = ARG_CASPOLZAMBA_REMOVESECURITYZAMBA;
            lista.Add(sec);
            HelperTrace.WriteAnswerFormat("Remove OK");

            HelperProcess hp = new HelperProcess();
            hp.ExecuteSecurityAddin(lista);
            HelperTrace.WriteFormatEndMethod("RemoveSecurityZamba");
        }

        /// <summary>
        /// Obtiene la ruta al ejecutable 
        /// caspol.exe
        /// </summary>
        /// <returns>path</returns>
        /// <remarks>
        /// osanchez - 11/05/2009 version inicial
        /// </remarks>
        private string GetCaspolPath()
        {
            HelperTrace.WriteFormatInitMethod("GetCaspolPath");

            HelperTrace.WriteAnswerFormat("getRegistryKey:" + REGKEY_FRAMEWORK);

            RegistryKey key = Registry.LocalMachine;
            RegistryKey subkey = key.OpenSubKey(REGKEY_FRAMEWORK);

            Object keyValue = subkey.GetValue(REGKEY_VALUE);

            HelperTrace.WriteQuestionFormat("getkeyValue",keyValue.ToString());

            string path = Path.Combine(keyValue.ToString(), PATH_CASPOL_GETCASPOLPATH);

            HelperTrace.WriteQuestionFormat("execFileExists",path);

            if (!File.Exists(path))
            {
                HelperTrace.WriteAnswerFormat("Error:" + string.Format(ERROR_GETCASPOLPATH, path));
                HelperTrace.WriteFormatEndMethod("GetCaspolPath");
                throw new Exception(string.Format(ERROR_GETCASPOLPATH, path));
            }
            HelperTrace.WriteAnswerFormat("OK, " + path + " exists.");
            HelperTrace.WriteFormatEndMethod("GetCaspolPath");
            return path;
        }

        /// <summary>
        /// obtiene la ruta de instalacion del
        /// office2003
        /// </summary>
        /// <returns>ruta de instalacion</returns>
        /// <remarks>
        /// osanchez - 13/05/2009 version inicial
        /// </remarks>
        private string GetOffice2003Path()
        {            
            HelperTrace.WriteFormatInitMethod("GetOffice2003Path");                       

            CheckVersionOffice2003();
            
            string clsid = GetClsidOutlook2003();
            
            RegistryKey key = Registry.LocalMachine;
            HelperTrace.WriteQuestionFormat("getkeyValue path");
            RegistryKey subkey = key.OpenSubKey(string.Format(REGKEY_OFFICE2003PATH, clsid));

            object valueKey = subkey.GetValue(string.Empty).ToString();

            if (valueKey == null)
            {
                HelperTrace.WriteAnswerFormat("Error: " + ERROR_GETOFFICE2003PATH);
                HelperTrace.WriteFormatEndMethod("GetOffice2003Path");
                throw new Exception(ERROR_GETOFFICE2003PATH);
            }

            string path = Path.GetDirectoryName(valueKey.ToString());
            HelperTrace.WriteAnswerFormat(path);
            
            HelperTrace.WriteQuestionFormat("execFolderExists", path);

            if (!Directory.Exists(path))
            {
                HelperTrace.WriteAnswerFormat("Error:" + string.Format(ERROR_GETOFFICE2003PATH, path));
                HelperTrace.WriteFormatEndMethod("GetOffice2003Path");
                throw new Exception(string.Format(ERROR_GETOFFICE2003PATH, path));
            }
            HelperTrace.WriteAnswerFormat("OK, " + path + " exists.");
            HelperTrace.WriteFormatEndMethod("GetOffice2003Path");
            return path;
        }

        /// <summary>
        /// Obtiene el CLSID para la aplicacion
        /// Outlook2003
        /// </summary>
        /// <returns>CSLID</returns>
        private string GetClsidOutlook2003()
        {
            HelperTrace.WriteFormatInitMethod("GetClsidOutlook2003");

            HelperTrace.WriteAnswerFormat("getRegistryKey:" + REGKEY_OFFICE2003CLSID);
            RegistryKey key = Registry.LocalMachine;
            RegistryKey subkey = key.OpenSubKey(REGKEY_OFFICE2003CLSID);

            HelperTrace.WriteQuestionFormat("getkeyValue clsid");
            object clsid = subkey.GetValue(string.Empty).ToString();

            if (clsid == null)
            {
                HelperTrace.WriteAnswerFormat("Error:" + ERROR_GETOFFICE2003CLSID);
                HelperTrace.WriteFormatEndMethod("GetClsidOutlook2003");
                throw new Exception(ERROR_GETOFFICE2003CLSID);
            }
            HelperTrace.WriteAnswerFormat(clsid.ToString());
            HelperTrace.WriteFormatEndMethod("GetClsidOutlook2003");
            return clsid.ToString();
        }

        /// <summary>
        /// Verifica que la version de office que esta instalada.
        /// Si la version no es Office2003(.Application.11) 
        /// lanza una excepcion
        /// </summary>        
        /// <remarks>
        /// osanchez - 14/05/2009 version inicial
        /// </remarks>
        private void CheckVersionOffice2003()
        {
            HelperTrace.WriteFormatInitMethod("CheckVersionOffice2003");
                        
            RegistryKey key = Registry.LocalMachine;
            RegistryKey subkey = key.OpenSubKey(REGKEY_OFFICE2003CURVER);
            object curver = subkey.GetValue(string.Empty).ToString();

            HelperTrace.WriteQuestionFormat("checkVersion");
            if (curver == null)
            {
                HelperTrace.WriteAnswerFormat("Error:" + ERROR_GETOFFICE2003CURVER);
                HelperTrace.WriteFormatEndMethod("CheckVersionOffice2003");
                throw new Exception(ERROR_GETOFFICE2003CURVER);
            }

            if (curver.ToString() != "Outlook.Application.11")
            {
                HelperTrace.WriteAnswerFormat("Error:" + ERROR_GETOFFICE2003INCORRECTVER);
                HelperTrace.WriteFormatEndMethod("CheckVersionOffice2003");
                throw new Exception(ERROR_GETOFFICE2003INCORRECTVER);
            }
            HelperTrace.WriteFormatEndMethod("CheckVersionOffice2003");           
        }

        /// <summary>
        /// Obtiene la ruta al ejecutable 
        /// gacutil.exe
        /// </summary>
        /// <returns>path</returns>
        /// <remarks>
        /// osanchez - 11/05/2009 version inicial
        /// </remarks>
        private string GetGacutilPath()
        {
            HelperTrace.WriteFormatInitMethod("GetGacutilPath");
            string target = GetTargetDir();
            string path = Path.Combine(target, EXE_NAME_GETGACUTILPATH);
            HelperTrace.WriteQuestionFormat("execFileExists", path);
            if (!File.Exists(path))
            {
                HelperTrace.WriteAnswerFormat("Error:" + string.Format(ERROR_GETGACUTILPATH, path));
                HelperTrace.WriteFormatEndMethod("GetGacutilPath");
                throw new Exception(string.Format(ERROR_GETGACUTILPATH, path));
            }
            HelperTrace.WriteAnswerFormat("OK, " + path + " exists.");
            HelperTrace.WriteFormatEndMethod("GetGacutilPath");
            return path;
        }

        /// <summary>
        /// Obtiene la ruta del addin 
        /// ExportaOutlook
        /// </summary>
        /// <returns>path</returns>
        /// <remarks>
        /// osanchez - 12/05/2009 version inicial
        /// </remarks> 
        private string GetExportOutlookPath()
        {
            HelperTrace.WriteFormatInitMethod("GetExportOutlookPath");
            string target = GetTargetDir();
            string path = Path.Combine(target, DDL_NAME_EXPORTAOUTLOOK);
            HelperTrace.WriteQuestionFormat("execFileExists",path);
            if (!File.Exists(path))
            {
                HelperTrace.WriteAnswerFormat("Error:" + string.Format(ERROR_GETEXPORTOUTLOOKPATH, path));
                HelperTrace.WriteFormatEndMethod("GetExportOutlookPath");
                throw new Exception(string.Format(ERROR_GETEXPORTOUTLOOKPATH, path));
            }
            HelperTrace.WriteAnswerFormat("OK, " + path + " exists.");
            HelperTrace.WriteFormatEndMethod("GetExportOutlookPath");
            return path;
        }

        /// <summary>
        /// Recupera la ruta de instalacion 
        /// del addin
        /// </summary>
        /// <returns>path</returns>
        /// <remarks>
        /// osanchez - 11/05/2009 version inicial
        /// </remarks>
        private string GetTargetDir()
        {
            HelperTrace.WriteQuestionFormat("execGetTargetDir",_targetPath);
            return _targetPath;
        }

        /// <summary>
        /// Verifica que existan los archivos 
        /// gacutil.exe y caspol.exe
        /// </summary>
        private void VerificaPrerequisitos()
        {
            HelperTrace.WriteFormatInitMethod("VerificaPrerequisitos");
            GetOffice2003Path();
            GetCaspolPath();
            GetGacutilPath();
            GetExportOutlookPath();            
            HelperTrace.WriteFormatEndMethod("VerificaPrerequisitos");
        }
        #endregion
        
        #region Metodos Publicos

        /// <summary>        
        /// Configura y llama a la ejecucion de las lineas de comando
        /// que habilitan la seguridad para el addin
        /// </summary>
        /// <remarks>
        /// osanchez - 11/05/2009 version inicial
        /// </remarks>
        public void SetSecurityAddin(string ruta)
        {
            HelperTrace.WriteFormatInitMethod("SetSecurityAddin");
            dicAssembliesError = new Dictionary<string, string>();
            string gaspolPath = GetCaspolPath();
            string gacUtilPath = GetGacutilPath();

            HelperTrace.WriteAnswerFormat("Ruta completa: " + ruta);
            HelperTrace.WriteAnswerFormat("Nombre ruta: " + Path.GetDirectoryName(ruta));

            string[] lista = Directory.GetFiles(ruta, "Zamba.*.dll", SearchOption.TopDirectoryOnly);
            if (lista != null)
            {
                HelperTrace.WriteAnswerFormat("Add Assemblies , count=" + lista.Length.ToString());
                foreach (string fitem in lista)
                {
                    HelperTrace.WriteAnswerFormat("- " + fitem);
                }
                
                HelperTrace.WriteAnswerFormat("OK, count=" + lista.Length.ToString());
                foreach (string f in lista)
                {
                    if (!f.Contains("Zamba.SetupLibrary") && !f.Contains("ieframe"))
                    {
                        HelperTrace.WriteAnswerFormat("Registrando DLL " + f);
                        HelperTrace.WriteAnswerFormat("Registrando DLL Nombre: " + Path.GetFileName(f));
                        registerAssembly(f, gaspolPath);
                    }
                    
                }
            }

            registerAssembly(Path.Combine(ruta, "SandBar.dll"), gaspolPath);
            registerAssembly(Path.Combine(ruta,  "ExportaOutlook.dll"), gaspolPath);
            HelperTrace.WriteAnswerFormat("-------------------------------------------");
            HelperTrace.WriteAnswerFormat("Assemblies que no se pudieron Installar: " + dicAssembliesError.Count.ToString());
            foreach (KeyValuePair<string, string> dicItem in dicAssembliesError)
            {
                HelperTrace.WriteAnswerFormat("-------------------------------------------");
                HelperTrace.WriteAnswerFormat("Assembly Name: " + dicItem.Key);
                HelperTrace.WriteAnswerFormat("Exception: " + dicItem.Value);
                HelperTrace.WriteAnswerFormat("-------------------------------------------");
            }


            HelperTrace.WriteAnswerFormat("-------------------------------------------");

            HelperTrace.WriteFormatEndMethod("SetSecurityAddin");
        }

        private void registerAssembly(string assemblyName, string caspolPath)
        {
            try
            {

                List<SecurityAddin> lista = new List<SecurityAddin>();
                SecurityAddin sec = getSecurityAddin();
                sec.path = caspolPath;
                HelperTrace.WriteAnswerFormat(sec.path);
                HelperTrace.WriteQuestionFormat("BuildArguments");
                sec.arg = string.Format(ARG_CASPOLADDIN_SETSECURITYADDIN, assemblyName);
                HelperTrace.WriteAnswerFormat(sec.arg);
                lista.Add(sec);

                HelperProcess hp = new HelperProcess();
                hp.ExecuteSecurityAddin(lista);

                HelperTrace.WriteAnswerFormat("Caspol Add OK");
                HelperTrace.WriteQuestionFormat("Adding Assemblies with GACInstaller");

                DotNetRemoting.GACInstaller _GACInstaller = new DotNetRemoting.GACInstaller();
                HelperTrace.WriteQuestionFormat("Assembly Name: " + assemblyName);
                if (dicAssembliesError == null) dicAssembliesError = new Dictionary<string, string>();

                try
                {
                    if (assemblyName != "Zamba.SetupLibrary")
                    {
                        _GACInstaller.Install( assemblyName );
                        HelperTrace.WriteAnswerFormat("Assembly Installed successfully");
                    }
                    else throw new Exception("No se puede Agregar Zamba.SetupLibrary.dll porque esta siendo usada por el instalador");

                    
                }
                catch (Exception ex)
                {
                    HelperTrace.WriteAnswerFormat("Assembly Install failed");
                    dicAssembliesError.Add(assemblyName, ex.ToString());

                }


                HelperTrace.WriteAnswerFormat("GAC Install Finished");
            }
            catch (Exception ex)
            {
                HelperTrace.WriteAnswerFormat(ex.ToString());
            }

            
            
         }

        private void UnregisterAssembly(string assemblyName, string caspolPath)
        {
            try
            {
                List<SecurityAddin> lista = new List<SecurityAddin>();
                SecurityAddin sec = getSecurityAddin();
                sec.path = caspolPath;
                HelperTrace.WriteAnswerFormat(sec.path);
                HelperTrace.WriteQuestionFormat("BuildArguments");
                sec.arg = string.Format(ARG_CASPOLADDIN_REMOVESECURITYADDIN, Path.Combine(sec.currentTargetPath, assemblyName));
                HelperTrace.WriteAnswerFormat(sec.arg);
                lista.Add(sec);
                HelperProcess hp = new HelperProcess();
                hp.ExecuteSecurityAddin(lista);

                HelperTrace.WriteAnswerFormat("Caspol Remove OK");


                HelperTrace.WriteQuestionFormat("Removing Assembly with GACInstaller");

                DotNetRemoting.GACInstaller _GACInstaller = new DotNetRemoting.GACInstaller();
                string AssembName = Path.GetFileNameWithoutExtension(assemblyName);
                string PublicToken = null;
                HelperTrace.WriteQuestionFormat("Assembly Name: " + AssembName);
                if (dicAssembliesError == null) dicAssembliesError = new Dictionary<string, string>();

                try
                {
                    if (AssembName != "Zamba.SetupLibrary")
                    {
                        _GACInstaller.RemoveAssembly(AssembName, PublicToken);
                        HelperTrace.WriteAnswerFormat("Assembly removed successfully!!");
                    }
                    else throw new Exception("No se puede eliminar Zamba.SetupLibrary.dll porque esta siendo usada por el instalador");

                    
                }
                catch (Exception ex)
                {
                    HelperTrace.WriteAnswerFormat("Assembly remove failed..");
                    dicAssembliesError.Add(assemblyName, ex.ToString());

                }


                
                HelperTrace.WriteAnswerFormat("GAC Remove Finished");
                
            }
            catch (Exception ex)
            {
                HelperTrace.WriteAnswerFormat(ex.ToString());
            }
        }

        /// <summary>        
        /// Configura y llama a la ejecucion de las lineas de comando
        /// que remueven la seguridad para el addin
        /// </summary>
        /// <remarks>
        /// osanchez - 11/05/2009 version inicial
        /// </remarks>
        public void RemoveSecurityAddin(string ruta)
        {
            
            try
                {
                    
                    HelperTrace.WriteFormatInitMethod("RemoveSecurityAddin");
                    dicAssembliesError = new Dictionary<string, string>();
                    string gaspolPath = GetCaspolPath();
                    string gacUtilPath = GetGacutilPath();

                    HelperTrace.WriteAnswerFormat("Ruta completa: " + ruta);
                    HelperTrace.WriteAnswerFormat("Nombre ruta: " + Path.GetDirectoryName(ruta));

                    string[] lista = Directory.GetFiles(ruta, "Zamba.*.dll", SearchOption.TopDirectoryOnly);
                    if (lista != null)
                    {
                        HelperTrace.WriteAnswerFormat("Remove Assemblies , count=" + lista.Length.ToString());
                        foreach (string  fitem in lista)
                        {
                            HelperTrace.WriteAnswerFormat("- " + fitem );
                        }

                        HelperTrace.WriteAnswerFormat("----------------------------------------------------------" );
                        
                        foreach (string f in lista)
                        {
                            HelperTrace.WriteAnswerFormat("Desregistrando DLL " + f);
                            HelperTrace.WriteAnswerFormat("Desregistrando DLL Nombre: " + Path.GetFileName(f));
                            UnregisterAssembly(Path.GetFileName(f), gaspolPath);
                        }
                    }

                    UnregisterAssembly("ExportaOutlook.dll", gaspolPath);
                    UnregisterAssembly("SandBar.dll", gaspolPath);
                    HelperTrace.WriteAnswerFormat("-------------------------------------------");
                    HelperTrace.WriteAnswerFormat("Assemblies que no se pudieron DesInstallar: "+ dicAssembliesError.Count.ToString()  );
                    foreach (KeyValuePair<string,string> dicItem in dicAssembliesError )
                    {
                        HelperTrace.WriteAnswerFormat("-------------------------------------------");
                        HelperTrace.WriteAnswerFormat("Assembly Name: "+ dicItem.Key);
                        HelperTrace.WriteAnswerFormat("Exception: "+ dicItem.Value);
                        HelperTrace.WriteAnswerFormat("-------------------------------------------");
                    }
                        
                    
                        HelperTrace.WriteAnswerFormat("-------------------------------------------");
                   

                    HelperTrace.WriteFormatEndMethod("RemoveSecurityAddin");
                }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }        

        /// <summary>
        /// Wrapper. Llama al metodo UpdateDependenciesZamba  de
        /// HelperProcess.
        /// </summary>        
        /// <remarks>
        /// osanchez - 12/05/2009 version inicial
        /// </remarks>
        public void UpdateDependenciesZamba()
        {
            HelperTrace.WriteFormatInitMethod("UpdateDependenciesZamba");
            string target;
            target = GetOffice2003Path();            
            //Remueve la seguridad .net
            RemoveSecurityZamba(target);
            //Copia las dependencias
            HelperProcess hp = new HelperProcess();
            hp.UpdateDependenciesZamba(GetTargetDir(), target);
            //Configura la seguridad .net
            SetSecurityZamba(target);
            HelperTrace.WriteFormatEndMethod("UpdateDependenciesZamba");
        }

        /// <summary>
        /// Elimina la configuracion de aplicada a zamba y a office      
        /// </summary>        
        /// <remarks>
        /// osanchez - 13/05/2009 version inicial        
        /// </remarks>
        public void UpdateDependenciesZambaRemove()
        {
            HelperTrace.WriteFormatInitMethod("UpdateDependenciesZambaRemove");
            string target;
            target = GetOffice2003Path();
            //Remueve la seguridad .net
            RemoveSecurityZamba(target);
            HelperTrace.WriteFormatEndMethod("UpdateDependenciesZambaRemove");
        }
        #endregion
    }
}
