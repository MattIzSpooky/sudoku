using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Board.States
{
    public abstract class State
    {
        private ISudokuComponent _context;
        protected ISudokuComponent Context { get { return _context; } private set { } }


        public abstract void Handle();

        public void SetContext(ISudokuComponent context)
        {
            this._context = context;
        }
    }
}