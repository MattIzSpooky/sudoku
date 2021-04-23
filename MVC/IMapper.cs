namespace MVC
{
    /// <summary>
    /// Maps an object from a given type to a given type
    /// </summary>
    /// <typeparam name="TFrom">Map from</typeparam>
    /// <typeparam name="TO">Map to</typeparam>
    public interface IMapper<in TFrom, out TO>
    {
        TO MapTo(TFrom from);
    }
}