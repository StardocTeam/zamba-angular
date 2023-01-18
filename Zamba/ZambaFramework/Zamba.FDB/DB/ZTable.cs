using System;
using System.Collections.Generic;
using System.Text;

    public class ZSqlTable
    {
        public string TableName = "";
        public ZSqlColumnList Columns = new ZSqlColumnList();

        public ZSqlTable()
        { }

        public ZSqlTable(string name)
        {
            TableName = name;
        }
    }
