using System;
using System.Collections;
using System.Text;
using System.Threading;

namespace Zamba
{
    namespace Thumbnails
    {
        public delegate void EventoTarea(Tarea tarea, ArrayList parametros);

        /// <summary>
        /// Es Una abstraccion de un hilo
        /// al que se le pueden paras parametros
        /// </summary>
        public class Tarea
        {
            private ArrayList parametros;
            private EventoTarea eTarea;
            private Thread hilo;

            public Thread Hilo
            {
                get
                {
                    return this.hilo;
                }
            }


            public Tarea(ArrayList parametros,
                          EventoTarea evento)
            {
                this.parametros = parametros;
                this.eTarea = evento;
            }

            public void start()
            {
                this.hilo = new Thread(new ThreadStart(this.proceso));
                hilo.Start();
            }

            public void Join(int milisengundos)
            {
                this.hilo.Join(milisengundos);
            }
            public void Interrupt()
            {
                this.hilo.Interrupt();
            }

            public void abort()
            {
                this.hilo.Abort();
            }
            /// <summary>
            /// Proceso principal del hilo
            /// </summary>
            private void proceso()
            {
                try
                {
                    this.eTarea(this, this.parametros);
                }
                catch (Exception ex)
                {
                    Zamba.Core.ZClass.raiseerror(ex);
                }
            }
        }
    }
}
