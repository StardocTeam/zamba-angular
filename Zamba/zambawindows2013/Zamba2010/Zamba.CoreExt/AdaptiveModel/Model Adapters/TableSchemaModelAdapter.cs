using System.Xml.Linq;

namespace ModelAdapterEF
{
    /// <summary>
    /// Adapts a workspace by changing the database owner to the specified schema value
    /// </summary>
    public class TableSchemaModelAdapter : BaseDecoratorModelAdapter
    {
        /// <summary>
        /// The database schema to use during adaptation
        /// </summary>
        protected string Schema { get; set; }

        /// <summary>
        /// The database schema to use during adaptation enclosed with brackets
        /// </summary>
        protected string SchemaWithBrackets { get; set; }

        /// <summary>
        /// Schema attribute name
        /// </summary>
        private const string SchemaAttribute = "Schema";

        /// <summary>
        /// Schema special attribute name
        /// </summary>
        private const string SchemaSpecialAttribute =
            "{http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator}Schema";

        /// <summary>
        /// Defining Query element special name
        /// </summary>
        private const string DefiningQueryElement = "{http://schemas.microsoft.com/ado/2009/02/edm/ssdl}DefiningQuery";

        /// <summary>
        /// Opening bracket char
        /// </summary>
        private const string OpeningBracketChar = "[";

        /// <summary>
        /// Closing bracket char
        /// </summary>
        private const string ClosingBracketChar = "]";

        /// <summary>
        /// Instantiates a TableSchemaModelAdapter using the given database schema
        /// </summary>
        /// <param name="schema">The database schema to use during adaptation</param>
        public TableSchemaModelAdapter(string schema)
            : base()
        {
            Schema = schema;
            SchemaWithBrackets = OpeningBracketChar + schema + ClosingBracketChar;
        }

        /// <summary>
        /// Instantiates a TableSchemaModelAdapter using the given schema AND decorates the passed-in
        /// IModelAdapter
        /// </summary>
        /// <param name="schema">The schema to use during adaptation</param>
        /// <param name="decoratedModel">The IModelAdapter to decorate</param>
        public TableSchemaModelAdapter(string schema, IModelAdapter decoratedModel)
            : base(decoratedModel)
        {
            Schema = schema;
            SchemaWithBrackets = OpeningBracketChar + schema + ClosingBracketChar;
        }

        #region IModelAdapter Members

        public override void AdaptStoreEntitySet(XElement storeEntitySet)
        {
            var xAttribute = storeEntitySet.Attribute(SchemaAttribute);
            if (xAttribute != null)
            {
                xAttribute.Value = Schema;
            }
            else
            {
                xAttribute = storeEntitySet.Attribute(SchemaSpecialAttribute);

                if (xAttribute != null)
                {
                    var fromSchema = OpeningBracketChar + xAttribute.Value + ClosingBracketChar;
                    xAttribute.Value = Schema;

                    if (storeEntitySet.HasElements)
                    {
                        var xElement = storeEntitySet.Element(DefiningQueryElement);
                        if (xElement != null)
                            xElement.Value = storeEntitySet.Value.Replace(fromSchema, SchemaWithBrackets);
                    }
                }
            }

            base.AdaptStoreEntitySet(storeEntitySet);
        }

        #endregion
    }
}
