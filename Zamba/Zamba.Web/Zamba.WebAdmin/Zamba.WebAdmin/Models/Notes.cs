using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zamba.WebAdmin.Models
{
    public class USRNOTES
    {


        public int id { get; set; }
        public string nombre { get; set; }
        public string conf_mailserver { get; set; } // =   "SRVLOTUS/SEGUROS"
        public string conf_basemail { get; set; } // =   "mail\\nferrara.nsf"  
        public string conf_patharch { get; set; } // =   "\\\\srvflow2\\ZambaVolumenes\\Produccion\\Mails\\Exporta\\ferraranic\\"  
        public string conf_vistaexportacion { get; set; } // =   "ExportoNotes" 
        public string Conf_Papelera { get; set; } // =   "(TrashExporto)"  
        public string Conf_Nomarchtxt { get; set; } // =   "Maestro2.txt" 
        public int Conf_seqatt { get; set; } // =   0  
        public int conf_lockeo { get; set; } // =   0  
        public int conf_acumimg { get; set; } // =   0  
        public int conf_limimg { get; set; } // =   20971520  
        public int conf_destext { get; set; } // =   10240  
        public string conf_textosubject { get; set; } // =   "EXPORTADO A ZAMBA"  
        public string Conf_Borrar { get; set; } // =   "SI"  
        public string conf_archctrl { get; set; } // =   "\\\\srvflow2\\ZambaVolumenes\\Produccion\\Mails\\Exporta\\ferraranic\\NotesCtrl.txt"  
        public int conf_schedulesel { get; set; } // =   1  
        public int conf_schedulevar { get; set; } // =   0  
        public string conf_ejecutable { get; set; } // =   "C:\\Archivos de programa\\Stardoc Argentina\\Zamba Software\\Zamba.LocalImport.exe"  
        public string conf_nomusernotes { get; set; } // =   "Nicolas Ferrara"  
        public string conf_nomuserred { get; set; } // =   "ferraranic"  
        public int conf_charsreempsubj { get; set; } // =   34  
        public int conf_reintento { get; set; } // =   100  
        public int activo { get; set; } // =   1  
        public int conf_seqimg { get; set; } // =   10 
        public Boolean conf_bodyandattachsinexportedmails { get; set; } // =   "true"




    }


    public class USERADMIN
    {


        public int id { get; set; }
        public int idadmin { get; set; }
        public string nombre { get; set; }

    }

    public class USERPARAM
    {
        public int id { get; set; }
        public int idparam { get; set; }
        public string nombre { get; set; }

    }
    
}