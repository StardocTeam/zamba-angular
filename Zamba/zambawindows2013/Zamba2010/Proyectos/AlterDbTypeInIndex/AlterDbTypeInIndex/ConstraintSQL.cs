using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Zamba.Servers;
using Zamba.Data;

namespace AlterDbTypeInIndex
{
    class ConstraintSQL
    {
        public enum CONSTRAINT
        {
            CONSTRAINT_CATALOG = 0,
            TABLE_NAME = 1,
            CONSTRAINT_SCHEMA = 2,
            CONSTRAINT_NAME = 3,
            COLUMN_NAME = 4,
            ORDINAL_POSITION = 5,
            DEFAULT_CLAUSE = 6
        }

       
        public string _constraintCatalog;
        public string _tableName;
        public string _constraintSchema;
        public string _constraintName;
        public string _columnName;
        public string _ordinalPosition;
        public string _defaultClause;
            
        

        public static DataSet GetConstraints(string table, string column)
        {
            StringBuilder qry = new StringBuilder();
            qry.Append("select	db_name()   as CONSTRAINT_CATALOG ");
            qry.Append(",t_obj.name     as TABLE_NAME ");
            qry.Append(",user_name(c_obj.uid)			as CONSTRAINT_SCHEMA ");
            qry.Append(",c_obj.name				as CONSTRAINT_NAME ");
            qry.Append(",col.name				as COLUMN_NAME ");
            qry.Append(",col.colid				as ORDINAL_POSITION ");
            qry.Append(",com.text				as DEFAULT_CLAUSE ");
            qry.Append("from	sysobjects	c_obj ");
            qry.Append("join 	syscomments	com on 	c_obj.id = com.id ");
            qry.Append("join 	sysobjects	t_obj on c_obj.parent_obj = t_obj.id  ");
            qry.Append("join    sysconstraints con on c_obj.id	= con.constid ");
            qry.Append("join 	syscolumns	col on t_obj.id = col.id ");
            qry.Append("and con.colid = col.colid ");
            qry.Append("where ");
            qry.Append("c_obj.xtype	= 'D' ");
            qry.Append("and t_obj.name = '" + table + "' ");
            qry.Append("and col.name = '" + column + "'");

            IConnection con;
            con = Server.get_Con(true, true, true);
            DataSet dsConstraint = con.ExecuteDataset(CommandType.Text, qry.ToString());
            con = null;

            return dsConstraint;
        }

        public static bool HasConstraints(string table, string column)
        {
            StringBuilder qry = new StringBuilder();
            qry.Append("select	count(*) ");
            qry.Append("from	sysobjects	c_obj ");
            qry.Append("join 	syscomments	com on 	c_obj.id = com.id ");
            qry.Append("join 	sysobjects	t_obj on c_obj.parent_obj = t_obj.id  ");
            qry.Append("join    sysconstraints con on c_obj.id	= con.constid ");
            qry.Append("join 	syscolumns	col on t_obj.id = col.id ");
            qry.Append("and con.colid = col.colid ");
            qry.Append("where ");
            qry.Append("c_obj.xtype	= 'D' ");
            qry.Append("and t_obj.name = '" + table + "' ");
            qry.Append("and col.name = '" + column + "'");

            IConnection con;
            con = Server.get_Con(true, true, true);
            Int32 cantidadConstraints = Convert.ToInt32(con.ExecuteScalar(CommandType.Text, qry.ToString()));
            con = null;

            if (cantidadConstraints > 0)
                return true;
            else
                return false;
            
        }

        public static void DropDefaultConstraint(ConstraintSQL cs)
        {
            

            StringBuilder qry = new StringBuilder();
            qry.Append("ALTER TABLE " +  cs._tableName + " DROP CONSTRAINT " + cs._constraintName);
            System.Diagnostics.Trace.WriteLine("Drop Constraint: " + qry.ToString());
            System.Diagnostics.Trace.Flush();

            IConnection con;
            con = Server.get_Con(true, true, true);
            con.ExecuteNonQuery(CommandType.Text, qry.ToString());
            con = null;

        }

        public static void CreateDefaultConstraint(ConstraintSQL cs)
        {
            
             StringBuilder qry = new StringBuilder();
            qry.Append("ALTER TABLE " +  cs._tableName + " ADD CONSTRAINT " + cs._constraintName + " DEFAULT " + cs._defaultClause + " FOR " + cs._columnName);
            System.Diagnostics.Trace.WriteLine("Crear Constraint: " + qry.ToString());
            System.Diagnostics.Trace.Flush();

            IConnection con;
            con = Server.get_Con(true, true, true);
            con.ExecuteNonQuery(CommandType.Text, qry.ToString());
            con = null;

        }

    }
}
