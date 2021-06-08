using System;
using System.Linq;
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
        
        public void Handle(CellLeaf cellLeaf, int value)
        {
            _state?.Handle(cellLeaf, value);
        }
        
        public void ChangeState()
        {
            _state?.ChangeState();
        }
        
        public Grid[] Construct()
        {
            return _state?.CreateGrid() ?? Array.Empty<Grid>();
        }
        
        public void Validate()
        {
            if (Sudoku == null) return;
            
            foreach (var sudoku in Sudoku)
            {
                var cells = sudoku.GetChildren().SelectMany(q => q.GetChildren()).Cast<CellLeaf>().ToList();
                
                foreach (var cell in cells)
                {
                    cell.IsValid = true;
                    
                    if (cell.IsLocked || cell.Value.Value == 0) continue;

                    var rowColumn = cells
                        .Where(c => (c.Coordinate.Y == cell.Coordinate.Y || c.Coordinate.X == cell.Coordinate.X) &&
                                    c != cell)
                        .FirstOrDefault(c => c.Value.Value == cell.Value.Value);

                    if (rowColumn != null) cell.IsValid = false;

                    var quadrant = sudoku.Find(c => c.GetChildren().Contains(cell)).First();

                    if (quadrant.GetChildren().Cast<CellLeaf>()
                        .FirstOrDefault(c => c.Value.Value == cell.Value.Value && c != cell) != null) cell.IsValid = false;
                }
            }
        }
    }
}