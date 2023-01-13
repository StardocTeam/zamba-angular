using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Zamba
{
    namespace Thumbnails
    {
        public static class FabricaLinkLabel
        {
            private static int currentTop;

            public static LinkLabel newLinkLabel(
                string text,
                LinkLabel anteriorLinkLabel,
                int beginArea,
                int endArea,
                LinkLabelLinkClickedEventHandler clickedEvent)
            {
                LinkLabel link = new LinkLabel();
                link.Location = getPosNextItem(
                                    anteriorLinkLabel,
                                    beginArea,
                                    endArea
                                );

                link.Text = text;
                link.Name = text;
                link.AutoSize = true;

                link.LinkClicked += clickedEvent;
                return link;
            }



            /// <summary>
            /// Devuelve la posicion siguiente al linkLabel
            /// Pasado com argumento 
            /// </summary>
            /// <param name="anteriorLinkLabel">LinkLabel</param>
            /// <param name="beginArea">Comienzo area</param>
            /// <param name="endArea">Fin area</param>
            /// <returns></returns>
            private static Point getPosNextItem(
                LinkLabel anteriorLinkLabel,
                int beginArea,
                int endArea)
            {
                if (null == anteriorLinkLabel)
                {
                    currentTop =
                        Constante.BarraNavegacionPagina.SEPARACION;

                    return new Point(
                                beginArea +
                                Constante.BarraNavegacionPagina.SEPARACION,
                                currentTop
                            );
                }
                else if (anteriorLinkLabel.Right > endArea - 20)
                {
                    currentTop =
                        anteriorLinkLabel.Bottom +
                        Constante.BarraNavegacionPagina.SEPARACION;

                    return new Point(
                        beginArea +
                        Constante.BarraNavegacionPagina.SEPARACION,
                        anteriorLinkLabel.Bottom +
                        Constante.BarraNavegacionPagina.SEPARACION
                    );
                }
                else
                    return new Point(
                        anteriorLinkLabel.Right +
                        Constante.BarraNavegacionPagina.SEPARACION,
                        currentTop
                    );
            }
        }
    }
}