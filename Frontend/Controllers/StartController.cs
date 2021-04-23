using System;
using Frontend.Views;
using MVC;
using MVC.Contexts;
using MVC.Views;

namespace Frontend.Controllers
{
    public class StartController : Controller<StartView, ConsoleKey>
    {
        public StartController(MvcContext root) : base(root)
        {
        }
        
        public override StartView CreateView()
        {
            var view = new StartView();

            // view.MapInput(new Input<ConsoleKey>(ConsoleKey.Spacebar, Start));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.Escape, Quit));

            return view;
        }

        // private void Start() => Root.OpenController<GameController, GameView, ConsoleKey>();
        private void Quit() => Root.Stop();
    }
}