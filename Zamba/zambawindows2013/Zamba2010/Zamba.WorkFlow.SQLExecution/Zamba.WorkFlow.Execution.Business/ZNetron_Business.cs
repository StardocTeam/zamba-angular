using System;
using System.Data;
using Zamba.WorkFlow.Factories;

namespace Zamba.WorkFlow.Business
{
    /// <summary>
    /// Descripción breve de ZNetron_Factory.
    /// </summary>
    public class ZNetron_Business
    {
        public static DataSet GetAllObjects(System.Int32 DocT, System.Int32 DocId)
        {
            return Zamba.WorkFlow.Factories.ZNetron_Factory.GetAllObjects(DocT, DocId);
        }

        public static void SaveConection(int FromId, int FromConnector, int ToId, int ToConnector, int Id, System.Int32 DocT, System.Int32 DocId)
        {
            Zamba.WorkFlow.Factories.ZNetron_Factory.SaveConection(FromId, FromConnector, ToId, ToConnector, Id, DocT, DocId);
        }

        public static void DeleteAllObjectsFromDB()
        {
            Zamba.WorkFlow.Factories.ZNetron_Factory.DeleteAllObjectsFromDB();
        }

        public static void DeleteAllConnectionsFromDB(System.Int32 DocT, System.Int32 DocId)
        {
            Zamba.WorkFlow.Factories.ZNetron_Factory.DeleteAllConnectionsFromDB(DocT, DocId);
        }

        public static void DeleteObject(int Id, System.Int32 DocT, System.Int32 DocId)
        {
            Zamba.WorkFlow.Factories.ZNetron_Factory.DeleteObject(Id, DocT, DocId);
        }

        public static DataSet GetAllConnections(System.Int32 DocT, System.Int32 DocId)
        {
            return Zamba.WorkFlow.Factories.ZNetron_Factory.GetAllConnections(DocT, DocId);
        }

        public static System.Collections.ArrayList GetAllIds(String Texto, System.Int32 DocT, System.Int32 DocId)
        {
            return Zamba.WorkFlow.Factories.ZNetron_Factory.GetAllIds(Texto, DocT, DocId);
        }

        public static void UpdateObject(System.Int32 Shape_Height, String ShapeColor, String Shape_Text, System.Int32 Shape_Width, System.Int32 Shape_X, System.Int32 Shape_Y, bool Shape_Opaque, int Shape_Id, System.Int32 DocT, System.Int32 DocId)
        {
            Zamba.WorkFlow.Factories.ZNetron_Factory.UpdateObject(Shape_Height, ShapeColor, Shape_Text, Shape_Width, Shape_X, Shape_Y, Shape_Opaque, Shape_Id, DocT, DocId);
        }

        public static void SaveObject(String FullName, System.Int32 Shape_Height, String Shape_Color, String Shape_Text, System.Int32 Shape_Width, System.Int32 Shape_X, System.Int32 Shape_Y, int Shape_Id, bool Shape_Opaque, System.Int32 DocT, System.Int32 DocId)
        {
            Zamba.WorkFlow.Factories.ZNetron_Factory.SaveObject(FullName, Shape_Height, Shape_Color, Shape_Text, Shape_Width, Shape_X, Shape_Y, Shape_Id, Shape_Opaque, DocT, DocId);
        }
    }
}
