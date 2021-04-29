using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Board
{
    public class Grid
    {
        private List<Quadrant> components;

        public List<Sudoku.Domain.Board.Quadrant> Quadrant
        {
            get => default;
            set
            {
            }
        }
    }
}