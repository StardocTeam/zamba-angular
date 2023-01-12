using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Zamba.Core;
using Zamba.Grid;
using Zamba.Grid.PageGroupGrid;

namespace Zamba
{
    namespace Thumbnails
    {

        /// <summary>
        /// Lista de imagenes paginadas
        /// </summary>
        public partial class NewThumbnails : UserControl
        {
            #region Atributos
                protected ListaPaginada listaPaginada;
                
                
                protected EventHandlerSelectedItemIndex
                                      dSelectedIndexChanged;

                protected EventHandlerDoubleClick dDoubleClick;


                protected ArrayList indexSeleccionados;

                /* Manejo de iconos por defecto
                 * para mostrar cuando no hay imagen
                 * o no se puede cargar.. 
                 */
                protected Hashtable indexExtension;
            #endregion End Atributos



            #region Propiedades



            protected ImageList DefaultImage
            {
                get { return defaultImage; }
                set { defaultImage = value; }
            }

            public ListaImagen Vista
            {
                set { this.vista = value; }
                get { return this.vista; }
            }

            /// <summary>
            /// Devuelve una ref.
            /// a la etiqueta ZOOM
            /// para correspondiente seteo
            /// de propiedades de este
            /// </summary>
            public Label ZoomName
            {
                get
                {
                    return this.lZoom;
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
                set
                {
                    this.barraNavPag.ForeColor  = value;
                    this.vista.ForeColor        = value;
                    this.lItem.ForeColor        = value;
                    this.lPage.ForeColor        = value;
                    base.ForeColor              = value;
                    this.zoom.ForeColor         = value;
                    this.lZoom.ForeColor        = value;
                }
            }



            #endregion End Propiedades




            #region Constructores

            //TODO: Borrar constructor...
            public NewThumbnails(): this(new ImageList(),new Hashtable()){
                this.indexExtension = null;
                this.defaultImage = new ImageList();
            }
            
            
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="defaultImage">
            /// Lista de iconos x defecto
            /// </param>
            /// <param name="IndexExtension">
            /// hash que asocia un indice de la lista
            /// defaultImage con una extencion.
            /// </param>
            /// <remarks>
            ///  Imagen para archivos no reconocidos
            ///  bienen por defaultImage y 
            ///  en indexExtension la asiciacion
            ///  Extencion - Atributo.
            /// 
            ///  Ej.: indexExtension["DOC"] => Atributo 21
            ///       en listadefaultImage.
            /// </remarks>
            public NewThumbnails(   
                ImageList defaultImage,
                Hashtable indexExtension ) {

                /* Manejo de iconos por defecto
                * para mostrar cuando no hay imagen
                * o no se puede cargar.. 
                */
                this.indexExtension = indexExtension;
                this.defaultImage   = defaultImage;             

                InitializeComponent();

                this.listaPaginada =
                    new ListaPaginada(
                    zamba.collections.Constant.ListaPaginada.PAGE_SIZE);

                this.barraNavPag.OnClickEvent +=
                    new BarraPaginaClickEventHandler(
                    this.barraNavPag_OnClickEvent);

                // Se setea el zoom al inicio...
                this.zoom.Value =
                        this.vista.ImageSize;
                this.indexSeleccionados =
                        new ArrayList();
                this.barraNavPag.DataSource =
                        this.listaPaginada;
                this.vista.CheckDuplicateImage = false;

                this.ForeColor =
                    Constante.ListaPaginadaImagen.DEFAULT_FONT_COLOR;
            }

            #endregion End Constructores



            #region Metodos Publicos

            /// <summary>
            /// Devuelve los objetos asociados 
            /// a las inmagenes seleccionadas
            /// </summary>
            public object[] getSelectedItems() {
                return this.vista.SelectedItems_;
            }

            /// <summary>
            ///  Agrega una lista de Result
            /// </summary>
            /// <param name="value">lista Result</param>
            public void add( IBaseImageFileResult[] value)
            {
                this.add(new ArrayList(value));
            }

            /// <summary>
            ///  Agrega una lista de Imagenes
            ///  segun path
            /// </summary>
            /// <param name="value">lista de path's</param>
            public void add(string[] value)
            {
                this.add(new ArrayList(value));
            }


            /// <summary>
            ///  Agrega una lista de Imagenes
            ///  segun path
            /// </summary>
            /// <param name="value">lista de path's</param>
            public void add(ArrayList value)
            {
                if (null == value || 0 == value.Count) return;
                this.listaPaginada.AddRange(value);
                this.vista.Clear();
                this.vista.add(this.barraNavPag.homePage());
                this.showItemsNumber();
                this.showCurrentPageNumber();
                this.barraNavPag.Show();
            }


            /// <summary>
            /// Vacia la lista de imagenes
            /// </summary>
            public void clear() 
            {
                this.vista.Clear();
                this.listaPaginada.clear();
                this.lPage.Text = String.Empty;
                this.lItem.Text = String.Empty;
                this.barraNavPag.clear();
            }


            public bool selectThis(IBaseImageFileResult value)
            {
                return this.vista.SelectThis(value);
            }


            public bool selectThis(string path)
            {
                return this.vista.SelectThis(path);
            }






            #endregion End Metodos Publicos



            #region Eventos Privados
            private void barraNavPag_OnClickEvent(object sender, IBarraPaginaClickEventArgs e)
            {
                this.showPage((ArrayList)e.ItemSelectedPage);
            }
            
            private void zoom_ValueChanged(object sender, EventArgs e)
            {
                this.vista.ImageSize = (int)this.zoom.Value;
            }

            /// <summary>
            /// Laamado al seleccionar un item de la lista
            /// </summary>
            /// <param name="sender">ListaImagen</param>
            /// <param name="e">Argumento</param>
            private void vista_SelectedItems(object sender, EventArgs e) {
                if (null != this.dSelectedIndexChanged)
                    this.dSelectedIndexChanged(
                        this,
                        new SelectedItemIndexArgs(this.vista)
                    );
            }


            /// <summary>
            /// Laamado al hacer Dobleclick
            /// </summary>
            /// <param name="sender">ListaImagen</param>
            /// <param name="e">Argumento</param>
            private void vista_DobleClick(object sender, EventArgs e) {
                if (null != this.dDoubleClick)
                    this.dDoubleClick(this,e);
            }




            #endregion End Eventos Privados



            #region Metodos Protegidos

            /// <summary>
            /// Muestra el numero de pagina 
            /// actual al usuario
            /// </summary>
            protected virtual void showCurrentPageNumber()
            {
                this.lPage.Text = String.Format(
                Constante.ListaPaginadaImagen.MESSAGE_CURRENT_PAGE_PAGES,
                this.listaPaginada.CurrentPageIndex,
                this.listaPaginada.PageCount);


                this.lPage.Left =
                    this.Width -
                    this.lPage.Width -
                    Constante.ListaPaginadaImagen.
                    RIGHT_WITH_MESSAGE_CURRENT_PAGE;
            }


            /// <summary>
            /// Muestra una pagina 
            /// </summary>
            /// <param name="page">pagina</param>
            protected virtual void showPage(ArrayList page)
            {
                if (null != page)
                {

                    this.vista.Clear();
                    this.showItemsNumber();
                    this.vista.add(page);

                }
            }


            /// <summary>
            /// Muestra en numero de items 
            /// por pagina al usuario
            /// </summary>
            protected virtual void showItemsNumber()
            {
                int sizeCurrentPage = this.listaPaginada.ItemsCount;

                if (this.listaPaginada.ItemsCount <
                    zamba.collections.Constant.ListaPaginada.PAGE_SIZE)
                    sizeCurrentPage = this.listaPaginada.ItemsCount;

                this.lItem.Text = String.Format(
                    Constante.ListaPaginadaImagen.MESSAGE_ITEMS_NUMBER,
                    this.listaPaginada.CurrentPageFirtItemIndex,
                    this.listaPaginada.CurrentPageLastItemIndex,
                    this.listaPaginada.ItemsCount,
                    this.listaPaginada.CurrentPageIndex
                );
            }


            #endregion End Metodos Protegidos



            #region Eventos Publicos

            /// <summary>
            /// Evento desencadenado al
            /// seleccionar ina imagen de la lista
            /// </summary>
            public event EventHandlerSelectedItemIndex
                                OnSelectedItemIndex
            {
                add
                {
                    this.dSelectedIndexChanged += value;
                }
                remove
                {
                    this.dSelectedIndexChanged -= value;
                }
            }

            public event EventHandlerDoubleClick OnListDoubleClick
            {
                add
                {
                    this.dDoubleClick += value;
                }
                remove
                {
                    this.dDoubleClick += value;
                }
            }




            #endregion End Eventos Publicos

        }
    }
}
