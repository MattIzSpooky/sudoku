using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Domain.Board;

namespace Sudoku.Domain
{
    public class Game
    {
        private Grid _grid;
        public Game(Grid grid)
        {
            _grid = grid;
        }
    }
}
