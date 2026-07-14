namespace Create.General;

public static class Enumerators
{
    extension(Enumerable)
    {
        public static IEnumerable<T> Single<T>(T value)
        {
            yield return value;
        }
    }

    extension<T>(IEnumerable<T> body)
    {
        public IEnumerable<T> Append(IEnumerable<T> value)
        {
            foreach (var v in body)
                yield return v;
            
            foreach (var v in value)
                yield return v;
        }
    }
}