using System;
using Sudoku.Domain.Board;
using Sudoku.Domain.Solvers;

namespace Sudoku.Domain.States
{
    public class Context
    {
        private State? _state;
        private State? State => _state;
        private ISolverStrategy? _strategy;
        public Board.Sudoku[]? Sudoku { get; private set; }
        
        public void SetSudoku(Board.Sudoku[]? sudoku)
        {
            Sudoku = sudoku;
        }

        public void TransitionTo(State newState)
        {
            _state = newState;
            _state.SetContext(this);
        }
        
        public void SetStrategy(ISolverStrategy newStrategy)
        {
            _strategy = newStrategy;
        }
        
        public void Handle(CellLeaf cellLeaf)
        {
            _state?.Handle(cellLeaf);
        }
        
        public Grid[] Construct()
        {
            return _state?.CreateGrid() ?? Array.Empty<Grid>();
        }
    }
}