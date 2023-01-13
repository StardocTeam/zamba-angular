using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InvocacionServWDigDepFiel;
using InvocacionServWDigDepFiel.wDigDepFiel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace InvocacionServWDigDipFielTEST
{
    [TestClass]
    public class InvocacionServWDigDipFielTEST
    {
        public const string SING = @"JC6CrSyn+TWxBtMdJYdR2MKlsDhiY/5Cv84MYyC1dvaYcnzgdoLxrlhBA1fK0s/eaTM5I726ijY7D
        AeJBdW2Lj44f8QJE5s2D3d6bgqWSdQ6mxbOjyS3DQgvIeeQjY6fo6U/SxJzT2Z9gWcF9Q6Yu6Jf/VVfYc+Q3XzHWDlK4fA=";

        public const string TOKEN = @"PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/Pgo8
        c3NvIHZlcnNpb249IjIuMCI+CiAgICA8aWQgc3JjPSJDTj13c2FhaG9tbywgTz1BRklQLCBDPUFSLCBTRVJJQUxOVU1CRVI9Q1VJVCAzMz
        Y5MzQ1MDIzOSIgZHN0PSJjbj13RGlnRGVwRmllbCIgdW5pcXVlX2lkPSIzNDE1MzE3NTIwIiBnZW5fdGltZT0iMTUzNjg0NTQzNyIgZXhw
        X3RpbWU9IjE1MzY4ODg2OTciLz4KICAgIDxvcGVyYXRpb24gdHlwZT0ibG9naW4iIHZhbHVlPSJncmFudGVkIj4KICAgICAgICA8bG9naW
        4gZW50aXR5PSIzMzY5MzQ1MDIzOSIgc2VydmljZT0id0RpZ0RlcEZpZWwiIHVpZD0iU0VSSUFMTlVNQkVSPUNVSVQgMjAwODYwODU1Nzgs
        IENOPW1vZG9jMiIgYXV0aG1ldGhvZD0iY21zIiByZWdtZXRob2Q9IjIyIj4KICAgICAgICAgICAgPHJlbGF0aW9ucz4KICAgICAgICAgIC
        AgICAgIDxyZWxhdGlvbiBrZXk9IjMwNzE0NDM5MzA0IiByZWx0eXBlPSI0Ii8+CiAgICAgICAgICAgIDwvcmVsYXRpb25zPgogICAgICAg
        IDwvbG9naW4+CiAgICA8L29wZXJhdGlvbj4KPC9zc28+Cg==";

        public const string TIPO_AGENTE = "PSAD";

        public const string ROL = "EXTE";

        public const string CUIT = "30714439304";


        [TestMethod]
        public void PuedoRealizarUnaPeticionAlWebMetodoAvisoDigitConParametrosVacios()
        {

            var servicioWDigDepFiel = new ServicioWDigDepFiel();

            var avisoDigitRequest = new AvisoDigitRequest();
            avisoDigitRequest.Body = new AvisoDigitRequestBody();
            avisoDigitRequest.Body.autentica = new Autenticacion();

            var wDigDepFielSoapClient = new wDigDepFielSoapClient("wDigDepFielSoap");
            wDigDepFielSoap wDigDepFielSoap = wDigDepFielSoapClient;

            Assert.IsNotNull(servicioWDigDepFiel.AvisoDigit(avisoDigitRequest, wDigDepFielSoapClient));

        }



        #region Validaciones Autenticacion
        [TestMethod]
        public void NoPuedoGenerarUnaAutenticacionConUnCUITVacio()
        {
            var servicioWDigDepFiel = new ServicioWDigDepFiel();
            var avisoDigitRequest = new AvisoDigitRequest();
            avisoDigitRequest.Body = new AvisoDigitRequestBody();
            try
            {
                servicioWDigDepFiel.CrearAutenticacion(string.Empty, ROL, SING, TIPO_AGENTE, TOKEN);
            }
            catch (Exception e)
            {
                Assert.AreEqual("El CUIT es vacio.", e.Message);
            }

        }

        [TestMethod]
        public void NoPuedoGenerarUnaAutenticacionConUnCUITNulo()
        {
            var servicioWDigDepFiel = new ServicioWDigDepFiel();
            var avisoDigitRequest = new AvisoDigitRequest();
            avisoDigitRequest.Body = new AvisoDigitRequestBody();
            try
            {
                servicioWDigDepFiel.CrearAutenticacion(null, ROL, SING, TIPO_AGENTE, TOKEN);
            }
            catch (Exception e)
            {
                Assert.AreEqual("El CUIT es nulo.", e.Message);
            }

        }

        [TestMethod]
        public void NoPuedoGenerarUnaAutenticacionConUnRolVacio()
        {
            var servicioWDigDepFiel = new ServicioWDigDepFiel();
            var avisoDigitRequest = new AvisoDigitRequest();
            avisoDigitRequest.Body = new AvisoDigitRequestBody();
            try
            {
                servicioWDigDepFiel.CrearAutenticacion(CUIT, string.Empty, SING, TIPO_AGENTE, TOKEN);
            }
            catch (Exception e)
            {
                Assert.AreEqual("El rol es vacio.", e.Message);
            }

        }

        [TestMethod]
        public void NoPuedoGenerarUnaAutenticacionConUnRolNulo()
        {
            var servicioWDigDepFiel = new ServicioWDigDepFiel();
            var avisoDigitRequest = new AvisoDigitRequest();
            avisoDigitRequest.Body = new AvisoDigitRequestBody();
            try
            {
                servicioWDigDepFiel.CrearAutenticacion(CUIT, null, SING, TIPO_AGENTE, TOKEN);
            }
            catch (Exception e)
            {
                Assert.AreEqual("El rol es nulo.", e.Message);
            }

        }

        [TestMethod]
        public void NoPuedoGenerarUnaAutenticacionConUnaFirmaVacia()
        {
            var servicioWDigDepFiel = new ServicioWDigDepFiel();
            var avisoDigitRequest = new AvisoDigitRequest();
            avisoDigitRequest.Body = new AvisoDigitRequestBody();
            try
            {
                servicioWDigDepFiel.CrearAutenticacion(CUIT, ROL, string.Empty, TIPO_AGENTE, TOKEN);
            }
            catch (Exception e)
            {
                Assert.AreEqual("La firma es vacia.", e.Message);
            }

        }

        [TestMethod]
        public void NoPuedoGenerarUnaAutenticacionConUnaFirmaNula()
        {
            var servicioWDigDepFiel = new ServicioWDigDepFiel();
            var avisoDigitRequest = new AvisoDigitRequest();
            avisoDigitRequest.Body = new AvisoDigitRequestBody();
            try
            {
                servicioWDigDepFiel.CrearAutenticacion(CUIT, ROL, null, TIPO_AGENTE, TOKEN);
            }
            catch (Exception e)
            {
                Assert.AreEqual("La firma es nula.", e.Message);
            }

        }


        [TestMethod]
        public void NoPuedoGenerarUnaAutenticacionConUnTipoDeAgenteVacio()
        {
            var servicioWDigDepFiel = new ServicioWDigDepFiel();
            var avisoDigitRequest = new AvisoDigitRequest();
            avisoDigitRequest.Body = new AvisoDigitRequestBody();
            try
            {
                servicioWDigDepFiel.CrearAutenticacion(CUIT, ROL, SING, string.Empty, TOKEN);
            }
            catch (Exception e)
            {
                Assert.AreEqual("El tipo de agente es vacio.", e.Message);
            }

        }

        [TestMethod]
        public void NoPuedoGenerarUnaAutenticacionConUnTipoDeAgenteNulo()
        {
            var servicioWDigDepFiel = new ServicioWDigDepFiel();
            var avisoDigitRequest = new AvisoDigitRequest();
            avisoDigitRequest.Body = new AvisoDigitRequestBody();
            try
            {
                servicioWDigDepFiel.CrearAutenticacion(CUIT, ROL, SING, null, TOKEN);
            }
            catch (Exception e)
            {
                Assert.AreEqual("El tipo de agente es nulo.", e.Message);
            }

        }

        [TestMethod]
        public void NoPuedoGenerarUnaAutenticacionConUnTokenVacio()
        {
            var servicioWDigDepFiel = new ServicioWDigDepFiel();
            var avisoDigitRequest = new AvisoDigitRequest();
            avisoDigitRequest.Body = new AvisoDigitRequestBody();
            try
            {
                servicioWDigDepFiel.CrearAutenticacion(CUIT, ROL, SING, TIPO_AGENTE, string.Empty);
            }
            catch (Exception e)
            {
                Assert.AreEqual("El Token es vacio.", e.Message);
            }
        }

        [TestMethod]
        public void NoPuedoGenerarUnaAutenticacionConUnTokenNulo()
        {
            var servicioWDigDepFiel = new ServicioWDigDepFiel();
            var avisoDigitRequest = new AvisoDigitRequest();
            avisoDigitRequest.Body = new AvisoDigitRequestBody();
            try
            {
                servicioWDigDepFiel.CrearAutenticacion(CUIT, ROL, SING, TIPO_AGENTE, null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("El Token es nulo.", e.Message);
            }
        }

        [TestMethod]
        public void PuedoGenerarUnaAutenticacion()
        {
            var servicioWDigDepFiel = new ServicioWDigDepFiel();
            var avisoDigitRequest = new AvisoDigitRequest();
            avisoDigitRequest.Body = new AvisoDigitRequestBody();

            Assert.IsNotNull(servicioWDigDepFiel.CrearAutenticacion(CUIT, ROL, SING, TIPO_AGENTE, null));

        }


        #endregion

        #region Validaciones parametros de entrada obligatorios

        [TestMethod]
        public void NoPuedoCreearUnCuerpoDeMsjConNumeroDeLegajoVacio()
        {
            var servicioWDigDepFiel = new ServicioWDigDepFiel();

            var autenticacion = servicioWDigDepFiel.CrearAutenticacion(CUIT, ROL, SING, TIPO_AGENTE, TOKEN);
            var nroLegajo = "";
            var cuitDeclarante = "30714439304";
            var cuitPSAD = "30714439304";
            var cuitIE = "";
            var cuitATA = "";
            var codigo = "";
            var url = "";
            var familias = new List<Familia>();
            var ticket = "";
            var hashing = "";
            var cantidadTotal = 1;
            var sigea = "";
            var nroReferencia = "";

            var avisoDigitRequest = servicioWDigDepFiel.CrearCuerpoRequest(autenticacion, "", null, null, null, null, null, null,
        familias.ToArray(), null, 0, null, null);

            //var avisoDigitRequest = servicioWDigDepFiel.CrearCuerpoRequest(autenticacion, nroLegajo, cuitDeclarante, cuitPSAD, cuitIE, cuitATA, codigo, url,
            //    familias.ToArray(), ticket, hashing, cantidadTotal, sigea, nroReferencia);

        }

        [TestMethod]
        public void NoPuedoCreearUnCuerpoDeMsjConNumeroDeLegajoNulo()
        {

        }

        [TestMethod]
        public void NoPuedoCreearUnCuerpoDeMsjConNumeroDeSIGEAVacioCuandoElCodigoNoEs001o000o101o100()
        {
            var servicioWDigDepFiel = new ServicioWDigDepFiel();

            var autenticacion = servicioWDigDepFiel.CrearAutenticacion(CUIT, ROL, SING, TIPO_AGENTE, TOKEN);
            var nroLegajo = "3213123";
            var cuitDeclarante = "30714439304";
            var cuitPSAD = "30714439304";
            var cuitIE = "43242343242";
            var cuitATA = "643534534";
            var codigo = "001";
            var url = "54534";
            var familias = new List<Familia>();
            var ticket = "423423423";
            var hashing = "234534252345234";
            var cantidadTotal = 1;
            var sigea = "";
            var nroReferencia = "3245234423";

            try
            {
                var avisoDigitRequest = servicioWDigDepFiel.CrearCuerpoRequest(autenticacion, nroLegajo, cuitDeclarante, cuitPSAD, cuitIE, cuitATA, codigo, url,
          familias.ToArray(), ticket, cantidadTotal, "", nroReferencia);
                Assert.Fail();

            }
            catch (Exception e)
            {
                Assert.AreEqual("El numero de SIGEA no puede ser vacio.", e.Message);
            }
            //var avisoDigitRequest = servicioWDigDepFiel.CrearCuerpoRequest(autenticacion, nroLegajo, cuitDeclarante, cuitPSAD, cuitIE, cuitATA, codigo, url,
            //    familias.ToArray(), ticket, hashing, cantidadTotal, sigea, nroReferencia);

        }


        [TestMethod]
        public void NoPuedoCreearUnCuerpoDeMsjConNumeroDeSIGEANuloCuandoElCodigoNoEs001o000o101o100()
        {
            var servicioWDigDepFiel = new ServicioWDigDepFiel();

            var autenticacion = servicioWDigDepFiel.CrearAutenticacion(CUIT, ROL, SING, TIPO_AGENTE, TOKEN);
            var nroLegajo = "3213123";
            var cuitDeclarante = "30714439304";
            var cuitPSAD = "30714439304";
            var cuitIE = "43242343242";
            var cuitATA = "643534534";
            var codigo = "001";
            var url = "54534";
            var familias = new List<Familia>();
            var ticket = "423423423";
            var hashing = "234534252345234";
            var cantidadTotal = 1;
            string sigea = "";
            var nroReferencia = "3245234423";

            try
            {
                var avisoDigitRequest = servicioWDigDepFiel.CrearCuerpoRequest(autenticacion, nroLegajo, cuitDeclarante, cuitPSAD, cuitIE, cuitATA, codigo, url,
                    familias.ToArray(), ticket, cantidadTotal, null, nroReferencia);
                Assert.Fail();

            }
            catch (Exception e)
            {
                Assert.AreEqual("El numero de SIGEA no puede ser nulo.", e.Message);
            }
            //var avisoDigitRequest = servicioWDigDepFiel.CrearCuerpoRequest(autenticacion, nroLegajo, cuitDeclarante, cuitPSAD, cuitIE, cuitATA, codigo, url,
            //    familias.ToArray(), ticket, hashing, cantidadTotal, sigea, nroReferencia);

        }



        [TestMethod]
        public void NoPuedoCreearUnCuerpoDeMsjConCantidadTotalMenorOIgualACero()
        {

            var servicioWDigDepFiel = new ServicioWDigDepFiel();

            var autenticacion = servicioWDigDepFiel.CrearAutenticacion(CUIT, ROL, SING, TIPO_AGENTE, TOKEN);
            var nroLegajo = "3213123";
            var cuitDeclarante = "30714439304";
            var cuitPSAD = "30714439304";
            var cuitIE = "43242343242";
            var cuitATA = "643534534";
            var codigo = "001";
            var url = "54534";
            var familias = new List<Familia>();
            var ticket = "423423423";
            var hashing = "234534252345234";
            var cantidadTotal = 0;
            string sigea = "5425435345";
            var nroReferencia = "3245234423";

            try
            {
                var avisoDigitRequest = servicioWDigDepFiel.CrearCuerpoRequest(autenticacion, nroLegajo, cuitDeclarante, cuitPSAD, cuitIE, cuitATA, codigo, url,
                    familias.ToArray(), ticket, cantidadTotal, sigea, nroReferencia);
                Assert.Fail();

            }
            catch (Exception e)
            {
                Assert.AreEqual("La cantidad total debe ser mayor a cero.", e.Message);
            }
            //var avisoDigitRequest = servicioWDigDepFiel.CrearCuerpoRequest(autenticacion, nroLegajo, cuitDeclarante, cuitPSAD, cuitIE, cuitATA, codigo, url,
            //    familias.ToArray(), ticket, hashing, cantidadTotal, sigea, nroReferencia);

        }



        [TestMethod]
        public void PuedoCrearUnCuerpoDeMensajeParaAvisoDigit()
        {
            ServicioWDigDepFiel servicioWDigDepFiel = new ServicioWDigDepFiel();
            var autenticacion = new Autenticacion();
            var nroLegajo = "";
            var cuitDeclarante = "";
            var cuitPSAD = "";
            var cuitIE = "";
            var cuitATA = "";
            var codigo = "";
            var url = "";
            var familias = new List<Familia>();
            var ticket = "";
            var hashing = "";
            var cantidadTotal = 1;
            var sigea = "";
            var nroReferencia = "";

            var avisoDigitRequest = servicioWDigDepFiel.CrearCuerpoRequest(autenticacion, nroLegajo, cuitDeclarante, cuitPSAD, cuitIE, cuitATA, codigo, url,
                familias.ToArray(), ticket, cantidadTotal, sigea, nroReferencia);

            Assert.IsNotNull(avisoDigitRequest);
        }




        #endregion

        [TestMethod]
        public void DeboEnviarMsjDeErrorUnaVezQueElServicioDevuelvaAlgo()
        {
            var servicioWDigDepFiel = new ServicioWDigDepFiel();

            var avisoDigitRequest = new AvisoDigitRequest();
            avisoDigitRequest.Body = new AvisoDigitRequestBody();
            avisoDigitRequest.Body.autentica = new Autenticacion();

            var wDigDepFielSoapClient = new wDigDepFielSoapClient("wDigDepFielSoap");
            wDigDepFielSoap wDigDepFielSoap = wDigDepFielSoapClient;

            var avisoDigitResponse = servicioWDigDepFiel.AvisoDigit(avisoDigitRequest, wDigDepFielSoapClient);

            Assert.IsNotNull(avisoDigitResponse);
        }



        [TestMethod]
        public void PuedoLeerUnArchivoDesdeSuURL()
        {
            var uri = @"http://www.ceautomatica.es/sites/default/files/upload/10/files/LIBRO%20BLANCO%20DE%20LA%20ROBOTICA%202_v1.pdf";

            var leerArchivoURLMock = new Mocks.LeerArchivoURLMock();
            var documento = leerArchivoURLMock.LeerArchivoURL(uri);

            Assert.IsNotNull(documento);
        }

        [TestMethod]
        public void Prueba2()
        {
            var uri = @"C:\Users\Stardocld\Downloads\Despacho 2.pdf";
            var leerArchivoURLMock = new Mocks.LeerArchivoURLMock();
            var documento = leerArchivoURLMock.LeerArchivoURL(uri);
            var calcularHashArchivoIngresadoMock = new Mocks.CalcularHashArchivoIngresadoMock();            
            var hash = calcularHashArchivoIngresadoMock.CalcularHashEnHexadecimalArchivoIngresado(documento);
                      
        }

        [TestMethod]
        public void Prueba()
        {
            string uri = @"http://www.ceautomatica.es/sites/default/files/upload/10/files/LIBRO%20BLANCO%20DE%20LA%20ROBOTICA%202_v1.pdf";
            var leerArchivoURLMock = new Mocks.LeerArchivoURLMock();
            leerArchivoURLMock.GenerarDocumentoAPartirDeUnArrayDeBytes(uri);
        }


        [TestMethod]
        public void TestLogin()
        {
            var login = new ClienteLoginCms.ProgramaPrincipal();
            login.Autenticacion();
        }

    }
}
