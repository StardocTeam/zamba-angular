using System;
using System.Windows.Forms;
using Zamba.Thumbnails;

namespace Zamba
{
    namespace Thumbnails
    {
        public class SelectedItemIndexArgs : EventArgs
        {
            private ListaImagen vista;

            public SelectedItemIndexArgs(
                ListaImagen vista)
            {
                this.vista = vista;
            }

            public ListaImagen ListaImagen
            {
                get
                {
                    return this.vista;
                }
            }
        }


        public delegate void
        EventHandlerSelectedItemIndex(object sender,
                   SelectedItemIndexArgs e);

        public delegate void EventHandlerDoubleClick
                   ( object sender, EventArgs e );
    }
}