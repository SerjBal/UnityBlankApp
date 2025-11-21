namespace Serjbal.App
{
    public interface IFactory<TParam, T>
    {
        T Create(TParam pageName);
    }
}