using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace Zamba
{
    namespace Thumbnails
    {
        /// <summary>
        /// Contantes unsadas en la libreria
        /// </summary>
        namespace Constante
        {

            public static class ListaImagen
            {

                // Size por default de una imagen...
                public const int DEFAULT_PERCENT_SIZE = 65;
                public const int MAX_PERCENT_SIZE = 100;
                public const int MAX_SIZE = 256;
                public const int IMAGE_NO_IMAGE_FILE_SIZE = 25;



                public const string MESSAGE_NO_SE_PUEDE_CARGAR_LA_IMAGEN
                                = "Imposible cargar la imagen, ";

                // Profundidad de color de la imagen...
                public const ColorDepth DEFAULT_IMAGE_DEPTH =
                                        ColorDepth.Depth32Bit;

                // Retardo al cargar una lista de imagenes... 
                public const double IMAGE_LOAD_DELAY = 9;

                // maximo delay de carga de una imagen...
                public const double MAX_IMAGE_LOAD_DELAY = 600;

                /* Delay de carga para imagenes con with/heigth 
                 * devalor media entre minSize...maxSize... */
                public const double MED_IMAGE_LOAD_DELAY = 400;


                // Calidad de imagen por defecto...
                public const ListaImagenDefinicion DEFAULT_DEFINICION =
                                        ListaImagenDefinicion.High;


                public const string SEPARADOR_PUNTO = ".";


                /* clave de la imagen predeterminada para un
                 * archivo no reconosido por ListaImagen... */
                public const string DEFAULT_NO_IMAGE_FILE_KEY = "XXX";

                /// <summary>
                /// Devuelve los atributos
                /// de la lista de iconos x defecto
                /// </summary>
                public static Hashtable IndexIcon
                {
                    get
                    {
                        Hashtable hash = new Hashtable();

                        hash["DOC"]  = 2;
                        hash["XXX"]  = 18;
                        hash["XLS"]  = 3;
                        hash["PDF"]  = 4;
                        hash["PPT"]  = 5;
                        hash["TXT"]  = 7;
                        hash["HTM"]  = 6;
                        hash["HTML"] = 6;
                        hash["ZIP"]  = 16;
                        hash["RAR"]  = 16;

                        return hash;
                    }
                }


                /// <summary>
                /// Valida si el archivo
                /// es una imagen soportada x
                /// lista imagen
                /// </summary>
                /// <param name="path">Ruta de la imagen</param>
                /// <returns>respuesta(boolean)</returns>
                public static bool isImage(string path) {
                    if (
                        path.EndsWith(".BMP")   ||
                        path.EndsWith(".JPG")   ||
                        path.EndsWith(".GIF")   ||                        
                        path.EndsWith(".TIF")   ||
                        path.EndsWith(".TIFF")  ||
                        path.EndsWith(".PCX")       )
                        return true;

                    return false;
                }
            }

            public static class ListaTarea {
                // Maximo numero de hilos simultaneos... 
                public const int DEFAULT_MAX_COUNT_LISTA_TAREAS = 10;
            }

            public static class ListaPaginadaImagen
            {
                public const string MESSAGE_CURRENT_PAGE_PAGES =
                                        "{0} de {1} Pagina/s";

                public const string MESSAGE_ITEMS_NUMBER =
                                        "{0} a {1} de {2} Item/s {3}";

                public const int RIGHT_WITH_MESSAGE_CURRENT_PAGE
                                        = 4;



                public static Color DEFAULT_FONT_COLOR
                {
                    get
                    {
                        return Color.Black;
                    }
                }
            }

            public static class BarraNavegacionPagina
            {
                public const int SEPARACION = 1;
                public const int DEFAULT_VISIBLE_PAGE_COUNT = 10;
                public const int MAX_PAGE_GROUP_WIDTH = 800;
                public const int LINE_HEIGHT = 24;

                public static Color SelectedPageColor {
                    get {
                        return Color.Yellow;
                    }
                }

            }
        }
    }
}
