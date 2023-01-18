
namespace Zamba.Simulator.Model
{
    public class TestCase
    {
        public Process Process { get; set; }
        public Dictionary Dictionary { get; set; }
        public string State { get; set; }

        public TestCase(Process process)
        {
            Process = process;
            Dictionary = new Dictionary();
        }

        public TestCase(Process process, Dictionary dictionary)
        {
            Process = process;
            Dictionary = dictionary;
        }

        /// <summary>
        /// Constructor de Demo
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="dictionaryName"></param>
        public TestCase(string processName, string dictionaryName)
        {
            Process = new Process(1002208);
            Dictionary = new Dictionary(1002208, dictionaryName);
        }
    }
}
