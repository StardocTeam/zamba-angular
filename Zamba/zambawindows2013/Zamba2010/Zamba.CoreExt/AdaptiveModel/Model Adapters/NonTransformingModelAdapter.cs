using System.Xml.Linq;

namespace ModelAdapterEF
	{
	/// <summary>
	/// Implements the IModelAdapter interface but does not perform any changes during workspace walking.
	/// </summary>
	public class NonTransformingModelAdapter : IModelAdapter
		{
		#region IModelAdapter Members

		public void AdaptStoreEntitySet(XElement storeEntitySet)
			{ }

		public void AdaptStoreAssociationEnd(XElement associationEnd)
			{ }

		public void AdaptMappingStoreEntitySetAttribute(XAttribute attribute)
			{ }

		#endregion
		}
	}
