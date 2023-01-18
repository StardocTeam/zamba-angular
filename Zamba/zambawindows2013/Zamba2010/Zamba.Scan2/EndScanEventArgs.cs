using System.Collections;
using System.Drawing;
using System;

namespace zamba.scan
{

public class EndScanEventArgs : EventArgs
{
    /// <summary>
    /// Lista de imagenes
    /// </summary>
    protected Bitmap image;
    
    
    /// <summary>
    /// Ultima imagen escaneada
    /// </summary>
    protected bool   lastImage;
    
    
    protected IScan  m_scan;


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="images">lista de imagenes</param>
    public EndScanEventArgs(  Bitmap image, 
                            bool lastImage,
                            IScan p_scan    )
    {
        this.image     = image;
        this.lastImage = lastImage;
        this.m_scan      = p_scan;  
    }


    /// <summary>
    /// Devuelve una lista de imagenes
    /// </summary>
    public Bitmap Image
    {
        get
        {
            return this.image;
        }
    }


    /// <summary>
    /// Devuelve true si es la 
    /// ultima imagen escaneada
    /// </summary>
    public bool IsListaImage
    {
        get
        {
            return this.lastImage;
        }
    }


    /// <summary>
    /// Devuelve true si es la 
    /// ultima imagen escaneada
    /// </summary>
    public void free()
    {
       this.m_scan.free();
    }
}

}