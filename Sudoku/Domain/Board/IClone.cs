using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Board
{
    public interface IClone<out T>
    {
        T Clone();
    }
}