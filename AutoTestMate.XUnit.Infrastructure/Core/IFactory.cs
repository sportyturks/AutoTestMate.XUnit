namespace AutoTestMate.XUnit.Infrastructure.Core
{
    public interface IFactory<T>
    {
        T Create();
    }
}