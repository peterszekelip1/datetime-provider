namespace DateTimeProvider;

public class DateTimeProviderContext : IDisposable
{
    private static readonly AsyncLocal<Stack<DateTimeProviderContext>> Stack = new ();

    public DateTime ContextUtcNow;

    public DateTimeProviderContext(DateTime contextUtcNow)
    {
        if (contextUtcNow.Kind != DateTimeKind.Utc)
        {
            contextUtcNow = DateTime.SpecifyKind(contextUtcNow, DateTimeKind.Utc);
        }

        ContextUtcNow = contextUtcNow;

        Stack.Value ??= new Stack<DateTimeProviderContext>();
        Stack.Value.Push(this);
    }

    public static DateTimeProviderContext? Current => (Stack.Value?.Count ?? 0) == 0 ? null : Stack.Value.Peek();

    public void Dispose()
    {
        Stack.Value.Pop();
    }
}