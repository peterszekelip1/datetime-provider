namespace DateTimeProvider;

public sealed class DateTimeProviderContext : IDisposable
{ 
    private bool _disposed;
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

    public static DateTimeProviderContext? Current => Stack is {Value.Count: > 0} ? Stack.Value.Peek() : null;
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }
        if (disposing)
        {
            if (Stack is {Value.Count: > 0})
            {
                Stack.Value.Pop();            
            }
        }                
        _disposed = true;
    }

    ~DateTimeProviderContext()
    {
        Dispose(false);
    }
}
