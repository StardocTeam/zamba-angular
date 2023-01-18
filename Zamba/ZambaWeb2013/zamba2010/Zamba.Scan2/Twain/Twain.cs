using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using zamba.scan;
using System.Windows.Forms;
using System.Drawing;
//using Zamba.Core;
using System.Threading;



namespace zamba.scan.twain
{
    /// <summary>
    /// Realiza eScaneo de imagenes
    /// desde un Scanner o camara digital
    /// </summary>
    /// <remarks>
    /// Es una implementación
    /// de driver Twain
    /// </remarks>
    internal class TWain : Twain , IMessageFilter, IScan
	{		
        protected EndScanEventHandler dEndScan;  
       
        protected override void  imageScan(IntPtr hbitmap, int imageCount )
        {
            if (null != this.dEndScan )            
                this.dEndScan(
                     this,
                     new EndScanEventArgs(
                       TwainService.getBitmapForDIB(hbitmap).Clone() as Bitmap,
                       imageCount != 0 ? false : true,
                       this     
                     )
                ); 
        }

        #region < Eventos publicos >
     
     
     /// <summary>
     /// Evento desencadenado al 
     /// finalizar el escaneo
     /// </summary>
     public event EndScanEventHandler OnEndScan {
       add{
         this.dEndScan += value; 
       
       } 
       remove {
         this.dEndScan -= value;                 
       }  
     }
     
     
     #endregion

	
        #region IMessageFilter Members

        public bool  PreFilterMessage(ref Message m)
        {
            TwainCommand cmd = base.PassMessage(ref m);
            if (cmd == TwainCommand.Not)
                return false;

            switch (cmd)
            {
                case TwainCommand.CloseRequest:
                    {
                        endingScan();
                        break;
                    }
                case TwainCommand.CloseOk:
                    {
                        endingScan();                        
                        break;
                    }
                case TwainCommand.DeviceEvent:
                    {
                        break;
                    }
                case TwainCommand.TransferReady:
                    {
                        base.TransferPictures();
                        endingScan();
                        break;
                    }
            }

            return true;
        }


        private void endingScan()
        {
                Application.RemoveMessageFilter(this);
                TwainService.EnableWindow(this.hwnd,true);
                base.CloseSrc();
        }

        #endregion


        public override bool scan(bool showGUI, bool guiModal)
        {            
            if( base.scan(showGUI, guiModal) ) {
               Application.AddMessageFilter(this);           
               return true; 
            } 
            else
               return false;
                
        }   


}
}
