using System;
using System.Collections.Generic;
using System.Text;
using Zamba.Core;

namespace Zamba.ThreadPool
{
    public class DocTypesList
    {

        private static List<Int64> _docTypeList = null;

        private static List<Int64> DocTypeList
        {
            get
            {
                if (null == _docTypeList)
                    _docTypeList = new List<Int64>();

                return _docTypeList;
            }
        }

        public static Boolean ExistsDocType(Int64 docTypeId)
        {
            Boolean Exists = false;
            if (DocTypeList.Contains (docTypeId))
                Exists = true;
            else
            {
                IDocType MyDocType = DocTypesBusiness.GetDocType(docTypeId);

                if (null != MyDocType)
                {
                    DocTypeList.Add(MyDocType.ID);
                    Exists = true;
                }
                else
                    Exists = false;
            }

            return Exists;
        }
    }
}
