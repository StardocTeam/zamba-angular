using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Zamba.Grid.PageGroupGrid
    {

        public partial class BarraNavegacionPagina : UserControl
        {
            private const int SEPARACION = 1;
            private const int DEFAULT_VISIBLE_PAGE_COUNT = 10;
            private const int MAX_PAGE_GROUP_WIDTH = 800;
            private const int LINE_HEIGHT = 24;

            private static Color SelectedPageColor
            {
                get
                {
                    return Color.Yellow;
                }
            }            


            #region Atributos
            // Numero de paginas total esixtente el ListaPaginada... 
            protected int pageCount;

            // numero de paginas visible...
            protected int visiblePageCount;

            /* Lista de LinkLabel correspondientes al grupo
             * de paginas visibles en el control... */

            protected List<LinkLabel> listPage;

            // delegado de evento servido por este control...
            protected BarraPaginaClickEventHandler dClickEvent;

            /* referencia a la lista pagianda
             * con la que trabada este control... */
            protected zamba.collections.IPageList listaPaginada;


            /* paginas actualmente con linc visibles en 
             * la barra...                             */
            protected int primeraPaginaGrupo;
            protected int UltimaPaginaGrupo;

            #endregion End Atributos



            #region Propiedades


            /// <summary>
            /// Devuelve el numero de paginas
            /// existentes en la lista paginada
            /// prasada en el constructor
            /// </summary>
            /// <remarks>
            ///     La asignacion es privada
            /// </remarks>
            public int PageCount
            {
                get { return this.pageCount; }
                private set
                {
                    if (value < 1) return;
                    this.pageCount = value;
                }
            }


            /// <summary>
            /// Asigna o devuelde el numero 
            /// de items por grupo que se muestra
            /// ene l control
            /// </summary>
            public int VisiblePageCount
            {
                get
                {
                    return this.visiblePageCount;
                }
                set
                {
                    this.visiblePageCount = value;
                }
            }


            /// <summary>
            /// Setea el color de la 
            /// fuente de todo el control
            /// </summary>
            public override Color ForeColor {
                get {
                    return base.ForeColor;
                }
                set {
                    base.ForeColor      = value;
                    this.home.ForeColor = value;
                    this.home.LinkColor = value;
                    this.back.ForeColor = value;
                    this.back.LinkColor = value;
                    this.next.ForeColor = value;
                    this.next.LinkColor = value;
                    this.end.ForeColor  = value;
                    this.end.LinkColor  = value;
                }
            }



            #endregion End Propiedades



            #region Constructor
            public BarraNavegacionPagina()
            {
                InitializeComponent();
                this.home.Top = SEPARACION;
                this.back.Top = SEPARACION;
                this.next.Top = SEPARACION;
                this.end.Top = SEPARACION;

                this.PageCount = 0;
                this.visiblePageCount =
                    DEFAULT_VISIBLE_PAGE_COUNT;
                this.Visible = false;
            }
            #endregion End Constructor



            #region Metodos Publicos
            /// <summary>
            /// Asigna una lista paguinada para
            /// que la maneje BarraNavegacionPagina
            /// </summary>
            public zamba.collections.IPageList DataSource
            {
                set {
                    if (null != value) {
                        this.listaPaginada = value;
                        this.primeraPaginaGrupo = 0;
                        this.Width = this.MinimumSize.Width;
                        this.selectHome();
                    }
                }
                get
                {
                    return this.listaPaginada;
                }
            }


            /// <summary>
            /// Carga la primera pagina del control
            /// </summary>
            /// <returns>
            /// Devuelve los items de la pagina
            /// </returns> 
            public ArrayList homePage()
            {
                this.primeraPaginaGrupo = 0;
                this.UltimaPaginaGrupo = this.visiblePageCount-1;
                this.loadNextGroupPages();
                this.SelectPage(this.listaPaginada.CurrentPageIndex);             
                return this.listaPaginada.home();
            }


            public void clear()
            {
                if (null != this.listaPaginada)
                {
                    this.listaPaginada.clear();
                    this.disposePages();
                    if( null != this.listPage )
                        this.listPage.Clear();
                    this.Width = this.MinimumSize.Width;
                }
            }

            #endregion End Metodos Publicos



            #region Metodos protegidos

            /// <summary>
            /// Carga los items del grupo especificado
            /// por parametros
            /// </summary>
            /// <remarks>
            ///     Se especifica el numero de
            ///     la pagina inicial y final
            ///     del grupo a tomar,
            ///     ej.: desde 1 a 10...
            ///                20 a 30..etc.
            /// </remarks>
            /// <param name="firtPage">Pagina inicial</param>
            /// <param name="lastPage">Pagina Final</param>
            protected virtual void ShowPageGroup(int firtPage,
                                                    int lastPage)
            {
                if (0 > firtPage ||
                    0 >= lastPage ||
                    firtPage > lastPage)
                {
                    this.Width =
                        this.back.Right +
                        (this.end.Right - this.next.Left);
                    return;
                }

                this.Visible = true;
                this.setPages(firtPage, lastPage);
            }

            /// <summary>
            /// muestra al usuario
            /// los numeros de paginas 
            /// restantes
            /// </summary>
            /// <remarks>
            ///     los parametros exrpesan 
            ///     mostra desde la pagina firtPage
            ///     hasta la pag. lastPage.
            /// </remarks>
            /// <param name="firtPage">primera pag del grupo(num.)</param>
            /// <param name="lastPage">ultima pag del grupo(num.)</param>
            protected virtual void setPages(int firtPage,
                                               int lastPage)
            {
                /* 
                 * Si ya existia un grupo de links por 
                 * la carga de un grupo anterior...
                 */
                if (null != this.listPage) {
                    // Se liberan los recursos de ese grupo...
                    this.disposePages();
                    //this.listPage.Clear();
                    // Se establese el lalgo de vista de paginador al default...
                    this.Width = this.MinimumSize.Width;
                }
                else
                    // Si se crea un group por primera vez...
                    this.listPage = new List<LinkLabel>();

                // Se crean los links y se agregan a su lista...
                LinkLabel linkPagina = null;

                // Se esconde vlista paginacion...
                this.Visible = false;
                this.AutoSize = false;                
                int i = 0 ;
                for ( i = firtPage; i <= lastPage; i++)
                {
                    // Se crea el link....
                    linkPagina =
                        FabricaLinkLabel.newLinkLabel(
                          i.ToString(),
                          linkPagina,
                          this.back.Right,
                          MAX_PAGE_GROUP_WIDTH,
                          new LinkLabelLinkClickedEventHandler(
                              this.page_LinkClicked)
                        );



                    this.listPage.Add(linkPagina);
                    this.Controls.Add(linkPagina);

                    // Estilo del link...
                    linkPagina.ForeColor = this.back.ForeColor;
                    linkPagina.Font = (Font)this.back.Font.Clone();

                    // Setea el nuevo with de la barra de navegacion...
                    int posRelInicio =
                        linkPagina.Right - this.back.Right;

                    /*this.Height = 
                        linkPagina.Bottom + 
                        Constante.BarraNavegacionPagina.SEPARACION;*/

                    /*
                     * Se suma el ancho de un link + separacion
                     * al control de paginacion...
                     */
                    if (
                        (posRelInicio <=
                            MAX_PAGE_GROUP_WIDTH) &&
                         this.Height <=
                         LINE_HEIGHT
                        )
                        this.Width +=
                            linkPagina.Width + 
                            SEPARACION;
                }

                // ????....
                if (listPage.Count > 1)
                   this.Width += listPage[0].Width + i * 13;

                // Se habilita vista paginación...
                //this.AutoSize = true;
                this.Visible = true;
            }




            /// <summary>
            /// Muestra el primer grupo de 
            /// paginas en la barra
            /// </summary>
            /// <returns>SE refresco?</returns>
            protected virtual bool LoadHomeGroupPages()
            {
                if (0 == this.primeraPaginaGrupo) return false;

                this.primeraPaginaGrupo = 0;
                this.UltimaPaginaGrupo = this.visiblePageCount;
                this.loadNextGroupPages();
                return true;
            }



            /// <summary>
            /// Muestra el Ultimo grupo de 
            /// paginas en la barra
            /// </summary>
            /// <returns>SE refresco?</returns>
            protected virtual bool LoadEndGroupPages()
            {
                if (this.UltimaPaginaGrupo >=
                    this.listaPaginada.PageCount)
                    return false;

                this.primeraPaginaGrupo =
                    this.listaPaginada.PageCount -
                    this.visiblePageCount;
                this.primeraPaginaGrupo++;

                if (0 > this.primeraPaginaGrupo)
                    this.primeraPaginaGrupo = 1;

                this.UltimaPaginaGrupo =
                    this.listaPaginada.PageCount;

                this.ShowPageGroup(
                        this.primeraPaginaGrupo,
                        this.UltimaPaginaGrupo);

                return true;
            }



            /// <summary>
            /// Muestra el Anterior grupo de paginas
            /// </summary>
            /// <remarks>
            /// Decrementa todo el grupo
            /// de numeros de pagina para ver una
            /// pagina anterior 
            /// </remarks>
            protected virtual bool loadBackGroupPages()
            {
                if (1 < this.primeraPaginaGrupo)
                {
                    this.primeraPaginaGrupo--;
                    this.UltimaPaginaGrupo--;

                    this.ShowPageGroup(
                         this.primeraPaginaGrupo,
                         this.UltimaPaginaGrupo);
                    return true;
                }
                return false;
            }



            protected void disableNavigator() {
                this.home.Enabled = false;
                this.back.Enabled = false;
                this.end.Enabled = false;
                this.next.Enabled = false;
            }

            protected void enableNavigator()
            {
                this.home.Enabled = true;
                this.back.Enabled = true;
                this.end.Enabled = true;
                this.next.Enabled = true;
            }



            /// <summary>
            /// Muestra el siguiente grupo de paginas
            /// </summary>
            /// <remarks>
            /// Incrementa todo el grupo
            /// de numeros de pagina para ver una
            /// pagina anterior 
            /// </remarks>
            protected virtual bool loadNextGroupPages()
            {
                /* 
                 * Si se quiere mostrar más 
                 * paginas de las existente,
                 * se deshabilitan las flechas de 
                 * navegacion para gropus de pagian 
                 * 
                 */
                if (this.UltimaPaginaGrupo
                        >= this.listaPaginada.PageCount)
                    this.disableNavigator();
                else
                    this.enableNavigator();

                // Calculo la primera pagina del grupo...
                this.primeraPaginaGrupo++;

                // Calculo la ultima pagina del grupo...
                if (this.listaPaginada.PageCount >
                    this.visiblePageCount)
                {
                    this.UltimaPaginaGrupo
                        = this.primeraPaginaGrupo +
                          this.VisiblePageCount - 1;
                }
                else
                    this.UltimaPaginaGrupo =
                        this.listaPaginada.PageCount;

                /* 
                 * Se generan links para un rango de 
                 * paginas explicitado como:
                 * desde la pagina "n"  a la "n + m"
                 * siendo m el numero de links por 
                 * grupo visibles para navegar...
                 */
                this.ShowPageGroup(
                        this.primeraPaginaGrupo,
                        this.UltimaPaginaGrupo);

                return true;
            }


            /// <summary>
            /// Selecciona una pagina de la barra
            /// segun si indice
            /// </summary>
            /// <param name="index">indice</param>
            protected virtual void SelectPage(int index)
            {
                if (null != this.listPage)
                {
                    IEnumerator iterador = this.listPage.GetEnumerator();
                    iterador.Reset();
                    while (iterador.MoveNext())
                    {
                        LinkLabel item = (LinkLabel)iterador.Current;
                        if (index == Int32.Parse(item.Text)) {
                            item.Font =
                              new Font(
                                item.Font.FontFamily,
                                item.Font.Size, FontStyle.Bold
                              );
                            item.LinkColor =
                                        SelectedPageColor;
                        }
                        else {
                            /*item.Font =
                                new Font(
                                    item.Font.FontFamily,
                                    item.Font.Size
                                );*/
                            item.Font =
                                (Font)this.back.Font.Clone();
                            item.LinkColor = 
                                this.back.ForeColor;
                        }

                    }
                }
            }



            /// <summary>
            /// Libera recursos de las LinkLabel
            /// correspondientes a el grupo
            /// de paginas actualmente en la barra
            /// </summary>
            private void disposePages()
            {
                if (null == this.listPage) return;

                foreach (LinkLabel item in this.listPage)
                {
                    item.LinkClicked -= page_LinkClicked;
                    item.Dispose();
                }
                this.listPage.Clear();
            }

            #endregion End Metodos protegidos




            #region Eventos Internos



            /// <summary>
            /// Se invoca al navegar por las pagians
            /// </summary>
            /// <param name="lista">
            /// items de pagina ingresada
            /// por el usuario
            /// </param>
            protected virtual void invocarEventoClick( ArrayList lista )
            {
                if (null == this.dClickEvent) return;

                this.dClickEvent(
                    this,
                    new BarraPaginaClickEventArgs(lista)
                );
            }



            /// <summary>
            /// Se llama al realizar el evento click
            /// sobre un numero de pagina
            /// </summary>
            private void page_LinkClicked(
                object sender, LinkLabelLinkClickedEventArgs e)
            {

                LinkLabel link = (LinkLabel)sender;
                this.invocarEventoClick(
                    this.listaPaginada.getPage(
                        Int32.Parse(link.Text) - 1
                    )
                );
                this.SelectPage(Int32.Parse(link.Text));
            }

            protected void selectHome() {
                this.invocarEventoClick(this.homePage());
            }

            /// <summary>
            /// LLamado al pulsar sobre home
            /// </summary>
            private void home_LinkClicked(
                object sender, LinkLabelLinkClickedEventArgs e)
            {
                if (!this.LoadHomeGroupPages()) return;

                LinkLabel link = (LinkLabel)sender;
                this.invocarEventoClick(
                    this.listaPaginada.home());
                this.SelectPage(this.listaPaginada.CurrentPageIndex);
            }



            /// <summary>
            /// LLamado al pulsar sobre back
            /// </summary>
            private void back_LinkClicked(
                object sender, LinkLabelLinkClickedEventArgs e)
            {
                if (!this.loadBackGroupPages()) return;

                LinkLabel link = (LinkLabel)sender;
                this.invocarEventoClick(
                    this.listaPaginada.back());
                this.SelectPage(this.listaPaginada.CurrentPageIndex);
            }


            /// <summary>
            /// LLamado al pulsar sobre next
            /// </summary>
            private void next_LinkClicked(
                object sender, LinkLabelLinkClickedEventArgs e)
            {
                if (!this.loadNextGroupPages()) return;

                LinkLabel link = (LinkLabel)sender;
                this.invocarEventoClick(
                    this.listaPaginada.next()
                );
                this.SelectPage(this.listaPaginada.CurrentPageIndex);
            }


            /// <summary>
            /// LLamado al pulsar sobre end
            /// </summary>
            private void end_LinkClicked(
                object sender, LinkLabelLinkClickedEventArgs e)
            {
                if (!this.LoadEndGroupPages()) return;

                //LinkLabel link = (LinkLabel)sender;
                this.invocarEventoClick(
                    this.listaPaginada.end()
                );
                this.SelectPage(this.listaPaginada.CurrentPageIndex);
            }

            /// <summary>
            /// Se desencadena a hacer click sobre
            /// un simbolo o numero del control
            /// </summary>
            public event BarraPaginaClickEventHandler OnClickEvent
            {
                add { this.dClickEvent += value; }
                remove { this.dClickEvent -= value; }
            }
            #endregion End Eventos Internos

        }
    }

