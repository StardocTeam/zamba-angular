using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;


namespace Zamba.SetupLibrary
{
    [RunInstaller(true)]
    public partial class ZambaAddinInstaller : Installer
    {
        private HelperSecurity _hs;

        public HelperSecurity hs
        {
            get {
                    if (_hs == null)
                    {
                        HelperTrace.WriteQuestionFormat("getEnviromentVariables");
                        string ruta = Context.Parameters["assemblypath"].ToString();
                        HelperTrace.WriteQuestionFormat("assemblypath",ruta);
                        HelperTrace.WriteQuestionFormat("execInstanceHelperSecurity");
                        _hs = new HelperSecurity(Path.GetDirectoryName(ruta));
                    }
                    return _hs; 
                }            
        }

        public ZambaAddinInstaller()
        {             
            InitializeComponent();                       
        }      

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);                        
        }

        public override void Commit(IDictionary savedState)
        {
            //Debugger.Break();
            base.Commit(savedState);
            HelperTrace.WriteFormatInitBlock("execCommit");
            hs.SetSecurityAddin();
            hs.UpdateDependenciesZamba();
            HelperTrace.WriteFormatEndBlock("execCommit");
        }

        public override void Uninstall(IDictionary savedState)
        {
            //Debugger.Break();
            base.Uninstall(savedState);
            HelperTrace.WriteFormatInitBlock("execCommit");
            hs.RemoveSecurityAddin();
            hs.UpdateDependenciesZambaRemove();
            HelperTrace.WriteFormatEndBlock("execCommit");
        }

        public override void Install(IDictionary stateSaver)
        {            
            base.Install(stateSaver);                                 
        }       
    }
}

