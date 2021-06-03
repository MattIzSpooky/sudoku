using System.Text;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.Board.GridItems
{
    public class Wall : IGridItem
    {
        public bool Horizontal { get; }

        public Wall(bool horizontal)
        {
            Horizontal = horizontal;
        }
        
        public void Accept(StringBuilder param, IGridItemVisitor<StringBuilder> visitor)
        {
            visitor.Visit(param, this);
        }
    }
}