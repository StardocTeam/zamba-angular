using System.Data;


namespace Zamba.Simulator.Model
{
    public class Dictionary
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DataTable Data; 


        public Dictionary()
        {
            Id = 0;
            Name = ""; 
            Data = new DataTable();
        }

        public Dictionary(long id, string name)
        {
            Id = id;
            Name = name;
            Data = new DataTable();
        }
    }
}
