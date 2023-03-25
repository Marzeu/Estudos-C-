using Factory.DataBase;
using Factory.WebServer;


namespace Factory.Core
{
    public class Core
    {

        public void createCore()
        {
        }

        public void Start()
        {
            var dataBase = new DataBase.DataBase();
            var webServer = new WebServer.WebServer();

            Console.WriteLine("> [core] Starting...");
            dataBase.Start();
            webServer.Start();
            Console.WriteLine("> [core] Starting done! System running...");

        }
        public void Stop()
        {
            var dataBase = new DataBase.DataBase();
            var webServer = new WebServer.WebServer();

            Console.WriteLine("> [core] Stopping...");
            webServer.Stop();
            dataBase.Stop();
            Console.WriteLine("> [core] Stopping done!");
        }
    }
}
