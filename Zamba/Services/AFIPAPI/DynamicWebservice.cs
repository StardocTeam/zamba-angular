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
            MethodInfo method = GetMethod(wsdl, methodName, _useCredentials);

            if (null == method)
                throw new Exception("Metodo no encontrado en el webservice");

            ParameterInfo[] p = method.GetParameters();
       
            for (int i = 0; i < p.Length; i++)
            {
                if (String.Compare(Parameters[i].ToString().ToUpper().Trim(), "NOTHING") == 0)
                    Parameters[i] = null;
                else
                {
                    
                        //Se le saca el "&" para que no tire error al pasarle un parametro byref
                        string strTipo = ((ParameterInfo)p.GetValue(i)).ParameterType.ToString();
                         strTipo = strTipo.Replace("&", String.Empty);
                        
                        
                        // Se le asignan valores por defecto para no tener conflictos con el casteo
                        if (Parameters[i].GetType().ToString() == typeof( object ).ToString())
                        {
                            if (strTipo == typeof(Int16).ToString() ||
                                strTipo == typeof(Int32).ToString() ||
                                strTipo == typeof(Int64).ToString() ||
                                strTipo == typeof(Boolean).ToString() )
                            {
                                Parameters[i] = 0;
                            }

                            if (strTipo == typeof(string).ToString()) Parameters[i] = string.Empty;
                            
                            if (strTipo == typeof(DateTime).ToString()) Parameters[i]= DateTime.Now;
                        
                        
                        }
                        
                        Parameters[i] = Convert.ChangeType(Parameters[i], System.Type.GetType(strTipo));  

                   
                }
            }

            //Invoke Method
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
     
            InstanceService(wsdl, _useCredentials);
       

            foreach (MethodInfo CurrentMethod in service.GetMethods())
            {
                if (string.Compare(CurrentMethod.Name, methodName) == 0)
                    return CurrentMethod;
    
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
            catch (Exception ex)
            {
                throw ex;
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
