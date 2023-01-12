using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zamba.LicenseCore
{
    /// <summary>
    /// Representa una conexión realizada a Zamba
    /// </summary>
    public class Connection: IConnection
    {
        /// <summary>
        /// Nombre de usuario de Zamba. El que se utiliza para iniciar sesión.
        /// </summary>
        public string ZambaUser { get; set; }

        /// <summary>
        /// Nombre de usuario de Windows del usuario conectado
        /// </summary>
        public string WindowsUser { get; set; }

        /// <summary>
        /// Puesto del usuario
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Nombre de la licencia utilizada
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// Tiempo de espera de la conexión
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        /// Momento en que esa conexión fué creada
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Momento en que esa conexión fue actualizada
        /// </summary>
        public DateTime Updated { get; set; }
    }
}
