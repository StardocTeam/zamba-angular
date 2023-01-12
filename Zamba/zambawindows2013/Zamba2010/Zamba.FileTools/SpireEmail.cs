using Spire.Email;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamba.FileTools
{
    public class SpireEmail
    {

        /// <summary>
        /// Metodo para extraer los archivos adjuntos de un mail con la libreria Spire
        /// retorna diccionario con nombre y contenido de cada adjunto
        /// </summary>
        /// <param name="msgFile">Mail del que extraer los adjuntos</param>
        /// <returns>Retorna un diccionario conteniendo el nombre y contenido de cada archivo adjunto</returns>
        public Dictionary<string,Stream> GetEmailAttachs(string msgFile)
        {
            Dictionary<string,Stream> attachs = new Dictionary<string, Stream>();
            try
            {
                MailMessage mail = MailMessage.Load(msgFile);

                foreach (Attachment attach in mail.Attachments)
                    attachs.Add(attach.ContentType.Name, attach.Data);

            }
            catch (Exception)
            {
                throw;
            }

            return attachs;
        }

        /// <summary>
        /// Metodo que retorna la cantidad de adjuntos de un mail
        /// </summary>
        /// <param name="msgFile">El mail que contiene los adjuntos</param>
        /// <returns>El numero de adjuntos</returns>
        public long GetEmailAttachsCount(string msgFile)
        {
            try
            {
                MailMessage mail = MailMessage.Load(msgFile);
                return mail.Attachments.Count;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Metodo que devuelve una lista con los nombres de los archivos adjuntos
        /// </summary>
        /// <param name="msgFile">El mail que contiene los adjuntos</param>
        /// <returns>List de nombres</returns>
        public List<string> GetEmailAttachsNames(string msgFile)
        {
            try
            {
                MailMessage mail = MailMessage.Load(msgFile);
                return mail.Attachments.Select(a => a.ContentType.Name).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
