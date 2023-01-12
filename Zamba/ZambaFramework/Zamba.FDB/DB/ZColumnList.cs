using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class ZSqlColumnList : IList<ZSqlColumn>
    {
        List<ZSqlColumn> _lst = new List<ZSqlColumn>();

        private void CheckColumnName(string colName)
        {
            for (int i = 0; i < _lst.Count; i++)
            {
                if (_lst[i].ColumnName == colName)
                    throw new Exception("Column name of \"" + colName + "\" is already existed.");
            }
        }

        public int IndexOf(ZSqlColumn item)
        {
            return _lst.IndexOf(item);
        }

        public void Insert(int index, ZSqlColumn item)
        {
            CheckColumnName(item.ColumnName);

            _lst.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _lst.RemoveAt(index);
        }

        public ZSqlColumn this[int index]
        {
            get
            {
                return _lst[index];
            }
            set
            {
                if (_lst[index].ColumnName != value.ColumnName)
                {
                    CheckColumnName(value.ColumnName);
                }

                _lst[index] = value;
            }
        }

        public void Add(ZSqlColumn item)
        {
            CheckColumnName(item.ColumnName);

            _lst.Add(item);
        }

        public void Clear()
        {
            _lst.Clear();
        }

        public bool Contains(ZSqlColumn item)
        {
            return _lst.Contains(item);
        }

        public void CopyTo(ZSqlColumn[] array, int arrayIndex)
        {
            _lst.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _lst.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ZSqlColumn item)
        {
            return _lst.Remove(item);
        }

        public IEnumerator<ZSqlColumn> GetEnumerator()
        {
            return _lst.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _lst.GetEnumerator();
        }
    }

