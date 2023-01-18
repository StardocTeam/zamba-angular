using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Xml;
using System.IO;
using wConsDepFiel;

namespace InvocacionServWConsDepFiel
{
    public class ServicioWConsDepFiel
    {

        public ServicioWConsDepFiel()
        {


        }

        public ListaEstadoResponse InvocarServicioAvisoRecepAceptRequest(Autenticacion autenticacion,
                                      string nroLegajo,
                                      string cuitDeclarante,
                                      string cuitPSAD,
                                      string cuitIE,
                                      string cuitATA,
                                      string codigo,
                                      string url,
                                      string ticket,
                                      string sigea,
                                      string nroReferencia, string nroGuia, int cantidadFojas, DateTime fechaDespacho, DateTime fechaGeneracion, DateTime fechaHoraAcept, string idEnvio, string indLugarFisico
                                      )
        {
            try
            {
                ListaEstadoRequest listaEstadoRequest = new ListaEstadoRequest();

                listaEstadoRequest.Body = new ListaEstadoRequestBody();

                listaEstadoRequest.Body = CrearlistaEstadoRequestBody(autenticacion, nroLegajo, cuitPSAD, codigo, ticket,sigea);

                listaEstadoRequest.Body.argAutentica = CrearAutenticacion(autenticacion.Cuit, autenticacion.Rol, autenticacion.Sign, autenticacion.TipoAgente, autenticacion.Token);
                
                var wConsDepFielSoapClient = new wConsDepFielSoapClient(wConsDepFiel.wConsDepFielSoapClient.EndpointConfiguration.wConsDepFielSoap);
                wConsDepFielSoap wConsDepFielSoap = wConsDepFielSoapClient;

                var newlistaEstadoRequest = JsonConvert.SerializeObject(listaEstadoRequest.Body, Newtonsoft.Json.Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                //   XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(newresultsavisoConsitRequest);

                return GetListaEstadoResponse(listaEstadoRequest, wConsDepFielSoap).Result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public ListaEstadoRequestBody CrearlistaEstadoRequestBody(Autenticacion autenticacion, string nroLegajo, string cuitPSAD,   string codigo, string ticket, string sigea)
        {
            ValidarNroDeLegajo(nroLegajo);
            ValidarCUITPSAD(autenticacion, cuitPSAD);
            ValidarCodigo(codigo);
            ValidarTicket(codigo, ticket);
            ValidarSigea(codigo, sigea);
 
            var listaEstadoRequestBody = new ListaEstadoRequestBody();
            InListaEstado inListaEstado = new InListaEstado();
            inListaEstado.NroLegajo = nroLegajo;
            inListaEstado.CodigoCarpeta = codigo;
            inListaEstado.Sigea = sigea;
            inListaEstado.Ticket = ticket;

            listaEstadoRequestBody.argInListaEstado = inListaEstado;

            return listaEstadoRequestBody;
        }



        private void ValidarCuitIE(string codigo, string cuitIE)
        {
            if (codigo != "100" || codigo != "101")
            {
                if (cuitIE == string.Empty)
                    throw new Exception("El cuit del importador/exportador no puede ser vacio.");

                if (cuitIE == null)
                    throw new Exception("El cuit del importador/exportador no puede ser nulo.");

            }
            else
            {
                if (!string.IsNullOrEmpty(cuitIE))
                    throw new Exception("El cuit del importador/exportador no se debe ingresar para este caso.");
            }

            if (cuitIE.Length != 11)
                throw new Exception("El cuit del I/E debe tener 11 digitos.");
        }

        private void ValidarCuitATA(string codigo, string cuitATA)
        {
            if (codigo == "100" || codigo == "101")
            {
                if (!string.IsNullOrEmpty(cuitATA))
                    //    throw new Exception("El cuit ATA no se debe ingresar para este caso.");
                    cuitATA = "";
            }
            else
            {
                if (cuitATA.Length > 0 && cuitATA.Length != 11)
                    throw new Exception("El cuit del ATA debe tener 11 digitos.");
            }
        }

        private void ValidarURL(string url)
        {
            if (url == string.Empty)
                throw new Exception("La url no puede ser vacia.");

            if (url == null)
                throw new Exception("La url no puede ser nula.");

            if (url.Length > 1000)
                throw new Exception("La longitud de la url es incorrrecta.");
        }

        private void ValidarCodigo(string codigo)
        {
            if (codigo == string.Empty)
                throw new Exception("El codigo no puede ser vacio.");

            if (codigo == null)
                throw new Exception("El codigo no puede ser nulo.");


            if (Regex.IsMatch(codigo, "[0 - 9]{ 0,3}"))
                throw new Exception(@"Debe ser un codigo de los siguientes:“000” (Carpeta Completa)
                                        “001” (Documentación Adicional)
                                        “002” (Rectificación B Total)
                                        “003” (Rectificación B Parcial)
                                        “004” (Post - Libramiento)
                                        “100” (Manifiesto general de carga de importación PDF)
                                        “101” (Manifiesto general de carga de importación físico)
                                        ");


        }

        private void ValidarCUITPSAD(Autenticacion autenticacion, string cuitPSAD)
        {
            //si el tipo de agente es PSAD es obligatorio, si es DESP no!!!
            if (autenticacion.TipoAgente == "PSAD")
            {
                if (cuitPSAD == string.Empty)
                    throw new Exception("El cuit del PSAD no puede ser vacio.");

                if (cuitPSAD == null)
                    throw new Exception("El cuit del PSAD no puede ser nulo.");
            }
            else
            {
                if (!string.IsNullOrEmpty(cuitPSAD))
                    throw new Exception("No debe iformar el cuit de PSAD.");
            }

            if (cuitPSAD.Length != 11)
                throw new Exception("El cuit del PSAD debe tener 11 digitos.");
        }

        private void ValidarNroDeLegajo(string nroLegajo)
        {
            if (nroLegajo == string.Empty)
                throw new Exception("El numero de legajo no puede ser vacio.");

            if (nroLegajo == null)
                throw new Exception("El numero de legajo no puede ser nulo.");

            if (!nroLegajo.Length.Equals(16))
                throw new Exception("El numero de legajo debe contener 16 caracteres.");
        }

        private void ValidarCuitDeclarante(string cuitDeclarante)
        {
            if (cuitDeclarante == string.Empty)
                throw new Exception("El cuit del declarante no puede ser vacio.");

            if (cuitDeclarante == null)
                throw new Exception("El cuit del declarante no puede ser nulo.");

            if (cuitDeclarante.Length != 11)
                throw new Exception("El cuit del declarante debe tener 11 digitos.");
        }

        private void ValidarTicket(string codigo, string ticket)
        {
            //Obligatorio para el código 001
            if (codigo == "001")
            {
                if (ticket == string.Empty)
                    throw new Exception("El numero de ticket no puede ser vacio.");

                if (ticket == null)
                    throw new Exception("El numero de ticket no puede ser nulo.");

                if (!Regex.IsMatch(ticket, "[0-9]{0,4}[0-9]{0,20}"))
                    throw new Exception("La cantidad de caracteres no es correcta o no son numericos.");
            }



        }

        private void ValidarHashing(string hashing, string codigo)
        {
            if (codigo == "000")
            {
                if (hashing == string.Empty)
                    throw new Exception("El hashing no puede ser vacio.");

                if (hashing == null)
                    throw new Exception("El hashing no puede ser nulo.");

                if (hashing.Length != 40)
                    throw new Exception("La cantidad de caracteres no es correcta.");
            }
        }

        private void ValidarSigea(string codigo, string sigea)
        {
            if (codigo != "000" && codigo != "001" && codigo != "100" && codigo != "101")
            {
                if (sigea == string.Empty)
                    throw new Exception("El numero de SIGEA no puede ser vacio.");

                if (sigea == null)
                    throw new Exception("El numero de SIGEA no puede ser nulo.");
            }


            //if (sigea.Length == 31 || sigea.Length == 36)
            //{ }else
            //    throw new Exception("Longitud invalida. El tamaño del dato debe ser de 31 o 36.");


            //if (sigea.Length == 31 && !Regex.IsMatch(sigea, "[0-9]{0,10}-[0-9]{0,15}-[0-9]{0,4}"))
            //    throw new Exception("");

            //if (sigea.Length == 36 && !Regex.IsMatch(sigea, "[0-9]{0,10}-[0-9]{0,15}-[0-9]{0,4}/[0-9]{0,4}"))
            //    throw new Exception("");
        }

        private void ValidarCantidadTotal(int cantidadTotal)
        {
            if (cantidadTotal <= 0)
                throw new Exception("La cantidad total debe ser mayor a cero.");

            if (cantidadTotal > 999999)
                throw new Exception("La cantidad total debe ser menor a 6 Consitos");
        }

        private void ValidarNroReferencia(string nroReferencia)
        {
            if (!string.IsNullOrEmpty(nroReferencia))
                throw new Exception("No debe ingresar numero de referencia.");
        }

        public async Task<wConsDepFiel.ListaEstadoResponse> GetListaEstadoResponse(ListaEstadoRequest listaEstadoRequest, wConsDepFielSoap wConsDepFielSoap)
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            return await wConsDepFielSoap.ListaEstadoAsync(listaEstadoRequest);
        }


        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public Autenticacion CrearAutenticacion(string cuit, string rol, string sign, string tipoAgente, string token)
        {
            ValidarCUIT(cuit);
            ValidarRol(rol);
            ValidarSign(sign);
            ValidarTipoAgente(tipoAgente);
            ValidarToken(token);

            return new Autenticacion() { Cuit = cuit, Rol = rol, Sign = sign, TipoAgente = tipoAgente, Token = token };
        }

        private void ValidarCUIT(string cuit)
        {
            if (cuit == string.Empty)
                throw new Exception("El CUIT es vacio.");
            if (cuit == null)
                throw new Exception("El CUIT es nulo.");

            if (cuit.Length != 11)
                throw new Exception("El CUIT no cumple con la contidad de caracteres correctos.");
        }

        private void ValidarRol(string rol)
        {
            if (rol == string.Empty)
                throw new Exception("El rol es vacio.");
            if (rol == null)
                throw new Exception("El rol es nulo.");

            if (rol != "EXTE")
                throw new Exception("El valor del rol es incorrecto.");
        }

        private void ValidarSign(string sign)
        {
            if (sign == string.Empty)
                throw new Exception("La firma es vacia.");
            if (sign == null)
                throw new Exception("La firma es nula.");
        }

        private void ValidarTipoAgente(string tipoAgente)
        {
            if (tipoAgente == string.Empty)
                throw new Exception("El tipo de agente es vacio.");
            if (tipoAgente == null)
                throw new Exception("El tipo de agente es nulo.");

        }

        private void ValidarToken(string token)
        {
            if (token == string.Empty)
                throw new Exception("El Token es vacio.");
            if (token == null)
                throw new Exception("El Token es nulo.");
        }


        private string CalcularHashEnHexadecimalArchivoIngresado(byte[] documentacion)
        {
            ValidarDocumento(documentacion);
            var calculoSHA1 = SHA1.Create();
            return BitConverter.ToString(calculoSHA1.ComputeHash(documentacion)).Replace("-", "");
        }


        private byte[] LeerArchivoURL(string URI)
        {
            ValidarURI(URI);
            var webClient = new WebClient();
            var documento = webClient.DownloadData(URI);
            ValidarDocumento(documento);

            return documento;
        }

        private byte[] LeerArchivo(string Path)
        {
            ValidarURI(Path);
            byte[] buff = File.ReadAllBytes(Path);
            ValidarDocumento(buff);

            return buff;
        }

        private void ValidarURI(string URI)
        {
            if (URI == string.Empty)
                throw new Exception("La URI/Path no puede ser vacia.");
            if (URI == null)
                throw new Exception("La URI/Path no puede ser nula.");
        }
        private void ValidarDocumento(byte[] documento)
        {
            if (documento == null)
                throw new Exception("El documento no puede ser nulo.");
            if (documento.Length.Equals(0))
                throw new Exception("El documento no puede ser vacio.");
        }
    }
}
