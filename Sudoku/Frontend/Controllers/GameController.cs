using System;
using Sudoku.Domain;
using Sudoku.Domain.Parsers;
using Sudoku.Frontend.Views;
using Sudoku.Mvc;
using Sudoku.Mvc.Contexts;

namespace Sudoku.Frontend.Controllers
{
    public class GameController : Controller<GameView, ConsoleKey>
    {
        private Game _game;
        
        public GameController(MvcContext root) : base(root)
        {
            var reader = new SudokuReader();

            // Try catch handle
            _game = reader.Read(@"./Frontend/Levels/puzzle.4x4");
        }

        public override GameView CreateView()
        {
            var view = new GameView();
            View = view;
            
            return view;
        }
    }
}