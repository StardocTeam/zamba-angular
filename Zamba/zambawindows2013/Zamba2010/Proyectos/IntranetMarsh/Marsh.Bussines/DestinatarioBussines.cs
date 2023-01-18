using Marsh.Data;

namespace Marsh.Bussines
{
    public class DestinatarioBussines
    {
        public string ObtenerDestinatario(int indice)
        {
            return new DestinatarioData().ObtenerDestinatario(indice);
        }
    }
}
