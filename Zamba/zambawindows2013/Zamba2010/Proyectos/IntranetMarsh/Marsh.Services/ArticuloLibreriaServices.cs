using System.Collections.Generic;
using Marsh.Bussines;

namespace Marsh.Services
{
    public static class ArticuloLibreriaServices
    {
        public static List<ArticuloLibreriaBussines> Listar()
        {
            return new ArticuloLibreriaBussines().Listar();
        }
    }
}
