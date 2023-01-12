using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Zamba.SetupLibrary
{
    /// <summary>
    /// Contiene metodos para formatear el trace.
    /// </summary>
    public class HelperTrace
    {
        private static bool isLoad = false;
        private const string TRACE_NAME = "SetupExportaOutlook.log";
        private const string FOLDER_NAME = "\\ZambaOutlook";

        private const string LARGE_SEP = "--------------------------------------------------------------------------------";
        private const string SMOLL_SEP = "--------------------";
        private const string FORMAT_DATE_DETAILS = "yyyyMMdd HH:mm:ss.fff";
        private const string FORMAT_DATE = "HH:mm:ss.fff";
        private const string WRITEFORMATBLOCK = "-- {0} |{1}";
        private const string WRITEFORMATINITMETHOD = "@{0} |{1}";
        private const string WRITEQUESTIONFORMAT = "{0} --> {1} |{2}";

        public static void WriteFormatInitBlock(string valor)
        {
            TraceWrite.WriteLine(LARGE_SEP);
            TraceWrite.WriteLine(string.Format(WRITEFORMATBLOCK, valor, DateTime.Now.ToString(FORMAT_DATE_DETAILS)));
            TraceWrite.WriteLine(LARGE_SEP);
            TraceWrite.Flush();
        }

        public static void WriteBlock(string valor)
        {
            TraceWrite.WriteLine(SMOLL_SEP);
            TraceWrite.WriteLine(valor);
            TraceWrite.WriteLine(SMOLL_SEP);
            TraceWrite.Flush();
        }

        public static void WriteFormatInitMethod(string valor)
        {
            TraceWrite.WriteLine(SMOLL_SEP);
            TraceWrite.WriteLine(string.Format(WRITEFORMATINITMETHOD, valor, DateTime.Now.ToString(FORMAT_DATE)));
            TraceWrite.IndentLevel++;
            TraceWrite.Flush();
        }

        public static void WriteFormatEndMethod(string valor)
        {
            TraceWrite.IndentLevel--;
            TraceWrite.WriteLine(string.Format(WRITEFORMATINITMETHOD, valor, DateTime.Now.ToString(FORMAT_DATE)));
            TraceWrite.WriteLine(SMOLL_SEP);
            TraceWrite.Flush();
        }

        public static void WriteQuestionFormat(string question, string valor)
        {
            TraceWrite.WriteLine(string.Format(WRITEQUESTIONFORMAT, question, valor, DateTime.Now.ToString(FORMAT_DATE)));
            TraceWrite.Flush();
        }

        public static void WriteQuestionFormat(string question)
        {
            TraceWrite.WriteLine(string.Format(WRITEQUESTIONFORMAT, question, string.Empty, DateTime.Now.ToString(FORMAT_DATE)));
            TraceWrite.Flush();
        }

        public static void WriteAnswerFormat(string valor)
        {
            TraceWrite.WriteLine(string.Format(WRITEQUESTIONFORMAT, string.Empty, valor, DateTime.Now.ToString(FORMAT_DATE)));
            TraceWrite.Flush();
        }

        public static void WriteFormatEndBlock(string valor)
        {
            TraceWrite.WriteLine(LARGE_SEP);
            TraceWrite.WriteLine(string.Format(WRITEFORMATBLOCK, valor, DateTime.Now.ToString(FORMAT_DATE_DETAILS)));
            TraceWrite.WriteLine(LARGE_SEP);
            TraceWrite.Flush();
        }

        /// <summary>
        /// Propiedad que inicia el trace 
        /// si no se inicio y retorna el trace especifico
        /// para esta aplicacion
        /// </summary>
        private static TraceListener TraceWrite
        {
            get
            {
                if (!isLoad)
                    initTrace();
                return Trace.Listeners[TRACE_NAME];
            }
        }

        /// <summary>
        /// Inicia el trace
        /// </summary>
        /// <remarks>
        /// osanchez - 14/5/2009
        /// </remarks>
        private static void initTrace()
        {
            string _temp = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + FOLDER_NAME;
            if (Directory.Exists(_temp) == false)
                Directory.CreateDirectory(_temp);
            TextWriterTraceListener trl = new TextWriterTraceListener(Path.Combine(_temp, TRACE_NAME), TRACE_NAME);
            if (!findTrace(trl))
            {
                Trace.Listeners.Add(trl);
                HelperTrace.WriteFormatInitBlock("Trace Ok");
                HelperTrace.WriteAnswerFormat("LocationProgram: " + System.Windows.Forms.Application.StartupPath);
                isLoad = true;
            }
        }

        /// <summary>
        /// Busca si ya existe el trace para el programa
        /// </summary>
        /// <param name="tr">TextWriterTraceListener</param>
        /// <returns>true si existe</returns>
        /// <remarks>
        /// osanchez - 13/5/2009
        /// </remarks>
        private static bool findTrace(TextWriterTraceListener tr)
        {
            bool ret = false;
            foreach (TraceListener trl in Trace.Listeners)
            {
                if (trl is TextWriterTraceListener)
                {
                    if (trl.Name == tr.Name)
                    {
                        ret = true;
                        break;
                    }
                }
            }
            return ret;
        }
    }
}