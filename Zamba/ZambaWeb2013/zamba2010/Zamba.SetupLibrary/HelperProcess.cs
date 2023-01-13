using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Zamba.SetupLibrary
{
    /// <summary>
    /// Estructura utilizada para indicar que comandos
    /// y que argumentos se deben utilizar para habilitar
    /// o deshabilitar la seguridad para el addin y librerias
    /// adicionales
    /// </summary>
    /// <remarks>
    /// osanchez - 11/05/2009 version inicial
    /// </remarks>
    public struct SecurityAddin
    {
        public string path;        
        public string currentTargetPath;
        public string arg;                
    }
    
    public class HelperProcess
    {
        /// <summary>
        /// Ejecuta las lineas de comando especificadas en
        /// el objeto SecurityAddin
        /// </summary>
        /// <param name="sec">SecurityAddin</param>
        /// <remarks>
        /// osanchez - 11/05/2009 version inicial
        /// </remarks>
        internal void ExecuteSecurityAddin(List<SecurityAddin> lista)
        {
            HelperTrace.WriteFormatInitMethod("ExecuteSecurityAddin");
            List<ProcessStartInfo> listaProcesos;

            listaProcesos = BuilderProcessStarInfo(lista);
            string output = string.Empty;
            using (Process p = new Process())
            {
                foreach (ProcessStartInfo ps in listaProcesos)
                {                   
                    p.StartInfo = ps;
                    HelperTrace.WriteQuestionFormat("execStartProcess");
                    HelperTrace.WriteQuestionFormat("name",ps.FileName);
                    HelperTrace.WriteQuestionFormat("arg",ps.Arguments);
                    p.Start();
                    p.StandardInput.WriteLine("yes");
                    p.WaitForExit();
                    output = p.StandardOutput.ReadToEnd();
                    Debug.WriteLine(output);                    
                }
            }
            HelperTrace.WriteFormatEndMethod("ExecuteSecurityAddin");
        }

        /// <summary>
        /// Crea los objectos ProcessStartInfo 
        /// a partir de SecurityAddin
        /// </summary>
        /// <param name="lista">lista de SecurityAddin</param>
        /// <returns>lista de ProcessStartInfo</returns>
        /// <remarks>
        /// osanchez - 12/05/2009 version inicial
        /// </remarks>
        private List<ProcessStartInfo> BuilderProcessStarInfo(List<SecurityAddin> lista)
        {
            HelperTrace.WriteFormatInitMethod("BuilderProcessStarInfo");

            List<ProcessStartInfo> listaProcesos = new List<ProcessStartInfo>();

            foreach (SecurityAddin sec in lista)
            {
                HelperTrace.WriteQuestionFormat("addProcess");
                HelperTrace.WriteQuestionFormat("name",sec.path);
                HelperTrace.WriteQuestionFormat("arg",sec.arg);
                ProcessStartInfo ps = new ProcessStartInfo(sec.path, sec.arg);
                ps.WindowStyle = ProcessWindowStyle.Hidden;
                ps.CreateNoWindow = true;
                ps.UseShellExecute = false;
                ps.RedirectStandardOutput = true;
                ps.RedirectStandardInput = true;
                ps.RedirectStandardError = true;
                listaProcesos.Add(ps);      
                HelperTrace.WriteAnswerFormat("OK");
            }

            HelperTrace.WriteFormatEndMethod("BuilderProcessStarInfo");

            return listaProcesos;
        }    

        /// <summary>
        /// Copia los archivos de workflow y ini 
        /// de la ruta de instalcion de zamba a la ruta de office 2003
        /// si existe sobreescribe.
        /// </summary>
        internal void UpdateDependenciesZamba(string source,string target)
        {
            HelperTrace.WriteFormatInitMethod("UpdateDependenciesZamba");
            BulkCopyFile(source, target, "Zamba.W*.dll");
            BulkCopyFile(source, target, "*.ini");
            HelperTrace.WriteFormatEndMethod("UpdateDependenciesZamba");
        }

        /// <summary>
        /// Copia los archivos segun el patron ingresado.
        /// Si existe sobreescribe.
        /// </summary>
        /// <param name="source">desde donde copia</param>
        /// <param name="target">hacia donde copia</param>
        /// <param name="pattern">patron de busqueda</param>
        /// <remarks>
        /// osanchez - 12/05/2009 version inicial
        /// </remarks>
        private void BulkCopyFile(string source, string target, string pattern)
        {
            HelperTrace.WriteFormatInitMethod("BulkCopyFile");
            HelperTrace.WriteQuestionFormat("from", source);
            HelperTrace.WriteQuestionFormat("execGetFile: pattern ",pattern);
            string[] lista = Directory.GetFiles(source, pattern, SearchOption.TopDirectoryOnly);
            if (lista != null)
            {
                HelperTrace.WriteAnswerFormat("OK, count=" + lista.Length.ToString());
                string despath;
                foreach (string f in lista)
                {
                    despath = Path.Combine(target, Path.GetFileName(f));
                    HelperTrace.WriteAnswerFormat("copy " + despath);
                    File.Copy(f, despath,true);                    
                }
            }
            HelperTrace.WriteFormatEndMethod("BulkCopyFile");
        }

    }


}
