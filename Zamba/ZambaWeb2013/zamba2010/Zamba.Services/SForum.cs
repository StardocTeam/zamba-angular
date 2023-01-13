using System;
using System.Collections.Generic;
using System.Text;
using Zamba.Core;
using System.Data;
using System.Collections;

namespace Zamba.Services
{
    public class SForum : IService
    {
        #region Attributes
        ZForoBusiness ZForoBusiness = null;
        #endregion

        #region Constructor
        public SForum()
        {
            ZForoBusiness = new ZForoBusiness();
        }
        #endregion

        #region IService Members

        public ServicesTypes ServiceType()
        {
            return ServicesTypes.Forum;
        }

        #endregion

        #region Methods

        public void GetAllMessages(long DocId, long DocTypeId, ref ArrayList ArrayMensajes, ref ArrayList ArrayRespuestas, bool CheckIfVersionMessagesShouldShow,IUser user)
        {
            ZForoBusiness.GetAllMessages(DocId, DocTypeId, ref ArrayMensajes, ref ArrayRespuestas, CheckIfVersionMessagesShouldShow);
        }

        public DataTable GetAttachsNames(int messageId)
        {
            return ZForoBusiness.GetAttachsNames(messageId);
        }

        public DataTable GetAttachFileByFileName(int messageId, string fileName)
        {
            return ZForoBusiness.GetAttachFileByFileName(messageId, fileName);
        }

        public bool ValidateExistMessage(int messageId)
        {
            return ZForoBusiness.GetIfExistMessage(messageId);
        }

        public bool ValidateExistAttach(int messageId)
        {
            return ZForoBusiness.GetIfExistAttach(messageId);
        }

        public long GetCreatorId(int messageId)
        {
            return ZForoBusiness.GetCreatorId(messageId);
        }

        public DataTable GetUserAndGroupsParticipantsId(int messageId)
        {
            return ZForoBusiness.GetUserAndGroupsParticipantsById(messageId);
        }

        public List<Int64> GetUserAndGroupsParticipantsIdAsList(int messageId)
        {
            return ZForoBusiness.GetUserAndGroupsParticipantsByIdAsList(messageId);
        }

        public DataTable GetInformation(int messageId)
        {
            //return _zForoBusiness.GetInformacion(messageId);
            return ZForoBusiness.GetInformation(messageId);
        }

        public string GetForumMails(long docId, int idMensaje, long parentId)
        {
            return ZForoBusiness.GetForumMails(docId, idMensaje, parentId);
        }

        public DataTable GetRealtedDocs(int parentId, long docId)
        {
            return ZForoBusiness.GetRealtedDocs(parentId, docId);
        }

        public void DeleteMessage(int messageId)
        {
            ZForoBusiness.DeleteMessage(messageId);
        }

        public void InsertAttach(int idMensaje,ref byte []file, Int64 maxLength, string fileName)
        {
            ZForoBusiness.InsertAttach(idMensaje, ref file, maxLength, fileName);
        }

        public void InsertMessage(int IdMensaje, int ParentId, string LinkName, string Mensaje, long UserId)
        {
            ZForoBusiness.InsertMessage(IdMensaje, ParentId, LinkName, Mensaje, UserId);
        }

        public void InsertMessageParticipant(int IdMensaje, long UserId)
        {
            ZForoBusiness.InsertMessageParticipant(IdMensaje, UserId);
        }

        public void RemoveMessageParticipants(int IdMensaje)
        {
            ZForoBusiness.RemoveParticipants(IdMensaje);
        }

        public void InsertMessageParticipants(int IdMensaje, List<long> UserIds)
        {
            ZForoBusiness.InsertMessageParticipants(IdMensaje, UserIds);
        }

        public void InsertMessageDoc(int IdMensaje, long DocId, long Doctypeid)
        {
            ZForoBusiness.InsertMessageDoc(IdMensaje, DocId, Doctypeid);
        }

        public void InsertAttachInAExistRecord(int MessageID, ref byte[] file, Int64 maxLength, string fileName)
        {
            ZForoBusiness.InsertAttachInAExistRecord(MessageID, ref file, maxLength, fileName);
        }

        /// <summary>
        /// Llama al ws para insertar un attach de foro
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="file"></param>
        /// <param name="userID"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool InsertForumAttachWS(int messageID, byte[] file, long userID, string fileName)
        {
            return ZForoBusiness.InsertForumAttachWS(messageID, ref file, userID, fileName);
        }

        public void InsertForumAttach(int messageID, byte[] file, long userID, string fileName)
        {
            ZOptBusiness zoptb = new ZOptBusiness();

            string maxAttachsSize = zoptb.GetValue("MaxLenghtForumAttach");
            Int64 maxSize;
            if (string.IsNullOrEmpty(maxAttachsSize))
                maxSize = 11111111;
            else
                maxSize = (Int64)Int64.Parse(maxAttachsSize) / 1024;

            //if (ValidateExistAttach(messageID))
            //    InsertAttachInAExistRecord(messageID, ref file, maxSize, fileName);
            //else
                InsertAttach(messageID, ref file, maxSize, fileName);
        }

        /// <summary>
        /// Llama al ws para obtener el attach de foro
        /// </summary>
        /// <param name="MessageID"></param>
        /// <param name="FileName"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public byte[] GetAttachFileByMessageIdAndNameWS(int MessageID, string FileName, long UserID)
        {
            return ZForoBusiness.GetAttachFileByMessageIdAndNameWS(MessageID, FileName, UserID);
        }

        /// <summary>
        /// Obtiene los nombres de attach por mensaje de foro
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string[] GetAttachsNamesByMessageIdWS(int messageId, long userId)
        {
            return ZForoBusiness.GetAttachsNamesByMessageIdWS(messageId, userId);
        }

        public string[] GetAttachsNamesByMessageId(int messageId)
        {
            DataTable tempTable = GetAttachsNames(messageId);
            List<string> returnList = new List<string>();
            int rowCount = tempTable.Rows.Count;

            if (rowCount > 0)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    object fileName = tempTable.Rows[i][0];
                    if (fileName is DBNull || string.IsNullOrEmpty((string)fileName))
                        returnList.Add("ADJUNTO Nº" + (i + 1).ToString());
                    else
                        returnList.Add((string)fileName);
                }

                return returnList.ToArray();
            }
            return null;
        }
        #endregion
    }
}
