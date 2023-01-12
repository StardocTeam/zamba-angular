using System.Data.EntityClient;
using System.Reflection;
using ModelAdapterEF;


namespace Zamba.CoreExt
{
    public partial class TCEntities: AdaptingObjectContext
    {
        


        /// <summary>
        /// Initialize a new TCEntities object.
        /// </summary>
        public TCEntities(string connectionString, string schema)
           : base(connectionString, "TCEntities",
                new ConnectionAdapter(new TableSchemaModelAdapter(schema), Assembly.GetExecutingAssembly()))
        {
            
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }

        /// <summary>
        /// Initialize a new TCEntities object.
        /// </summary>
        public TCEntities(EntityConnection connection, string schema)
            : base(connection, "TCEntities",
                new ConnectionAdapter(new TableSchemaModelAdapter(schema), Assembly.GetExecutingAssembly()))
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }

        /// <summary>
        /// Initialize a new TCEntities object. It must be used when Active Directory is ON.
        /// </summary>
        /// <param name="connectionString"></param>
        public TCEntities(string connectionString)
            : base(connectionString, "TCEntities",
                new ConnectionAdapter(null, Assembly.GetExecutingAssembly()))
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    }
}
