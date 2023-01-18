namespace Marsh.Bussines
{
    public class ServicioBussines
    {
        int _id;
        string _descripcion;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }        

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
    }
}