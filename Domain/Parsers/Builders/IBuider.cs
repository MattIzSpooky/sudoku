namespace Domain.Parsers.Builders
{
    public interface IBuilder<out T>
    {
        public T GetResult();

        public void Reset();
    }
}