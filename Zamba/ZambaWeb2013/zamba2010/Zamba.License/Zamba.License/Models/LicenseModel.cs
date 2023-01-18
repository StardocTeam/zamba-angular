using System.ComponentModel.DataAnnotations;
using Zamba.LicenseCore;

namespace Zamba.License.Models
{
    public class LicenseModel : ILicense
    {
        [Display(Name = "Licencias")]
        public string Name { get; set; }

        [Display(Name = "Configuradas")]
        public int Configured { get; set; }

        [Display(Name = "Utilizadas")]
        public int Used { get; set; }

        [Display(Name = "Disponibles")]
        public int Available
        {
            get
            {
                return Configured - Used;
            }
        }

        public LicenseModel(ILicense l)
        {
            Name = l.Name;
            Configured = l.Configured;
            Used = l.Used;
        }
    }
}