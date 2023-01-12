
namespace Zamba.LicenseCore
{
    /// <summary>
    /// Representa la información de una licencia contratada por un cliente
    /// </summary>
    public class License: ILicense
    {
        /// <summary>
        /// Nombre del tipo de licencia
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Licencias configuradas. Puede que sean mayores a las contratadas por el cliente.
        /// </summary>
        public int Configured { get; set; }

        /// <summary>
        /// Licencias activas en UCM.
        /// </summary>
        public int Used { get; set; }

        /// <summary>
        /// Licencias disponibles.
        /// </summary>
        public int Available 
        { 
            get
            {
                return Configured - Used;
            }
        }

        /// <summary>
        /// Genera un objeto de un tipo de licencia sin información
        /// </summary>
        public License()
        { }

        /// <summary>
        /// Genera un objeto con la información completa de un tipo de licencia
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configured"></param>
        /// <param name="used"></param>
        public License(string name, int configured, int used)
        {
            Name = name;
            Configured = configured;
            Used = used;
        }
    }
}
