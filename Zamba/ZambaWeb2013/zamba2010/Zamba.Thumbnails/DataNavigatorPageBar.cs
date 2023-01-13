using System;
using System.Collections;
using System.Text;
using zamba.collections;
using Zamba.Grid;
using Zamba.Grid.PageGroupGrid;

namespace Zamba.Thumbnails
{
    public class DataNavigatorPageBar : BarraNavegacionPagina
    {

        /// <summary>
        /// Se invoca al navegar por las pagians
        /// </summary>
        /// <param name="lista">
        /// items de pagina ingresada
        /// por el usuario
        /// </param>
        protected override void invocarEventoClick(ArrayList lista)
        {
            if (null == this.dClickEvent) return;

            this.dClickEvent(
                this,
                new DataNavigatorPageBarClickEventArgs(
                        lista,
                        (TemplateDataPageGroupList)this.listaPaginada        
                )
            );
        }

    }
}
