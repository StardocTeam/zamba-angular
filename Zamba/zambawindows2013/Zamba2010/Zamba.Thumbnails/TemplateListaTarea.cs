using System;
using System.Collections;
using System.Text;
using System.Threading;

namespace Zamba
{
    namespace Thumbnails
    {
        /// <summary>
        /// Template para aplicar una accion
        /// a todos los objetos de la lista
        /// </summary>
        public abstract class TemplateArrayList : ArrayList
        {
            protected delegate void eventoAplicar(object hilo);

            /// <summary>
            /// Aplica una accion a la coleccion
            /// </summary>
            protected void aplicarATodos(eventoAplicar e)
            {
                IEnumerator iterador = this.GetEnumerator();
                iterador.Reset();
                while (iterador.MoveNext())
                    e(iterador.Current);
            }
        }
    }
}
