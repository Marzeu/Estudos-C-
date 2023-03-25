namespace Factory.WebServer
{
    public class WebServer
    {        
            public void CreateWebServerConnection()
            {
            }


            public void Start()
            {
                Console.WriteLine("> [webserver] Starting...");
                Console.WriteLine("> [webserver] Waiting for port to be available...");
                Console.WriteLine("> [webserver] Starting done!");
            }
            public void Stop()
            {
                Console.WriteLine("> [webserver] Stopping...");
                Console.WriteLine("> [webserver] Gracefully waiting for all clients");
                Console.WriteLine("> [webserver] Closing all ports...");
                Console.WriteLine("> [webserver] Stopping done!");
            }
        }
    }


