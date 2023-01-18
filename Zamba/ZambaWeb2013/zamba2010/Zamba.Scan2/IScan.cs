using System;
using System.Drawing;
using System.Collections.Generic;


namespace zamba.scan
{

    /// <summary>
    /// interface para escaneo de imagenes
    /// </summary>
    public interface IScan{
    
        
        void initialize(IntPtr hwndp);  
      
        /// <summary>
        /// Realiza el eScaneo
        /// </summary>
        /// <param name="hwndp">
        ///    Handler de la ventana principal
        /// </param>
        /// <param name="showGUI">
        ///    muetra el gui de escaneo de twain
        /// </param>
        /// <param name="guiModal">VEntana modal o no</param>
        bool scan(bool showGUI, bool guiModal);
       
        /// <summary>
        /// Pide al usuario la
        /// el dispositivo a 
        /// usar
        /// </summary>  
        void select();
            
        /// <summary>
        /// Libera el dispositivo 
        /// seleccionado
        /// </summary>
        void free();   
       
        /// <summary>
        /// Evento desencadenado al finalizar
        /// la digitalizacion
        /// </summary> 
        event EndScanEventHandler OnEndScan;      
    }

}