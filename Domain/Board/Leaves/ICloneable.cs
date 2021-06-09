namespace Sudoku.Domain.Board.Leaves
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}