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
using System.Web.Services.Protocols;
using Zamba.Core.Cache;

namespace Zamba.WorkFlow.Business
{
    public class DynamicWebservice
    {
        /// <summary>
        /// Consume 1 metodo de 1 webservice y devuelve el valor de retorno.
        /// </summary>
        /// <param name="wsdl"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object Consume(string wsdl, string methodName, ref object[] Parameters, Boolean useCredentials, int timeOut)
        {
            ZTrace.WriteLineIf(ZTrace.IsInfo, "----------------------------------------------------");
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Dentro de consume en WebService");
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor de las variables: ");
            ZTrace.WriteLineIf(ZTrace.IsInfo, "wsdl: " + wsdl);
            ZTrace.WriteLineIf(ZTrace.IsInfo, "methodName: " + methodName);
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Parameters: " + Parameters);

            MethodInfo method;
            ParameterInfo[] p;
            Type service;
            object obj;

            string wsCompleteKey = wsdl + "|" + methodName + "|" + useCredentials.ToString();
            string wsTypeKey = wsdl + "|" + useCredentials.ToString();

            try
            {
                service = GetServiceType(wsTypeKey, wsdl, useCredentials);
                method = GetMethod(wsCompleteKey,methodName, service);
                p = GetParameterInfo(wsCompleteKey, method);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor obtenido de GetParameters: " + p);

                if (p.Length > 0)
                {
                    string strInt16Type = typeof(Int16).ToString();
                    string strInt32Type = typeof(Int32).ToString();
                    string strInt64Type = typeof(Int64).ToString();
                    string strBooleanType = typeof(Boolean).ToString();

                    for (int i = 0; i < p.Length; i++)
                    {
                        if (String.Compare(Parameters[i].ToString().ToUpper().Trim(), "NOTHING") == 0)
                            Parameters[i] = null;
                        else
                        {
                            try
                            {
                                //Se le saca el "&" para que no tire error al pasarle un parametro byref
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Tipo de parametro: " + ((ParameterInfo)p.GetValue(i)).ParameterType.ToString());
                                string strTipo = ((ParameterInfo)p.GetValue(i)).ParameterType.ToString().Replace("&", String.Empty);

                                // Se le asignan valores por defecto para no tener conflictos con el casteo
                                if (Parameters[i].GetType().ToString() == typeof(object).ToString())
                                {
                                    if (strTipo == strInt16Type ||
                                        strTipo == strInt32Type ||
                                        strTipo == strInt64Type ||
                                        strTipo == strBooleanType)
                                    {
                                        Parameters[i] = 0;
                                    }

                                    if (strTipo == typeof(string).ToString()) Parameters[i] = string.Empty;
                                    if (strTipo == typeof(DateTime).ToString()) Parameters[i] = DateTime.Now;
                                }

                                Parameters[i] = Convert.ChangeType(Parameters[i], System.Type.GetType(strTipo));
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor de  Parameters[i] " + Parameters[i]);
                            }
                            catch (Exception ex)
                            {
                                ZClass.raiseerror(ex);
                            }
                        }
                    }
                }

                //Invoke Method
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Invocando");
                obj = GetServiceInstance(wsCompleteKey, service, timeOut);
                return method.Invoke(obj, Parameters);
            }
            finally
            {      
                obj = null;
                p = null;
                method = null;
                service = null;
            }
        }

        /// <summary>
        /// Obtiene la instancia del tipo del servicio
        /// </summary>
        /// <param name="wsTypeKey">Clave de la instancia del servicio</param>
        /// <param name="wsdl">Wsdl del servicio en caso de no estar instanciado</param>
        /// <param name="useCredentials">Credenciales en caso de no estar instanciado el servicio</param>
        /// <returns>Tipo del servicio</returns>
        private Type GetServiceType(string wsTypeKey, string wsdl, bool useCredentials)
        {
            if (!WebServices.hsWebServiceTypes.Contains(wsTypeKey))
                WebServices.hsWebServiceTypes.Add(wsTypeKey, InstanceService(wsdl, useCredentials));
            
            if (WebServices.hsWebServiceTypes[wsTypeKey] == null)
                throw new Exception("Webservice no instanciado");
            else
                return (Type)WebServices.hsWebServiceTypes[wsTypeKey];
        }

        /// <summary>
        /// Obtiene la instancia del webmethod
        /// </summary>
        /// <param name="wsCompleteKey">Clave de la instancia del webmethod</param>
        /// <param name="methodName">Nombre del webmethod en caso de no estar instanciado</param>
        /// <param name="service">Servicio en caso de no estar instanciado el webservice</param>
        /// <returns>Webmethod instanciado del servicio</returns>
        private MethodInfo GetMethod(string wsCompleteKey, string methodName, Type service)
        {
            if (!WebServices.hsWebMethods.Contains(wsCompleteKey))
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando el método " + methodName);
                foreach (MethodInfo method in service.GetMethods())
                {
                    if (string.Compare(method.Name, methodName) == 0)
                    {
                        WebServices.hsWebMethods.Add(wsCompleteKey, method);
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El método fué encontrado");
                        break;
                    }
                }
            }

            if (WebServices.hsWebMethods[wsCompleteKey] == null)
                throw new Exception("Método no encontrado en el webservice");
            else
                return (MethodInfo)WebServices.hsWebMethods[wsCompleteKey];
        }

        /// <summary>
        /// Obtiene la información de los parametros
        /// </summary>
        /// <param name="wsCompleteKey">Clave de la instancia de los parametros</param>
        /// <param name="method">Webmethod en caso de no estar instanciados los parametros</param>
        /// <returns>Conjunto de parametros del webmethod</returns>
        private ParameterInfo[] GetParameterInfo(string wsCompleteKey, MethodInfo method)
        {
            if (!WebServices.hsWebMethodsParameters.Contains(wsCompleteKey))
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Entrando a GetParameters");
                WebServices.hsWebMethodsParameters.Add(wsCompleteKey, method.GetParameters());
            }
            return (ParameterInfo[])WebServices.hsWebMethodsParameters[wsCompleteKey];
        }

        /// <summary>
        /// Obtiene la instancia de un servicio
        /// </summary>
        /// <param name="wsTypeKey">Clave de la instancia del servicio</param>
        /// <param name="service">Tipo del servicio en caso de no encontrarse instanciado</param>
        /// <returns>La instancia del webservice lista para ejecutar el webmethod</returns>
        private Object GetServiceInstance(string wsTypeKey, Type service, int timeOut)
        {
            if (!WebServices.hsWebServiceInstances.Contains(wsTypeKey))
            {
                object obj = Activator.CreateInstance(service);
                PropertyInfo propInfo = service.GetProperty("Credentials");
                propInfo.SetValue(obj, System.Net.CredentialCache.DefaultCredentials, null);
                propInfo = service.GetProperty("Timeout");
                propInfo.SetValue(obj, timeOut, null);
                WebServices.hsWebServiceInstances.Add(wsTypeKey, obj);
            }

            return WebServices.hsWebServiceInstances[wsTypeKey];
        }

        /// <summary>
        /// Instacia el contenido de 1 webservice en memoria local.
        /// </summary>
        /// <param name="wsdl"></param>
        private Type InstanceService(String wsdl, Boolean _useCredentials)
        {
            Uri uri;
            WebRequest webRequest;
            Stream requestStream=null;
            ServiceDescription sd;
            ServiceDescriptionImporter servImport;
            CodeNamespace nameSpace;
            CodeCompileUnit codeCompileUnit;
            StringWriter stringWriter = null;
            CSharpCodeProvider prov=null;
            CompilerParameters param;
            CompilerResults results;
            Assembly assembly;

            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor de wsdl: " + wsdl);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Usar credenciales: " + _useCredentials);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Instanciando el servicio");

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Servicio instanciado");

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
                string sdName = sd.Services[0].Name;

                // Initialize a service description servImport
                servImport = new ServiceDescriptionImporter();
                servImport.AddServiceDescription(sd, String.Empty, String.Empty);
                servImport.ProtocolName = "Soap";
                servImport.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;

                nameSpace = new CodeNamespace();
                codeCompileUnit = new CodeCompileUnit();
                codeCompileUnit.Namespaces.Add(nameSpace);
                // Set Warnings
                ServiceDescriptionImportWarnings warnings = servImport.Import(nameSpace, codeCompileUnit);

                if (warnings == 0)
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

                    results = prov.CompileAssemblyFromDom(param, codeCompileUnit);
                    assembly = results.CompiledAssembly;
                    return assembly.GetType(sdName);
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Warning: " + warnings);
                    return null;
                }
            }
            finally
            {
                if (requestStream!=null)
                {
                    requestStream.Dispose();
                    requestStream = null;
                }

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

                assembly = null;
                results = null;
                param = null;
                codeCompileUnit = null;
                nameSpace = null;
                servImport = null;
                sd = null;
                webRequest = null;
                uri = null;
            }
        }
    }
}