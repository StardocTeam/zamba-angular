using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace zamba.scan.twain
{

    /// <summary>
    /// Api Twain para control de escaneo
    /// </summary>
    internal static class TwainService
    {    
        /// <summary>
        /// Convierte una imagen
        /// de DIB a Bitmap
        /// </summary>
        /// <param name="hbitmap">hbitmap</param>
        /// <returns>imagen Btimap</returns>
        public static Bitmap getBitmapForDIB( IntPtr hbitmap ) 
        {
            unsafe
            {
                IntPtr bmpptr = GlobalLock(hbitmap);

                Rectangle bmprect;
                IntPtr pixptr = GetPixelInfo(bmpptr,out bmprect);

                Bitmap mapa = new Bitmap(bmprect.Width, bmprect.Height);
                Graphics g = Graphics.FromImage(mapa);
                using (g)
                {
                    IntPtr hdcMapa = g.GetHdc();
                    SetDIBitsToDevice(
                                hdcMapa,
                                0, 0,
                                bmprect.Width, bmprect.Height,
                                 0, 0, 0,
                                bmprect.Height, pixptr, bmpptr, 0);

                        g.ReleaseHdc(hdcMapa);
                        g.Flush();
                }
                return mapa;
            }
        }



       

          /// <summary>
          ///  Habilita/Deshabilita ina ventana
          /// </summary>
          /// <param name="hWnd">
          ///  Handler de la ventana(identificador)
          /// </param>
          /// <param name="bEnable">booleano</param>
          /// <remarks>
          ///  bEnable = true Habilita la ventana
          /// </remarks>
          /// <returns></returns>
         [DllImport("user32.dll")]
         internal static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

         // ------ DSM entry point DAT_ variants:
         [DllImport("twain_32.dll", EntryPoint = "#1")]
         internal static extern TwRC DSMparent(
                                      [In, Out] TwIdentity origin, 
                                      IntPtr zeroptr, 
                                      TwDG dg, 
                                      TwDAT dat, 
                                      TwMSG msg, 
                                      ref IntPtr refptr);

         /// <summary>
         /// Se usa para comunición con un
         /// dispocitivo espesifico
         /// </summary>
         /// <param name="origin"></param>
         /// <param name="zeroptr"></param>
         /// <param name="dg"></param>
         /// <param name="dat"></param>
         /// <param name="msg"></param>
         /// <param name="idds"></param>
         /// <returns></returns>           
         [DllImport("twain_32.dll", EntryPoint = "#1")]
         internal static extern TwRC DSMident(
                                      [In, Out] TwIdentity origin, 
                                      IntPtr zeroptr, 
                                      TwDG dg, 
                                      TwDAT dat, 
                                      TwMSG msg, 
                                      [In, Out] TwIdentity idds);


         [DllImport("twain_32.dll", EntryPoint = "#1")]
         internal static extern TwRC DSMstatus(
                                      [In, Out] TwIdentity origin, 
                                      IntPtr zeroptr,                                       
                                      TwDG dg, 
                                      TwDAT dat, 
                                      TwMSG msg, 
                                      [In, Out] TwStatus dsmstat);


         // ------ DSM entry point DAT_ variants to DS:
         [DllImport("twain_32.dll", EntryPoint = "#1")]
         internal static extern TwRC DSuserif(
                                      [In, Out] TwIdentity origin, 
                                      [In, Out] TwIdentity dest, 
                                      TwDG dg, 
                                      TwDAT dat, 
                                      TwMSG msg, 
                                      TwUserInterface guif);

         [DllImport("twain_32.dll", EntryPoint = "#1")]
         internal static extern TwRC DSevent(
                                      [In, Out] TwIdentity origin, 
                                      [In, Out] TwIdentity dest,
                                      TwDG dg, 
                                      TwDAT dat, 
                                      TwMSG msg, 
                                      ref TwEvent evt);

         [DllImport("twain_32.dll", EntryPoint = "#1")]
         internal static extern TwRC DSstatus(
                                      [In, Out] TwIdentity origin, 
                                      [In] TwIdentity dest, 
                                      TwDG dg, 
                                      TwDAT dat,                                       
                                      TwMSG msg, 
                                      [In, Out] TwStatus dsmstat);

         /// <summary>
         /// Setea las capasidades de digitalizacion
         /// para un dispositivo 
         /// </summary>
         /// <param name="origin"></param>
         /// <param name="dest"></param>
         /// <param name="dg"></param>
         /// <param name="dat"></param>
         /// <param name="msg"></param>
         /// <param name="capa"></param>
         /// <remarks>
         ///    se puede seter la calidad
         ///    cantidad de paginas a escanear etc...
         /// </remarks>
         /// <returns></returns>
         [DllImport("twain_32.dll", EntryPoint = "#1")]
         internal static extern TwRC DScap(
                                      [In, Out] TwIdentity origin, 
                                      [In] TwIdentity dest, 
                                      TwDG dg, 
                                      TwDAT dat, 
                                      TwMSG msg, 
                                      [In, Out] TwCapability capa
                                   );

         [DllImport("twain_32.dll", EntryPoint = "#1")]
         internal static extern TwRC DSiinf(
                                      [In, Out] TwIdentity origin,
                                      [In] TwIdentity dest, 
                                      TwDG dg, 
                                      TwDAT dat, 
                                      TwMSG msg, 
                                      [In, Out] TwImageInfo imginf
                                   );

         [DllImport("twain_32.dll", EntryPoint = "#1")]
         internal static extern TwRC DSixfer(
                                      [In, Out] TwIdentity origin, 
                                      [In] TwIdentity dest, 
                                      TwDG dg, 
                                      TwDAT dat, 
                                      TwMSG msg, 
                                      ref IntPtr hbitmap
                                   );


         [DllImport("twain_32.dll", EntryPoint = "#1")]
         internal static extern TwRC DSpxfer(
                                      [In, Out] TwIdentity origin, 
                                      [In] TwIdentity dest, 
                                      TwDG dg, 
                                      TwDAT dat, 
                                      TwMSG msg, 
                                      [In, Out] TwPendingXfers pxfr
                                   );


         [DllImport("kernel32.dll", ExactSpelling = true)]
         internal static extern IntPtr GlobalAlloc(int flags, int size);


         [DllImport("kernel32.dll", ExactSpelling = true)]
         internal static extern bool GlobalUnlock(IntPtr handle);



         [DllImport("user32.dll", ExactSpelling = true)]
         internal static extern int GetMessagePos();

         [DllImport("user32.dll", ExactSpelling = true)]
         internal static extern int GetMessageTime();


         [DllImport("gdi32.dll", ExactSpelling = true)]
         internal static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

         [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
         internal static extern IntPtr CreateDC(
                                      string szdriver, 
                                      string szdevice, 
                                      string szoutput, 
                                      IntPtr devmode
                                     );

         [DllImport("gdi32.dll", ExactSpelling = true)]
         internal static extern bool DeleteDC(IntPtr hdc);



         [DllImport("kernel32.dll", ExactSpelling = true)]
         internal static extern IntPtr GlobalLock(IntPtr handle);
         [DllImport("kernel32.dll", ExactSpelling = true)]
         internal static extern IntPtr GlobalFree(IntPtr handle);

         [DllImport("gdi32.dll", ExactSpelling = true)]
         internal static extern int SetDIBitsToDevice(
                                      IntPtr hdc, 
                                      int xdst, 
                                      int ydst,
                                      int width, 
                                      int height, 
                                      int xsrc,                                       
                                      int ysrc, 
                                      int start, 
                                      int lines,
                                      IntPtr bitsptr, 
                                      IntPtr bmiptr, 
                                      int color
                                  );

         [StructLayout(LayoutKind.Sequential, Pack = 2)]






        /// <summary>
        /// Definiciones Api Twain
        /// </summary>

        internal class BITMAPINFOHEADER
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
        }  
       
      
     
	public static  IntPtr GetPixelInfo( IntPtr bmpptr, out Rectangle p_bmprect )
		{
       BITMAPINFOHEADER bmi = new BITMAPINFOHEADER();
		Marshal.PtrToStructure( bmpptr, bmi );


       Rectangle bmprect = new Rectangle();

		bmprect.X = bmprect.Y = 0;
		bmprect.Width = bmi.biWidth;
		bmprect.Height = bmi.biHeight;
		

		if( bmi.biSizeImage == 0 )
			bmi.biSizeImage = ((((bmi.biWidth * bmi.biBitCount) + 31) & ~31) >> 3) * bmi.biHeight;

		int p = bmi.biClrUsed;
		if( (p == 0) && (bmi.biBitCount <= 8) )
			p = 1 << bmi.biBitCount;
		p = (p * 4) + bmi.biSize + (int) bmpptr;
		
		
		p_bmprect = bmprect;
		return (IntPtr) p;
		}




	[DllImport("kernel32.dll", CharSet=CharSet.Auto) ]
	public static extern void OutputDebugString( string outstr );



	} // class PicForm


	[StructLayout(LayoutKind.Sequential, Pack=2)]
    internal class BITMAPINFOHEADER
	{
	    public int      biSize;
	    public int      biWidth;
	    public int      biHeight;
	    public short    biPlanes;
	    public short    biBitCount;
	    public int      biCompression;
	    public int      biSizeImage;
	    public int      biXPelsPerMeter;
	    public int      biYPelsPerMeter;
	    public int      biClrUsed;
	    public int      biClrImportant;
	}     
     
     
     
     
     


}