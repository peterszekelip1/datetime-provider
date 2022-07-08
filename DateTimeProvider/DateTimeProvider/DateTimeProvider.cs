namespace DateTimeProvider;

public class DateTimeProvider
{
    private static readonly Lazy<DateTimeProvider> LazyInstance = new (() => new DateTimeProvider());

    public static DateTimeProvider Instance => LazyInstance.Value;

    private readonly Func<DateTime> _defaultUtcNow = () => DateTime.UtcNow;

    public DateTime UtcNow => DateTimeProviderContext.Current == null
        ? _defaultUtcNow.Invoke()
        : DateTimeProviderContext.Current.ContextUtcNow;
}
