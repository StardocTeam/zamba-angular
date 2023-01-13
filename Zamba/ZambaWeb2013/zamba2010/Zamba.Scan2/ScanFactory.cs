using System;
using zamba.scan.twain;

namespace zamba.scan
{
    /// <summary>
    /// Devuelve un objeto scan
    /// </summary>
    internal static class ScanFactory
    {
        /// <summary>
        /// Devuelve un objeto scan 
        /// de la implementacion
        /// que se le pida
        /// </summary>    
        /// <param name="name">
        ///    tipo de Implementación 
        /// </param>
        public static IScan getProduct( EnumScanDriver driver )
        {
           switch(driver) {
            case EnumScanDriver.Twain:
                return new TWain();               
           } 
           return null;          
        }                                 
    }   
}



