using System.Xml.Linq;

namespace ModelAdapterEF
	{
	/// <summary>
	/// Adapts a workspace by appending a specified table suffix where applicable
	/// </summary>
	public class TableSuffixModelAdapter : BaseDecoratorModelAdapter
		{
		/// <summary>
		/// The table suffix to use during adaptation
		/// </summary>
		protected string Suffix { get; set; }

		/// <summary>
		/// Instantiates a TableSuffixModelAdapter using the given suffix
		/// </summary>
		/// <param name="suffix">The table suffix to use during adaptation</param>
		public TableSuffixModelAdapter(string suffix)
			: base()
			{ Suffix = suffix; }

		/// <summary>
		/// Instantiates a TableSuffixModelAdapter using the given suffix AND decorates the passed-in
		/// IModelAdapter
		/// </summary>
		/// <param name="suffix">The suffix to use during adaptation</param>
		/// <param name="decoratedModel">The IModelAdapter to decorate</param>
		public TableSuffixModelAdapter(string suffix, IModelAdapter decoratedModel)
			: base(decoratedModel)
			{ Suffix = suffix; }

		#region IModelAdapter Members

		public override void AdaptStoreEntitySet(XElement storeEntitySet)
			{
			AdaptAttribute(storeEntitySet.Attribute("Name"));
			base.AdaptStoreEntitySet(storeEntitySet);
			}

		public override void AdaptStoreAssociationEnd(XElement associationEnd)
			{
			AdaptAttribute(associationEnd.Attribute("EntitySet"));
			base.AdaptStoreAssociationEnd(associationEnd);
			}

		public override void AdaptMappingStoreEntitySetAttribute(XAttribute attribute)
			{
			AdaptAttribute(attribute);
			base.AdaptMappingStoreEntitySetAttribute(attribute);
			}

		#endregion

		/// <summary>
		/// Given an XAttribute, appends the instance suffix to that attribute
		/// </summary>
		private void AdaptAttribute(XAttribute attribute)
			{ attribute.Value += Suffix; }
		}
	}
