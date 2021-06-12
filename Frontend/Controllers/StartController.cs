using System;
using Domain.Selector;
using Frontend.Views;
using MVC;
using MVC.Contexts;
using MVC.Views;

namespace Frontend.Controllers
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
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.Escape, Back));

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

        private void Redraw() => View.SudokuFiles = _selector.SudokuFiles;

        private void Start()
        {
            try
            {
                var selected = _selector.GetSelected();
                Root.OpenController<GameController, GameView, ConsoleKey>(selected.Read(), selected.Name);
            }
            catch (Exception e)
            {
                View.Error = e.Message;
            }
        }

        private void Back() => Root.Stop();
    }
}