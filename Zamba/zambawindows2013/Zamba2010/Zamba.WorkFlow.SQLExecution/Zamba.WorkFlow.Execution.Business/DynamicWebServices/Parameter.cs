using System;
using System.Collections.Generic;
using System.Text;

namespace Zamba.WorkFlow.Business
{
    /// <summary>
    /// Representa 1 parametro de 1 metodo de 1 WebService
    /// </summary>
    public sealed class Parameter :
        IDisposable
    {
        #region Atributos
        private object _value = null;
        private string _name = null;
        #endregion

        #region Propiedades
        public string Name
        {
            get { return _name; }
        }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
        #endregion

        #region Constructores
        public Parameter(String name)
        {
            _name = name;
        }
        public Parameter(String name, object value)
            : this(name)
        {
            _value = value;
        }
        #endregion

        public void Dispose()
        {
            _name = null;
            _value = null;
        }
    }
}