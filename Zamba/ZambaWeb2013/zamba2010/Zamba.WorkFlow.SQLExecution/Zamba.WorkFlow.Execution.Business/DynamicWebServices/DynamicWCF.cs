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
//para WCF dinamico
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Data.Services;
using System.Data.Services.Common;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

namespace Zamba.WorkFlow.Business//.DynamicWebServices
{
    public class DynamicWCF
    {

        public object Consume(string wsdl, string contract, string methodName, ref object[] Parameters, Boolean useCredentials)
        {
            CompilerResults compilerResults = null;

            object proxyInstance = GetWCFInstance(ref compilerResults, wsdl, contract);
            string operationName = methodName;
            var methodInfo = proxyInstance.GetType().GetMethod(operationName);
            object[] operationParameters = Parameters;

            #region Trace
            ZTrace.WriteLineIf(ZTrace.IsInfo, "methodName: " + methodName);
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Parameters: " + Parameters);
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Invoco metodo " + methodName);
            #endregion
            object ReturnObject = methodInfo.Invoke(proxyInstance, BindingFlags.InvokeMethod, null, operationParameters, null);
            return ReturnObject;
        }
        private object GetWCFInstance(ref CompilerResults compilerResults, string WCFAddress, string contractName)
        {
            #region Trace
            ZTrace.WriteLineIf(ZTrace.IsInfo, "----------------------------------------------------");
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Dentro de consume en GetWCFInstance");
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor de las variables: ");
            ZTrace.WriteLineIf(ZTrace.IsInfo, "wsdl: " + WCFAddress);
            ZTrace.WriteLineIf(ZTrace.IsInfo, "contractName: " + contractName);
            #endregion

            //http://www.c-sharpcorner.com/uploadfile/25c78a/consuming-wcf-service-via-reflection/
            object proxyInstance = null;
            // Define the WSDL Get address, contract name and parameters, with this we can extract WSDL details any time
            Uri address = new Uri(WCFAddress);
            // For HttpGet endpoints use a Service WSDL address a mexMode of .HttpGet and for MEX endpoints use a MEX address and a mexMode of .MetadataExchange
            MetadataExchangeClientMode mexMode = MetadataExchangeClientMode.HttpGet;
            //string contractName = "IService1";
            // Get the metadata file from the service.
            MetadataExchangeClient metadataExchangeClient = new MetadataExchangeClient(address, mexMode);
            metadataExchangeClient.ResolveMetadataReferences = true;

            //One can also provide credentials if service needs that by the help following two lines.
            //ICredentials networkCredential = new NetworkCredential("", "", "");
            //metadataExchangeClient.HttpCredentials = networkCredential;

            //Gets the meta data information of the service.
            MetadataSet metadataSet = metadataExchangeClient.GetMetadata();

            // Import all contracts and endpoints.
            WsdlImporter wsdlImporter = new WsdlImporter(metadataSet);
            Collection<ContractDescription> contracts = wsdlImporter.ImportAllContracts();
            ServiceEndpointCollection allEndpoints = wsdlImporter.ImportAllEndpoints();

            // Generate type information for each contract.
            ServiceContractGenerator serviceContractGenerator = new ServiceContractGenerator();

            //Dictinary has been defined to keep all the contract endpoints present, contract name is key of the dictionary item.
            var endpointsForContracts = new Dictionary<string, IEnumerable<ServiceEndpoint>>();

            foreach (ContractDescription contract in contracts)
            {
                serviceContractGenerator.GenerateServiceContractType(contract);
                // Keep a list of each contract's endpoints.
                endpointsForContracts[contract.Name] = allEndpoints.Where(ep => ep.Contract.Name == contract.Name).ToList();
            }

            // Generate a code file for the contracts.
            CodeGeneratorOptions codeGeneratorOptions = new CodeGeneratorOptions();
            codeGeneratorOptions.BracingStyle = "C";
            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("C#");

            // Adding WCF-related assemblies references as copiler parameters, so as to do the compilation of particular service contract.
            CompilerParameters compilerParameters = new CompilerParameters(new string[] { "System.dll", "System.ServiceModel.dll", "System.Runtime.Serialization.dll" });
            compilerParameters.GenerateInMemory = true;
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtengo el compilado del assembly");
            //Gets the compiled assembly.
            compilerResults = codeDomProvider.CompileAssemblyFromDom(compilerParameters, serviceContractGenerator.TargetCompileUnit);

            if (compilerResults.Errors.Count <= 0)
            {
                // Find the proxy type that was generated for the specified contract (identified by a class that implements the contract and ICommunicationbject - this is contract
                //implemented by all the communication oriented objects).
                Type proxyType = compilerResults.CompiledAssembly.GetTypes().First(t => t.IsClass && t.GetInterface(contractName) != null &&
                    t.GetInterface(typeof(ICommunicationObject).Name) != null);

                // Now we get the first service endpoint for the particular contract.
                ServiceEndpoint serviceEndpoint = endpointsForContracts[contractName].First();

                //Time out and size of objects
                TimeSpan timeOut = new TimeSpan(0, 10, 0);//10 minutes
                const int maxSize = 2147483647;

                foreach (OperationDescription operation in serviceEndpoint.Contract.Operations)
                {
                    DataContractSerializerOperationBehavior dataContractBehavior =
                    operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
                    if (dataContractBehavior != null)
                        dataContractBehavior.MaxItemsInObjectGraph = maxSize;
                }

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Tipo de binding " + serviceEndpoint.Binding.GetType().ToString());
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Seteo timeOuts (" + timeOut.TotalMinutes + ") minutos, y maxSize de buffers " + maxSize);

                switch (serviceEndpoint.Binding.GetType().ToString())
                {
                    case "System.ServiceModel.WSHttpBinding":
                        WSHttpBinding _wsBinding = (WSHttpBinding)serviceEndpoint.Binding;
                        _wsBinding.MaxBufferPoolSize = maxSize;
                        _wsBinding.MaxReceivedMessageSize = maxSize;
                        _wsBinding.OpenTimeout = timeOut;
                        _wsBinding.ReaderQuotas.MaxStringContentLength = maxSize;
                        break;

                    case "System.ServiceModel.WebHttpBinding":
                        WebHttpBinding _webBinding = (WebHttpBinding)serviceEndpoint.Binding;
                        _webBinding.MaxBufferPoolSize = maxSize;
                        _webBinding.MaxBufferSize = maxSize;
                        _webBinding.MaxReceivedMessageSize = maxSize;
                        _webBinding.OpenTimeout = timeOut;
                        break;

                    default:
                    case "System.ServiceModel.BasicHttpBinding":
                        BasicHttpBinding _basicBinding = (BasicHttpBinding)serviceEndpoint.Binding;
                        _basicBinding.MaxBufferPoolSize = maxSize;
                        _basicBinding.MaxBufferSize = maxSize;
                        _basicBinding.MaxReceivedMessageSize = maxSize;
                        _basicBinding.OpenTimeout = timeOut;
                        break;
                }
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Seteo propiedades ReaderQuotas() del .config");
                // Set reader quotas in client before create instance
                XmlDictionaryReaderQuotas _bindingRQ = new XmlDictionaryReaderQuotas();
                _bindingRQ.MaxArrayLength = maxSize;
                _bindingRQ.MaxBytesPerRead = maxSize;
                _bindingRQ.MaxDepth = maxSize;
                _bindingRQ.MaxNameTableCharCount = maxSize;
                _bindingRQ.MaxStringContentLength = maxSize;
                serviceEndpoint.Binding.GetType().GetProperty("ReaderQuotas").SetValue(serviceEndpoint.Binding, _bindingRQ, null);

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Creo instancia del assembly");

                // Create an instance of the proxy by passing the endpoint binding and address as parameters.
                proxyInstance = compilerResults.CompiledAssembly.CreateInstance(proxyType.Name, false, System.Reflection.BindingFlags.CreateInstance, null,
                    new object[] { serviceEndpoint.Binding, serviceEndpoint.Address }, System.Globalization.CultureInfo.CurrentCulture, null);

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Seteo timeOut de propiedad ChannelFactory de la instancia");
                //Set timeOut in minutes
                PropertyInfo channelFactoryProperty = proxyInstance.GetType().GetProperty("ChannelFactory");
                if (channelFactoryProperty == null)
                    throw new InvalidOperationException("There is no ''ChannelFactory'' property on the DomainClient.");

                ChannelFactory factory = (ChannelFactory)channelFactoryProperty.GetValue(proxyInstance, null);
                factory.Endpoint.Binding.SendTimeout = timeOut;
                factory.Endpoint.Binding.OpenTimeout = timeOut;
                factory.Endpoint.Binding.ReceiveTimeout = timeOut;
                factory.Endpoint.Binding.CloseTimeout = timeOut;

                PropertyInfo channelFactoryPropert = proxyInstance.GetType().GetProperty("InnerChannel");
                System.ServiceModel.IClientChannel factor = (System.ServiceModel.IClientChannel)channelFactoryPropert.GetValue(proxyInstance, null);
                factor.OperationTimeout = timeOut;
            }
            return proxyInstance;
        }
    }
}
