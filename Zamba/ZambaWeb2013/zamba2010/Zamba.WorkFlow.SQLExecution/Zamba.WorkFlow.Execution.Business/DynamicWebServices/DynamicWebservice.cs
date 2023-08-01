using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Web.Services.Description;
using System.Xml.Serialization;
using Microsoft.CSharp;
using System.Diagnostics;
using Zamba.Core;

namespace Zamba.WorkFlow.Business
{
    public class DynamicWebservice
    {
        #region Atributos
        private Type service;
        #endregion

        /// <summary>
        /// Consume 1 metodo de 1 webservice y devuelve el valor de retorno.
        /// </summary>
        /// <param name="wsdl"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object Consume(string wsdl, string methodName, ref object[] Parameters, Boolean _useCredentials)
        {

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "----------------------------------------------------");
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Dentro de consume en WebService");
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Valor de las variables: ");
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "wsdl: " + wsdl);
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "methodName: " + methodName);
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Parameters: " + Parameters);

            MethodInfo method = GetMethod(wsdl, methodName, _useCredentials);

            if (null == method)
                throw new Exception("Metodo no encontrado en el webservice");

            //  object[] Parameters = new object[parameters.Count];
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Parametros del Metodo: " + methodName);
            ParameterInfo[] p = method.GetParameters();

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Parametros encontrados: " + p.Length);

            for (int i = 0; i < p.Length; i++)
            {
                if (String.Compare(Parameters[i].ToString().ToUpper().Trim(), "NOTHING") == 0)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se asigna al parametros nro " + (i + 1).ToString() + " valor null");
                    Parameters[i] = null;
                }
                else
                {
                    try
                    {
                        //Se le saca el "&" para que no tire error al pasarle un parametro byref
                        string strTipo = ((ParameterInfo)p.GetValue(i)).ParameterType.ToString();
                        strTipo = strTipo.Replace("&", String.Empty);

                        // Se le asignan valores por defecto para no tener conflictos con el casteo
                        if (Parameters[i].GetType().ToString() == typeof(object).ToString())
                        {
                            if (strTipo == typeof(Int16).ToString() ||
                                strTipo == typeof(Int32).ToString() ||
                                strTipo == typeof(Int64).ToString() ||
                                strTipo == typeof(Boolean).ToString())
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se asigna al parametro nro " + (i + 1).ToString() + " de tipo: " + strTipo + " valor 0");
                                Parameters[i] = 0;
                            }

                            if (strTipo == typeof(string).ToString())
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se asigna al parametro nro " + (i + 1).ToString() + " de tipo: " + strTipo + " valor vacio");
                                Parameters[i] = string.Empty;
                            }

                            if (strTipo == typeof(DateTime).ToString())
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se asigna al parametro nro " + (i + 1).ToString() + " de tipo: " + strTipo + " el valor de fecha Actual " + DateTime.Now.ToString());
                                Parameters[i] = DateTime.Now;
                            }
                        }

                        Parameters[i] = Convert.ChangeType(Parameters[i], System.Type.GetType(strTipo));
                    }
                    catch (Exception ex)
                    {
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "ERROR al asignar valor al parametro nro " + (i + 1).ToString() + " " + ex.Message);
                        ZClass.raiseerror(ex);
                    }
                }
            }

            //Invoke Method
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Invocando");
            object obj = Activator.CreateInstance(service);
            object ReturnObject = method.Invoke(obj, Parameters);
            return ReturnObject;
        }

        /// <summary>
        /// Devuelve 1 metodo de 1 webservice
        /// </summary>
        /// <param name="wsdl"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private MethodInfo GetMethod(string wsdl, string methodName, Boolean _useCredentials)
        {
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Endpoint de wsdl: " + wsdl);
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Usar credenciales: " + _useCredentials);
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Instanciando el servicio");
            InstanceService(wsdl, _useCredentials);
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Servicio instanciado");

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando metodo: " + methodName);

            foreach (MethodInfo CurrentMethod in service.GetMethods())
            {
                if (string.Compare(CurrentMethod.Name, methodName) == 0)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Metodo encontrado: " + CurrentMethod);
                    return CurrentMethod;
                }
                else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Otro metodo: " + CurrentMethod);
            }

            return null;
        }
        /// <summary>
        /// Instacia el contenido de 1 webservice en memoria local.
        /// </summary>
        /// <param name="wsdl"></param>
        private void InstanceService(String wsdl, Boolean _useCredentials)
        {
            Uri uri = null;
            WebRequest webRequest = null;
            Stream requestStream = null;
            ServiceDescription sd = null;
            string sdName = null;
            ServiceDescriptionImporter servImport = null;
            CodeNamespace nameSpace = null;
            CodeCompileUnit codeCompileUnit = null;
            StringWriter stringWriter = null;
            CSharpCodeProvider prov = null;
            CompilerParameters param = null;
            CompilerResults results = null;

            try
            {
                uri = new Uri(wsdl);

                webRequest = WebRequest.Create(uri);

                if (_useCredentials == true)
                {
                    webRequest.PreAuthenticate = true;
                    webRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }

                requestStream = webRequest.GetResponse().GetResponseStream();

                // Get a WSDL file describing a service
                sd = ServiceDescription.Read(requestStream);
                sdName = sd.Services[0].Name;

                // Initialize a service description servImport
                servImport = new ServiceDescriptionImporter();
                servImport.AddServiceDescription(sd, String.Empty, String.Empty);
                servImport.ProtocolName = "Soap";
                servImport.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;

                nameSpace = new CodeNamespace();
                codeCompileUnit = new CodeCompileUnit();
                codeCompileUnit.Namespaces.Add(nameSpace);

                //Si no hubieron advertencias
                if (servImport.Import(nameSpace, codeCompileUnit) == 0)
                {
                    stringWriter = new StringWriter(CultureInfo.CurrentCulture);
                    prov = new CSharpCodeProvider();
                    prov.GenerateCodeFromNamespace(nameSpace, stringWriter, new CodeGeneratorOptions());

                    // Compile the assembly with the appropriate references
                    string[] assemblyReferences = { "System.Web.Services.dll", "System.Xml.dll", "System.Data.dll", "mscorlib.dll" };
                    param = new CompilerParameters(assemblyReferences);
                    param.GenerateExecutable = false;
                    param.GenerateInMemory = true;
                    param.TreatWarningsAsErrors = false;
                    param.WarningLevel = 4;

                    results = new CompilerResults(new TempFileCollection());
                    results = prov.CompileAssemblyFromDom(param, codeCompileUnit);
                    service = results.CompiledAssembly.GetType(sdName);
                }
            }
           
            finally
            {
                if (uri != null)
                    uri = null;

                if (webRequest != null)
                    webRequest = null;

                if (requestStream != null)
                {
                    requestStream.Close();
                    requestStream.Dispose();
                    requestStream = null;
                }

                if (sd != null)
                    sd = null;

                sdName = null;

                if (servImport != null)
                    servImport = null;

                if (nameSpace != null)
                    nameSpace = null;

                if (codeCompileUnit != null)
                    codeCompileUnit = null;

                if (stringWriter != null)
                {
                    stringWriter.Close();
                    stringWriter.Dispose();
                    stringWriter = null;
                }

                if (prov != null)
                {
                    prov.Dispose();
                    prov = null;
                }

                if (param != null)
                    param = null;

                if (results != null)
                    results = null;
            }
        }
    }
}
