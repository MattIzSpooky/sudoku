using System;
using Sudoku.Domain;
using Sudoku.Frontend.Views;
using Sudoku.Mvc;
using Sudoku.Mvc.Contexts;
using Sudoku.Mvc.Views;

namespace Sudoku.Frontend.Controllers
{
    public class GameController : Controller<GameView, ConsoleKey>
    {
        private readonly Game _game;

        public GameController(MvcContext root, Game game, string gameName) : base(root, new GameView(gameName))
        {
            _game = game;
        }

        public override void SetupView()
        {
            // Map arrow keys
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.UpArrow, () => Move(0, -1)));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.LeftArrow, () => Move(-1, 0)));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.RightArrow, () => Move(1, 0)));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.DownArrow, () => Move(0, 1)));
            
            // Map digit keys
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.D0, () => EnterNumber(0)));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.D1, () => EnterNumber(1)));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.D2, () => EnterNumber(2)));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.D3, () => EnterNumber(3)));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.D4, () => EnterNumber(4)));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.D5, () => EnterNumber(5)));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.D6, () => EnterNumber(6)));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.D7, () => EnterNumber(7)));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.D8, () => EnterNumber(8)));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.D9, () => EnterNumber(9)));

            // Other keys
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.Spacebar, SwitchState));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.S, Solve));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.C, ValidateInput));
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.Escape, () => Root.OpenController<StartController, StartView, ConsoleKey>()));
            
            View.MapInput(new Input<ConsoleKey>(ConsoleKey.Delete, () => EnterNumber(0)));
            
            Redraw();
        }

        private void Solve()
        {
            _game.Solve();
            
            Redraw();
        }
        
        private void ValidateInput()
        {
            _game.Validate();
            
            Redraw();
        }
        private void SwitchState()
        {
            _game.SwitchState();
            
            Redraw();
        }

        private void EnterNumber(int number)
        {
            _game.EnterValue(number);
            
            Redraw();
        }
        
        private void Move(int x, int y)
        {
            _game.MoveCursor(x, y);
            
            Redraw();
        }

        private void Redraw()
        {
            View.Grids = _game.Fields;
            View.Cursor = _game.Cursor;
            View.StateName = _game.GetStateName();
        }
    }
}