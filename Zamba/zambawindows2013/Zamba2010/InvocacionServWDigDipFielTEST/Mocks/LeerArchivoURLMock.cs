using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InvocacionServWDigDipFielTEST.Mocks
{
    public class LeerArchivoURLMock
    {
        public byte[] LeerArchivoURL(string URI)
        {
            ValidarURI(URI);
            var webClient = new WebClient();
            var documento = webClient.DownloadData(URI);
            ValidarDocumento(documento);

            return documento;
        }

        public void GenerarDocumentoAPartirDeUnArrayDeBytes(string URI)
        {
            var byteDocumento = LeerArchivoURL(URI);        
            System.IO.File.WriteAllBytes(@"c:\pepe.pdf", byteDocumento);
        }

        public void ValidarURI(string URI)
        {
            if (URI == string.Empty)
                throw new Exception("La URI no puede ser vacia.");
            if (URI == null)
                throw new Exception("La URI no puede ser nula.");
        }
        public void ValidarDocumento(byte[] documento)
        {
            if (documento == null)
                throw new Exception("El documento no puede ser nulo.");
            if (documento.Length.Equals(0))
                throw new Exception("El documento no puede ser vacio.");
        }

    }
}
