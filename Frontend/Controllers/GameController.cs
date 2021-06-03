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
            _game = reader.Read(@"./Frontend/Levels/puzzle.4x4");
        }

        public override GameView CreateView()
        {
            var view = new GameView {Grids = _game.Grids, Cursor = _game.Cursor};

            // Map arrow keys
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.UpArrow, MoveUp));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.LeftArrow, MoveLeft));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.RightArrow, MoveRight));
            view.MapInput(new Input<ConsoleKey>(ConsoleKey.DownArrow, MoveDown));

            return view;
        }

        private void MoveDown()
        {
            _game.MoveCursor(0, 1);
            
            Redraw();
        }

        private void MoveRight()
        {
            _game.MoveCursor(1, 0);
            
            Redraw();
        }

        private void MoveUp()
        {
            _game.MoveCursor(0, -1);
            
            Redraw();
        }

        private void MoveLeft()
        {
            _game.MoveCursor(-1, 0);

            Redraw();
        }

        private void Redraw()
        {
            View.Grids = _game.Grids;
            View.Cursor = _game.Cursor;
        }
    }
}