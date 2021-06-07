namespace Sudoku.Domain.Board.Builders
{
    public interface IBuilder<out T>
    {
        public T GetResult();

        public void Reset();
    }
}