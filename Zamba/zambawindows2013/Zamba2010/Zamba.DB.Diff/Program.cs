using System;
using System.Collections.Generic;
using System.Text;

namespace Zamba.DB.Diff
{
    class Program
    {
        static void Main(string[] args)
        {
            DBDiff db = new DBDiff("zambaprd", "zambapre");

            if (args.Length == 0)
            {
                //genera sp con los scripts para migracion completa de todos los Doc_Types
                db.GenerateDiffScript("DiffScript");
            }
            else
            {
                if (args.Length == 2)
                {
                    if (args[1] == "1")
                    {
                        db.DeleteDocT(Int64.Parse(args[0]));
                    }
                    else if (args[1] == "2")
                    {
                        db.Migrate(Int64.Parse(args[0]));
                    }
                    else if (args[1] == "3")
                    {
                        db.MigrateDiffs(Int64.Parse(args[0]));
                    }
                    else
                    {
                        Console.WriteLine("Parametros incorrectos");
                    }
                }
                else
                {
                    Console.WriteLine("Parametros incorrectos");
                }
            }

            db = null;
        }
    }
}