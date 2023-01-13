using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvocacionServWDigDepFiel.wDigDepFiel;

namespace InvocacionServWDigDepFiel
{
    public class SolicitudFirmaDigital
    {
        public Int64 userId { get; set; }
        public string nroLegajo { get; set; }
        public string cuitDeclarante { get; set; }
        public string cuitPSAD { get; set; }
        public string cuitIE { get; set; }
        public string cuitATA { get; set; }
        public string codigo { get; set; }
        public string url { get; set; }
        public Familia[] familias { get; set; }
        public string ticket { get; set; }
        public int cantidadTotal { get; set; }
        public string sigea { get; set; }
        public string nroReferencia { get; set; }

        public string nroGuia { get; set; }
        public string nroDespacho { get; set; }
        
        public int cantidadFojas { get; set; }
        public DateTime fechaDespacho { get; set; }
        public DateTime fechaGeneracion { get; set; }
        public DateTime fechaHoraAcept { get; set; }
        public string idEnvio { get; set; }
        public string indLugarFisico { get; set; }
        public string hashing { get; set; }

    }

}
