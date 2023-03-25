using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    public class DataBase
    {
        
        public void Start()
        {
            Console.WriteLine("> [database] Starting...");
            Console.WriteLine("> [database] Connection to SQL Server...");
            Console.WriteLine("> [database] Running migrations...");
            Console.WriteLine("> [database] Starting done!");
        }
        public void Stop()
        {
            Console.WriteLine("> [database] Stopping...");
            Console.WriteLine("> [database] Closing SQL Server connection...");
            Console.WriteLine("> [database] Stopping done!");
        }
    }
}
