using System.Text;
using Sudoku.Domain.Board.GridItems;
using Sudoku.Domain.Visitors;

namespace Sudoku.Frontend.Visitors
{
    public class RenderVisitor : IGridItemVisitor<StringBuilder>
    {
        public void Visit(StringBuilder param, Wall wall)
        {
            param.Append(wall.Horizontal ? "-" : "|");
        }

        public void Visit(StringBuilder param, Cell cell)
        {
            var text = $"{cell.CellLeaf.Value.Value}";
            param.Append(text);
        }

        public void Visit(StringBuilder param, Row row)
        {
            param.AppendLine();
        }

        public void Visit(StringBuilder param, EmptySpace emptySpace)
        {
            param.Append(' ');
        }
    }
}