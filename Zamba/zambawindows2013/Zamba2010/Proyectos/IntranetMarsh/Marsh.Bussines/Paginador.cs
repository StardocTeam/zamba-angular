using System;
using System.Collections.Generic;
using Zamba.Core;

namespace Marsh.Bussines
{
    public class Paginador<T>
    {
        private int _cant_por_pagina;
        private int _pagina;
        private int _total_paginas;

        List<T> _lista;

        public int CantidadPagina
        {
            get { return _cant_por_pagina; }
            set 
            {
                if (value < 1)
                    throw new ArgumentException("El numero de items por pagina debe ser un valor mayor a cero");
                else
                    _cant_por_pagina = value;
            }
        }

        public int Pagina
        {
            get { return _pagina; }
            set 
            {
                if (value < 1)
                    throw new ArgumentException("El numero de pagina debe ser un valor mayor a cero");               
                else
                    _pagina = value; 
            }
        }

        public int TotalPaginas
        {
            get { return _total_paginas; }
        }

        public Paginador(List<T> aPaginar)
        {
            _lista = aPaginar;
            _total_paginas = 0;
            _pagina = 1;
        }

        public List<T> Paginar()
        {
            return doPaging();
        }

        public List<T> Paginar(int Pagina)
        {
            _pagina = Pagina;
            return doPaging();
        }

        private List<T> doPaging()
        {
            int desde = 0, cant = 0;

            try
            {
                if (_lista.Count > 0)
                {
                    // Total de paginas que se arman
                    _total_paginas = (int)Math.Ceiling((double)_lista.Count / (double)_cant_por_pagina);

                    if (_pagina > _total_paginas)
                        throw new ArgumentOutOfRangeException("La pagina actual supera a la cantidad total de paginas");

                    // Desde que item devuelvo
                    desde = (_pagina - 1) * _cant_por_pagina;

                    // Cuantos items se devuelven
                    cant = _cant_por_pagina;

                    // Si se intentan devolver mas items de los que se tiene
                    if ((desde + cant) > _lista.Count)
                        cant = _lista.Count - desde;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            // Devolver los items en el rango calculado
            return _lista.GetRange(desde, cant);
        }
    }
}
