namespace ConfigLoaders
{
    public interface IConfigLoader<T>
    {
        T Load();
    }
}
