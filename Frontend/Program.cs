using System;
using Frontend.Controllers;
using Frontend.Views;
using MVC.Contexts;

namespace Frontend
{
    public static class Program
    {
        public static void Main()
        {
            Console.WindowWidth = 100;
            Console.WindowHeight = 50;

            using var mvcContext = new MvcContext();
            mvcContext.OpenController<StartController, StartView, ConsoleKey>();
            mvcContext.Run();
        }
    }
}