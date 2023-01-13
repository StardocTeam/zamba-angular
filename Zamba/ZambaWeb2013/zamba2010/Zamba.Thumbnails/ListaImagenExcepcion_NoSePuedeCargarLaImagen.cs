using System;
using System.Collections.Generic;
using System.Text;

namespace Zamba
{
    namespace Thumbnails
    {
        /// <summary>
        /// Exepcion lanzada al cargar una imagen en ListaImagen
        /// </summary>
        class ListaImagenExcepcion_NoSePuedeCargarLaImagen : Exception
        {
            string path;
            public ListaImagenExcepcion_NoSePuedeCargarLaImagen(string path)
            {
                this.path = path;
            }

            public override string Message
            {
                get
                {
                    return Constante.ListaImagen.MESSAGE_NO_SE_PUEDE_CARGAR_LA_IMAGEN
                           + this.path;
                }
            }
        }
    }
}
