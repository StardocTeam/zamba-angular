using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamba.Tools;

namespace Zamba.FUS
{
   public class SQLInjection
    {
        public Boolean FixInputInjection(ref string indexValue)
        {
            const string invalidCharacters = "--,;,',exec,loop,declare,";
            bool InjectionDetected = false;
            foreach (string c in invalidCharacters.Split(char.Parse(","))) {
                while (indexValue.Contains(c))
                {
//                    ZTrace.WriteLineIf(ZTrace.IsError, "Injection detected!!!, Blocked: " indexValue);
                    indexValue = indexValue.Replace(c, "");
                    InjectionDetected = true;
                }
            }

            return !InjectionDetected;
        }

        //class SurroundingClass
        //{
        //    /// <summary>

        //    ///     ''' Verifica si el result posee un documento encriptado, verificando si existe su registro en ZSer

        //    ///     ''' </summary>

        //    ///     ''' <param name="docId"></param>

        //    ///     ''' <param name="docTypeId"></param>

        //    ///     ''' <returns></returns>

        //    ///     ''' <remarks></remarks>
        //    public bool IsDocumentEncrypted(long docId, long docTypeId)
        //    {
        //        string sKey = docId + "|" + docTypeId;
        //        if (!Cache.Results.CacheEncryptedDocumets.ContainsKey(sKey))
        //        {
        //            ResultFactoryExt resF = new ResultFactoryExt();
        //            long encResult = resF.GetEnccryptionCount(docId, docTypeId);
        //            Cache.Results.CacheEncryptedDocumets(sKey) = encResult > 0;
        //        }

        //        return Cache.Results.CacheEncryptedDocumets(sKey);
        //    }

        //    /// <summary>
        //    ///     ''' Obtiene la password de encriptacion/decriptacion, ya decodificada.
        //    ///     ''' </summary>
        //    ///     ''' <param name="docId"></param>
        //    ///     ''' <param name="docTypeId"></param>
        //    ///     ''' <returns></returns>
        //    ///     ''' <remarks></remarks>
        //    private string GetDecryptionPassword(long docId, long docTypeId)
        //    {
        //        string sKey = docId + "|" + docTypeId;

        //        if (!Cache.Results.CacheDecryptPassword.ContainsKey(sKey))
        //        {
        //            ResultFactoryExt resF = new ResultFactoryExt();
        //            string sPasswordDB = resF.GetFilePassword(docId, docTypeId);
        //            string decodedPass = Encryption.DecryptString(sPasswordDB);

        //            Cache.Results.CacheDecryptPassword(sKey) = decodedPass;
        //        }

        //        return Cache.Results.CacheDecryptPassword(sKey);
        //    }

        //    /// <summary>
        //    ///     ''' Copia el documento a un temporal y lo desencripta si la password de parametro coincide con la de la DB.
        //    ///     ''' </summary>
        //    ///     ''' <param name="result"></param>
        //    ///     ''' <param name="decrypPass"></param>
        //    ///     ''' <returns></returns>
        //    ///     ''' <remarks></remarks>
        //    public string CopyAndDecrypt(IResult result, string decrypPass)
        //    {
        //        if (string.IsNullOrEmpty(result.FullPath))
        //            throw new NotImplementedException("Encriptacion no soportada para archivos blob");

        //        // Password con la que fue encriptado el archivo
        //        string docDBPassword = GetDecryptionPassword(result.ID, result.DocTypeId);
        //        // Password ingresada
        //        string decryptionPassword = result.ID + decrypPass + result.DocTypeId;

        //        if (docDBPassword == decryptionPassword)
        //        {
        //            CryptoFileManager encTool = new CryptoFileManager();
        //            byte[] fileKey;
        //            byte[] fileKeyIV;
        //            encTool.getKeysFromPassword(decryptionPassword, fileKey, fileKeyIV);
        //            System.IO.DirectoryInfo dir;
        //            dir = Zamba.Tools.EnvironmentUtil.GetTempDir(@"\OfficeTemp");
        //            if (dir.Exists == false)
        //                dir.Create();
        //            string name = result.ID.ToString();
        //            FileInfo fTemp;

        //            if (result.FullPath(result.FullPath.Length - 4) == ".")
        //                name = name + result.FullPath.Substring(result.FullPath.Length - 4);
        //            else if (result.FullPath(result.FullPath.Length - 5) == ".")
        //                name = name + result.FullPath.Substring(result.FullPath.Length - 5);
        //            if (result.IsExcel || result.IsWord)
        //                // Esto evita el error de abrir 2 excel con el mismo nombre (abrirlo en resultado y tareas)
        //                fTemp = new FileInfo(FileBusiness.GetUniqueFileName(dir.FullName, name));
        //            else
        //                fTemp = new FileInfo(dir.FullName + @"\" + name);
        //            name = null;

        //            encTool.DecryptData(result.FullPath, fTemp.FullName, fileKey, fileKeyIV);

        //            UserBusiness.Rights.SaveAction(result.ID, ObjectTypes.Documents, RightsType.Decrypt, "El usuario: " + Membership.MembershipHelper.CurrentUser.Name + " ha desencriptado el documento");
        //            return fTemp.FullName;
        //        }
        //        else
        //            return string.Empty;
        //    }

        //    /// <summary>
        //    ///     ''' Encripta un documento y lo copia al full path del result
        //    ///     ''' </summary>
        //    ///     ''' <param name="result"></param>
        //    ///     ''' <param name="localPath"></param>
        //    ///     ''' <returns></returns>
        //    ///     ''' <remarks></remarks>
        //    public bool EncryptAndCopy(IResult result, string localPath)
        //    {
        //        if (string.IsNullOrEmpty(result.FullPath) || string.IsNullOrEmpty(localPath))
        //            throw new NotImplementedException("Encriptacion no soportada para archivos blob");

        //        string docDBPassword = GetDecryptionPassword(result.ID, result.DocTypeId);
        //        byte[] fileKey;
        //        byte[] fileKeyIV;
        //        CryptoFileManager encTool = new CryptoFileManager();
        //        encTool.getKeysFromPassword(docDBPassword, fileKey, fileKeyIV);

        //        encTool.EncryptData(localPath, result.FullPath, fileKey, fileKeyIV);

        //        return true;
        //    }
        //}

    }
}
