using System;
using Sudoku.Domain.Selector;
using Sudoku.Frontend.Views;
using Sudoku.Mvc;
using Sudoku.Mvc.Contexts;
using Sudoku.Mvc.Views;

namespace Sudoku.Frontend.Controllers
{
    public class StartController : Controller<StartView, ConsoleKey>
    {
        private readonly SudokuSelector _selector = new();

        public StartController(MvcContext root) : base(root, new StartView())
        {
        }

        public override void SetupView()
        {
            _selector.ReadFromDisk();

            // Map inputs
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.DownArrow, Next));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.UpArrow, Previous));

            // Map others
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.Spacebar, Start));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.Escape, Quit));
            
            Redraw();
        }

        private void Next()
        {
            _selector.Next();

            Redraw();
        }

        private void Previous()
        {
            _selector.Previous();

            Redraw();
        }

        private void Redraw()
        {
            View.SudokuFiles = _selector.SudokuFiles;
        }

        private void Start() => Root.OpenController<GameController, GameView, ConsoleKey>(_selector.GetSelected());
        private void Quit() => Root.Stop();
    }
}