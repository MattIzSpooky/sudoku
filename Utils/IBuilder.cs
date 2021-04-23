namespace Utils
{
    public interface IBuilder<out T>
    {
        public T GetResult();

        public void Reset();
    }
}