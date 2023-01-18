using System;
using System.Collections;
using System.Data;
using Zamba.Grid;

namespace zamba.collections
{
    public abstract class PageGroupList : ListaPaginada, IDataSource
    {
        protected Object value;

        public Object DataSource 
        {
            get { return this.value; }
            set {
                if (null == value) 
                    return;

                ArrayList list = this.templateGetObjects(value);

                if( null != list && 0 < list.Count )
                    this.AddRange(list);

                this.value = value;
            }
        }

        /// <summary>
        /// Template method to soport 
        /// distincts type of  collections
        /// </summary>
        /// <param name="source">Source collection</param>
        /// <returns>return a ArrayList</returns>
        protected abstract ArrayList templateGetObjects( Object dataSource );

    }

}
