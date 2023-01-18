using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel;
using System.Threading;
using Zamba.Core;

namespace Zamba
{
    namespace Thumbnails
    {
        public enum ListaImagenDefinicion
        {
            Low = 0, High = 1 
        }

        /// <summary>
        /// Control de usuario donde se visualiza 
        /// una lista de imagenes con su
        /// respectivo path
        /// </summary>
        public class ListaImagen : ListaImagenAbstracta                        
        {




            #region Atributos

            // Definicion de calidad de imagen...(size real)...
            private ListaImagenDefinicion definicionImagen;

            // Atributos compartidos por Tareas(Threads)
            protected double imageLoadDelay;


            /* Manejo de iconos por defecto
            * para mostrar cuando no hay imagen
            * o no se puede cargar.. 
            */
            protected ImageList defaultImage;
            protected Hashtable indexExtension;

            // Delegado interno llamado por thread's...
            internal delegate void dEvento( KeyReg reg, Image imagen );

            #endregion End Atributos




            #region Propiedades


            /// <summary>
            /// Establese o devuelve la 
            /// calidad de las imagenes a mostrar
            /// </summary>
            /// <remarks>
            ///     Para obtener menor consumo de 
            ///     recursos unsar el valor "Low",
            ///     por lo contrario High.
            ///     Con este valor se obtiene toda
            ///     la calidad de la imagen para
            ///     el tamaño establesido
            /// </remarks>
            public ListaImagenDefinicion Definicion
            {
                get
                {
                    return this.definicionImagen;
                }
                set
                {
                    this.definicionImagen = value;
                }
            }



            /// <summary>
            /// Setea o devuelve la profundidad de color
            /// con la que se muestra cada imagen de la lista
            /// </summary>
            public virtual ColorDepth ColorDepth
            {
                get { return this.LargeImageList.ColorDepth; }
                set { this.LargeImageList.ColorDepth = value; }
            }


            /// <summary>
            /// Asigna o devuelve el tamaño de todas las imagenes.
            /// </summary>
            /// <remarks>
            ///     el valor de tamanio se expresa en 
            ///     porcontaje de 1..100%.
            /// </remarks>
            public virtual int ImageSize
            {
                set
                {
                    // Se borran todas las taread agergar Imagenes...
                    if (0 < this.poolTareaAgrearImagenes.Count)
                    {
                        this.poolTareaAgrearImagenes.abortAll();
                        this.poolTareaAgrearImagenes.Clear();
                    }

                    // Se borran las imagenes de la vista...
                    this.borrarVista();

                    // Se Limpia y inicializa la lista de imagenes...
                    this.inicializarImageList(this.getSize(value));

                    // Solo si Hay imagenes cargadas...
                    if (0 < this.listaPathImagenesRegistradas.Count)
                        this.add(this.listaPathImagenesRegistradas);
                }
                get
                {
                    return
                      this.getPercentSize(
                      this.LargeImageList.ImageSize);
                }
            }


            /// <summary>
            /// Delay de carga de imagenes en la lista
            /// </summary>
            public virtual double ImageLoadDelay
            {
                set { this.imageLoadDelay = value; }
                get { return this.imageLoadDelay; }
            }



            #endregion End Propiedades


            public ListaImagen() : base(10)
            {
                this.initListaImagen(
                    null,
                    null,
                    this.getSize(
                    Constante.ListaImagen.DEFAULT_PERCENT_SIZE),
                    Constante.ListaImagen.DEFAULT_IMAGE_DEPTH,
                    Constante.ListaImagen.IMAGE_LOAD_DELAY,
                    Constante.ListaImagen.DEFAULT_DEFINICION,
                    Constante.ListaTarea.DEFAULT_MAX_COUNT_LISTA_TAREAS
                );
            }



            #region Contructores

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="defaultImage">imagen de precarga</param>
            public ListaImagen(
                ImageList defaultImage,
                Hashtable indexExtension)
            {
                this.initListaImagen(
                    defaultImage,
                    indexExtension,
                    this.getSize(
                    Constante.ListaImagen.DEFAULT_PERCENT_SIZE),
                    Constante.ListaImagen.DEFAULT_IMAGE_DEPTH,
                    Constante.ListaImagen.IMAGE_LOAD_DELAY,
                    Constante.ListaImagen.DEFAULT_DEFINICION,
                    Constante.ListaTarea.DEFAULT_MAX_COUNT_LISTA_TAREAS
                );
            }


            /// <summary>
            /// Inicializar comun a todos los constructores
            /// </summary>
            /// <param name="defaultImage">
            ///     imagen por defecto
            /// </param>
            /// <param name="IndexExtension">
            ///     asociacion indice imagen
            /// </param>
            /// <param name="imageSize">Tamaño inicial</param>
            /// <param name="imageColorDepth">Cantidad de colores</param>
            /// <param name="definicion">calidad de imagen</param>
            /// <param name="imageLoadDelay">Delay de carga</param>
            /// <param name="maxNumeroPedidoAdd">
            ///     Numero maximo de llamadas a "Add" simultaneas
            /// </param>
            /// <remarks>
            ///    con respectoa al parametro "maxNumeroPedidoAdd",
            ///    si se realizan mas de las llamadas seteadas
            ///    no se agregara nada a ListaImagen hasta
            ///    que termine alguna de las llamadas anteriores.
            /// </remarks>
            private void initListaImagen(   ImageList defaultImage,
                                            Hashtable IndexExtension,
                                            Size imageSize,
                                            ColorDepth imageColorDepth,
                                            double imageLoadDelay,
                                            ListaImagenDefinicion
                                                        definicion,
                                            int maxNumeroPedidoAdd)
            {
                this.LargeImageList = new ImageList();
                this.LargeImageList.ImageSize = imageSize;
                this.ColorDepth = imageColorDepth;
                this.ImageLoadDelay = imageLoadDelay;
                this.DoubleBuffered = true;
                this.definicionImagen = definicion;

                /* Manejo de iconos por defecto
                * para mostrar cuando no hay imagen
                * o no se puede cargar.. 
                */
                this.defaultImage    = defaultImage;
                this.indexExtension = IndexExtension;
            }

            #endregion End Contructores




            #region Metodos Publicos



            /// <summary>
            /// Agrega Result
            /// </summary>
            /// <param name="list">ArrayList de Result</param>                 
            public virtual void add(IBaseImageFileResult[] result)
            {
                if (null == result) return;
                if (0 == result.Length) return;

                ArrayList lista = new ArrayList();
                lista.AddRange(result);
                this.add(lista);
            }


            /// <summary>
            /// Agrega imagenes segun path
            /// </summary>
            /// <param name="list">ArrayList de Result</param>
            public virtual void add(string[] paths)
            {
                this.add(new ArrayList(paths));
            }





            /// <summary>
            /// Selecciona un item de la lista
            /// </summary>
            /// <returns>true si pudo se seleccionarlo</returns>
            public virtual bool SelectThis(string path)
            {
                return this.SelectThis((object)path);
            }


            /// <summary>
            /// Selecciona un item de la lista
            /// </summary>
            /// <returns>true si pudo se seleccionarlo</returns>
            public virtual bool SelectThis(IBaseImageFileResult value)
            {
                return this.SelectThis((IBaseImageFileResult)value);
            }


            




            #endregion Metodos Publicos




            #region Metodos Protegidos


            /// <summary>
            /// Carga una imagen desde archivo
            /// </summary>
            /// <param name="path">ruta</param>
            /// <returns>instancia de imagen</returns>
            protected virtual Image getImage(string path)
            {
                    Image imagen = Image.FromFile(path);
                    if (ListaImagenDefinicion.Low ==
                        this.Definicion)
                    {
                        imagen = imagen.GetThumbnailImage(
                                    this.LargeImageList.
                                            ImageSize.Width * 2,
                                    this.LargeImageList.
                                            ImageSize.Height * 2,
                                    null,
                                    System.IntPtr.Zero
                                );
                    } 
                    return imagen;
            }


            /// <summary>
            /// Toma el texto de una imagen
            /// </summary>
            /// <remarks>
            ///     Segun que tipo de objeto sea
            ///     topa el texto corespondiente
            ///     a su imagen.
            /// </remarks>
            /// <param name="item">object</param>
            /// <returns>texto de la imagen</returns>
            protected override KeyReg getKeyReg(object item)
            {
                if ( item.GetType() ==  typeof(IBaseImageFileResult) ||
                     item.GetType().IsSubclassOf(typeof(IBaseImageFileResult))  )
                {
                    IBaseImageFileResult res = ((IBaseImageFileResult)item);
                    return new KeyReg(res.ID.ToString(),
                                        item,
                                        res.Name,
                                        res.FullPath);
                }
                else if (item.GetType() == typeof(string))
                    return new KeyReg((String)item, null, null,null);
                return null;
            }














            /// <summary>
            /// Devuelve una imagen segun criterio
            /// </summary>
            /// <remarks>
            ///     El criterio es por archivos soportados
            ///     y no soportados. 
            /// 
            ///     No soportados(No Imagen):  
            ///                     Devuelve un icono a asociado
            ///                     al tipo no imagen(doc,xls...etc.)
            ///                     y un icono por defecto para los
            ///                     tipos no contemplados.
            /// 
            ///     Soportados  :   Devuelve una imagen y 
            ///                     un icono por defecto
            ///                     para archivos que no existan
            ///                     fisicamente.
            /// </remarks>
            /// <param name="keyReg">clave</param>
            /// <returns>imagen o icono</returns>
            protected virtual Image LoadCorrectimage( KeyReg keyReg )
            {
                string path;
                try
                {
                    if (null != keyReg &&
                         System.IO.File.Exists(keyReg.Path))
                    {
                        path = keyReg.Path.ToUpper();
                        /* Si es una imagen */
                        if (Constante.ListaImagen.isImage(path))
                            return this.getImage(path);
                        else
                        {
                            // Si no es una imagen...

                            /* y hay un icono asociado....  */
                            Image imagen = this.getIcon(path);
                            if (null != imagen)
                                return imagen;
                            else
                                /* y no hay un icono asociado
                                 * se carga un icono por default... */
                                return this.getDefaultIcon();
                        }
                    }
                    else
                        /* Si no hay un path => Icono x default... */
                        return this.getDefaultIcon();
                }
                catch
                {
                    /* Si la imagen esta corupta... */
                    return this.getDefaultIcon();
                }
            }



            /// <summary>
            /// Agrega la imagen a la vista
            /// </summary>
            /// <param name="reg">clave</param>
            /// <param name="imagen">imagen</param>
            public void addImage( KeyReg reg, Image imagen )
            {
                try {
                    // Carga la imagen...
                    this.LargeImageList.Images.Add(
                        reg.Key, 
                        imagen
                    );
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                }
            }


            /// <summary>
            /// Devuelve el icono asociado
            /// al archivo paramentro
            /// </summary>
            /// <param name="path">Ruta Archivo</param>
            /// <returns>Imagen</returns>
            protected Image getIcon( string path ) {
                Hashtable hash = 
                    Constante.ListaImagen.IndexIcon;
                string extension = 
                    path.Substring(path.LastIndexOf(".")+1).ToUpper();

                if (hash.ContainsKey(extension) &&
                    null != this.defaultImage )
                {
                    Image imagen = 
                        this.defaultImage.Images[(int)hash[extension]];
                    return this.agregaMarcoImagen(imagen);
                }
                return null;
             }


            /// <summary>
            /// Devuelve el icono por default
            /// </summary>
            /// <remarks>}
            ///     usado Cuando no se 
            ///     reconoce la estension
            /// </remarks>
            /// <returns>Default Icono</returns>
            protected virtual Image getDefaultIcon() {
                int indice =
                    (int)Constante.ListaImagen.IndexIcon[
                     Constante.ListaImagen.
                     DEFAULT_NO_IMAGE_FILE_KEY];

                Image imagen = null;
                if (null != this.defaultImage) {
                    imagen = this.defaultImage.Images[indice];
                    return this.agregaMarcoImagen(imagen);
                }

                return null;
            }


            /// <summary>
            /// Agrega un marco para la imagen pasada
            /// </summary>
            /// <param name="imagen">imagen</param>
            /// <returns>imagen con marco</returns>
            protected virtual Image agregaMarcoImagen(Image imagen)
            {
                Bitmap mapa = null;
                try
                {
                    mapa =
                        new Bitmap(
                              this.LargeImageList.ImageSize.Width,
                              this.LargeImageList.ImageSize.Height
                            );

                    mapa.MakeTransparent(Color.White);

                    int x = (this.LargeImageList.ImageSize.Width -
                              Constante.ListaImagen.IMAGE_NO_IMAGE_FILE_SIZE) / 2;

                    int y = (this.LargeImageList.ImageSize.Height -
                              Constante.ListaImagen.IMAGE_NO_IMAGE_FILE_SIZE) / 2;

                    Graphics grap = Graphics.FromImage(mapa);


                    // Pinta el fondo...
                    grap.FillRectangle(
                     Brushes.White,
                     new Rectangle(0, 0, mapa.Width, mapa.Height)
                    );

                    // Pinta la imagen...
                    grap.DrawImage(
                     imagen,
                     new Rectangle(
                            x, y,
                            Constante.ListaImagen.IMAGE_NO_IMAGE_FILE_SIZE,
                            Constante.ListaImagen.IMAGE_NO_IMAGE_FILE_SIZE
                         )
                    );

                    // pinta el rectangulo...
                    grap.DrawRectangle(
                         new Pen(Color.Gray, (float)1),
                         new Rectangle(0, 0, mapa.Width - 1, mapa.Height - 1)
                    );
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return mapa;
            }


            /// <summary>
            /// Metodo llamado por un thread, para,
            /// agregar imagenes a la lista con un cierto
            /// delay de carga
            /// </summary>
            protected override void agregarImagenes(Tarea tarea,
                                                     ArrayList parametros)
            {          
                int delay = this.getResultadoFuncionDelay();
                IEnumerator iterador = parametros.GetEnumerator();
                iterador.Reset();
                while (iterador.MoveNext())
                {
                    Thread.Sleep(delay);                    
                    try {
                        if (iterador != null && iterador.Current != null)
                        {
                            this.Invoke(
                                new dEvento(this.addImage),
                                    iterador.Current,
                                    this.LoadCorrectimage(
                                        (KeyReg)iterador.Current)
                                );
                        }
                    }
                    catch
                    (ListaImagenExcepcion_NoSePuedeCargarLaImagen e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                // Al terminar la tarea esta de borra de la lista...
                this.poolTareaAgrearImagenes.Remove(tarea);
            } 



            /// <summary>
            /// Funcion que devuelde el delay 
            /// de carga de imagenes segun el size
            /// de la misma
            /// </summary>
            /// <returns>delay de carga de imagen</returns>
            protected virtual int getResultadoFuncionDelay()
            {
                double resultado =
                    this.imageLoadDelay * this.ImageSize;

                if (resultado >
                    Constante.ListaImagen.MED_IMAGE_LOAD_DELAY &&
                    resultado <
                    Constante.ListaImagen.MAX_IMAGE_LOAD_DELAY)
                    return
                      (int)Constante.ListaImagen.MED_IMAGE_LOAD_DELAY;

                if (resultado >
                    Constante.ListaImagen.MAX_IMAGE_LOAD_DELAY)
                    return
                     (int)Constante.ListaImagen.MAX_IMAGE_LOAD_DELAY;

                return (int)resultado;
            }

            /// <summary>
            /// Devuelve la extension
            /// de un path archivo
            /// </summary>
            /// <param name="path">path</param>
            /// <returns>extesion</returns>
            protected virtual string getExtension(string path)
            {
                int pos = path.LastIndexOf(
                            Constante.ListaImagen.SEPARADOR_PUNTO);
                pos++;
                return path.Substring(pos, path.Length - pos).ToUpper();
            }






            /// <summary>
            /// Devuelve el size de imagen segun porcentaje
            /// </summary>
            /// <param name="porcentajeTamaño">
            ///     porcentaje Tamanio imagen
            /// </param>
            /// <returns>Size imagen</returns>
            protected Size getSize(int porcentajeTamaño)
            {
                int resultado =
                    (porcentajeTamaño *
                    Constante.ListaImagen.MAX_SIZE) /
                    Constante.ListaImagen.MAX_PERCENT_SIZE;

                return new Size(resultado, resultado);
            }


            /// <summary>
            /// Devuelve el porcentaje de tamanio
            /// de imagen
            /// </summary>
            /// <param name="porcentajeTamaño">
            ///     size de imagen
            /// </param>
            /// <returns>Size imagen</returns>
            protected int getPercentSize(Size size)
            {
                return
                    (size.Height *
                    Constante.ListaImagen.MAX_PERCENT_SIZE) /
                        Constante.ListaImagen.MAX_SIZE;
            }



            #endregion End Metodos Protegidos




        }

    }
}
