using System;
using System.Data;

namespace Zamba.WorkFlow.Factories
{
	/// <summary>
	/// Descripción breve de ZNetron_Factory.
	/// </summary>
	public class ZNetron_Factory
	{
		public ZNetron_Factory()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//

		}


		
		public static  DataSet GetAllObjects(System.Int32 DocT,System.Int32 DocId) 
		{ 
			string strSelect = "Select Shape_Id,Shape_Tipo,Shape_Height,Shape_Color,Shape_Text,Shape_Width,Shape_X,Shape_Y,Shape_Opaque From ZNetronShapes Where Shape_Tipo <> 4 And Shape_Doct = " + DocT + " And Shape_DocId = " + DocId; 
			return Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, strSelect); 
		} 

		public static void SaveConection(int FromId,int FromConnector,int ToId,int ToConnector,int Id,System.Int32 DocT, System.Int32 DocId) 
		{ 
			try
			{ 
				string strInsert = "Insert Into ZNetronShapes (Shape_StartId,Shape_EndId,Shape_StartNum,Shape_EndNum,Shape_Id,Shape_Tipo,Shape_DocT,Shape_DocId) Values (" + FromId + "," + + ToId + "," + FromConnector + "," + ToConnector + "," + Id + ",4," + DocT + "," + DocId + ")"; 
				Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, strInsert); 
			} 
			catch
			{ 
			} 
		} 

		public static void DeleteAllObjectsFromDB() 
		{ 
			string strDelete = "Delete * From ZNetronShapes Where Shape_Tipo <> 4"; 
			Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, strDelete); 
		}
 
		public static void DeleteAllConnectionsFromDB(System.Int32 DocT,System.Int32 DocId) 
		{ 
			string strDelete = "Delete From ZNetronShapes Where Shape_Tipo = 4 And Shape_DocT = " + DocT + " And Shape_DocId = " + DocId; 
			Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, strDelete); 
		} 

		public static void DeleteObject(int Id,System.Int32 DocT,System.Int32 DocId) 
		{ 
			string strDelete = "Delete From ZNetronShapes Where Shape_Id = " + Id + " And Shape_DocT = " + DocT + " And Shape_DocId = " + DocId; 
			Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, strDelete); 
		}


		public static DataSet GetAllConnections(System.Int32 DocT,System.Int32 DocId) 
		{ 
			string strSelect = "Select Shape_Id, Shape_StartId, Shape_EndId, Shape_StartNum, Shape_EndNum From ZNetronShapes Where Shape_Tipo = 4 And Shape_DocT = " + DocT + " And Shape_DocId = " + DocId; 
			return Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, strSelect); 
		} 

		public static System.Collections.ArrayList GetAllIds(String Texto,System.Int32 DocT,System.Int32 DocId) 
		{ 
			string strSelect = "";
			if (Texto=="Obj")
			{ 
				strSelect = "Select Shape_Id From ZNetronShapes Where Shape_Tipo <> 4 And Shape_DocT = " + DocT + " And Shape_DocId = " + DocId; 
			}
			else
			{ 
				strSelect = "Select Shape_Id From ZNetronShapes Where Shape_Tipo = 4 And Shape_DocT = " + DocT + " And Shape_DocId = " + DocId; 
			}

			System.Collections.ArrayList ArrayAllId = new System.Collections.ArrayList();
			DataSet DsIds = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, strSelect); 
			for (int i = 0; i <= DsIds.Tables[0].Rows.Count - 1; i++) 
			{ 
				ArrayAllId.Add( DsIds.Tables[0].Rows[i].ItemArray[0]);
			}
			return ArrayAllId;
		} 


		public static void UpdateObject(System.Int32 Shape_Height,String ShapeColor, String Shape_Text,System.Int32 Shape_Width,System.Int32 Shape_X,System.Int32 Shape_Y,bool Shape_Opaque,int Shape_Id,System.Int32 DocT,System.Int32 DocId)
		{
			try
			{

				int ShapeOpaque =0;
				if (Shape_Opaque == true)
				{
					ShapeOpaque=1;
				}

				string strUpdate = "Update ZNetronShapes Set Shape_Height=" + Shape_Height + ",Shape_Color=" + ShapeColor  + ",Shape_Text='" + Shape_Text + "',Shape_Width=" + Shape_Width + ",Shape_X=" + Shape_X + ",Shape_Y=" + Shape_Y + ",Shape_Opaque=" + ShapeOpaque + " Where Shape_Id = " + Shape_Id + " And Shape_Tipo <> 4 And Shape_DocT = " + DocT + " And Shape_DocId = " + DocId; 
				Zamba.Servers.Server.get_Con().ExecuteNonQuery (CommandType.Text, strUpdate); 
			}				
			catch
				  {
				  }
			}

		public static void SaveObject(String FullName,System.Int32 Shape_Height,String  Shape_Color,String Shape_Text,System.Int32 Shape_Width,System.Int32 Shape_X,System.Int32 Shape_Y,int Shape_Id,bool Shape_Opaque,System.Int32 DocT,System.Int32 DocId)
		{
			try
			{
				string strInsert ="";
				int ShapeOpaque =0;
				if (Shape_Opaque == true)
				{
					ShapeOpaque=1;
				}

				if(FullName.IndexOf("SimpleRectangle", StringComparison.CurrentCultureIgnoreCase) != -1)
				{
					strInsert = "Insert Into ZNetronShapes (Shape_Height,Shape_Color,Shape_Text,Shape_Width,Shape_X,Shape_Y,Shape_Id,Shape_Tipo,Shape_Opaque,Shape_DocT,Shape_DocId) Values (" + Shape_Height + "," + Shape_Color + ",'" + Shape_Text + "'," + Shape_Width + "," + Shape_X + "," + Shape_Y + "," + Shape_Id + ",1," + ShapeOpaque + "," + DocT + "," + DocId + ")"; 
				}
				else if(FullName.IndexOf("OvalShape", StringComparison.CurrentCultureIgnoreCase) != -1)
				{
					strInsert = "Insert Into ZNetronShapes (Shape_Height,Shape_Color,Shape_Text,Shape_Width,Shape_X,Shape_Y,Shape_Id,Shape_Tipo,Shape_Opaque,Shape_DocT,Shape_DocId) Values (" + Shape_Height + "," + Shape_Color + ",'" + Shape_Text + "'," + Shape_Width + "," + Shape_X + "," + Shape_Y + "," + Shape_Id + ",2," + ShapeOpaque + "," + DocT + "," + DocId + ")";  
				}
				else if(FullName.IndexOf("TextLabel", StringComparison.CurrentCultureIgnoreCase) != -1)
				{
					strInsert = "Insert Into ZNetronShapes (Shape_Height,Shape_Color,Shape_Text,Shape_Width,Shape_X,Shape_Y,Shape_Id,Shape_Tipo,Shape_Opaque,Shape_DocT,Shape_DocId) Values (" + Shape_Height + "," + Shape_Color+ ",'" + Shape_Text + "'," + Shape_Width + "," + Shape_X + "," + Shape_Y + "," + Shape_Id + ",3," + ShapeOpaque + "," + DocT + "," + DocId + ")";  
				}


				Zamba.Servers.Server.get_Con().ExecuteNonQuery (CommandType.Text, strInsert); 
			}				
			catch
			{
			}
			
		}


	}
}
