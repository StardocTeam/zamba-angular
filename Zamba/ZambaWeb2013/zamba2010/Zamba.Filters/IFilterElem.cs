using System;
using Zamba.Core;

namespace Zamba.Filters
{
    public interface IFilterElem
    {
        Int64 Id { get; set; }
        string Filter { get; }
        string Description { get; }
        IndexDataType DataType { get; }
        string Value { get; }
        bool NullValue { get; }
        string Comparator { get; }
        string Type { get; }
        string Text { get; }
        Boolean Enabled { get; set; }
        string ToString();
        Int64 DocTypeId { get; set; }
        Int64 UserId { get; set; }

        /// <summary>
        /// Formatea el valor a filtrar para que se realize el filtrado de manera correcta.
        /// </summary>
        /// <remarks>
        /// La razón de que primero se remueven los espacios entre las comas y luego las comillas simples
        /// es porque si el usuario desea filtrar algo como " Nombre de Usuario  " no podría.
        /// Por eso primero se remueven los espacios, luego las posibles comillas (ya en este momento el
        /// valor a filtrar queda definido) y luego se agregan las comillas simples entre los valores.
        /// </remarks>
        /// <param name="column"></param>
        /// <param name="val">Valores múltiples a filtrar sin formatear</param>
        /// <param name="indexType" />
        /// <param name="compareComparator"></param>
        /// <returns>Valores múltiples formateados</returns>
        string FormatMultipleValues(String column, string val, IndexDataType indexType, String compareComparator);
    }
}