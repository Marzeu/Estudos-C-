
namespace Factory
{
    public class Core
    {
        private readonly DataBase _dataBase;
        private readonly WebServer _webServer;

        public Core(DataBase dataBase, WebServer webServer)
        {
            _dataBase = dataBase;
            _webServer = webServer;
        }        

        public void Start()
        {  
            Console.WriteLine("> [core] Starting...");
            _dataBase.Start();
            _webServer.Start();
            Console.WriteLine("> [core] Starting done! System running...");
        }
        public void Stop()
        {       
            Console.WriteLine("> [core] Stopping...");
            _webServer.Stop();
            _dataBase.Stop();
            Console.WriteLine("> [core] Stopping done!");
        }
    }
}
