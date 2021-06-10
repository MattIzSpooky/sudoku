using System;
using Sudoku.Frontend.Controllers;
using Sudoku.Frontend.Views;
using Sudoku.Mvc.Contexts;

namespace Sudoku
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