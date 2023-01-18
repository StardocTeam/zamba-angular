using System;
using System.Collections.Generic;
using System.Text;

namespace Zamba.Services
{
    public static class Results_Business
    {
        public static Zamba.Core.NewResult GetNewNewResult(long docTypeId)
        {
            return Zamba.Core.Results_Business.GetNewNewResult(docTypeId);
        }

        public static Zamba.Core.NewResult GetNewNewResult(long docId, Zamba.Core.DocType docType)
        {
            return Zamba.Core.Results_Business.GetNewNewResult(docId, docType);
        }

        public static Zamba.Core.NewResult GetNewNewResult(Zamba.Core.DocType DocType, int _UserId, string File)
        {
            return Zamba.Core.Results_Business.GetNewNewResult(DocType, _UserId, File);
        }

        public static Zamba.Core.Result GetNewResult(long docId, Zamba.Core.DocType docType)
        {
            return Zamba.Core.Results_Business.GetNewResult(docId, docType);
        }

        public static Zamba.Core.NewResult GetNewResult(long docTypeId, string File)
        {
            return Zamba.Core.Results_Business.GetNewResult(docTypeId, File);
        }

    }
}
