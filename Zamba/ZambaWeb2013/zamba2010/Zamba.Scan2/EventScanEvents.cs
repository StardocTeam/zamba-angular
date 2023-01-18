using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace zamba.scan
{

    /// <summary>
    /// Delegado llamado al finalizar la 
    /// digitalizacion de una imagen
    /// </summary>
    /// <param name="e"></param>
    public delegate void EndScanEventHandler( 
                          object sender, 
                          EndScanEventArgs e
                       );
}
