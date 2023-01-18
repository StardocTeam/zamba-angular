using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace InvocacionServWDigDipFielTEST.Mocks
{
    public class CalcularHashArchivoIngresadoMock
    {
        public string CalcularHashEnHexadecimalArchivoIngresado(byte[] documentacion)
        {
            ValidarDocumento(documentacion);
            var calculoSHA1 = SHA1.Create();
            return BitConverter.ToString(calculoSHA1.ComputeHash(documentacion)).Replace("-", "");
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
