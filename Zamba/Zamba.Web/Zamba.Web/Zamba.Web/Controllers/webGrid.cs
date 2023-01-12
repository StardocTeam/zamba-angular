using System;
using System.Collections.Generic;
using System.Web;


namespace Zamba.Web
{
    public class webGrid : System.Web.UI.UserControl
    {
        protected string _fieldData;
        protected string _columnsData;
        protected string _idProperty;
        protected Int32 _pageSize;

        public string FieldData { get { return _fieldData; } set { _fieldData = value; } }
        public string ColumnsData { get { return _columnsData; } set { _columnsData = value; } }
        public string IdProperty { get { return _idProperty; } set { _idProperty = value; } }
        public Int32 PageSize { get { return _pageSize; } set { _pageSize = value; } }
    }
}
