using System;
using System.IO;
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

        public StartController(MvcContext root) : base(root)
        {
        }

        public override StartView CreateView()
        {
            _selector.ReadFromDisk();

            var view = new StartView {SudokuFiles = _selector.SudokuFiles};

            // Map inputs
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.DownArrow, Next));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.UpArrow, Previous));

            // Map others
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.Spacebar, Start));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.Escape, Quit));

            return view;
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