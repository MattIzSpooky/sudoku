using System;
using Sudoku.Domain;
using Sudoku.Domain.Parsers;
using Sudoku.Frontend.Views;
using Sudoku.Mvc;
using Sudoku.Mvc.Contexts;
using Sudoku.Mvc.Views;

namespace Sudoku.Frontend.Controllers
{
    public class GameController : Controller<GameView, ConsoleKey>
    {
        private readonly Game _game;

        public GameController(MvcContext root) : base(root)
        {
            var reader = new SudokuReader();

            // Try catch handle
            _game = reader.Read(@"./Frontend/Levels/puzzle.samurai");
        }

        public override GameView CreateView()
        {
            var view = new GameView {Grids = _game.Grids, Cursor = _game.Cursor};

            // Map arrow keys
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.UpArrow, () => Move(0, -1)));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.LeftArrow, () => Move(-1, 0)));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.RightArrow, () => Move(1, 0)));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.DownArrow, () => Move(0, 1)));
            
            // Map digit keys
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.D0, () => EnterNumber(0)));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.D1, () => EnterNumber(1)));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.D2, () => EnterNumber(2)));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.D3, () => EnterNumber(3)));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.D4, () => EnterNumber(4)));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.D5, () => EnterNumber(5)));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.D6, () => EnterNumber(6)));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.D7, () => EnterNumber(7)));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.D8, () => EnterNumber(8)));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.D9, () => EnterNumber(9)));

            // Other keys
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.Spacebar, SwitchState));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.S, Solve));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.C, ValidateInput));
            
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.Delete, () => EnterNumber(0)));
            
            return view;
        }

        private void Solve()
        {
            _game.Solve();
            
            Redraw();
        }
        
        private void ValidateInput()
        {
            _game.ValidateNumbers();
            
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
            View.Grids = _game.Grids;
            View.Cursor = _game.Cursor;
        }
    }
}