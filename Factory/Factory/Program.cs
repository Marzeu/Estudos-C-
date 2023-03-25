// See https://aka.ms/new-console-template for more information

//Estudo sobre arquiterura de software usando o Desing Pattern Factory.

//O .Net 6  foi adicionada uma nova funcionalidade chamada "Top Level Statements" (Instruções em nível superior). Essa funcionalidade simplifica a interface omitindo 
//namespace, os imports e main. É possivel voltar ao modo antigo marcando a opção "Do not use top-level statements" na criação do projeto em "Additional information" ou
//no CLI usando: dotnet new console --use-program-main.

//Ref:
//https://www.youtube.com/watch?v=arAz2Ff8s88&ab_channel=FilipeDeschamps
//https://www.youtube.com/watch?v=uyOJ2jjBtBs&ab_channel=
//
using Factory;

var core = new Core(new DataBase(), new WebServer());

try
{
    core.Start();
    core.Stop();
}
catch (Exception ex)
{
    Console.WriteLine("[index] Uncaught error!");
    Console.WriteLine(ex.Message.ToString());
}

Console.WriteLine();
Console.WriteLine("Aperte qualquer coisa para terminar.");
Console.ReadLine();
