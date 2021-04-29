using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Domain.Board.States
{
    public abstract class State
    {
        private ICell _context;
        protected ICell Context { get { return _context; } private set { } }


        public abstract void Handle();

        public void SetContext(ICell context)
        {
            this._context = context;
        }
    }
}