using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Board
{
    public class Grid
    {
        private readonly List<Quadrant> _quadrants;

        public IReadOnlyList<Quadrant> Quadrants => _quadrants;

        public Grid(List<Quadrant> quadrants)
        {
            _quadrants = quadrants;
        }
    }
}