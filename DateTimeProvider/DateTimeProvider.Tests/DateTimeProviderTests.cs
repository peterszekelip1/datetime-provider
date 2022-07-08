namespace DateTimeProvider.Tests;

public class DateTimeProviderTests
{
    [Fact(DisplayName = "Given DateTimeProviderContext is set to use a fake UtcNow value " +
                        "When DateTimeProvider.Instance.UtcNow is retrieved " +
                        "Then the previously set fakeUtc value is returned")]
    public void DateTimeProvider_001()
    {
        var fakeUtcNow = new DateTime(1900, 8, 13, 15, 26, 12, DateTimeKind.Utc);
        using var _ = new DateTimeProviderContext(fakeUtcNow);
        
        var utcNow = DateTimeProvider.Instance.UtcNow;

        utcNow.Should().Be(fakeUtcNow);
    }
}
