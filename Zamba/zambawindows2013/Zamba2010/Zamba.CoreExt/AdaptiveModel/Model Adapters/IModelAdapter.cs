using System.Xml.Linq;

namespace ModelAdapterEF
	{
	/// <summary>
	/// An interface for adapting an EDMX subtree (CSDL, MSL, or SSDL) before reconstruction into a valid
	/// Entity Framework workspace
	/// </summary>
	public interface IModelAdapter
		{
		/// <summary>
		/// Given an XML storage SSDL subtree, adapts that subtree
		/// </summary>
		/// <param name="storeEntitySet">The SSDL subtree to adapt</param>
		void AdaptStoreEntitySet(XElement storeEntitySet);
		
		/// <summary>
		/// Given an XML SSDL association endpoint, adapts that subtree
		/// </summary>
		/// <param name="associationEnd">The assocition endpoint to adapt</param>
		void AdaptStoreAssociationEnd(XElement associationEnd);

		/// <summary>
		/// Given an XML mapping subtree, adapts that subtree
		/// </summary>
		/// <param name="attribute">The mapping attribute to adapt</param>
		void AdaptMappingStoreEntitySetAttribute(XAttribute attribute);
		}
	}
