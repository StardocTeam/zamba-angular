using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IntelliSense_2
{
    class Program
    {
        static void Main(string[] args)
        {
            MetodoStandard();
        }

        private static void MetodoStandard()
        {
            string bodyAutomail = "Este es un Mensaje de Prueba \n  Enviado por:  <USUARIOACTUAL>.<NAME>";
            System.Console.WriteLine(bodyAutomail);

            MatchCollection lista = Regex.Matches(bodyAutomail, "\\<\\w+\\>\\.\\<\\w+\\>");
            foreach (Match item in lista)
                //buscar en el base
                System.Console.WriteLine(item.Value);
        }
    }
    public class pedazoString
    {
        private bool _esEsoQuePareceIntellisense;
        private string _pedazoString;

        public bool EsEsoQuePareceIntellisense
        {
            get { return _esEsoQuePareceIntellisense; }
            set { _esEsoQuePareceIntellisense = value; }
        }
        public string PedazoString
        {
            get { return _pedazoString; }
            set { _pedazoString = value; }
        }

        public pedazoString()
        {
        }
        public pedazoString(bool esEsoQuePareceIntellisense, string pedazoString)
            : this()
        {
            _pedazoString = pedazoString;
            _esEsoQuePareceIntellisense = esEsoQuePareceIntellisense;
        }
    }
}