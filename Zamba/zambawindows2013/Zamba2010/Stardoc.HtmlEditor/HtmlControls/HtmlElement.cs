using System;
using Stardoc.HtmlEditor.Enumerators;
using System.Text;

namespace Stardoc.HtmlEditor.HtmlControls
{
    public abstract class BaseHtmlElement
        : IHtmlElement 
    {

        #region Constantes
        private const String NAME_TAG = "Nombre: ";
        private const String DISABLED_TAG = "Deshabilitado"; 
        #endregion

        #region Atributos
        protected String _id;
        protected String _name;
        protected Boolean _enabled = true;
        protected Boolean _isFormButton = false;
        #endregion

        #region Propiedades
        public String Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public Boolean Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public Boolean IsFormButton
        {
            get { return _isFormButton; }
            set { _isFormButton = value; }
        }
        abstract public HtmlControlType Type();
        #endregion

        #region Constructores
        public BaseHtmlElement()
        {
            _enabled = true;
        }
        public BaseHtmlElement(String name)
            : this()
        {
            _name = name;
            
        }
        public BaseHtmlElement(String name, String id)
            : this(name)
        {
            _id = id;
        }
        public BaseHtmlElement(String name, String id, Boolean enabled)
            : this(name, id)
        {
            _enabled = enabled;
        }

        public BaseHtmlElement(String name, String id, Boolean enabled,Boolean isFormButton)
            : this(name, id,enabled)
        {
            _isFormButton = isFormButton;
        } 
        #endregion

        public override string ToString()
        {
            StringBuilder DisplayBuilder = new StringBuilder();

                DisplayBuilder.Append(NAME_TAG);
                DisplayBuilder.AppendLine(_name);
                if (!_enabled)
                    DisplayBuilder.AppendLine(DISABLED_TAG);
                DisplayBuilder.Remove(DisplayBuilder.Length - Environment.NewLine.Length, Environment.NewLine.Length);
           
            return DisplayBuilder.ToString();
        }
    }
}
