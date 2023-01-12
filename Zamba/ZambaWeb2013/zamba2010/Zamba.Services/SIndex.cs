using System;
using System.Collections.Generic;
using System.Data;
using Zamba.Core;
using System.Collections;
using System.Text;

namespace Zamba.Services
{
    /// <summary>
    /// Clase que se encarga del manejo de los indices
    /// </summary>
    public class SIndex : IService
    {
        #region IService Members
        public ServicesTypes ServiceType()
        {
            return ServicesTypes.Index;
        }
        #endregion

      
        private IndexsBusiness IndexsBusiness;
        private DocTypesBusiness DocTypesBusiness;
        private RightsBusiness RightsBusiness;

       

        public SIndex()
        {
            IndexsBusiness = new IndexsBusiness();
            DocTypesBusiness = new DocTypesBusiness();
            RightsBusiness = new RightsBusiness();
        }

      

        /// <summary>
        /// Trae los datos de los indices de un entidad
        /// </summary>
        /// <param name="docTypeId">Id del tipo de Documento</param>
        /// <returns>Dataset con los datos de los indices</returns>
        public DataSet getIndexByDocTypeId(Int64 docTypeId)
        {
            return IndexsBusiness.GetIndexSchemaAsDataSet(docTypeId);
        }
        

       

       

       
      


        /// <summary>
        /// Get indexs
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="DocTypeId"></param>
        /// <returns></returns>
        public List<IIndex> GetIndexs(Int64 docId, Int64 DocTypeId)
        {
          
          
            return IndexsBusiness.GetIndexsData(docId, DocTypeId);
        }

        public DataTable GetIndexData(Int64 Indexid, bool Reload)
        {           
            return new AutoSubstitutionBusiness().GetIndexData(Indexid, Reload);
        }

        public IIndex GetIndex(long IndexId)
        {
            return IndexsBusiness.GetIndex(IndexId);
        }

        public List<string> GetDropDownList(int IndexID)
        {
            return IndexsBusiness.GetDropDownList(IndexID);            
        }

        //public ArrayList retrieveArraylistHierachical(int DocTypeId, int IndexId, System.Collections.Hashtable paramsIndexId)
        //{
        //    return IndexsBusiness.retrieveArraylistHierachical(DocTypeId, IndexId, paramsIndexId);
        //}

        public Int64 SelectMaxIndexValue(long IndexId, long DocTypeId)
        {
            return IndexsBusiness.SelectMaxIndexValue(IndexId, DocTypeId);
        }

        #region "Hierarchical indexs"

        public string GetHierarchyOptions(long IndexId, IIndex ParentIndex )
        {
            DataTable tableToReturn = IndexsBusiness.GetHierarchicalTableByValue(IndexId, ParentIndex);
            return MakeOptionsFromTable(tableToReturn, IndexId);
        }

        public DataTable GetHierarchyTableByValue(long IndexId, IIndex ParentIndex)
        {
            DataTable tableToReturn = IndexsBusiness.GetHierarchicalTableByValue(IndexId, ParentIndex);
            return tableToReturn;
        }

        public DataTable GetHierarchyTable(long IndexId, long ParentIndexId)
        {
            DataTable tableToReturn = IndexsBusiness.GetHierarchicalTable(IndexId, ParentIndexId);
            return tableToReturn;
        }

        public bool ValidateHierarchyValue(string ValueToValidate, long IndexId, long ParentIndexId, string ParentValue)
        {
            return IndexsBusiness.ValidateHierarchyValue(ValueToValidate, IndexId, ParentIndexId, ParentValue);
        }

        private string MakeOptionsFromTable(DataTable Table, long IndexId)
        {
            if (Table == null || Table.Rows.Count == 0)
            {
                return string.Empty;
            }

            bool isSLST = IndexsBusiness.GetIndexDropDownType(IndexId) == 2 ||
                            IndexsBusiness.GetIndexDropDownType(IndexId) == 4;
            int count = Table.Rows.Count;
            DataRow theRow;
            string optionTemplate = "<option value=\"{0}\" title=\"{1}\" >{1}</option>";
            StringBuilder sbOptions = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                theRow = Table.Rows[i];
                if (isSLST)
                {
                    sbOptions.Append(string.Format(optionTemplate, theRow["Value"], theRow["Description"]));
                }
                else
                {
                    sbOptions.Append(string.Format(optionTemplate, theRow["Value"], theRow["Value"]));
                }
            }

            return sbOptions.ToString();
        }

        #endregion

        public Dictionary<long, IndexDataType> GetIndexTypesDictionary(List<IIndex> indexs)
        {
            int max = indexs.Count;
            Dictionary<long, IndexDataType> returnDictionary = new Dictionary<long, IndexDataType>();
            for (int i = 0; i < max; i++)
            {
                returnDictionary.Add(indexs[i].ID, indexs[i].Type);
            }

            return returnDictionary;
        }
    }
}