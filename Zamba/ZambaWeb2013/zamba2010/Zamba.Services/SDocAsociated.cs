using System;
using System.Collections.Generic;
using System.Text;
using Zamba.Core.DocTypes.DocAsociated;
using Zamba.Core;
using System.Collections;
using System.Data;

namespace Zamba.Services
{
    public class SDocAsociated : IService
    {
        #region IService Members

        public ServicesTypes ServiceType()
        {
            return ServicesTypes.DocType;
        }

        #endregion

        DocAsociatedBusiness DocAsociatedBusiness;

        public SDocAsociated()
        {
            DocAsociatedBusiness = new DocAsociatedBusiness();
        }

        public ArrayList getAsociatedResultsFromResult(IResult result, Int32 PageSize, Int64 UserId)
        {
            return DocAsociatedBusiness.getAsociatedResultsFromResult(result, PageSize, UserId);
        }

        public int getAsociatedFormId(int DocTypeId)
        {
            return DocAsociatedBusiness.getAsociatedFormId(DocTypeId);
        }

        public  List<Int64> getDocTypesAsociated(Int64 DTID)
        {
            return DocAsociatedBusiness.GetUniqueDocTypeIdsAsociation(DTID);
        }

        public DataTable getAsociatedResultsFromResultAsList(Int64 DocTypeId, IResult res, Int64 UserId)
        {
            return DocAsociatedBusiness.getAsociatedResultsFromResultAsList(DocTypeId, res, 200, UserId);
        }

    }
}
