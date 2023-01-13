using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using Zamba.Core;

namespace Zamba.Outlook.GAL
{
    class Importador
    {        
        private Outlook appOutlook;
        private Configuration config;
        private bool log_query;
        private bool use_transaction;        
        private int hora_ini;
        private int hora_fin;
        private int espera;
        private int zlevel;

        public void Iniciar()
        {
            LeerConfig();
            ImportarContactos();
        }

        private void LeerConfig()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            log_query = bool.Parse(config.AppSettings.Settings["log_query"].Value.ToString());
            use_transaction = bool.Parse(config.AppSettings.Settings["use_transaction"].Value.ToString());

            hora_ini = int.Parse(config.AppSettings.Settings["hora_ini"].Value.ToString());
            hora_fin = int.Parse(config.AppSettings.Settings["hora_fin"].Value.ToString());
            espera = int.Parse(config.AppSettings.Settings["min_espera"].Value.ToString());

            zlevel = int.Parse(config.AppSettings.Settings["zlevel"].Value.ToString());
        }

        private void ImportarContactos()
        {
            List<OutlookContact> contactos = new List<OutlookContact>();

            try
            {
                // nombre de la libretas que hay que guardar
                string libretas_a_leer = config.AppSettings.Settings["libretas"].Value.ToString();
                string[] libretasGuardar = libretas_a_leer.Split('|');

                // libretas que ya se procesaron (no volver a hacerlas hasta que se termine con todas)
                string libretas_procesadas = config.AppSettings.Settings["libretas_procesadas"].Value.ToString();

                // validar si ya se hicieron todas para volver a empezar
                if (libretas_a_leer + "|" == libretas_procesadas)
                    libretas_procesadas = "";

                foreach (string libreta in libretasGuardar)
                {
                    // si esta libreta fue procesada saltearla
                    if (!libretas_procesadas.Contains(libreta))
                    {
                        ZTrace.SetLevel(zlevel,  "GAL - Exchange - " + libreta);

                        Log("---------------------------------------------");
                        Log("");

                        CheckTime();

                        do
                        {
                            appOutlook = new Outlook();

                            Log("Cargando contactos de la libreta: " + libreta);
                            contactos = CargarContactos(libreta);

                            // no quedan contactos
                            if (contactos == null)
                                break;

                            CheckTime();

                            Log("Resolviendo direcciones ...");
                            ResolverDirecciones(contactos);

                            CheckTime();

                            Log("Guardando contactos ...");
                            UpdateContactosWork(contactos);

                            CheckTime();

                            appOutlook.Dispose();
                        } 
                        while (true);

                        // mover a la tabla final
                        Log("Moviendo contactos ...");
                        MoveContactos(libreta);

                        ZTrace.RemoveListener("GAL - Exchange - " + libreta);

                        // guardar las libretas procesadas
                        libretas_procesadas = libretas_procesadas + libreta + "|";

                        config.AppSettings.Settings["libretas_procesadas"].Value = libretas_procesadas;
                        config.Save(ConfigurationSaveMode.Modified);
                    }
                }

                // si termino todo ok poner el id en cero para la proxima
                config.AppSettings.Settings["last_id_inserted"].Value = "0";
                config.Save(ConfigurationSaveMode.Modified);                
            }
            catch (System.Exception ex)
            {
                Log("Exception no controlada: " + ex.ToString());
            }

            appOutlook = null;
        }

        private List<OutlookContact> CargarContactos(string libreta)
        {
            OutlookDAL outlookDAL = new OutlookDAL();
            List<OutlookContact> contactos = new List<OutlookContact>();

            outlookDAL.LogQuery = log_query;

            // ver si hay pendientes en la tabla temp para esta libreta
            contactos = outlookDAL.CargarContactosWork(libreta);
            
            // si no hay pendientes
            if (contactos == null)
            {
                // ver si hay datos guardados, si hay entonces es que no se pasaron
                // a la tabla final, si no hay, traer los contactos desde exchange
                if (outlookDAL.cantContactosWork(libreta) <= 0)
                {
                    contactos = appOutlook.GetAdressBookContacts(libreta);
                    contactos = LimpiaListaContactos(contactos, libreta);
                }
                else
                {
                    return null;
                }

                // guardar los contactos traidos desde exchagne en la tabla temporal
                foreach (OutlookContact contacto in contactos)
                    outlookDAL.SaveContactWork(contacto);
            }
            else
            {  
                contactos = LimpiaListaContactos(contactos, libreta);
            }

            if (contactos.Count > 0)
                return contactos;
            else
                return null;
        }

        // elimina contactos sin email y actualiza el nombre de la libreta de direcciones
        private List<OutlookContact> LimpiaListaContactos(List<OutlookContact> contactos, string libreta)
        {
            Log("Total de contactos: " + contactos.Count);

            if (contactos.Count > 0)
            {
                Log(" Recorriendo contactos");
                Log("");

                // eliminar contactos sin mail y actualizar nombre de libreta en el contacto
                foreach (OutlookContact contacto in contactos)
                {
                    CheckTime();

                    Log("  Contacto: " + contacto.FullName);

                    contacto.AddressBookName = libreta;

                    if (contacto.EmailAddress == null)
                        contactos.Remove(contacto);
                }
            }

            return contactos;
        }

        private void ResolverDirecciones(List<OutlookContact> contactos)
        {
            foreach (OutlookContact contacto in contactos)
            {
                CheckTime();

                if (!contacto.Resolved)
                {
                    if (contacto.AddressType == OutlookContact.olAddressTypes.EXCHANGE)
                    {
                        if (contacto.EmailAddress.LastIndexOf("@") == -1)
                            contacto.EmailAddress = appOutlook.ResolveExchangeAddress(contacto);

                        contacto.Resolved = true;
                        contacto.AddressType = OutlookContact.olAddressTypes.SMTP;
                    }

                    Log("   Contacto: " + contacto.FullName + " - Email: " + contacto.EmailAddress);
                }
            }
        }

        private void UpdateContactosWork(List<OutlookContact> contactos)
        {
            OutlookDAL outlookDAL = new OutlookDAL();

            outlookDAL.LogQuery = log_query;

            foreach (OutlookContact contacto in contactos)
            {
                CheckTime();
                outlookDAL.UpdateContactoWork(contacto);
            }            
        }

        private void MoveContactos(string libreta)
        {
            OutlookDAL outlookDAL = new OutlookDAL();

            outlookDAL.LogQuery = log_query;

            outlookDAL.MoveContactos(libreta);
        }

        private void GuardarContactos(List<OutlookContact> contactos, string libreta)
        {
            // guardar los contactos
            OutlookDAL outlookDAL = new OutlookDAL(int.Parse(config.AppSettings.Settings["last_id_inserted"].Value.ToString()));

            outlookDAL.LogQuery = log_query;

            try
            {
                if (use_transaction)
                    outlookDAL.Begin();

                outlookDAL.deleteContacts(libreta);

                foreach (OutlookContact contacto in contactos)
                {
                    // no esperar si estoy en medio de una transaccion
                    if (!use_transaction)
                        CheckTime();

                    outlookDAL.SaveContact(contacto);
                }

                if (use_transaction)
                    outlookDAL.Commit();

                Log("Contactos guardados!");

            }
            catch (System.Exception ex)
            {
                if (use_transaction)
                    outlookDAL.Rollback();

                Log("Exception: " + ex.ToString());
            }

            config.AppSettings.Settings["last_id_inserted"].Value = outlookDAL.LastIdInserted.ToString();
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void CheckTime()
        {
            if (!(DateTime.Now.Hour >= hora_ini && DateTime.Now.Hour <= hora_fin))
            {
                Log("  Esperando " + espera + " minutos .... ");

                System.Threading.Thread.Sleep(espera * 60 * 1000);
                CheckTime();
            }
        }

        private void Log(string mensaje)
        {
            string aux = DateTime.Now.ToString("hh:mm:ss") + ": " + mensaje;
            Trace.WriteLineIf(ZTrace.IsVerbose, aux);
            Console.WriteLine(aux);
        }
    }
}