using Sudoku.Domain.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Domain
{
    public class Game
    {
        public Board.Grid Grid
        {
            get => default;
            set
            {
            }
        }

        /// <summary></summary>
        public void ChangeCell(Coordinate coordinate, int value)
        {
            throw new System.NotImplementedException();
        }
    }
}
