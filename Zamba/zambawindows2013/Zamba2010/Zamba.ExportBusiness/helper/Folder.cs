using System;
using System.Collections.Generic;
using System.Text;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Diagnostics;
using Zamba.Services.Remoting;
using Zamba.Core;


namespace ExportaOutlook.Helper
{
    /// <summary>
    /// Brinda funciones para verificar la existencia de carpetas y crearlas.
    /// </summary>
    public class Folder
    {


      
        /// <summary>
        /// Comprueba si una carpeta dentro del Outlook existe o no.
        /// </summary>
        /// <param name="strFolderName">Nombre de la carpeta a buscar</param>
        /// <returns>True: Existe</returns>
        /// <history>
        /// [Tomas 04/03/09]    Created
        /// </history>
        public static Outlook.MAPIFolder CheckFolderExists(Outlook.Application app, String strFolderEntryId, String strFolderName)
        {
           
            Outlook.MAPIFolder customFolder = null;

            try
            {
                Outlook.NameSpace outlookNS = app.GetNamespace("MAPI");
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo Carpeta ");

                try
                {
                    
                        customFolder = (Outlook.MAPIFolder)outlookNS.GetFolderFromID(strFolderEntryId, null);
                    
                }
                catch(Exception ex)
                {
                    ZClass.raiseerror(ex);

                    customFolder = null;
                }
                // Busco si la carpeta existe.
                if (customFolder != null) 
                    ZTrace.WriteLineIf(ZTrace.IsError, "Carpeta existente.");
                else 
                    ZTrace.WriteLineIf(ZTrace.IsError, "Carpeta inexistente.");

                return customFolder;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new ArgumentException("Error al intentar comprobar la existencia de la carpeta  - - " + ex.ToString());
            }
        }
        public static Boolean GetFolder(String FullFolderPath, Outlook.MAPIFolder Parent, ref Outlook.MAPIFolder expFolder)
        {

            // Busco si la carpeta existe.

            if (Parent != null)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Folder = " + Parent.FullFolderPath);
                foreach (Outlook.MAPIFolder subFolder in Parent.Folders)
                {

                    if (subFolder != null)
                    {
                        if (subFolder.FullFolderPath == FullFolderPath)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsError, "Folder = " + subFolder.FullFolderPath + "  Carpeta encontrada!");
                            expFolder = subFolder;
                            return true;
                        }
                        else
                        {

                            if (GetFolder(FullFolderPath, subFolder, ref expFolder) == true)
                            {

                                return true;
                            }
                        }
                    }
                    else ZTrace.WriteLineIf(ZTrace.IsError, Parent.FullFolderPath + "\\subFolder=null");

                }
            }
            else ZTrace.WriteLineIf(ZTrace.IsError, "Parent = null ");


            return false;
        }
    }
}
