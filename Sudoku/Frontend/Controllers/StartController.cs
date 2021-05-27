using System;
using Sudoku.Frontend.Views;
using Sudoku.Mvc;
using Sudoku.Mvc.Contexts;
using Sudoku.Mvc.Views;

namespace Sudoku.Frontend.Controllers
{
    public class StartController : Controller<StartView, ConsoleKey>
    {
        public StartController(MvcContext root) : base(root)
        {
        }
        
        public override StartView CreateView()
        {
            var view = new StartView();

            view.MapInput(new Input<ConsoleKey>(ConsoleKey.Spacebar, Start));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.Escape, Quit));

            return view;
        }

        private void Start() => Root.OpenController<GameController, GameView, ConsoleKey>();
        private void Quit() => Root.Stop();
    }
}