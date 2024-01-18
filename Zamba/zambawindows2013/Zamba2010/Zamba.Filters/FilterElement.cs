﻿using System;
using System.Text;
using Zamba.Core;
using Zamba.Data;
namespace Zamba.Filters
{
    /// Clase privada que sirve para contener un elemento que guarda nombre de columna y valor que se quiere filtrar. 
    /// (después de la creación del objeto)
    /// <history> 
    ///    [Gaston]     16/10/2008  Created
    ///    [Marcelo]    06/11/2009  Modified
    ///    [Javier]     22/07/2010 Modified
    /// </history>
    public class FilterElem : IFilterElem, IComparable<IFilterElem>
    {
        #region Atributos

        //private string mLogicalOperator;
        private readonly string _mComparator;
        private readonly string _mFilter;
        private readonly string _mDescription;
        private readonly string _mType;
        private readonly string _mValue;
        private readonly string _mFormatValue;
        private bool _nValue;

        #endregion

        #region Propiedades

        public long Id { get; set; }

        public IndexAdditionalType IndexSubsType { get; private set; }

        public string Filter
        {
            get { return _mFilter; }
        }

        public string Description
        {
            get { return _mDescription; }
        }

        public IndexDataType DataType { get; private set; }

        public string Value
        {
            get { return _mValue; }
        }

        public string FormatValue
        {
            get { return _mFormatValue; }
        }

        public bool NullValue
        {
            get { return _nValue; }
        }

        public string Comparator
        {
            get { return _mComparator; }
        }

        public string Type
        {
            get { return _mType; }
        }

        public string Text
        {
            get
            {
                return Description + " " +
                        Comparator + " " +
                        _mFormatValue;
            }
        }



        public Boolean Enabled { get; set; }

        private long _docTypeId;
        public long DocTypeId
        {
            get { return _docTypeId; }
            set { _docTypeId = value; }
        }

        public long UserId { get; set; }

        #endregion

        public int CompareTo(IFilterElem other)
        {
            if (_docTypeId == other.DocTypeId && _mComparator == other.Comparator && Filter == other.Filter && Value == other.Value) return 0;
            return -1;
        }

        public override string ToString()
        {
            return Text;
        }

        #region Constructores

        public FilterElem(Int64 filterId, Int64 indexId, string filterName, string v, string comparator, string filterType, IndexDataType indexType,
                          Int64 currentUserId, Int64 docTypeId, string description, IndexAdditionalType additionalType)
        {
            Enabled = true;
            DataType = indexType;
            DocTypeId = docTypeId;
            UserId = currentUserId;
            Id = filterId;
            IndexSubsType = additionalType;

            _nValue = false;

            //Se agrego este if por si en la cofiguracion de valores para la busqueda por 
            //defecto se cargo una consulta de sql, para que esta se pueda ejecutar[sebastian 04/11/2008]
            string queryResult = String.Empty;

            //Evaluo si el valor del filtro por defecto es una consulta de sql
            if (v.ToLower().Contains("select"))
            {
                string select = v.ToLower().Substring(v.ToLower().IndexOf("select"));

                select = select.ToLower().Replace("currentuserid", currentUserId.ToString());
                select = select.Replace('\"', '\'');

                if (!String.IsNullOrEmpty(select))
                    queryResult = Indexs_Factory.GetIndexFilterText(select);

                v = queryResult;
            }
            else if (v.ToLower().Contains("currentuserid"))
            {
                v = v.ToLower().Replace("currentuserid", currentUserId.ToString());
            }

            _mFilter = indexId == 0 ? filterName : "I" + indexId;
            if (v.Contains(","))
            {
                _mFormatValue = "(" + v + ")";
                v = v.Replace("'", String.Empty);

                //En caso de que el filtrado sea manual, se formatea el valor a filtrar.
                //v = FormatMultipleValues("I" + indexId, v, indexType, comparator);
                // IVAN: ver bien eston.
                v = FormatMultipleValues(_mFilter, v, indexType, comparator);

            }
            else
            {
                v = "(" + v + ")";
                _mFormatValue = v;
            }

            _mValue = v;
            _mDescription = description.Trim();
            _mType = filterType;
            //this.mLogicalOperator = logicalOperator;

            if (string.IsNullOrEmpty(comparator))
                _mComparator = v.Contains(",") ? String.Empty : "=";
            else
                _mComparator = comparator;
        }

        #endregion

        #region Métodos

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
        public string FormatMultipleValues(String column, string val, IndexDataType indexType, String compareComparator)
        {
            //Se separan todos los filtros para remover los espacios de las puntas de cada uno
            Char[] comma = { Char.Parse(",") };
            Array stFiltros = val.Split(comma);

            var sbFiltros = new StringBuilder();
            sbFiltros.Append("(");

            foreach (string filtro in stFiltros)
            {
                if (!String.IsNullOrEmpty(filtro.Trim()))
                {
                    //en el caso de que el filtro por defecto este en forma de consulta 
                    if (String.Compare(compareComparator, string.Empty) == 0)
                    {
                        sbFiltros.Append("[" + column + "] like ");
                    }

                    if (String.Compare(compareComparator, "Contiene") == 0 || compareComparator == "=" ||
                        compareComparator == "<" ||
                        compareComparator == ">" || compareComparator == "<>")
                    {
                        sbFiltros.Append("[" + column + "] like ");
                    }
                    else if (String.Compare(compareComparator, "No Contiene") == 0)
                    {
                        sbFiltros.Append("[" + column + "] not like ");
                    }

                    sbFiltros.Append("'");
                    if (indexType == IndexDataType.Alfanumerico || indexType == IndexDataType.Alfanumerico_Largo || indexType == IndexDataType.Numerico || indexType == IndexDataType.Numerico_Largo)
                        sbFiltros.Append("%");
                    sbFiltros.Append(filtro.Trim());
                    if (indexType == IndexDataType.Alfanumerico || indexType == IndexDataType.Alfanumerico_Largo || indexType == IndexDataType.Numerico || indexType == IndexDataType.Numerico_Largo)
                        sbFiltros.Append("%");
                    sbFiltros.Append("'");

                    if (String.Compare(compareComparator, "Contiene") == 0 || compareComparator == "=" ||
                        compareComparator == "<" ||
                        compareComparator == ">" || compareComparator == "<>")
                    {
                        sbFiltros.Append(" or ");
                    }
                    if (String.Compare(compareComparator, string.Empty) == 0)
                    {
                        sbFiltros.Append(" or ");
                    }
                    if (String.Compare(compareComparator, "No Contiene") == 0)
                    {
                        sbFiltros.Append(" and ");
                    }
                }
                else
                    _nValue = true;
            }

            sbFiltros.Append(")");
            sbFiltros.Replace("or )", ")");
            sbFiltros.Replace("and )", ")");

            val = sbFiltros.ToString();

            return val;
        }

        #endregion
    }
}