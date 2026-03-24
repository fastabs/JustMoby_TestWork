namespace JustMoby_TestWork
{
    public interface IParameter<T>
        where T : struct
    {
        T Value { get; }
    }
}
