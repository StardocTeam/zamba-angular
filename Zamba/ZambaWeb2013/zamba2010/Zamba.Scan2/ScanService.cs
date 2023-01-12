using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using zamba.scan.twain;
//using Zamba.Core;
using System.Threading;

namespace zamba.scan
{
    public delegate void ResultScanEventHandler(List<Bitmap> Bitmaps);

    /// <summary>
    /// Proporciona servicios de escaneo de imagenes
    /// tanto desde un escaner como de una camara digital.
    /// </summary>
    public class ScanService     
    {
        private static ScanService instance = null;

        Image imageToReturn;

        private int cont = 0;
        
        
        protected ScanService() {}
        
        
        public static ScanService getInstance() {
           if( null == instance )
               instance = new ScanService();
              
           return instance;          
        }
    
    
    
    
        /// <summary>
        /// Escanea n imagenes,
        /// las guarda en disco(como cache) y
        /// devuelve en un evento una lista 
        /// con el path completo de cada imagen.
        /// </summary>
        /// <param name="path">
        ///    Ruta donde se salvaran 
        ///    las imagenes.
        /// </param>
        /// <param name="fileName">
        ///    Nombre de archivos.
        /// </param>
        /// <param name="imageFormat">
        ///  Formato de la imagen(bmp,jpg,etc...).
        /// </param>
        /// <param name="scanDriver">
        ///    Driver de escaneo.
        /// </param>
        /// <param name="clientWindowHandler">
        ///    Identificador de ventana cliente.
        /// </param>
        /// <param name="selectDevice">
        ///    Si se espesifica true, muestra una ventana
        ///    donde se pide al usuario seleccionar un
        ///    dispositivo que se transformara el 
        ///    predeterminado luego se seleccionado.
        /// </param>
        /// <param name="eventEndScan">
        ///    Evento donde se devuelve una lista 
        ///    con el path completo de cada imagen 
        ///    escaneada.
        /// </param>
        /// <returns>retorna falso si el dispositivo esta apagado</returns>

        public static List<Bitmap> ListofBitmaps = new List<Bitmap>();

        public bool scan
        (
                string path,
                string fileName,
                ImageFormat imageFormat,
                EnumScanDriver scanDriver,
                IntPtr clientWindowHandler,
                bool selectDevice,
                ResultScanEventHandler eventEndScan,
                bool showGUI,
                bool guiModal
        )
        {
            //int          cont       = 0;
            StringBuilder imagePath = new StringBuilder();
            List<string> paths = new List<string>();
            
          
            WaitCallback wait = new WaitCallback(saveImage);

            IScan scanner = ScanFactory.getProduct(scanDriver);
            scanner.initialize(clientWindowHandler);

            if (selectDevice) scanner.select();

            scanner.OnEndScan +=
               delegate(object sender,
                        EndScanEventArgs e)
               {
                   //scanner.select();
                   /* Se arma el path destino 
                       * de la imagen..           */
                   imagePath.Length = 0;
                   imagePath.Append(path);
                   imagePath.Append("\\");
                   imagePath.Append(fileName);
                   imagePath.Append((++cont).ToString());
                   imagePath.Append(".");
                   switch (imageFormat.ToString().ToLower())
                   {
                       //bmp
                       case "bmp":
                           imagePath.Append("bmp");
                           break;
                       //emf
                       case "emf":
                           imagePath.Append("emf");
                           break;
                       //exif
                       case "exif":
                           imagePath.Append("exif");
                           break;
                       //gif
                       case "gif":
                           imagePath.Append("gif");
                           break;
                       //icon
                       case "icon":
                           imagePath.Append("icon");
                           break;
                       //jpeg
                       case "jpeg":
                           imagePath.Append("jpeg");
                           break;
                       //wmf
                       case "wmf":
                           imagePath.Append("wmf");
                           break;
                       //png
                       case "png":
                           imagePath.Append("png");
                           break;
                       //tiff
                       case "tif":
                           imagePath.Append("tif");
                           break;
                       //case else
                       default:
                           imagePath.Append("tif");
                           break;
                   } 
                   
                   paths.Add(imagePath.ToString());
                   ListofBitmaps.Add(e.Image);

                   // Se salva la imagen...
                   /*ThreadPool.QueueUserWorkItem(
                       wait,
                       new ImageSave(
                          e.Image,
                          imagePath.ToString(), 
                          imageFormat      
                       )
                     );*/

                   if (File.Exists(imagePath.ToString()))
                       File.Delete(imagePath.ToString());

                   e.Image.Save(imagePath.ToString());
                   //e.Image.Dispose();


                   if (e.IsListaImage && null != eventEndScan)

                       eventEndScan(ListofBitmaps);
               };

            if (!scanner.scan(showGUI, guiModal))
            {
                MessageBox.Show(
                    zamba.scan.constant.ScanService.NOT_SCAN_MESSAGE,
                    "Zamba Software",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
                return false;
            }
            return true;
        }

        /// <summary>
        /// Escanea n imagenes,
        /// las guarda en disco(como cache) y
        /// devuelve en un evento una lista 
        /// con el path completo de cada imagen.
        /// </summary>
        /// <param name="imageFormat">
        ///  Formato de la imagen(bmp,jpg,etc...).
        /// </param>
        /// <param name="scanDriver">
        ///    Driver de escaneo.
        /// </param>
        /// <param name="clientWindowHandler">
        ///    Identificador de ventana cliente.
        /// </param>
        /// <param name="selectDevice">
        ///    Si se espesifica true, muestra una ventana
        ///    donde se pide al usuario seleccionar un
        ///    dispositivo que se transformara el 
        ///    predeterminado luego se seleccionado.
        /// </param>
        /// <param name="eventEndScan">
        ///    Evento donde se devuelve una lista 
        ///    con el path completo de cada imagen 
        ///    escaneada.
        /// </param>        
        /// <remarks>
        ///   Por defecto se toma como directorio temporal
        ///   pathAplicacion\temp, si "temp" no existe se crea,
        ///   y luego se salva las imagenes en este 
        ///   con el nombre"image" y la secuencia:
        ///   image1.extencion, image2.extencion....
        ///   imageN.extencion.
        /// </remarks>
        public bool scan
        ( 
                ImageFormat              imageFormat,
                EnumScanDriver            scanDriver,
                IntPtr                   clientWindowHandler,
                bool                     selectDevice,
                ResultScanEventHandler     eventEndScan,
                bool                     showGUI,
                bool                     guiModal                       
        )
        {
                string tempPath =  
                  Application.StartupPath + 
                  zamba.scan.constant.ScanService.TEMPORAL_PATH;
                
                if(!Directory.Exists(tempPath)) 
                  Directory.CreateDirectory(tempPath); 
               
                      
                return scan(
		            tempPath,		        
		            Constant.DEFAULT_IMAGE_NAME,
		            imageFormat,
		            scanDriver,
		            clientWindowHandler,
		            selectDevice,
                   eventEndScan,
		            showGUI, 
		            guiModal
		         );
		    return true;		     
        }

     
     
     
     
     
     
     
     
     
        
                                                  
        internal struct ImageSave
        {
            public Bitmap image;
            public string path;
            public ImageFormat imageFormat;

            public ImageSave(
                Bitmap image,
                string path,
                ImageFormat imageFormat
            )
            {
                this.image = image;
                this.path = path;
                this.imageFormat = imageFormat;
            }
        } 
      
        private static void saveImage( Object param )
        {           
           
           Bitmap mapa = ((ImageSave)param).image;
           
           mapa.Save(
               ((ImageSave)param).path,                          
               ((ImageSave)param).imageFormat
           );          
           
      
        } 
       
       
        
      
                                                                                 
    }

}
