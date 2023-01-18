using System;
using System.Collections;
using System.Text;
using System.Threading;


namespace Zamba
{
    namespace Thumbnails
    {
        /// <summary>
        /// Lista de tareas
        /// </summary>
        /// <remarks>ver tarea</remarks>
        public class ListaTarea : TemplateArrayList
        {
            private int maxCount;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="maxCount">
            ///     maximo numero de Tareas
            /// </param>
            public ListaTarea(int maxCount)
            {
                this.maxCount = maxCount;
            }


            private void eventoAbort(object tarea)
            {
                ((Tarea)tarea).abort();
            }

            public override int Add(object value)
            {
                if (this.Count < this.maxCount)
                    return base.Add(value);

                return -1;
            }

            /// <summary>
            /// Pregunta si esta llena la lista
            /// </summary>
            /// <returns>booleano</returns>
            public bool isFill()
            {
                if (this.Count == this.maxCount)
                    return true;

                return false;
            }

            /// <summary>
            /// Aborta todos los thread de la lista
            /// </summary>
            public virtual void abortAll()
            {
                this.aplicarATodos(new eventoAplicar(this.eventoAbort));
            }
        }
    }
}
